// El siguiente bloque ifdef muestra la forma est�ndar de crear macros que facilitan 
// la exportaci�n de archivos DLL. Todos los archivos de este archivo DLL se compilan con el s�mbolo PRUDLL_EXPORTS
// definido en la l�nea de comandos. Este s�mbolo no se debe definir en ning�n proyecto
// que utilice este archivo DLL. De este modo, otros proyectos cuyos archivos de c�digo fuente incluyan el archivo 
// interpretan que las funciones PRUDLL_API se importan de un archivo DLL, mientras que este archivo DLL interpreta los s�mbolos
// definidos en esta macro como si fueran exportados.
#ifdef PRUDLL_EXPORTS
#define PRUDLL_API __declspec(dllexport)
#else
#define PRUDLL_API __declspec(dllimport)
#endif

// Clase exportada de pruDLL.dll
class PRUDLL_API CpruDLL {
public:
	CpruDLL(void);
	// TODO: agregar m�todos aqu�.
};

extern PRUDLL_API int contador;

PRUDLL_API int revelar(void);
