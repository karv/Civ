namespace CivWin
{
    partial class FrmCiudad
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listTrabajos = new System.Windows.Forms.ListBox();
            this.listRecursos = new System.Windows.Forms.ListBox();
            this.textInfo = new System.Windows.Forms.TextBox();
            this.listEdificios = new System.Windows.Forms.ListBox();
            this.numTrabajador = new System.Windows.Forms.NumericUpDown();
            this.comboConstruir = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numTrabajador)).BeginInit();
            this.SuspendLayout();
            // 
            // listTrabajos
            // 
            this.listTrabajos.IntegralHeight = false;
            this.listTrabajos.Location = new System.Drawing.Point(396, 12);
            this.listTrabajos.Name = "listTrabajos";
            this.listTrabajos.Size = new System.Drawing.Size(330, 195);
            this.listTrabajos.TabIndex = 19;
            this.listTrabajos.SelectedIndexChanged += new System.EventHandler(this.listTrabajos_SelectedIndexChanged);
            // 
            // listRecursos
            // 
            this.listRecursos.FormattingEnabled = true;
            this.listRecursos.IntegralHeight = false;
            this.listRecursos.Location = new System.Drawing.Point(12, 90);
            this.listRecursos.MultiColumn = true;
            this.listRecursos.Name = "listRecursos";
            this.listRecursos.Size = new System.Drawing.Size(378, 142);
            this.listRecursos.TabIndex = 20;
            // 
            // textInfo
            // 
            this.textInfo.Location = new System.Drawing.Point(12, 12);
            this.textInfo.Multiline = true;
            this.textInfo.Name = "textInfo";
            this.textInfo.ReadOnly = true;
            this.textInfo.Size = new System.Drawing.Size(186, 72);
            this.textInfo.TabIndex = 21;
            // 
            // listEdificios
            // 
            this.listEdificios.FormattingEnabled = true;
            this.listEdificios.IntegralHeight = false;
            this.listEdificios.Location = new System.Drawing.Point(204, 12);
            this.listEdificios.Name = "listEdificios";
            this.listEdificios.Size = new System.Drawing.Size(186, 72);
            this.listEdificios.TabIndex = 22;
            // 
            // numTrabajador
            // 
            this.numTrabajador.Enabled = false;
            this.numTrabajador.Location = new System.Drawing.Point(396, 213);
            this.numTrabajador.Name = "numTrabajador";
            this.numTrabajador.Size = new System.Drawing.Size(134, 20);
            this.numTrabajador.TabIndex = 25;
            this.numTrabajador.ValueChanged += new System.EventHandler(this.numTrabajador_ValueChanged);
            // 
            // comboConstruir
            // 
            this.comboConstruir.FormattingEnabled = true;
            this.comboConstruir.Location = new System.Drawing.Point(13, 239);
            this.comboConstruir.Name = "comboConstruir";
            this.comboConstruir.Size = new System.Drawing.Size(155, 21);
            this.comboConstruir.TabIndex = 26;
            this.comboConstruir.SelectedIndexChanged += new System.EventHandler(this.comboConstruir_SelectedIndexChanged);
            // 
            // FrmCiudad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 441);
            this.Controls.Add(this.comboConstruir);
            this.Controls.Add(this.numTrabajador);
            this.Controls.Add(this.listTrabajos);
            this.Controls.Add(this.listRecursos);
            this.Controls.Add(this.textInfo);
            this.Controls.Add(this.listEdificios);
            this.Name = "FrmCiudad";
            this.Text = "FrmCiudad";
            ((System.ComponentModel.ISupportInitialize)(this.numTrabajador)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listTrabajos;
        private System.Windows.Forms.ListBox listRecursos;
        private System.Windows.Forms.TextBox textInfo;
        private System.Windows.Forms.ListBox listEdificios;
        private System.Windows.Forms.NumericUpDown numTrabajador;
        private System.Windows.Forms.ComboBox comboConstruir;


    }
}