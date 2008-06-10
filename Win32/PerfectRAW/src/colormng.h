#ifndef _COLORMNG_H_
#define _COLORMNG_H_

#if BUILDING_DLL
# define DLLIMPORT __declspec (dllexport)
#else /* Not BUILDING_DLL */
# define DLLIMPORT __declspec (dllimport)
#endif /* Not BUILDING_DLL */

#define PX8  unsigned char
#define PX16 unsigned short

#define FORC(cnt) for (c=0; c < cnt; c++)
#define FORC3 FORC(3)
#define FORCC FORC(colors)

#define MIN(a,b) ((a) < (b) ? (a) : (b))
#define MAX(a,b) ((a) > (b) ? (a) : (b))
#define LIM(x,min,max) MAX(min,MIN(x,max))
#define CLIP(x) LIM(x,0,65535)

float lastGamma;
PX16 sRGBgLUT[65536];
PX16 gLUT[65536];

PX16 sRGBgamma(PX16 );
PX16 Gamma(PX16 , float );
double powF (double , double );

DLLIMPORT void Convert48RGBto24BGR(PX16 *, PX8 *, int, int, int, float *, int, float, char *, char *);
DLLIMPORT void DrawImage(PX8 *, PX8 *, int , int , int , int , int , int , int , int , float , PX8 );

#endif /* _COLORMNG_H_ */
