#ifndef _PERFECTIMAGEDLL_H_
#define _PERFECTIMAGEDLL_H_

#if BUILDING_DLL
#define DLLIMPORT __declspec(dllexport)
#else /* Not BUILDING_DLL */
#define DLLIMPORT __declspec(dllimport)
#endif /* Not BUILDING_DLL */

#define PX8 unsigned char

DLLIMPORT void FastDrawImage(PX8 * ,PX8 * , int , int , int , int , int , int , int , int , float , PX8);

#endif // _PERFECTIMAGEDLL_H_
