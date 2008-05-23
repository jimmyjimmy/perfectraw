#ifndef _DLL_H_
#define _DLL_H_

#if BUILDING_DLL
# define DLLIMPORT __declspec (dllexport)
#else /* Not BUILDING_DLL */
# define DLLIMPORT __declspec (dllimport)
#endif /* Not BUILDING_DLL */

#define PX8 unsigned char

DLLIMPORT void FastDrawImage(PX8 * ,PX8 * ,int ,int, int, int, int, int, int, int, float, PX8 );
DLLIMPORT void FastDrawBiHImage(PX8 * ,PX8 *, PX8 *, int, int, int, int, int, int, int, int, float, PX8 );
DLLIMPORT void FastDrawBiVImage(PX8 * ,PX8 *, PX8 *, int, int, int, int, int, int, int, int, float, PX8 );
DLLIMPORT void FastDrawImageFlash(PX8 * ,PX8 *, int, int, int, int, int, int, int, int, float, PX8, int );

#endif /* _DLL_H_ */
