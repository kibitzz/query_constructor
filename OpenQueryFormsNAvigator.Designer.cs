/*
 * Created by SharpDevelop.
 * User: igor_only
 * Date: 20.08.2010
 * Time: 19:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace sql_constructor
{
	partial class OpenQueryFormsNAvigator
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// OpenQueryFormsNAvigator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(35, 15);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OpenQueryFormsNAvigator";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.TransparencyKey = System.Drawing.SystemColors.Control;
			this.Deactivate += new System.EventHandler(this.OpenQueryFormsNAvigatorDeactivate);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.OpenQueryFormsNAvigatorPaint);
			this.Shown += new System.EventHandler(this.OpenQueryFormsNAvigatorShown);
			this.MouseLeave += new System.EventHandler(this.OpenQueryFormsNAvigatorMouseLeave);
			this.MouseHover += new System.EventHandler(this.OpenQueryFormsNAvigatorMouseHover);
			this.ResumeLayout(false);
		}
	}
}
