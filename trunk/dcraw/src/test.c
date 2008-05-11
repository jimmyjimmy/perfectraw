#include <stdio.h>
#include <stdlib.h>
#include "dcraw.h"

int main(int argc, char *argv[])
{
  IMAGE_INFO info;
    
  int w,h;
  
  printf("Resultado de leer el RAW: %i\n",DCRAW_Init("c:\\test\\IMG_4118.CR2",&w,&h));
  DCRAW_GetInfo(&info);
  
  printf("Camera Maker: %s\n",info.camera_make);  
  printf("Model: %s\n",info.camera_model);  
  printf("Owner: %s\n",info.artist);  
  DCRAW_Process(1);  
  DCRAW_End();
  system("PAUSE");	
  return 0;
}
