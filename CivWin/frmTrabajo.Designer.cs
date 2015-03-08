namespace CivWin
{
	partial class frmTrabajo
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
			this.ttNumTrab = new CivWin.TrackText();
			this.ttPrior = new CivWin.TrackText();
			this.SuspendLayout();
			// 
			// ttNumTrab
			// 
			this.ttNumTrab.Location = new System.Drawing.Point(13, 13);
			this.ttNumTrab.Máximo = 10;
			this.ttNumTrab.Name = "ttNumTrab";
			this.ttNumTrab.Size = new System.Drawing.Size(263, 76);
			this.ttNumTrab.TabIndex = 0;
			this.ttNumTrab.texto = "Trabajadores";
			this.ttNumTrab.Valor = 0;
			this.ttNumTrab.OnCambio += new System.Action(this.ttNumTrab_OnCambio);
			// 
			// ttPrior
			// 
			this.ttPrior.Location = new System.Drawing.Point(13, 95);
			this.ttPrior.Máximo = 10;
			this.ttPrior.Name = "ttPrior";
			this.ttPrior.Size = new System.Drawing.Size(263, 76);
			this.ttPrior.TabIndex = 1;
			this.ttPrior.texto = "Prioridad";
			this.ttPrior.Valor = 0;
			this.ttPrior.OnCambio += new System.Action(this.trackText1_OnCambio);
			// 
			// frmTrabajo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(285, 189);
			this.Controls.Add(this.ttPrior);
			this.Controls.Add(this.ttNumTrab);
			this.Name = "frmTrabajo";
			this.Text = "frmTrabajo";
			this.ResumeLayout(false);

		}

		#endregion

		private TrackText ttNumTrab;
		private TrackText ttPrior;


	}
}