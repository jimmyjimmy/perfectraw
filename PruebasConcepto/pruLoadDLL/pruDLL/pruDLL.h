// El siguiente bloque ifdef muestra la forma estándar de crear macros que facilitan 
// la exportación de archivos DLL. Todos los archivos de este archivo DLL se compilan con el símbolo PRUDLL_EXPORTS
// definido en la línea de comandos. Este símbolo no se debe definir en ningún proyecto
// que utilice este archivo DLL. De este modo, otros proyectos cuyos archivos de código fuente incluyan el archivo 
// interpretan que las funciones PRUDLL_API se importan de un archivo DLL, mientras que este archivo DLL interpreta los símbolos
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
	// TODO: agregar métodos aquí.
};

extern PRUDLL_API int contador;

PRUDLL_API int revelar(void);
