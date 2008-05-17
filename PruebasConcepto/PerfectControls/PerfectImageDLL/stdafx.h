// stdafx.h: archivo de inclusión de los archivos de inclusión estándar del sistema
// o archivos de inclusión específicos de un proyecto utilizados frecuentemente,
// pero rara vez modificados
//

#pragma once

// Modifique las siguientes definiciones si tiene que seleccionar como destino una plataforma antes que las especificadas a continuación.
// Consulte la referencia MSDN para obtener la información más reciente sobre los valores correspondientes a las diferentes plataformas.
#ifndef WINVER				// Permitir el uso de características específicas de Windows XP o posterior.
#define WINVER 0x0501		// Cambiar al valor apropiado correspondiente a otras versiones de Windows.
#endif

#ifndef _WIN32_WINNT		// Permitir el uso de características específicas de Windows XP o posterior.                   
#define _WIN32_WINNT 0x0501	// Cambiar al valor apropiado correspondiente a otras versiones de Windows.
#endif						

#ifndef _WIN32_WINDOWS		// Permitir el uso de características específicas de Windows 98 o posterior.
#define _WIN32_WINDOWS 0x0410 // Cambiar a fin de establecer el valor apropiado para Windows Me o posterior.
#endif

#ifndef _WIN32_IE			// Permitir el uso de las características específicas de IE 6.0 o posterior.
#define _WIN32_IE 0x0600	// Cambiar para establecer el valor apropiado a otras versiones de IE.
#endif

#define WIN32_LEAN_AND_MEAN		// Excluir material rara vez utilizado de encabezados de Windows
// Archivos de encabezado de Windows:
#include <windows.h>
#include <stdio.h>
#include <stdlib.h>
