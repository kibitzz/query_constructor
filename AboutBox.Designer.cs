/*
 * Created by SharpDevelop.
 * User: igor_only
 * Date: 19.10.2010
 * Time: 12:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace sql_constructor
{
	partial class AboutBox
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.OkCtrl = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.skoba = new System.Windows.Forms.ImageList(this.components);
			this.gameBox = new System.Windows.Forms.PictureBox();
			this.reload = new System.Windows.Forms.PictureBox();
			this.winImg = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gameBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.reload)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(21, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(73, 66);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(100, 55);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(258, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Query constructor ver. 1.0 beta";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(12, 91);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(346, 18);
			this.label2.TabIndex = 2;
			this.label2.Text = "(c) 2010 Igor Proskochilo  ";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(9, 112);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(376, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "Released under the terms of the GNU General Public License";
			// 
			// OkCtrl
			// 
			this.OkCtrl.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkCtrl.Location = new System.Drawing.Point(254, 157);
			this.OkCtrl.Name = "OkCtrl";
			this.OkCtrl.Size = new System.Drawing.Size(75, 23);
			this.OkCtrl.TabIndex = 4;
			this.OkCtrl.Text = "O&K";
			this.OkCtrl.UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.HideSelection = false;
			this.textBox1.Location = new System.Drawing.Point(56, 134);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(137, 20);
			this.textBox1.TabIndex = 5;
			this.textBox1.TabStop = false;
			this.textBox1.Text = " ipro.mbox@gmail.com";
			this.textBox1.MouseLeave += new System.EventHandler(this.TextBox1MouseLeave);
			this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox1KeyDown);
			this.textBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TextBox1MouseDown);
			this.textBox1.MouseEnter += new System.EventHandler(this.TextBox1MouseEnter);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(9, 137);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 23);
			this.label4.TabIndex = 6;
			this.label4.Text = "E-mail:";
			// 
			// skoba
			// 
			this.skoba.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("skoba.ImageStream")));
			this.skoba.TransparentColor = System.Drawing.Color.White;
			this.skoba.Images.SetKeyName(0, "dot.bmp");
			this.skoba.Images.SetKeyName(1, "c_mini.bmp");
			this.skoba.Images.SetKeyName(2, "c_gray.bmp");
			this.skoba.Images.SetKeyName(3, "c_mini_RED.bmp");
			this.skoba.Images.SetKeyName(4, "c_red.bmp");
			this.skoba.Images.SetKeyName(5, "win.bmp");
			// 
			// gameBox
			// 
			this.gameBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.gameBox.Location = new System.Drawing.Point(92, 261);
			this.gameBox.Name = "gameBox";
			this.gameBox.Size = new System.Drawing.Size(193, 0);
			this.gameBox.TabIndex = 7;
			this.gameBox.TabStop = false;
			// 
			// reload
			// 
			this.reload.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.reload.Image = ((System.Drawing.Image)(resources.GetObject("reload.Image")));
			this.reload.InitialImage = ((System.Drawing.Image)(resources.GetObject("reload.InitialImage")));
			this.reload.Location = new System.Drawing.Point(56, 261);
			this.reload.Name = "reload";
			this.reload.Size = new System.Drawing.Size(20, 19);
			this.reload.TabIndex = 8;
			this.reload.TabStop = false;
			this.reload.Click += new System.EventHandler(this.ReloadClick);
			// 
			// winImg
			// 
			this.winImg.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("winImg.ImageStream")));
			this.winImg.TransparentColor = System.Drawing.Color.White;
			this.winImg.Images.SetKeyName(0, "win.bmp");
			// 
			// AboutBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(385, 187);
			this.Controls.Add(this.reload);
			this.Controls.Add(this.gameBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.OkCtrl);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutBox";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "AboutBox";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gameBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.reload)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ImageList winImg;
		private System.Windows.Forms.PictureBox reload;
		private System.Windows.Forms.PictureBox gameBox;
		private System.Windows.Forms.ImageList skoba;
		private System.Windows.Forms.Button OkCtrl;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}
