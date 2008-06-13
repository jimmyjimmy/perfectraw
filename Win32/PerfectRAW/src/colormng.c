#include "lcms.h"
#include "colormng.h"
#include <windows.h>
#include <stdio.h>
#include <stdlib.h>
#include <math.h>

void ApplyColorProfile16RGB(PX16 *buffer16, int width, int height, char *inProfileFile, char *outProfileFile)
{
     char sError[4000];
     int error=0;
     //int i;
     
     // Use LCMS for ColorProfile conversion
     cmsHPROFILE hInProfile, hOutProfile;
     cmsHTRANSFORM hTransform;
     
     cmsErrorAction(LCMS_ERROR_SHOW);     
                   
     hInProfile  = cmsOpenProfileFromFile(inProfileFile, "r");
     hOutProfile = cmsOpenProfileFromFile(outProfileFile, "r");     
                                                
     if(hInProfile==NULL) error++;
     if(hOutProfile==NULL) error+=2;
     switch(error){
        case 0:
           hTransform  = cmsCreateTransform(hInProfile,
                                           TYPE_RGB_16,
                                           hOutProfile,
                                           TYPE_RGB_16,
                                           INTENT_PERCEPTUAL, 0);
     
           //cmsDoTransform(hTransform, buffer16, buffer16, width*height);
           //clock_t start, end;
           //double elapsed;    
           //start = clock();
           cmsDoTransform(hTransform, buffer16, buffer16, width*height);
           //for(i=0;i<1000;i++) cmsDoTransform(hTransform, buffer16, buffer16, 480000);
           //end = clock();
           //elapsed = ((double) (end - start)) / CLOCKS_PER_SEC;
           //sprintf(sError,"Tiempo CPU: %f, fps: %f\n",elapsed,1000.0/elapsed);
           //MessageBox(0,sError,"Timing",0);           
     
           cmsDeleteTransform(hTransform);
           cmsCloseProfile(hInProfile);
           cmsCloseProfile(hOutProfile);     
           break;
        case 1:
           sprintf(sError,"ERROR applying ICC profile. Error description:\ninput ICC Profile not found '%s'",inProfileFile);                                           
           MessageBox(0,sError,"ERROR",0);                                              
           break;
        case 2:
           sprintf(sError,"ERROR applying ICC profile. Error description:\noutput ICC Profile not found '%s'",inProfileFile);                                           
           MessageBox(0,sError,"ERROR",0);                                             
           break;
        case 3:
           sprintf(sError,"ERROR applying ICC profile. Error description:\ninput ICC Profile not found '%s'\noutput ICC Profile not found '%s'",inProfileFile,outProfileFile);
           MessageBox(0,sError,"ERROR",0);                                           
           break;
     }     
}


void ApplyGamma16RGB(PX16 *buffer16, int width, int height, float gamma){     
     // Create LUT if doesn't exist
     PX16 *LUT;
     int i;
     
     if(gamma==0){
        // Create the sRGB gamma LUT
        if(sRGBgLUT[65535]==0){
           for(i=0;i<65536;i++){
             sRGBgLUT[i] = (PX16)sRGBgamma(i);
           }        
        }   
        LUT=sRGBgLUT;
     }else{           
        // Create (and cache) normal gamma LUT        
        if(lastGamma!=gamma){
          for(i=0;i<65536;i++){
            gLUT[i] = (PX16)Gamma(i,gamma);
          }   
          lastGamma=gamma;     
        }
        LUT=gLUT;
     }
     for(i=0;i<width*height*3;i++) buffer16[i]=LUT[buffer16[i]];
}

