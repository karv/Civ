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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCiudad));
			this.listTrabajos = new System.Windows.Forms.ListBox();
			this.listRecursos = new System.Windows.Forms.ListBox();
			this.textInfo = new System.Windows.Forms.TextBox();
			this.listEdificios = new System.Windows.Forms.ListBox();
			this.numTrabajador = new System.Windows.Forms.NumericUpDown();
			this.comboConstruir = new System.Windows.Forms.ComboBox();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.pbEdif = new System.Windows.Forms.ProgressBar();
			((System.ComponentModel.ISupportInitialize)(this.numTrabajador)).BeginInit();
			this.toolStrip2.SuspendLayout();
			this.SuspendLayout();
			// 
			// listTrabajos
			// 
			this.listTrabajos.IntegralHeight = false;
			this.listTrabajos.Location = new System.Drawing.Point(396, 28);
			this.listTrabajos.Name = "listTrabajos";
			this.listTrabajos.Size = new System.Drawing.Size(330, 195);
			this.listTrabajos.TabIndex = 19;
			this.listTrabajos.SelectedIndexChanged += new System.EventHandler(this.listTrabajos_SelectedIndexChanged);
			// 
			// listRecursos
			// 
			this.listRecursos.FormattingEnabled = true;
			this.listRecursos.IntegralHeight = false;
			this.listRecursos.Location = new System.Drawing.Point(12, 106);
			this.listRecursos.MultiColumn = true;
			this.listRecursos.Name = "listRecursos";
			this.listRecursos.Size = new System.Drawing.Size(378, 142);
			this.listRecursos.TabIndex = 20;
			// 
			// textInfo
			// 
			this.textInfo.Location = new System.Drawing.Point(12, 28);
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
			this.listEdificios.Location = new System.Drawing.Point(204, 28);
			this.listEdificios.Name = "listEdificios";
			this.listEdificios.Size = new System.Drawing.Size(186, 72);
			this.listEdificios.TabIndex = 22;
			// 
			// numTrabajador
			// 
			this.numTrabajador.Enabled = false;
			this.numTrabajador.Location = new System.Drawing.Point(396, 229);
			this.numTrabajador.Name = "numTrabajador";
			this.numTrabajador.Size = new System.Drawing.Size(134, 20);
			this.numTrabajador.TabIndex = 25;
			this.numTrabajador.ValueChanged += new System.EventHandler(this.numTrabajador_ValueChanged);
			// 
			// comboConstruir
			// 
			this.comboConstruir.FormattingEnabled = true;
			this.comboConstruir.Location = new System.Drawing.Point(13, 255);
			this.comboConstruir.Name = "comboConstruir";
			this.comboConstruir.Size = new System.Drawing.Size(155, 21);
			this.comboConstruir.TabIndex = 26;
			this.comboConstruir.SelectedIndexChanged += new System.EventHandler(this.comboConstruir_SelectedIndexChanged);
			// 
			// toolStrip2
			// 
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2});
			this.toolStrip2.Location = new System.Drawing.Point(0, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new System.Drawing.Size(738, 25);
			this.toolStrip2.TabIndex = 27;
			this.toolStrip2.Text = "toolStrip2";
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(66, 22);
			this.toolStripButton2.Text = "&Refresh";
			this.toolStripButton2.ToolTipText = "Avanza un turno.";
			this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
			// 
			// pbEdif
			// 
			this.pbEdif.Location = new System.Drawing.Point(174, 255);
			this.pbEdif.Name = "pbEdif";
			this.pbEdif.Size = new System.Drawing.Size(100, 23);
			this.pbEdif.TabIndex = 28;
			// 
			// FrmCiudad
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(738, 441);
			this.Controls.Add(this.pbEdif);
			this.Controls.Add(this.toolStrip2);
			this.Controls.Add(this.comboConstruir);
			this.Controls.Add(this.numTrabajador);
			this.Controls.Add(this.listTrabajos);
			this.Controls.Add(this.listRecursos);
			this.Controls.Add(this.textInfo);
			this.Controls.Add(this.listEdificios);
			this.Name = "FrmCiudad";
			this.Text = "FrmCiudad";
			((System.ComponentModel.ISupportInitialize)(this.numTrabajador)).EndInit();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
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
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
		private System.Windows.Forms.ProgressBar pbEdif;


    }
}