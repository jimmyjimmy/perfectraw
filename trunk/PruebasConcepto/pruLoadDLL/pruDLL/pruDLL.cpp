// pruDLL.cpp: define el punto de entrada de la aplicación DLL.
//

#include "stdafx.h"
#include "pruDLL.h"


#ifdef _MANAGED
#pragma managed(push, off)
#endif

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
    return TRUE;
}

#ifdef _MANAGED
#pragma managed(pop)
#endif

// Ejemplo de variable exportada
PRUDLL_API int contador=0;

// Ejemplo de función exportada.
//extern "C" 
PRUDLL_API int  revelar(void)
{
	return ++contador;
}