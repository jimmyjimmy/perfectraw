#ifndef _DLL_H_
#define _DLL_H_

#if BUILDING_DLL
# define DLLIMPORT __declspec (dllexport)
#else /* Not BUILDING_DLL */
# define DLLIMPORT __declspec (dllimport)
#endif /* Not BUILDING_DLL */

// Estructura para informar fuera de la DLL de la imagen
// Falta DNG version y camera white balance coef.
typedef struct DCRAW_ImageInfo
{
       char  *timestamp;
       char  *camera_make;
       char  *camera_model;
       char  *artist;
       int   iso_speed;
       float shutter;
       float aperture;
       float focal_length;
       float aspect_ratio;
       int   raw_width;
       int   raw_height;
       int   in_width;
       int   in_height;
       int   out_width;
       int   out_height;
       int   raw_colors;
       char  *filter_pattern;
}IMAGE_INFO;

DLLIMPORT int  DCRAW_Init(char *,int *,int *);
DLLIMPORT void DCRAW_GetInfo(IMAGE_INFO *);
DLLIMPORT unsigned short *DCRAW_Process(int);
DLLIMPORT void DCRAW_End();

#endif /* _DLL_H_ */
