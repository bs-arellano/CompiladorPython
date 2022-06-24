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
            if (tokens.Count > 0)
            {
                labelLexico.ForeColor = System.Drawing.Color.Green;
            }
        }
    }
}