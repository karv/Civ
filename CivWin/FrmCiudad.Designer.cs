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
			this.textInfo = new System.Windows.Forms.TextBox();
			this.numTrabajador = new System.Windows.Forms.NumericUpDown();
			this.comboConstruir = new System.Windows.Forms.ComboBox();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.pbEdif = new System.Windows.Forms.ProgressBar();
			this.chkAutoReclutar = new System.Windows.Forms.CheckBox();
			this.cmdReclutar = new System.Windows.Forms.Button();
			this.listUnidades = new System.Windows.Forms.ListView();
			this.listRecursos = new System.Windows.Forms.ListView();
			this.listEdificios = new System.Windows.Forms.ListView();
			this.listTrabajos = new System.Windows.Forms.ListView();
			((System.ComponentModel.ISupportInitialize)(this.numTrabajador)).BeginInit();
			this.toolStrip2.SuspendLayout();
			this.SuspendLayout();
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
			// chkAutoReclutar
			// 
			this.chkAutoReclutar.AutoSize = true;
			this.chkAutoReclutar.Location = new System.Drawing.Point(537, 229);
			this.chkAutoReclutar.Name = "chkAutoReclutar";
			this.chkAutoReclutar.Size = new System.Drawing.Size(83, 17);
			this.chkAutoReclutar.TabIndex = 29;
			this.chkAutoReclutar.Text = "&Autoreclutar";
			this.chkAutoReclutar.UseVisualStyleBackColor = true;
			this.chkAutoReclutar.CheckedChanged += new System.EventHandler(this.chkAutoReclutar_CheckedChanged);
			// 
			// cmdReclutar
			// 
			this.cmdReclutar.Location = new System.Drawing.Point(281, 255);
			this.cmdReclutar.Name = "cmdReclutar";
			this.cmdReclutar.Size = new System.Drawing.Size(75, 23);
			this.cmdReclutar.TabIndex = 30;
			this.cmdReclutar.Text = "&Reclutar";
			this.cmdReclutar.UseVisualStyleBackColor = true;
			this.cmdReclutar.Click += new System.EventHandler(this.cmdReclutar_Click);
			// 
			// listUnidades
			// 
			this.listUnidades.Location = new System.Drawing.Point(12, 283);
			this.listUnidades.MultiSelect = false;
			this.listUnidades.Name = "listUnidades";
			this.listUnidades.Size = new System.Drawing.Size(344, 146);
			this.listUnidades.TabIndex = 31;
			this.listUnidades.UseCompatibleStateImageBehavior = false;
			this.listUnidades.View = System.Windows.Forms.View.SmallIcon;
			// 
			// listRecursos
			// 
			this.listRecursos.Location = new System.Drawing.Point(13, 106);
			this.listRecursos.MultiSelect = false;
			this.listRecursos.Name = "listRecursos";
			this.listRecursos.Size = new System.Drawing.Size(377, 140);
			this.listRecursos.TabIndex = 32;
			this.listRecursos.UseCompatibleStateImageBehavior = false;
			this.listRecursos.View = System.Windows.Forms.View.SmallIcon;
			// 
			// listEdificios
			// 
			this.listEdificios.Location = new System.Drawing.Point(204, 28);
			this.listEdificios.MultiSelect = false;
			this.listEdificios.Name = "listEdificios";
			this.listEdificios.Size = new System.Drawing.Size(186, 72);
			this.listEdificios.TabIndex = 33;
			this.listEdificios.UseCompatibleStateImageBehavior = false;
			this.listEdificios.View = System.Windows.Forms.View.SmallIcon;
			// 
			// listTrabajos
			// 
			this.listTrabajos.Location = new System.Drawing.Point(396, 28);
			this.listTrabajos.MultiSelect = false;
			this.listTrabajos.Name = "listTrabajos";
			this.listTrabajos.Size = new System.Drawing.Size(330, 195);
			this.listTrabajos.TabIndex = 34;
			this.listTrabajos.UseCompatibleStateImageBehavior = false;
			this.listTrabajos.View = System.Windows.Forms.View.SmallIcon;
			this.listTrabajos.SelectedIndexChanged += new System.EventHandler(this.listTrabajos_SelectedIndexChanged);
			this.listTrabajos.DoubleClick += new System.EventHandler(this.listTrabajos_DoubleClick);
			// 
			// FrmCiudad
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(738, 441);
			this.Controls.Add(this.listTrabajos);
			this.Controls.Add(this.listEdificios);
			this.Controls.Add(this.listRecursos);
			this.Controls.Add(this.listUnidades);
			this.Controls.Add(this.cmdReclutar);
			this.Controls.Add(this.chkAutoReclutar);
			this.Controls.Add(this.pbEdif);
			this.Controls.Add(this.toolStrip2);
			this.Controls.Add(this.comboConstruir);
			this.Controls.Add(this.numTrabajador);
			this.Controls.Add(this.textInfo);
			this.Name = "FrmCiudad";
			this.Text = "FrmCiudad";
			((System.ComponentModel.ISupportInitialize)(this.numTrabajador)).EndInit();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textInfo;
		private System.Windows.Forms.NumericUpDown numTrabajador;
		private System.Windows.Forms.ComboBox comboConstruir;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
		private System.Windows.Forms.ProgressBar pbEdif;
		private System.Windows.Forms.CheckBox chkAutoReclutar;
		private System.Windows.Forms.Button cmdReclutar;
		private System.Windows.Forms.ListView listUnidades;
		private System.Windows.Forms.ListView listRecursos;
		private System.Windows.Forms.ListView listEdificios;
		private System.Windows.Forms.ListView listTrabajos;


	}
}