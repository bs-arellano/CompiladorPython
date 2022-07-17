//Brayan Arellano - 20191020151
//Jesus Lozada - 20191020098
//Juan Hurtado - 20191020082

//Compilador de Python3
namespace CompiladorPython
{
    internal static class Principal
    {
        /// Inicia la interfaz grafica
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new VentanaPrincipal());
        }
    }
}