//This Function does the following operations:
//Receives a 16bit 3 channel RGB image (inputImg) of size inImgWidth x inImgHeight
//and crops and scale it in an 8 bit 3 channel RGB 
DLLIMPORT void DrawImage(PX8 *outputImg, PX16 *inputImg, int outImgWidth, int outImgHeight, int inImgWidth, int inImgHeight, int outImgRowPadding, int inImgRowPadding, int x, int y, float z, PX8 fillColor)
{
   int i,j,k,l,n;
   float zf;
   int zz, zzf;
   int cx,cy;
   int w,h;
   
   // Initialize colors for blinking pixels
   //InitColorMask();
   
   // Some precalcs
   zf=1/z;
   zz=(int)z;
   zzf=(int)zf;
   
   // Fill destination outputImg outImgWidth specified background color
   memset(outputImg,fillColor,(outImgWidth*3+outImgRowPadding)*outImgHeight);   
      
   // Get real sizes (avoid overbuffering)
   w=outImgWidth;
   h=outImgHeight;
  
   if(x<0) x=0;
   if(y<0) y=0;

   if(z>1){
      // Solution for out of bounds with zoom > 1.0
      if((int)(x+zf*(w+0.5))>=inImgWidth) x=(int)(inImgWidth-w*zf);
      if((int)(y+zf*(h+0.5))>=inImgHeight) y=(int)(inImgHeight-h*zf);
   }else{        
      // Solution for out of bounds with zoom <= 1.0      
      if((int)(x+zf*(w-0.5))>=inImgWidth) {w=(int)(inImgWidth*z-x);}
      if((int)(y+zf*(h-0.5))>=inImgHeight) {h=(int)(inImgHeight*z-y);}
   }
         
   // Center render in outputImg    
   cx=(int)((float)outImgWidth/2-(int)((float)inImgWidth*z/2.0));
   cy=(int)((float)outImgHeight/2-(int)((float)inImgHeight*z/2.0));   
   if(cx<0) cx=0;
   if(cy<0) cy=0;
   if((int)(cx+inImgWidth*z)>=outImgWidth) cx=0;
   if((int)(cy+inImgHeight*z)>=outImgHeight) cy=0;
   
   // Multiply x coordinates for 3 channels
   w=w*3;   
   outImgWidth=outImgWidth*3;
   inImgWidth=inImgWidth*3;    
   x=x*3;
   cx=cx*3;
      
   // Nearest Neighbour interpolation
   if( z < 1) {
		inputImg+=y*(inImgWidth+inImgRowPadding);
		outputImg+=cy*(outImgWidth+outImgRowPadding);
		for(i=0;i<h;i++){
		   outputImg+=cx;
		   for(j=0;j<w;j+=3){
			  ConvertPixelFrom16to8(outputImg+j,inputImg+x+j*zzf);
		   }
		   outputImg+=outImgWidth+outImgRowPadding-cx;
		   inputImg+=zzf*inImgWidth+inImgRowPadding;
		}
   } else {
		outputImg+=cy*(outImgWidth+outImgRowPadding);
		for(i=0;i<h;i+=zz){   
		   outputImg+=cx;
		   k=(int)((y+i*zf)*(inImgWidth+inImgRowPadding)+x);       
		   for(j=0;j<w;j+=3*zz){                              
			  l=(int)(k+j*zf);                  
				n=0;
			  // Pixel copy
			  while((n<3*zz)&&(j+n<w)){
				  ConvertPixelFrom16to8(outputImg+j+n,inputImg+l);                      
				  n+=3;
			  }
		   }                                 
		   // Full lines replication
		   j=1;
		   while((j+i<h)&&(j<zz)){
			 memcpy(outputImg+(outImgWidth+outImgRowPadding)*j,outputImg,w);
			 j++;
		   }
		   outputImg+=zz*(outImgWidth+outImgRowPadding)-cx;
		}
   }
}

