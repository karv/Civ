namespace CivWin
{
	partial class TrackText
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.gbTexto = new System.Windows.Forms.GroupBox();
			this.txNumTrab = new System.Windows.Forms.TextBox();
			this.tbNumTrab = new System.Windows.Forms.TrackBar();
			this.gbTexto.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tbNumTrab)).BeginInit();
			this.SuspendLayout();
			// 
			// gbTexto
			// 
			this.gbTexto.Controls.Add(this.txNumTrab);
			this.gbTexto.Controls.Add(this.tbNumTrab);
			this.gbTexto.Location = new System.Drawing.Point(3, 3);
			this.gbTexto.Name = "gbTexto";
			this.gbTexto.Size = new System.Drawing.Size(257, 64);
			this.gbTexto.TabIndex = 2;
			this.gbTexto.TabStop = false;
			// 
			// txNumTrab
			// 
			this.txNumTrab.Location = new System.Drawing.Point(176, 19);
			this.txNumTrab.Name = "txNumTrab";
			this.txNumTrab.ReadOnly = true;
			this.txNumTrab.Size = new System.Drawing.Size(74, 20);
			this.txNumTrab.TabIndex = 1;
			// 
			// tbNumTrab
			// 
			this.tbNumTrab.Location = new System.Drawing.Point(6, 19);
			this.tbNumTrab.Name = "tbNumTrab";
			this.tbNumTrab.Size = new System.Drawing.Size(164, 45);
			this.tbNumTrab.TabIndex = 0;
			this.tbNumTrab.ValueChanged += new System.EventHandler(this.tbNumTrab_ValueChanged);
			// 
			// TrackText
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gbTexto);
			this.Name = "TrackText";
			this.Size = new System.Drawing.Size(263, 76);
			this.gbTexto.ResumeLayout(false);
			this.gbTexto.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tbNumTrab)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbTexto;
		private System.Windows.Forms.TextBox txNumTrab;
		private System.Windows.Forms.TrackBar tbNumTrab;
	}
}
