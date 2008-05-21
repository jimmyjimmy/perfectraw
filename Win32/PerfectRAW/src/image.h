#ifndef _IMAGE_H_
#define _IMAGE_H_

#if BUILDING_DLL
# define DLLIMPORT __declspec (dllexport)
#else /* Not BUILDING_DLL */
# define DLLIMPORT __declspec (dllimport)
#endif /* Not BUILDING_DLL */

#define PX8 unsigned char
#define PX16 unsigned short

double powF (double , double );
PX16 sRGBgLUT[65536]; // Provisional LUT for sRGB gamma this will be inside dcraw.dll in the near future
PX16 sRGBgamma(PX16 );

DLLIMPORT void Convert48RGBto24BGR(PX16 *, PX8 *, int, int, int);

#endif /* _IMAGE_H_ */
