CP   = copy
PRAWDIR   = ..\PerfectRAW\bin\Debug
PRAWDIRD   = ..\PerfectRAW\bin\Release

all-after:
	$(CP) bin\dcraw.dll $(PRAWDIR)
	$(CP) bin\dcraw.dll $(PRAWDIRD)	