using Lexico;
using System.Collections;

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

        private void compilarBtn_Click(object sender, EventArgs e)
        {
            Lexico.Lexico analizadorLexico = new Lexico.Lexico();
            analizadorLexico.Analizar(selectedFileURL);
            ArrayList tokens = analizadorLexico.getTokens();

            Sintactico.Sintactico analizadorSintactico = new Sintactico.Sintactico();
            analizadorSintactico.Analizar(tokens);

            tokensList.Visible = true;
            tokensList.Columns.Clear();
            tokensList.Rows.Clear();

            tokensList.Columns.Add("Key", "Key");
            tokensList.Columns.Add("KeyType", "Key Type");

            foreach ( Tuple<string, string> token in tokens)
            {
                tokensList.Rows.Add(new Object[] { token.Item1.ToString(), token.Item2.ToString() });
            }

            if (tokens.Count > 0)
            {
                labelLexico.ForeColor = System.Drawing.Color.Green;
            }
            if(analizadorSintactico.getCorrecto() == true)
            {
                labelLexico.ForeColor = System.Drawing.Color.Blue;
            }
        }

    }
}