//This Function does the following operations:
//Receives a two 16bit 3 channel RGB image (inputLeftImg,inputRightImg) of size inImgWidth x inImgHeight
//and crops and scale them  in an 8 bit 3 channel RGB. The output images would be 
//split in two parts, left one with image from inputLeftImg and right one from inputRightImg.
DLLIMPORT void DrawImageVSplit(PX8 *outputImg, PX16 *inputLeftImg, PX16 *inputRightImg, int outImgWidth, int outImgHeight, int inImgWidth, int inImgHeight, int outImgRowPadding, int inImgRowPadding, int x, int y, float z, PX8 fillColor)
{
   int i,j,k,l,n;
   float zf;
   int zz, zzf;
   int cx,cy;
   int w,h;   
   int m1,m2;
   int ww;

   // Some precalcs
   zf=1/z;
   zz=(int)z;
   zzf=(int)zf;
   
   // Fill destination outputImg outImgWidth specified background color
   memset(outputImg,fillColor,(outImgWidth*3+outImgRowPadding)*outImgHeight);   
      
   // Get real sizes (avoid overbuffering)
   w=outImgWidth;
   h=outImgHeight;
  
   if(x<0) x=0;
   if(y<0) y=0;

   if(z>1){
      // Solution for out of bounds with zoom > 1.0
      if((int)(x+zf*(w+0.5))>=inImgWidth) x=(int)(inImgWidth-w*zf);
      if((int)(y+zf*(h+0.5))>=inImgHeight) y=(int)(inImgHeight-h*zf);
   }else{        
	  // Solution for out of bounds with zoom <= 1.0	  
	  if((int)(x+zf*(w-0.5))>=inImgWidth) {w=(int)(inImgWidth*z-x);}
	  if((int)(y+zf*(h-0.5))>=inImgHeight) {h=(int)(inImgHeight*z-y);}
   }
         
   // Center render in outputImg    
   cx=(int)((float)outImgWidth/2-(int)((float)inImgWidth*z/2.0));
   cy=(int)((float)outImgHeight/2-(int)((float)inImgHeight*z/2.0));   
   if(cx<0) cx=0;
   if(cy<0) cy=0;
   if((int)(cx+inImgWidth*z)>=outImgWidth) cx=0;
   if((int)(cy+inImgHeight*z)>=outImgHeight) cy=0;      

   // Multiply x coordinates for 3 channels
   w=w*3;   
   outImgWidth=outImgWidth*3;
   inImgWidth=inImgWidth*3;    
   x=x*3;
   cx=cx*3;

   ww= w/2;
   m1=(int)(ww*zf);
   m2=(int)(w*zf-m1);
      
   // Nearest Neighbour interpolation
   
   if(z<1) {
		inputLeftImg+=y*(inImgWidth+inImgRowPadding);
		inputRightImg+=y*(inImgWidth+inImgRowPadding);
		outputImg+=cy*(outImgWidth+outImgRowPadding);
		for(i=0;i<h;i++){
		   outputImg+=cx;
		   //Copy selected pixels  from left image
		   for(j=0;j<ww;j+=3){
			  ConvertPixelFrom16to8(outputImg+j,inputLeftImg+x+j*zzf);
		   }
		   //Copy selected `pixels from right image
		   for(;j<w;j+=3){
			  ConvertPixelFrom16to8(outputImg+j,inputRightImg+x+j*zzf);
		   }
		   outputImg+=outImgWidth+outImgRowPadding-cx;
		   inputLeftImg+=zzf*inImgWidth+inImgRowPadding;
		   inputRightImg+=zzf*inImgWidth+inImgRowPadding;
		}
   }else {
		outputImg+=cy*(outImgWidth+outImgRowPadding);
		for(i=0;i<h;i+=zz){   
		   outputImg+=cx;
		   k=(int)((y+i*zf)*(inImgWidth+inImgRowPadding)+x);       
		   for(j=0;j<w;j+=3*zz){                              
			  l=(int)(k+j*zf);				  

			  // Pixel copy
			  n=0;
			  //Copy pixels from left image
			  while((n<3*zz)&&(j+n<ww)){
				  ConvertPixelFrom16to8(outputImg+j+n,inputLeftImg+l);
				  n+=3;
			  }
			  //Copy pixels from right image
			  while((n<3*zz)&&(j+n<w)){
				  ConvertPixelFrom16to8(outputImg+j+n,inputRightImg+l);
				  n+=3;
			  }
		   }                                  
		   // Full lines replication
		   j=1;
		   while((j+i<h)&&(j<zz)){
			 memcpy(outputImg+(outImgWidth+outImgRowPadding)*j,outputImg,w);
			 j++;
		   }
		   outputImg+=zz*(outImgWidth+outImgRowPadding)-cx;       
		}
   }
}