void ApplyColorSpace16RGB(PX16 *img, int width, int height, float rgb_cam[3][4], int output_color)
{
  // This code is adapted here from convert_to_rgb function in dcraw.c by Coffin
  // So we use the same variable
  int colors;
  int row, col, c, i, j, k;
  
  float out[3], out_cam[3][4];
  double num, inverse[3][3];  
  static const double xyz_rgb[3][3] = {			/* XYZ from RGB */
  { 0.412453, 0.357580, 0.180423 },
  { 0.212671, 0.715160, 0.072169 },
  { 0.019334, 0.119193, 0.950227 } };
  static const double xyzd50_srgb[3][3] =
  { { 0.436083, 0.385083, 0.143055 },
    { 0.222507, 0.716888, 0.060608 },
    { 0.013930, 0.097097, 0.714022 } };
  static const double rgb_rgb[3][3] =
  { { 1,0,0 }, { 0,1,0 }, { 0,0,1 } };
  static const double adobe_rgb[3][3] =
  { { 0.715146, 0.284856, 0.000000 },
    { 0.000000, 1.000000, 0.000000 },
    { 0.000000, 0.041166, 0.958839 } };
  static const double wide_rgb[3][3] =
  { { 0.593087, 0.404710, 0.002206 },
    { 0.095413, 0.843149, 0.061439 },
    { 0.011621, 0.069091, 0.919288 } };
  static const double prophoto_rgb[3][3] =
  { { 0.529317, 0.330092, 0.140588 },
    { 0.098368, 0.873465, 0.028169 },
    { 0.016879, 0.117663, 0.865457 } };
  static const double (*out_rgb[])[3] =
  { rgb_rgb, adobe_rgb, wide_rgb, prophoto_rgb, xyz_rgb };

  // Here, we have an RGB image, so we always have 3 colors
  colors=3;
  for (i=0; i < 3; i++){      
    for (j=0; j < colors; j++){
	  for (out_cam[i][j] = k=0; k < 3; k++) out_cam[i][j] += out_rgb[output_color-1][i][k] * rgb_cam[k][j];
    }
  }    
  
  for (row=0; row < height; row++)
    for (col=0; col < width; col++, img+=3) {  
	out[0] = out[1] = out[2] = 0;
	FORCC {
	  out[0] += out_cam[0][c] * img[c];
	  out[1] += out_cam[1][c] * img[c];
	  out[2] += out_cam[2][c] * img[c];
	}	
	
	FORC3 img[c] = CLIP((int) out[c]);   	
  }
}

DLLIMPORT void Convert48RGBto24BGR(PX16 *buffer16, PX8 *buffer8, int width, int height, int extra, float *cam_RGB,int colorspace, float gamma, char *inProfileFile, char *outProfileFile)
{
   int i,j;
   int w;      
    
   // Color manage: colorspace, gamma and convert to color profile of output device
   if(colorspace!=0)       ApplyColorSpace16RGB(buffer16,width,height,cam_RGB,colorspace);
   if(gamma!=1)            ApplyGamma16RGB(buffer16,width,height,gamma);   
   if(inProfileFile[0]!=0) ApplyColorProfile16RGB(buffer16,width,height,inProfileFile,outProfileFile);
      
   // Convert from 16 RGB to 8 BGR
   w = width*3;   
   for(i=0;i<height;i++)
   {
      for(j=0;j<w;j+=3)
      {
         buffer8[j]   = (buffer16[j+2]>>8); 
         buffer8[j+1] = (buffer16[j+1]>>8);
         buffer8[j+2] = (buffer16[j]>>8); 
      }
      buffer8  += w+extra;
      buffer16 += w;
   }      
}

