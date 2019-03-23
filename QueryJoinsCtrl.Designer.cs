/*
 * Created by SharpDevelop.
 * User: igor_only
 * Date: 09.10.2010
 * Time: 19:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace sql_constructor
{
	partial class QueryJoinsCtrl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
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
			// QueryJoinsCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Name = "QueryJoinsCtrl";
			this.Size = new System.Drawing.Size(410, 150);
			this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.QueryJoinsCtrlControlAdded);
			this.SizeChanged += new System.EventHandler(this.QueryJoinsCtrlSizeChanged);
			this.ResumeLayout(false);
		}
	}
}
