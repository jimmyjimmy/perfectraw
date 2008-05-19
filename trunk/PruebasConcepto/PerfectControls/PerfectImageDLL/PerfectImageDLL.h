#ifndef _PERFECTIMAGEDLL_H_
#define _PERFECTIMAGEDLL_H_

#if BUILDING_DLL
#define DLLIMPORT __declspec(dllexport)
#else /* Not BUILDING_DLL */
#define DLLIMPORT __declspec(dllimport)
#endif /* Not BUILDING_DLL */

#define PX8 unsigned char
extern "C" {
DLLIMPORT void FastDrawImage(PX8 * ,PX8 * ,int ,int, int, int, int, int, int, int, float, PX8 );
DLLIMPORT void FastDrawBiHImage(PX8 * ,PX8 *, PX8 *, int, int, int, int, int, int, int, int, float, PX8 );
DLLIMPORT void FastDrawBiVImage(PX8 * ,PX8 *, PX8 *, int, int, int, int, int, int, int, int, float, PX8 );
DLLIMPORT void FastDrawImageFlash(PX8 * ,PX8 *, int, int, int, int, int, int, int, int, float, PX8, int );
}
#endif // _PERFECTIMAGEDLL_H_
