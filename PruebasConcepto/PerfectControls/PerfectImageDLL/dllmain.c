/* Replace "dll.h" with the name of your header */
#include "dll.h"
#include <windows.h>
#include <stdio.h>
#include <stdlib.h>

// This only works with 24bpsRGB/24bpsBGR buffers
DLLIMPORT void FastDrawImage(PX8 *buffer, PX8 *buffer2, int width, int height, int width2, int height2, int extra, int extra2, int x, int y, float z, PX8 color)
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
   
   // Fill destination buffer width specified background color
   memset(buffer,color,(width*3+extra)*height);   
      
   // Get real sizes (avoid overbuffering)
   w=width;
   h=height;
  
   if(x<0) x=0;
   if(y<0) y=0;

   if(z>1){
      // Solution for out of bounds with zoom > 1.0
      if((int)(x+zf*(w+0.5))>=width2) x=(int)(width2-w*zf);
      if((int)(y+zf*(h+0.5))>=height2) y=(int)(height2-h*zf);
   }else{        
	  // Solution for out of bounds with zoom <= 1.0	  
	  if((int)(x+zf*(w-0.5))>=width2) {w=(int)(width2*z-x);}
	  if((int)(y+zf*(h-0.5))>=height2) {h=(int)(height2*z-y);}
   }
         
   // Center render in buffer    
   cx=(int)((float)width/2-(int)((float)width2*z/2.0));
   cy=(int)((float)height/2-(int)((float)height2*z/2.0));   
   if(cx<0) cx=0;
   if(cy<0) cy=0;
   if((int)(cx+width2*z)>=width) cx=0;
   if((int)(cy+height2*z)>=height) cy=0;
   
   // Multiply x coordinates for 3 channels
   w=w*3;   
   width=width*3;
   width2=width2*3;    
   x=x*3;
   cx=cx*3;
      
   // Nearest Neighbour interpolation
   switch((int)(z*32)){
       case 1:    // Zoom x1/32            
       case 2:    // Zoom x1/16
       case 4:    // Zoom x1/8
       case 8:    // Zoom x1/4
       case 16:   // Zoom x1/2            
            buffer2+=y*(width2+extra2);
            buffer+=cy*(width+extra);
            for(i=0;i<h;i++){
               buffer+=cx;
               for(j=0;j<w;j+=3){
                  buffer[j]=buffer2[x+j*zzf];   
                  buffer[j+1]=buffer2[x+j*zzf+1];
                  buffer[j+2]=buffer2[x+j*zzf+2];
               }
               buffer+=width+extra-cx;
               buffer2+=zzf*width2+extra2;
            }       
            break;  
       case 32:   // Zoom x1   		    		    		   
            buffer2+=y*(width2+extra2);
            for(i=0;i<h;i++){
               memcpy(buffer+cx+cy*(width+extra),buffer2+x,w);
               buffer+=width+extra;
               buffer2+=width2+extra2;
            }       
            break;       
       case 64:   // Zoom x2            
       case 128:  // Zoom x4             
       case 256:  // Zoom x8		    
            buffer+=cy*(width+extra);
			for(i=0;i<h;i+=zz){   
               buffer+=cx;
               k=(int)((y+i*zf)*(width2+extra2)+x);       
               for(j=0;j<w;j+=3*zz){                              
                  l=(int)(k+j*zf);				  
  				  n=0;
				  // Pixel copy
				  while((n<3*zz)&&(j+n<w)){
                      buffer[j+n]=buffer2[l];
                      buffer[j+n+1]=buffer2[l+1];
                      buffer[j+n+2]=buffer2[l+2];
					  n+=3;
				  }
               }                                  
               // Full lines replication
               j=1;
               while((j+i<h)&&(j<zz)){
                 memcpy(buffer+(width+extra)*j,buffer,w);
                 j++;
               }
               buffer+=zz*(width+extra)-cx;               
            }              
   }
}

