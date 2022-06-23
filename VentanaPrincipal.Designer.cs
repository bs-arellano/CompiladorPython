namespace CompiladorPython
{
    partial class VentanaPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VentanaPrincipal));
            this.opnBttn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textPath = new System.Windows.Forms.TextBox();
            this.showCodeBttn = new System.Windows.Forms.Button();
            this.showTableBttn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // opnBttn
            // 
            this.opnBttn.Location = new System.Drawing.Point(701, 12);
            this.opnBttn.Name = "opnBttn";
            this.opnBttn.Size = new System.Drawing.Size(75, 23);
            this.opnBttn.TabIndex = 0;
            this.opnBttn.Text = "Abrir";
            this.opnBttn.UseVisualStyleBackColor = true;
            this.opnBttn.Click += new System.EventHandler(this.openBttn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Archivo Python";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textPath
            // 
            this.textPath.Location = new System.Drawing.Point(145, 12);
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(525, 23);
            this.textPath.TabIndex = 2;
            // 
            // showCodeBttn
            // 
            this.showCodeBttn.Location = new System.Drawing.Point(26, 52);
            this.showCodeBttn.Name = "showCodeBttn";
            this.showCodeBttn.Size = new System.Drawing.Size(110, 23);
            this.showCodeBttn.TabIndex = 3;
            this.showCodeBttn.Text = "Mostrar codigo";
            this.showCodeBttn.UseVisualStyleBackColor = true;
            this.showCodeBttn.Click += new System.EventHandler(this.showCodeBttn_Click);
            // 
            // showTableBttn
            // 
            this.showTableBttn.Location = new System.Drawing.Point(145, 52);
            this.showTableBttn.Name = "showTableBttn";
            this.showTableBttn.Size = new System.Drawing.Size(165, 23);
            this.showTableBttn.TabIndex = 4;
            this.showTableBttn.Text = "Mostrar tabla de simbolos";
            this.showTableBttn.UseVisualStyleBackColor = true;
            this.showTableBttn.Click += new System.EventHandler(this.showTableBttn_Click);
            // 
            // VentanaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.showTableBttn);
            this.Controls.Add(this.showCodeBttn);
            this.Controls.Add(this.textPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.opnBttn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VentanaPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compilador Pyhton";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button opnBttn;
        private Label label1;
        private TextBox textPath;
        private Button showCodeBttn;
        private Button showTableBttn;
    }
}