DLLIMPORT void DrawImage(PX8 *outputImg, PX8 *inputImg, int outImgWidth, int outImgHeight, int inImgWidth, int inImgHeight, int outImgRowPadding, int inImgRowPadding, int x, int y, float z, PX8 fillColor)
{
   int i,j,k,l,n;
   float zf;
   int zz, zzf;
   int cx,cy;
   int w,h;
      
   // Some precalcs
   zf=1/z;
   zz=(int)z;
   zzf=(int)zf;
   
   // Fill destination outputImg outImgWidth specified background color
   memset(outputImg,fillColor,(outImgWidth*3+outImgRowPadding)*outImgHeight);   
      
   // Get real sizes (avoid overbuffering)
   w=outImgWidth;
   h=outImgHeight;
  
   if(x<0) x=0;
   if(y<0) y=0;

   if(z>1){
      // Solution for out of bounds with zoom > 1.0
      if((int)(x+zf*(w+0.5))>=inImgWidth) x=(int)(inImgWidth-w*zf);
      if((int)(y+zf*(h+0.5))>=inImgHeight) y=(int)(inImgHeight-h*zf);
   }else{        
      // Solution for out of bounds with zoom <= 1.0      
      if((int)(x+zf*(w-0.5))>=inImgWidth) {w=(int)(inImgWidth*z-x);}
      if((int)(y+zf*(h-0.5))>=inImgHeight) {h=(int)(inImgHeight*z-y);}
   }
         
   // Center render in outputImg    
   cx=(int)((float)outImgWidth/2-(int)((float)inImgWidth*z/2.0));
   cy=(int)((float)outImgHeight/2-(int)((float)inImgHeight*z/2.0));   
   if(cx<0) cx=0;
   if(cy<0) cy=0;
   if((int)(cx+inImgWidth*z)>=outImgWidth) cx=0;
   if((int)(cy+inImgHeight*z)>=outImgHeight) cy=0;
   
   // Multiply x coordinates for 3 channels
   w=w*3;   
   outImgWidth=outImgWidth*3;
   inImgWidth=inImgWidth*3;    
   x=x*3;
   cx=cx*3;
      
   // Nearest Neighbour interpolation
   if(z < 1) {
		inputImg+=y*(inImgWidth+inImgRowPadding);
		outputImg+=cy*(outImgWidth+outImgRowPadding);
		for(i=0;i<h;i++){
		   outputImg+=cx;
		   for(j=0;j<w;j+=3){
			  outputImg[j]=inputImg[x+j*zzf];
			  outputImg[j+1]=inputImg[x+j*zzf+1];
			  outputImg[j+2]=inputImg[x+j*zzf+2];
		   }
		   outputImg+=outImgWidth+outImgRowPadding-cx;
		   inputImg+=zzf*(inImgWidth+inImgRowPadding);
		}
   } else {
		outputImg+=cy*(outImgWidth+outImgRowPadding);
		for(i=0;i<h;i+=zz){   
		   outputImg+=cx;
		   k=(int)((y+(int)(i*zf))*(inImgWidth+inImgRowPadding)+x);       
		   for(j=0;j<w;j+=3*zz){                              
			  l=(int)(k+(int)(j*zf));
			  n=0;
			  // Pixel copy
			  while((n<3*zz)&&(j+n<w)){
				  outputImg[j+n]=inputImg[l];
				  outputImg[j+n+1]=inputImg[l+1];
				  outputImg[j+n+2]=inputImg[l+2];
				  n+=3;
			  }
		   }                                 
		   // Full rows replication
		   j=1;
		   while((j+i<h)&&(j<zz)){
			 memcpy(outputImg+(outImgWidth+outImgRowPadding)*j,outputImg,w);
			 j++;
		   }
		   outputImg+=zz*(outImgWidth+outImgRowPadding)-cx;		   
		}
   }
}

// Faster than MinGW library pow implementation
double powF (double a, double b) {
    return exp(b*log(a));
}

// Calculate pure sRGB gamma
PX16 sRGBgamma(PX16 value){
      double r,f;
      double c1;
      
      f=1.0/2.4;  
      c1=12.92*65535.0;          
      r=(double)value/65535.0;
      if(r<=0.0031308) return (PX16)(c1*r); else return (PX16)((1.055*pow(r,f)-0.055)*65535.0);
}

// Calculate any other gamma
PX16 Gamma(PX16 value,float gamma){
      double r,f;      
      
      f=1.0/((double)gamma);
      r=(double)value/65535.0;
      return (PX16)(65535.0*powF(r,f));
}

BOOL APIENTRY DllMain (HINSTANCE hInst     /* Library instance handle. */ ,
                       DWORD reason        /* Reason this function is being called. */ ,
                       LPVOID reserved     /* Not used. */ )
{
    int i;
    
    switch (reason)
    {
      case DLL_PROCESS_ATTACH:
        lastGamma=-1;
        break;

      case DLL_PROCESS_DETACH:
        break;

      case DLL_THREAD_ATTACH:
        break;

      case DLL_THREAD_DETACH:
        break;
    }

    /* Returns TRUE on success, FALSE on failure */
    return TRUE;
}