//This Function does the following operations:
//Receives a two 16bit 3 channel RGB image (inputLeftImg,inputRightImg) of size inImgWidth x inImgHeight
//and crops and scale them  in an 8 bit 3 channel RGB. The output images would be 
//split in two parts, left one with image from inputLeftImg and right one from inputRightImg.
DLLIMPORT void DrawImageHSplit(PX8 *outputImg, PX16 *inpTopImg,PX16 *inpBottomImg, int outImgWidth, int outImgHeight, int inImgWidth, int inImgHeight, int outImgRowPadding, int inImgRowPadding, int x, int y, float z, PX8 fillColor)
{
   int i,j,k,l,n;
   float zf;
   int zz, zzf;
   int cx,cy;
   int w,h,hh;
   

   // Some precalcs
   zf=1/z;
   zz=(int)z;
   zzf=(int)zf;
   
   // Fill destination outputImg outImgWidth specified background color
   memset(outputImg,fillColor,(outImgWidth*3+outImgRowPadding)*outImgHeight);   
      
   // Get real sizes (avoid overbuffering)
   w=outImgWidth;
   h=outImgHeight;
  
   if(x<0) x=0;
   if(y<0) y=0;

   if(z>1){
      // Solution for out of bounds with zoom > 1.0
      if((int)(x+zf*(w+0.5))>=inImgWidth) x=(int)(inImgWidth-w*zf);
      if((int)(y+zf*(h+0.5))>=inImgHeight) y=(int)(inImgHeight-h*zf);
   }else{        
      // Solution for out of bounds with zoom <= 1.0      
      if((int)(x+zf*(w-0.5))>=inImgWidth) {w=(int)(inImgWidth*z-x);}
      if((int)(y+zf*(h-0.5))>=inImgHeight) {h=(int)(inImgHeight*z-y);}
   }
   // Center render in outputImg    
   cx=(int)((float)outImgWidth/2-(int)((float)inImgWidth*z/2.0));
   cy=(int)((float)outImgHeight/2-(int)((float)inImgHeight*z/2.0));   
   if(cx<0) cx=0;
   if(cy<0) cy=0;
   if((int)(cx+inImgWidth*z)>=outImgWidth) cx=0;
   if((int)(cy+inImgHeight*z)>=outImgHeight) cy=0;
   
   // Multiply x coordinates for 3 channels
   w=w*3;   
   outImgWidth=outImgWidth*3;
   inImgWidth=inImgWidth*3;    
   x=x*3;
   cx=cx*3;
      
   hh= h/2;
   // Nearest Neighbour interpolation
   if( z < 1) {
		outputImg+=cy*(outImgWidth+outImgRowPadding);
		//Copy Top image lines
		inpTopImg+=y*(inImgWidth+inImgRowPadding);
		for(i=0;i<hh;i++){
		   outputImg+=cx;
		   for(j=0;j<w;j+=3){
			  ConvertPixelFrom16to8(outputImg+j,inpTopImg+x+j*zzf);
		   }
		   outputImg+=outImgWidth+outImgRowPadding-cx;
		   inpTopImg+=zzf*inImgWidth+inImgRowPadding;
		}       
		//Copy bottom image lines
		inpBottomImg+=(y+(int)(zf*hh))*(inImgWidth+inImgRowPadding);
		for(;i<h;i++){
		   outputImg+=cx;
		   for(j=0;j<w;j+=3){
			  ConvertPixelFrom16to8(outputImg+j,inpBottomImg+x+j*zzf);
		   }
		   outputImg+=outImgWidth+outImgRowPadding-cx;
		   inpBottomImg+=zzf*inImgWidth+inImgRowPadding;
		}       
   } else {
	   	outputImg+=cy*(outImgWidth+outImgRowPadding);
		//Scale Top part of image
		for(i=0;i<hh;i+=zz){   
		   outputImg+=cx;
		   k=(int)((y+i*zf)*(inImgWidth+inImgRowPadding)+x);       
		   for(j=0;j<w;j+=3*zz){                              
			  l=(int)(k+j*zf);                  
				n=0;
			  // Pixel copy
			  while((n<3*zz)&&(j+n<w)){
				  ConvertPixelFrom16to8(outputImg+j+n,inpTopImg+l);                      
				  n+=3;
			  }
		   }                                 
		   // Full lines replication
		   j=1;
		   while((j+i<h)&&(j<zz)){
			 memcpy(outputImg+(outImgWidth+outImgRowPadding)*j,outputImg,w);
			 j++;
		   }
		   outputImg+=zz*(outImgWidth+outImgRowPadding)-cx;
		}
		//Scale Bottom part of image
		for(;i<h;i+=zz){   
		   outputImg+=cx;
		   k=(int)((y+i*zf)*(inImgWidth+inImgRowPadding)+x);       
		   for(j=0;j<w;j+=3*zz){                              
			  l=(int)(k+j*zf);                  
				n=0;
			  // Pixel copy
			  while((n<3*zz)&&(j+n<w)){
				  ConvertPixelFrom16to8(outputImg+j+n,inpBottomImg+l);                      
				  n+=3;
			  }
		   }                                 
		   // Full lines replication
		   j=1;
		   while((j+i<h)&&(j<zz)){
			 memcpy(outputImg+(outImgWidth+outImgRowPadding)*j,outputImg,w);
			 j++;
		   }
		   outputImg+=zz*(outImgWidth+outImgRowPadding)-cx;
		}

   }
}