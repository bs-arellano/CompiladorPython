//Brayan Arellano - 20191020151
//Jesus Lozada - 20191020098
//Juan Hurtado - 20191020082

//Fases del compilador
using Lexico;
using Sintactico;
using Intermedio;

using System.Collections;

//ventana Principal
namespace CompiladorPython
{
    public partial class VentanaPrincipal : Form
    {
        string selectedFileURL = string.Empty;
        public VentanaPrincipal()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        //Lanza explorador de windows
        private void openBttn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Codigo Python|*.py|Todos los archivos|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedFileURL = ofd.InitialDirectory+ofd.FileName;
                    textPath.Text = selectedFileURL;
                }
            }
        }
        //Boton para compilar, 
        private void compilarBtn_Click(object sender, EventArgs e)
        {
            //Genera nueva instancia del analizador lexico
            Lexico.Lexico analizadorLexico = new Lexico.Lexico();
            //Pasa el archivo seleccionado y obtiene los tokens identificados
            try
            {
                analizadorLexico.Analizar(selectedFileURL);
            }
            catch(ArgumentException)
            {
                MessageBox.Show("Por favor ingrese una direccion valida");
            }
            ArrayList tokens = analizadorLexico.getTokens();
            //Intancia del analizador sintactico
            Sintactico.Sintactico analizadorSintactico = new Sintactico.Sintactico();
            //Analiza sintacticamente los tokens obtenidos y obtiene el arbol
            analizadorSintactico.Analizar(tokens);
            ArbolSintactico arbol = analizadorSintactico.getArbol();
            arbol.imprimir();
            //Analiza semanticamenta la consistencia de tipos
            Semantico.Semantico analizadorSemantico = new Semantico.Semantico();
            if(!analizadorSemantico.AnalizarTipos(tokens, arbol))
            {
                MessageBox.Show("Error en tipos");
            }
            analizadorSemantico.imprimirTipos();
            Intermedio.Intermedio intermedio = new Intermedio.Intermedio();
            intermedio.generar(arbol);
            //Muestra la tabla de tokens identificados
            tokensList.Visible = true;
            //Limpia la tabla
            tokensList.Columns.Clear();
            tokensList.Rows.Clear();
            //Añade los tokens
            tokensList.Columns.Add("Key", "Key");
            tokensList.Columns.Add("KeyType", "Key Type");
            foreach ( Tuple<string, string> token in tokens)
            {
                tokensList.Rows.Add(new Object[] { token.Item1.ToString(), token.Item2.ToString() });
            }
            //Si el analizador lexico ha generado tokens
            if (tokens.Count > 0)
            {
                labelLexico.ForeColor = System.Drawing.Color.Green;
            }
            //Si el programa es sintacticamente correcto
            if(analizadorSintactico.getCorrecto() == true)
            {
                labelLexico.ForeColor = System.Drawing.Color.Blue;
            }
        }

    }
}