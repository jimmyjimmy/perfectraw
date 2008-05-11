CP   = copy
PRAWDIR2008		= ..\PerfectRAW\VisualStudio2008\bin\Debug
PRAWDIRD2008   	= ..\PerfectRAW\VisualStudio2008\bin\Release

all-after:
	$(CP) bin\dcraw.dll $(PRAWDIR2008)
	$(CP) bin\dcraw.dll $(PRAWDIRD2008)	