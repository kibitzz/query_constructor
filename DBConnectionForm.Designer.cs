/*
 * Created by SharpDevelop.
 * User: Proskochilo_I_Y
 * Date: 11.06.2010
 * Time: 14:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace sql_constructor
{
	partial class DBConnectionForm
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
			this.ConnStrBox = new System.Windows.Forms.TextBox();
			this.button1 = new Glass.GlassButton();
			this.label1 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.button2 = new Glass.GlassButton();
			this.oleProvCtrl = new System.Windows.Forms.ListBox();
			this.suorceCtrl = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.usrNameCtrl = new System.Windows.Forms.TextBox();
			this.pwdCtrl = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ConnStrBox
			// 
			this.ConnStrBox.Location = new System.Drawing.Point(6, 247);
			this.ConnStrBox.Name = "ConnStrBox";
			this.ConnStrBox.Size = new System.Drawing.Size(440, 20);
			this.ConnStrBox.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.button1.ForeColor = System.Drawing.SystemColors.MenuText;
			this.button1.GlowColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.button1.Location = new System.Drawing.Point(240, 278);
			this.button1.Name = "button1";
			this.button1.ShineColor = System.Drawing.SystemColors.ButtonFace;
			this.button1.Size = new System.Drawing.Size(94, 28);
			this.button1.TabIndex = 1;
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(159, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "For conection string details visit  ";
			this.label1.Click += new System.EventHandler(this.Label1Click);
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(177, 9);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(197, 17);
			this.linkLabel1.TabIndex = 3;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "http://www.connectionstrings.com/";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1LinkClicked);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.ForeColor = System.Drawing.SystemColors.MenuText;
			this.button2.GlowColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.button2.Location = new System.Drawing.Point(340, 278);
			this.button2.Name = "button2";
			this.button2.ShineColor = System.Drawing.SystemColors.ButtonFace;
			this.button2.Size = new System.Drawing.Size(99, 28);
			this.button2.TabIndex = 4;
			this.button2.Text = "Cancel";
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// oleProvCtrl
			// 
			this.oleProvCtrl.FormattingEnabled = true;
			this.oleProvCtrl.Location = new System.Drawing.Point(6, 29);
			this.oleProvCtrl.Name = "oleProvCtrl";
			this.oleProvCtrl.Size = new System.Drawing.Size(440, 160);
			this.oleProvCtrl.TabIndex = 5;
			this.oleProvCtrl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OleProvCtrlMouseDoubleClick);
			// 
			// suorceCtrl
			// 
			this.suorceCtrl.Location = new System.Drawing.Point(84, 195);
			this.suorceCtrl.Name = "suorceCtrl";
			this.suorceCtrl.Size = new System.Drawing.Size(160, 20);
			this.suorceCtrl.TabIndex = 6;
			this.suorceCtrl.TextChanged += new System.EventHandler(this.SuorceCtrlTextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 195);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 23);
			this.label2.TabIndex = 7;
			this.label2.Text = "Data source";
			// 
			// usrNameCtrl
			// 
			this.usrNameCtrl.Location = new System.Drawing.Point(334, 195);
			this.usrNameCtrl.Name = "usrNameCtrl";
			this.usrNameCtrl.Size = new System.Drawing.Size(112, 20);
			this.usrNameCtrl.TabIndex = 8;
			this.usrNameCtrl.TextChanged += new System.EventHandler(this.UsrNameCtrlTextChanged);
			// 
			// pwdCtrl
			// 
			this.pwdCtrl.Location = new System.Drawing.Point(334, 221);
			this.pwdCtrl.Name = "pwdCtrl";
			this.pwdCtrl.Size = new System.Drawing.Size(112, 20);
			this.pwdCtrl.TabIndex = 9;
			this.pwdCtrl.TextChanged += new System.EventHandler(this.PwdCtrlTextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(272, 195);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 23);
			this.label3.TabIndex = 10;
			this.label3.Text = "User";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(272, 221);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(54, 23);
			this.label4.TabIndex = 11;
			this.label4.Text = "Password";
			this.label4.Click += new System.EventHandler(this.Label4Click);
			// 
			// DBConnectionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(451, 308);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.pwdCtrl);
			this.Controls.Add(this.usrNameCtrl);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.suorceCtrl);
			this.Controls.Add(this.oleProvCtrl);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.ConnStrBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DBConnectionForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DB connection";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.TextBox suorceCtrl;
		private System.Windows.Forms.TextBox usrNameCtrl;
		private System.Windows.Forms.TextBox pwdCtrl;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox oleProvCtrl;
		private Glass.GlassButton button2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox ConnStrBox;
		private Glass.GlassButton button1;
	}
}
