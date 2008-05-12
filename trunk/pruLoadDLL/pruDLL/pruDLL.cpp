// pruDLL.cpp: define el punto de entrada de la aplicación DLL.
//

#include "pruDLL.h"
#include  <windows.h>

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
    return TRUE;
}
int contador=0;
DLLIMPORT int revelar(void)
{
	return ++contador;
}