// This only works with 24bpsRGB/24bpsBGR buffers
DLLIMPORT void FastDrawBiHImage(PX8 *buffer, PX8 *buffer1, PX8 *buffer3, int width, int height, int width2, int height2, int extra, int extra2, int x, int y, float z, PX8 color)
{
   int i,j,k,l,n;
   float zf;
   int zz, zzf;
   int cx,cy;
   int w,h;   
   int m1,m2;
   int ww;

   // Some precalcs
   zf=1/z;
   zz=(int)z;
   zzf=(int)zf;
   
   // Fill destination buffer width specified background color
   memset(buffer,color,(width*3+extra)*height);   
      
   // Get real sizes (avoid overbuffering)
   w=width;
   h=height;
  
   if(x<0) x=0;
   if(y<0) y=0;

   if(z>1){
      // Solution for out of bounds with zoom > 1.0
      if((int)(x+zf*(w+0.5))>=width2) x=(int)(width2-w*zf);
      if((int)(y+zf*(h+0.5))>=height2) y=(int)(height2-h*zf);
   }else{        
	  // Solution for out of bounds with zoom <= 1.0	  
	  if((int)(x+zf*(w-0.5))>=width2) {w=(int)(width2*z-x);}
	  if((int)(y+zf*(h-0.5))>=height2) {h=(int)(height2*z-y);}
   }
         
   // Center render in buffer    
   cx=(int)((float)width/2-(int)((float)width2*z/2.0));
   cy=(int)((float)height/2-(int)((float)height2*z/2.0));   
   if(cx<0) cx=0;
   if(cy<0) cy=0;
   if((int)(cx+width2*z)>=width) cx=0;
   if((int)(cy+height2*z)>=height) cy=0;      

   // Multiply x coordinates for 3 channels
   w=w*3;   
   width=width*3;
   width2=width2*3;    
   x=x*3;
   cx=cx*3;

   ww=(int)((float)w/6.0)*3;
   m1=(int)(ww*zf);
   m2=(int)(w*zf-m1);
      
   // Nearest Neighbour interpolation
   switch((int)(z*32)){
       case 1:    // Zoom x1/32            
       case 2:    // Zoom x1/16
       case 4:    // Zoom x1/8
       case 8:    // Zoom x1/4
       case 16:   // Zoom x1/2            
            buffer1+=y*(width2+extra2);
			buffer3+=y*(width2+extra2);
            buffer+=cy*(width+extra);
            for(i=0;i<h;i++){
               buffer+=cx;
               for(j=0;j<w;j+=3){
				   if(j<ww){
					  buffer[j]=buffer1[x+j*zzf];   
					  buffer[j+1]=buffer1[x+j*zzf+1];
					  buffer[j+2]=buffer1[x+j*zzf+2];
				   }else{
					  buffer[j]=buffer3[x+j*zzf];   
					  buffer[j+1]=buffer3[x+j*zzf+1];
					  buffer[j+2]=buffer3[x+j*zzf+2];
				   }
               }
               buffer+=width+extra-cx;
               buffer1+=zzf*width2+extra2;
			   buffer3+=zzf*width2+extra2;
            }       
            break;  
       case 32:   // Zoom x1   		    		    		   
            buffer1+=y*(width2+extra2);
			buffer3+=y*(width2+extra2);
            for(i=0;i<h;i++){
               memcpy(buffer+cx+cy*(width+extra),buffer1+x,m1);
			   memcpy(buffer+cx+cy*(width+extra)+m1,buffer3+x+m1,m2);
               buffer+=width+extra;
               buffer1+=width2+extra2;
			   buffer3+=width2+extra2;
            }       
            break;       
       case 64:   // Zoom x2            
       case 128:  // Zoom x4             
       case 256:  // Zoom x8		    
            buffer+=cy*(width+extra);
			for(i=0;i<h;i+=zz){   
               buffer+=cx;
               k=(int)((y+i*zf)*(width2+extra2)+x);       
               for(j=0;j<w;j+=3*zz){                              
                  l=(int)(k+j*zf);				  

  				  // Pixel copy
				  n=0;				  
				  while((n<3*zz)&&(j+n<w)){
					  if(j+n<ww){
						buffer[j+n]=buffer1[l];
						buffer[j+n+1]=buffer1[l+1];
						buffer[j+n+2]=buffer1[l+2];
					  }else{
					 	buffer[j+n]=buffer3[l];
						buffer[j+n+1]=buffer3[l+1];
						buffer[j+n+2]=buffer3[l+2];					
					  }
					  n+=3;
				  }
               }                                  
               // Full lines replication
               j=1;
               while((j+i<h)&&(j<zz)){
                 memcpy(buffer+(width+extra)*j,buffer,w);
                 j++;
               }
               buffer+=zz*(width+extra)-cx;               
            }              
   }
}

