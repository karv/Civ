namespace CivWin
{
	partial class frmRecluta
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
			this.lbUnidades = new System.Windows.Forms.ListBox();
			this.cmdCancelar = new System.Windows.Forms.Button();
			this.cmdOk = new System.Windows.Forms.Button();
			this.ttCantidad = new CivWin.TrackText();
			this.SuspendLayout();
			// 
			// lbUnidades
			// 
			this.lbUnidades.FormattingEnabled = true;
			this.lbUnidades.Location = new System.Drawing.Point(13, 13);
			this.lbUnidades.Name = "lbUnidades";
			this.lbUnidades.Size = new System.Drawing.Size(271, 95);
			this.lbUnidades.TabIndex = 0;
			this.lbUnidades.SelectedIndexChanged += new System.EventHandler(this.lbUnidades_SelectedIndexChanged);
			// 
			// cmdCancelar
			// 
			this.cmdCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancelar.Location = new System.Drawing.Point(128, 197);
			this.cmdCancelar.Name = "cmdCancelar";
			this.cmdCancelar.Size = new System.Drawing.Size(75, 23);
			this.cmdCancelar.TabIndex = 2;
			this.cmdCancelar.Text = "Cancelar";
			this.cmdCancelar.UseVisualStyleBackColor = true;
			// 
			// cmdOk
			// 
			this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOk.Location = new System.Drawing.Point(209, 197);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(75, 23);
			this.cmdOk.TabIndex = 3;
			this.cmdOk.Text = "&Aceptar";
			this.cmdOk.UseVisualStyleBackColor = true;
			// 
			// ttCantidad
			// 
			this.ttCantidad.Location = new System.Drawing.Point(13, 115);
			this.ttCantidad.Máximo = 10;
			this.ttCantidad.Name = "ttCantidad";
			this.ttCantidad.Size = new System.Drawing.Size(271, 76);
			this.ttCantidad.TabIndex = 1;
			this.ttCantidad.texto = "";
			this.ttCantidad.Valor = 0;
			// 
			// frmRecluta
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(297, 238);
			this.Controls.Add(this.cmdOk);
			this.Controls.Add(this.cmdCancelar);
			this.Controls.Add(this.ttCantidad);
			this.Controls.Add(this.lbUnidades);
			this.Name = "frmRecluta";
			this.Text = "frmRecluta";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox lbUnidades;
		private TrackText ttCantidad;
		private System.Windows.Forms.Button cmdCancelar;
		private System.Windows.Forms.Button cmdOk;
	}
}