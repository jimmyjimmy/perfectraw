#include "PerfectImageDLL.h"
//#include "stdafx.h"
#include <windows.h>
#include <stdio.h>
#include <stdlib.h>


// This only works with 24bpsRGB/24bpsBGR buffers
DLLIMPORT void FastDrawImage(PX8 *buffer, PX8 *buffer2, int width, int height, int width2, int height2, int extra, int extra2, int x, int y, float z, PX8 color)
{
   int i,j,k,l,n;  
    
   // Some precalcs
   float zf=1/z;
   int zz=(int)z;
   int zzf=(int)zf;
   int cx,cy;              
   int w=width;
   int h=height;


   // Clear destination buffer
   memset(buffer,color,(width*3+extra)*height);   
   
   // Get real sizes (avoid overbuffering)
   // This is not working in some cases, probably
   // because rounding
   if(x<0) x=0;
   if(y<0) y=0;
   if(x>=width2) x=width2-(int)((width-1)*zf);
   if(y>=height2) y=height2-(int)((height-1)*zf);
   if(x+zf*width>=width2) w=(int)(width2*z-x);
   if(y+zf*height>=height2) h=(int)(height2*z-y);
         
   // Center render in buffer      
   cx=(int) (float)width/2-(int)((float)width2*z/2.0);
   cy=(int) (float)height/2-(int)((float)height2*z/2.0);   
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
                  for(n=0;n<3*zz;n+=3){
                     buffer[j+n]=buffer2[l];
                     buffer[j+n+1]=buffer2[l+1];
                     buffer[j+n+2]=buffer2[l+2];
                  }
               }    
                              
               // Lines replication
               j=0;           
               while((j+i+1<h)&&(j<(int)z)){
                 memcpy(buffer+(width+extra)*j,buffer,w);
                 j++;
               }
               buffer+=zz*(width+extra)-cx;               
            }              
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
