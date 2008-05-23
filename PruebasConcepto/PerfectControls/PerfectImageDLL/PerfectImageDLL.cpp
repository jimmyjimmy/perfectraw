#include "PerfectImageDLL.h"
#include <windows.h>
#include <stdio.h>
#include <stdlib.h>



//This function converts a 16bit color in an 8bit one.
//It does so just dividing by 256.
inline void PixelFrom16to8(PX8 *data8,PX16 *data16)
{
    data8[0]= data16[0]>>8;
    data8[1]= data16[1]>>8;
    data8[2]= data16[2]>>8;
}


//Funcion to mark a burned pixel in at least one channel, changing its color.
//If the pixel has no burned channel, it will receive the original color.
//The color is assigned by a color table.
/*
We will mark the burned channel with its bit to one in an index variable.
Bit 0 is Red channel.
Bit 1 is Gren channel
Bit 2 is Blue channel
Following tableLa siguiente tabla con las posibles combinaciones de valores quemados y los colores que se quieren asignar:
Following combination of burned channels are possible and its desired marked color.
The index generated with the combination of bits will served as an entry in the color
table.
index | Blue | Green | Red | Desired burned color
0       0      0       0      Original pixel color
1       0      0       1      RGB=(128,  0,  0)
2       0      1       0      RGB=(  0,128,  0)
3       0      1       1      RGB=(128,128,  0)
4       1      0       0      RGB=(  0,  0,128)
5       1      0       1      RGB=(128,  0,128)
6       1      1       0      RGB=(  0,128,128)
7       1      1       1      RGB=(128,128,128)
*/
//const PX8 tablaR[8]= {0,128,0,128,0,128,0,128};
//const PX8 tablaB[8]= {0,0,0,0,128,128,128,128};
//const PX8 tablaG[8]= {0,0,128,128,0,0,128,128};
const PX8 tablaR[8]= {0,128,0,128,0,128,0,0};
const PX8 tablaB[8]= {0,0,0,0,128,128,128,0};
const PX8 tablaG[8]= {0,0,128,128,0,0,128,0};

//Maximum value of a channel to be considered as a non burned channel.
const PX16 umbral= 65530;

inline void PixelFrom16to8MarkBP(PX8 *data8,PX16 *data16)
{
	int indice=0;
	indice = 
	//Mar bit of burned channels in pixel
	indice = ((data16[2]>=umbral)<<2) | 
	         ((data16[1]>=umbral)<<1) |
	          (data16[0]>=umbral);

    if( indice ) {
		//If index is not zero, it has some burned channel.
		//Assign its color through the table.
		data8[0]= tablaR[indice];
		data8[1]= tablaG[indice];
		data8[2]= tablaB[indice];
		return;
	}
	//The pixel has no burned channel, Assign the color from the original 16bit one.
	PixelFrom16to8(data8,data16);
}


//Here come the functions that Scale a 16 bit image to a 8 bit image
//There will be two versions of each of them. One of them makes no burned pixel marking.
//The other with the same name and ended in MarkBP will make the same but marking the
//burned pixels.
//Both versions are identical, but one just calls PixelFrom16to8 to make the
//conversion from the 16 to 8 bit color. The second one calls PixelFrom16to8MarkBP
//to mark the burned channels and make the conversion.
//As they are identical, they are defined in PerfectImageDLL.inc and macros are used
//to generate the two version.
//This method is prefer to using a variable to decide if we want burned pixel processing or not
//because if done so, we need to make a flag comparation for each pixel in the image.
//The macro method is used not to duplicate the code that would be error prone if we
//make later changes to it.

//Use the name of the color 16 to 8 bit converter with burned channel marking
#define ConvertPixelFrom16to8 PixelFrom16to8MarkBP

//Define the name of the function that scales the image and marks the burned channels
#define DrawImage FastDrawImage16MarkBP
#define DrawImageVSplit FastDrawImage16VSplitMarkBP
#define DrawImageHSplit FastDrawImage16HSplitMarkBP
#include "PerfectImageDLL.inc.h"
#undef DrawImage
#undef DrawImageVSplit
#undef DrawImageHSplit
#undef ConvertPixelFrom16to8

//Use the name of the color 16 to 8 bit converter with burned channel marking
#define ConvertPixelFrom16to8 PixelFrom16to8

//Define the name of the function that scales the image with no burned channel marking.
#define DrawImage FastDrawImage16
#define DrawImageVSplit FastDrawImage16VSplit
#define DrawImageHSplit FastDrawImage16HSplit
#include "PerfectImageDLL.inc.h"
#undef DrawImage
#undef DrawImageVSplit
#undef DrawImageHSplit
#undef ConvertPixelFrom16to8