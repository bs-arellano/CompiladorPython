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

        private void showCodeBttn_Click(object sender, EventArgs e)
        {
            
        }

        private void showTableBttn_Click(object sender, EventArgs e)
        {

        }
    }
}