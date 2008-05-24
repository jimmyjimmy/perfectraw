#ifndef _DLL_H_
#define _DLL_H_

#if BUILDING_DLL
# define DLLIMPORT __declspec (dllexport)
#else /* Not BUILDING_DLL */
# define DLLIMPORT __declspec (dllimport)
#endif /* Not BUILDING_DLL */

// Image info struct
// DNG version and camera white balance coef. still missing
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

typedef struct DLL_TEST
{
        int  a;
        char *b;
}TEST;
            
// Struct for passing parameters to the development process
typedef struct DCRAW_Parameters
{       
       float    threshold;         // -n -> buffer 2
       double   aber[4];           // -r -> buffer 2
       int      use_auto_wb;       // -a -> buffer 2
       int      use_camera_wb;     // -w -> buffer 2
       unsigned greybox[4];        // -A -> buffer 2      
       int      user_black;        // -K -> buffer 2
       int      user_sat;          // -S -> buffer 2
       int      test_pattern;      //    -> buffer 2       
       int      level_greens;      // -l -> buffer 2       
       int      user_qual;         // -q -> buffer 3
       int      four_color_rgb;    // -f -> buffer 3
       int      med_passes;        // -m -> buffer 4
       int      highlight;         // -H -> buffer 4
       int      output_color;      // -o -> buffer 5                     
       int      use_fuji_rotate;   // -J -> buffer 5
       float    user_gamma;        // -g -> buffer 5       
       float    exposure;          //    -> buffer 3
       int      exposure_mode;     //    -> buffer 3
}DLL_PARAMETERS;

// Struct for saving and restoring DLL state
typedef struct DCRAW_State
{
       int    valid;       
       int    filters;
       int    colors;
       int    shrink;
       int    half_size;
       int    mix_green;
       int    width;
       int    height;
       int    iwidth;
       int    iheight;
       int    top_margin;
       int    left_margin;     
       int    raw_color;  
       float  cam_mul[4];
       float  pre_mul[4];
       float  cmatrix[3][4];
       float  rgb_cam[3][4];
       unsigned short (*buffer)[4];
}DLL_STATE;

DLLIMPORT void DCRAW_DefaultParameters(DLL_PARAMETERS *);
DLLIMPORT int  DCRAW_Init(char *,int *,int *,int *, int *);
DLLIMPORT void DCRAW_GetInfo(IMAGE_INFO *);
DLLIMPORT unsigned short *DCRAW_Process(DLL_PARAMETERS *, int *, int *);
DLLIMPORT void DCRAW_End();
DLLIMPORT int Coffin(int, char **);

#endif /* _DLL_H_ */
