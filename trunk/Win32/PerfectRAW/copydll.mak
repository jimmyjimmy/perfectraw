CP              = xcopy /Y /Q /I /C
PRAWDIRD2005    = VisualStudio2005\bin\Release
PRAWDIR2008		  = VisualStudio2008\bin\Debug
PRAWDIRD2008   	= VisualStudio2008\bin\Release

all-after:	
	$(CP) VisualStudio2005\bin\Debug\image.dll $(PRAWDIRD2005)	          
	$(CP) VisualStudio2005\bin\Debug\image.dll $(PRAWDIR2008)
	$(CP) VisualStudio2005\bin\Debug\image.dll $(PRAWDIRD2008)	
