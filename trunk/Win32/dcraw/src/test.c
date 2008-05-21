#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include "dcraw.h"

int main(int argc, char *argv[])
{
  IMAGE_INFO info;
  DLL_PARAMETERS params;
  unsigned short (*image)[4];  
    
  int w,h;
  
  printf("Resultado de leer el RAW: %i\n",DCRAW_Init("C:\\test\\a_1.dng",&w,&h));
  DCRAW_GetInfo(&info);  
  printf("Model: %s\n",info.camera_model);  
  DCRAW_DefaultParameters(&params);  
  params.user_gamma=0;
  params.output_color=1;
  params.exposure=pow(2,-1.0);
  printf("%f\n",params.exposure);
  params.exposure_mode=1;
  image=(unsigned short (*)[4])DCRAW_Process(&params);
  //image=(unsigned short (*)[4])DCRAW_Process(&params);  
  DCRAW_End();
  
  system("PAUSE");	
  return 0;            
}