// This only works with 24bpsRGB/24bpsBGR buffers
DLLIMPORT void FastDrawBiVImage(PX8 *buffer, PX8 *buffer1, PX8 *buffer3, int width, int height, int width2, int height2, int extra, int extra2, int x, int y, float z, PX8 color)
{
   int i,j,k,l,n;
   float zf;
   int zz, zzf;
   int cx,cy;
   int w,h;   
   int m1,m2;
   int ww;

   // Some precalcs
   zf=1/z;
   zz=(int)z;
   zzf=(int)zf;
   
   // Fill destination buffer width specified background color
   memset(buffer,color,(width*3+extra)*height);   
      
   // Get real sizes (avoid overbuffering)
   w=width;
   h=height;
  
   if(x<0) x=0;
   if(y<0) y=0;

   if(z>1){
      // Solution for out of bounds with zoom > 1.0
      if((int)(x+zf*(w+0.5))>=width2) x=(int)(width2-w*zf);
      if((int)(y+zf*(h+0.5))>=height2) y=(int)(height2-h*zf);
   }else{        
	  // Solution for out of bounds with zoom <= 1.0	  
	  if((int)(x+zf*(w-0.5))>=width2) {w=(int)(width2*z-x);}
	  if((int)(y+zf*(h-0.5))>=height2) {h=(int)(height2*z-y);}
   }
         
   // Center render in buffer    
   cx=(int)((float)width/2-(int)((float)width2*z/2.0));
   cy=(int)((float)height/2-(int)((float)height2*z/2.0));   
   if(cx<0) cx=0;
   if(cy<0) cy=0;
   if((int)(cx+width2*z)>=width) cx=0;
   if((int)(cy+height2*z)>=height) cy=0;      

   // Multiply x coordinates for 3 channels
   w=w*3;   
   width=width*3;
   width2=width2*3;    
   x=x*3;
   cx=cx*3;

   ww=(int)((float)w/6.0)*3;
   m1=(int)(ww*zf);
   m2=(int)(w*zf-m1);
      
   // Nearest Neighbour interpolation
   switch((int)(z*32)){
       case 1:    // Zoom x1/32            
       case 2:    // Zoom x1/16
       case 4:    // Zoom x1/8
       case 8:    // Zoom x1/4
       case 16:   // Zoom x1/2            
            buffer1+=y*(width2+extra2);
			buffer3+=y*(width2+extra2);
            buffer+=cy*(width+extra);
            for(i=0;i<h;i++){
               buffer+=cx;
               for(j=0;j<w;j+=3){
				   if(j<ww){
					  buffer[j]=buffer1[x+j*zzf];   
					  buffer[j+1]=buffer1[x+j*zzf+1];
					  buffer[j+2]=buffer1[x+j*zzf+2];
				   }else{
					  buffer[j]=buffer3[x+j*zzf];   
					  buffer[j+1]=buffer3[x+j*zzf+1];
					  buffer[j+2]=buffer3[x+j*zzf+2];
				   }
               }
               buffer+=width+extra-cx;
               buffer1+=zzf*width2+extra2;
			   buffer3+=zzf*width2+extra2;
            }       
            break;  
       case 32:   // Zoom x1   		    		    		   
            buffer1+=y*(width2+extra2);
			buffer3+=y*(width2+extra2);
            for(i=0;i<h;i++){
               memcpy(buffer+cx+cy*(width+extra),buffer1+x,m1);
			   memcpy(buffer+cx+cy*(width+extra)+m1,buffer3+x+m1,m2);
               buffer+=width+extra;
               buffer1+=width2+extra2;
			   buffer3+=width2+extra2;
            }       
            break;       
       case 64:   // Zoom x2            
       case 128:  // Zoom x4             
       case 256:  // Zoom x8		    
            buffer+=cy*(width+extra);
			for(i=0;i<h;i+=zz){   
               buffer+=cx;
               k=(int)((y+i*zf)*(width2+extra2)+x);       
               for(j=0;j<w;j+=3*zz){                              
                  l=(int)(k+j*zf);				  

  				  // Pixel copy
				  n=0;				  
				  while((n<3*zz)&&(j+n<w)){
					  if(j+n<ww){
						buffer[j+n]=buffer1[l];
						buffer[j+n+1]=buffer1[l+1];
						buffer[j+n+2]=buffer1[l+2];
					  }else{
					 	buffer[j+n]=buffer3[l];
						buffer[j+n+1]=buffer3[l+1];
						buffer[j+n+2]=buffer3[l+2];					
					  }
					  n+=3;
				  }
               }                                  
               // Full lines replication
               j=1;
               while((j+i<h)&&(j<zz)){
                 memcpy(buffer+(width+extra)*j,buffer,w);
                 j++;
               }
               buffer+=zz*(width+extra)-cx;               
            }              
   }
}

