// This is provisional code in a provisional DLL until this code is blended into perfectImage.dll
#include "image.h"
#include <windows.h>
#include <stdio.h>
#include <stdlib.h>
#include <math.h>

DLLIMPORT void Convert48RGBto24BGR(PX16 *buffer16, PX8 *buffer8, int width, int height, int extra)
{
   int i,j;
   int w;
      
   w = width*3;   
   for(i=0;i<height;i++)
   {
      for(j=0;j<w;j+=3)
      {
         buffer8[j]   = sRGBgLUT[buffer16[j+2]]; 
         buffer8[j+1] = sRGBgLUT[buffer16[j+1]];
         buffer8[j+2] = sRGBgLUT[buffer16[j]]; 
      }
      buffer8  += w+extra;
      buffer16 += w;
   }
}

// Faster than gcc library pow implementation
double powF (double a, double b) {
    return exp(b*log(a));
}

PX16 sRGBgamma(PX16 value){
      double r,f;
      double c1;
      
      f=1.0/2.4;  
      c1=12.92*65535.0;          
      r=(double)value/65535.0;
      if(r<=0.0031308) return (PX16)(c1*r); else return (PX16)((1.055*pow(r,f)-0.055)*65535.0);
}

BOOL APIENTRY DllMain (HINSTANCE hInst     /* Library instance handle. */ ,
                       DWORD reason        /* Reason this function is being called. */ ,
                       LPVOID reserved     /* Not used. */ )
{
    int i;
    
    switch (reason)
    {
      case DLL_PROCESS_ATTACH:
        // Create the sRGB gamma LUT
        for(i=0;i<65536;i++){
          sRGBgLUT[i] = (PX8)(sRGBgamma(i)>>8);
        }        
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
