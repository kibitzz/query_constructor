/*
 * Created by SharpDevelop.
 * User: Proskochilo_I_Y
 * Date: 12.06.2010
 * Time: 10:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 using Glass;
namespace sql_constructor
{
	partial class QueryTextBox
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
			this.richTextBox2 = new System.Windows.Forms.RichTextBox();
			this.button1 = new Glass.GlassButton();
			this.button2 = new Glass.GlassButton();
			this.label1 = new System.Windows.Forms.Label();
			this.button3 = new Glass.GlassButton();
			this.button4 = new Glass.GlassButton();
			this.rowcolCtrl = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// richTextBox2
			// 
			this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox2.BackColor = System.Drawing.SystemColors.ControlLight;
			this.richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.richTextBox2.Location = new System.Drawing.Point(6, 31);
			this.richTextBox2.Name = "richTextBox2";
			this.richTextBox2.ReadOnly = true;
			this.richTextBox2.Size = new System.Drawing.Size(783, 554);
			this.richTextBox2.TabIndex = 6;
			this.richTextBox2.Text = "";
			this.richTextBox2.WordWrap = false;
			this.richTextBox2.SelectionChanged += new System.EventHandler(this.RichTextBox2SelectionChanged);
			this.richTextBox2.RegionChanged += new System.EventHandler(this.RichTextBox2RegionChanged);
			this.richTextBox2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RichTextBox2KeyUp);
			this.richTextBox2.TextChanged += new System.EventHandler(this.RichTextBox2TextChanged);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.button1.ForeColor = System.Drawing.SystemColors.MenuText;
			this.button1.GlowColor = System.Drawing.Color.Gold;
			this.button1.Location = new System.Drawing.Point(120, 5);
			this.button1.Name = "button1";
			this.button1.ShineColor = System.Drawing.SystemColors.MenuBar;
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 7;
			this.button1.Text = "&E d i t";
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.ForeColor = System.Drawing.SystemColors.MenuText;
			this.button2.GlowColor = System.Drawing.Color.LimeGreen;
			this.button2.Location = new System.Drawing.Point(201, 5);
			this.button2.Name = "button2";
			this.button2.ShineColor = System.Drawing.SystemColors.MenuBar;
			this.button2.Size = new System.Drawing.Size(81, 23);
			this.button2.TabIndex = 8;
			this.button2.Text = "O&K";
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(676, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 16);
			this.label1.TabIndex = 9;
			// 
			// button3
			// 
			this.button3.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.button3.ForeColor = System.Drawing.SystemColors.MenuText;
			this.button3.GlowColor = System.Drawing.Color.CornflowerBlue;
			this.button3.Location = new System.Drawing.Point(6, 5);
			this.button3.Name = "button3";
			this.button3.ShineColor = System.Drawing.SystemColors.MenuBar;
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 10;
			this.button3.Text = "To &clipboard";
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// button4
			// 
			this.button4.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.button4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button4.ForeColor = System.Drawing.SystemColors.MenuText;
			this.button4.GlowColor = System.Drawing.Color.OrangeRed;
			this.button4.Location = new System.Drawing.Point(288, 5);
			this.button4.Name = "button4";
			this.button4.ShineColor = System.Drawing.SystemColors.MenuBar;
			this.button4.Size = new System.Drawing.Size(86, 23);
			this.button4.TabIndex = 11;
			this.button4.Text = "Ca&ncel";
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// rowcolCtrl
			// 
			this.rowcolCtrl.Location = new System.Drawing.Point(522, 12);
			this.rowcolCtrl.Name = "rowcolCtrl";
			this.rowcolCtrl.Size = new System.Drawing.Size(148, 16);
			this.rowcolCtrl.TabIndex = 12;
			// 
			// QueryTextBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button4;
			this.ClientSize = new System.Drawing.Size(793, 586);
			this.Controls.Add(this.rowcolCtrl);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.richTextBox2);
			this.MinimizeBox = false;
			this.Name = "QueryTextBox";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Query Text Box";
			this.Shown += new System.EventHandler(this.QueryTextBoxShown);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label rowcolCtrl;
		private Glass.GlassButton button4;
		private Glass.GlassButton button3;
		private System.Windows.Forms.Label label1;
		private Glass.GlassButton button2;
		private Glass.GlassButton button1;
		private System.Windows.Forms.RichTextBox richTextBox2;
	}
}