// This only works with 24bpsRGB/24bpsBGR buffers
DLLIMPORT void FastDrawImageFlash(PX8 *buffer, PX8 *buffer2, int width, int height, int width2, int height2, int extra, int extra2, int x, int y, float z, PX8 color, int flash)
{
   int i,j,k,l,n;
   float zf;
   int zz, zzf;
   int cx,cy;
   int w,h;
   PX8 *buf;

   buf=buffer;

   // Some precalcs
   zf=1/z;
   zz=(int)z;
   zzf=(int)zf;
   
   // Fill destination buffer width specified background color
   memset(buffer,color,(width*3+extra)*height);   
      
   // Get real sizes (avoid overbuffering)
   w=width;
   h=height;
  
   if(x<0) x=0;
   if(y<0) y=0;

   if(z>1){
      // Solution for out of bounds with zoom > 1.0
      if((int)(x+zf*(w+0.5))>=width2) x=(int)(width2-w*zf);
      if((int)(y+zf*(h+0.5))>=height2) y=(int)(height2-h*zf);
   }else{        
	  // Solution for out of bounds with zoom <= 1.0	  
	  if((int)(x+zf*(w-0.5))>=width2) {w=(int)(width2*z-x);}
	  if((int)(y+zf*(h-0.5))>=height2) {h=(int)(height2*z-y);}
   }
         
   // Center render in buffer    
   cx=(int)((float)width/2-(int)((float)width2*z/2.0));
   cy=(int)((float)height/2-(int)((float)height2*z/2.0));   
   if(cx<0) cx=0;
   if(cy<0) cy=0;
   if((int)(cx+width2*z)>=width) cx=0;
   if((int)(cy+height2*z)>=height) cy=0;
   
   // Multiply x coordinates for 3 channels
   w=w*3;   
   width=width*3;
   width2=width2*3;    
   x=x*3;
   cx=cx*3;
      
   // Nearest Neighbour interpolation
   switch((int)(z*32)){
       case 1:    // Zoom x1/32            
       case 2:    // Zoom x1/16
       case 4:    // Zoom x1/8
       case 8:    // Zoom x1/4
       case 16:   // Zoom x1/2            
            buffer2+=y*(width2+extra2);
            buffer+=cy*(width+extra);
            for(i=0;i<h;i++){
               buffer+=cx;
               for(j=0;j<w;j+=3){
                  buffer[j]=buffer2[x+j*zzf];   
                  buffer[j+1]=buffer2[x+j*zzf+1];
                  buffer[j+2]=buffer2[x+j*zzf+2];
               }
               buffer+=width+extra-cx;
               buffer2+=zzf*width2+extra2;
            }       
            break;  
       case 32:   // Zoom x1   		    		    		   
            buffer2+=y*(width2+extra2);
            for(i=0;i<h;i++){
               memcpy(buffer+cx+cy*(width+extra),buffer2+x,w);
               buffer+=width+extra;
               buffer2+=width2+extra2;
            }       
            break;       
       case 64:   // Zoom x2            
       case 128:  // Zoom x4             
       case 256:  // Zoom x8		    
            buffer+=cy*(width+extra);
			for(i=0;i<h;i+=zz){   
               buffer+=cx;
               k=(int)((y+i*zf)*(width2+extra2)+x);       
               for(j=0;j<w;j+=3*zz){                              
                  l=(int)(k+j*zf);				  
  				  n=0;
				  // Pixel copy
				  while((n<3*zz)&&(j+n<w)){
                      buffer[j+n]=buffer2[l];
                      buffer[j+n+1]=buffer2[l+1];
                      buffer[j+n+2]=buffer2[l+2];
					  n+=3;
				  }
               }                                  
               // Full lines replication
               j=1;
               while((j+i<h)&&(j<zz)){
                 memcpy(buffer+(width+extra)*j,buffer,w);
                 j++;
               }
               buffer+=zz*(width+extra)-cx;               
            }              
   }

   // Make output pixels flash
   buffer=buf;	
   buffer+=cy*(width+extra);
   for(i=0;i<h;i++){
	   buffer+=cx;
	   for(j=0;j<w;j+=3){
		   if(flash>0){
			   if((buffer[j]>240)||(buffer[j+1]>240)||(buffer[j+2]>240)) buffer[j]=buffer[j+1]=buffer[j+2]=255;
		   }else{
			   if((buffer[j]>240)||(buffer[j+1]>240)||(buffer[j+2]>240)) buffer[j]=buffer[j+1]=buffer[j+2]=0;
		   }		   
	   }
	   buffer+=width+extra-cx;
   }
}

BOOL APIENTRY DllMain (HINSTANCE hInst     /* Library instance handle. */ ,
                       DWORD reason        /* Reason this function is being called. */ ,
                       LPVOID reserved     /* Not used. */ )
{
    switch (reason)
    {
      case DLL_PROCESS_ATTACH:
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
