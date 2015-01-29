namespace CivWin
{
    partial class FrmCiv
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCiv));
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.cmdTurn = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.listCiudades = new System.Windows.Forms.ListBox();
			this.lstCiencias = new System.Windows.Forms.CheckedListBox();
			this.toolStrip2.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip2
			// 
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdTurn,
            this.toolStripButton2});
			this.toolStrip2.Location = new System.Drawing.Point(0, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new System.Drawing.Size(601, 25);
			this.toolStrip2.TabIndex = 0;
			this.toolStrip2.Text = "toolStrip2";
			// 
			// cmdTurn
			// 
			this.cmdTurn.Image = ((System.Drawing.Image)(resources.GetObject("cmdTurn.Image")));
			this.cmdTurn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdTurn.Name = "cmdTurn";
			this.cmdTurn.Size = new System.Drawing.Size(59, 22);
			this.cmdTurn.Text = "&Turno";
			this.cmdTurn.ToolTipText = "Avanza un turno.";
			this.cmdTurn.Click += new System.EventHandler(this.acTurno);
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
			// listCiudades
			// 
			this.listCiudades.Location = new System.Drawing.Point(12, 28);
			this.listCiudades.Name = "listCiudades";
			this.listCiudades.Size = new System.Drawing.Size(171, 95);
			this.listCiudades.TabIndex = 1;
			this.listCiudades.DoubleClick += new System.EventHandler(this.listCiudades_DoubleClick);
			// 
			// lstCiencias
			// 
			this.lstCiencias.FormattingEnabled = true;
			this.lstCiencias.Location = new System.Drawing.Point(189, 28);
			this.lstCiencias.Name = "lstCiencias";
			this.lstCiencias.Size = new System.Drawing.Size(278, 94);
			this.lstCiencias.TabIndex = 2;
			// 
			// FrmCiv
			// 
			this.ClientSize = new System.Drawing.Size(601, 286);
			this.Controls.Add(this.lstCiencias);
			this.Controls.Add(this.listCiudades);
			this.Controls.Add(this.toolStrip2);
			this.Name = "FrmCiv";
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton cmdTurn;
        private System.Windows.Forms.ListBox listCiudades;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
		private System.Windows.Forms.CheckedListBox lstCiencias;
    }
}

