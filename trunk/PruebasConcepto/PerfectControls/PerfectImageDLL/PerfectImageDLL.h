#ifndef _PERFECTIMAGEDLL_H_
#define _PERFECTIMAGEDLL_H_

#if BUILDING_DLL
#define DLLIMPORT __declspec(dllexport)
#else /* Not BUILDING_DLL */
#define DLLIMPORT __declspec(dllimport)
#endif /* Not BUILDING_DLL */

typedef unsigned char PX8;
typedef unsigned short PX16;

extern "C" {
DLLIMPORT void FastDrawImage16MarkBP(PX8 *outputImg, PX16 *inputImg, int outImgWidth, int outImgHeight, int inImgWidth, int inImgHeight, int outImgRowPadding, int inImgRowPadding, int x, int y, float z, PX8 fillColor);
DLLIMPORT void FastDrawImage16(PX8 *outputImg, PX16 *inputImg, int outImgWidth, int outImgHeight, int inImgWidth, int inImgHeight, int outImgRowPadding, int inImgRowPadding, int x, int y, float z, PX8 fillColor);
DLLIMPORT void FastDrawImage16VSplitMarkBP(PX8 *outputImg, PX16 *inputLeftImg, PX16 *inputRightImg, int outImgWidth, int outImgHeight, int inImgWidth, int inImgHeight, int outImgRowPadding, int inImgRowPadding, int x, int y, float z, PX8 fillColor);
DLLIMPORT void FastDrawImage16VSplit(PX8 *outputImg, PX16 *inputLeftImg, PX16 *inputRightImg, int outImgWidth, int outImgHeight, int inImgWidth, int inImgHeight, int outImgRowPadding, int inImgRowPadding, int x, int y, float z, PX8 fillColor);
DLLIMPORT void FastDrawImage16HSplitMarkBP(PX8 *outputImg, PX16 *inpTopImg, PX16 *inpBottomImg, int outImgWidth, int outImgHeight, int inImgWidth, int inImgHeight, int outImgRowPadding, int inImgRowPadding, int x, int y, float z, PX8 fillColor);
DLLIMPORT void FastDrawImage16HSplit(PX8 *outputImg, PX16 *inpTopImg, PX16 *inpBottomImg, int outImgWidth, int outImgHeight, int inImgWidth, int inImgHeight, int outImgRowPadding, int inImgRowPadding, int x, int y, float z, PX8 fillColor);
}
#endif // _PERFECTIMAGEDLL_H_
