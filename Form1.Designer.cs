/*
 * Created by SharpDevelop.
 * User: Proskochilo_I_Y
 * Date: 24.06.2010
 * Time: 11:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace sql_constructor
{
	partial class AgrChooseForm
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
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.count = new System.Windows.Forms.Button();
			this.avg = new System.Windows.Forms.Button();
			this.min = new System.Windows.Forms.Button();
			this.max = new System.Windows.Forms.Button();
			this.sum = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(12, 93);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 7;
			this.button3.Tag = "variance";
			this.button3.Text = "variance";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.FuncClick);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(93, 64);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 6;
			this.button2.Tag = "stddev";
			this.button2.Text = "stddev";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.FuncClick);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(93, 93);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 5;
			this.button1.Tag = " ";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.FuncClick);
			// 
			// count
			// 
			this.count.Location = new System.Drawing.Point(93, 6);
			this.count.Name = "count";
			this.count.Size = new System.Drawing.Size(75, 23);
			this.count.TabIndex = 4;
			this.count.Tag = "count";
			this.count.Text = "count";
			this.count.UseVisualStyleBackColor = true;
			this.count.Click += new System.EventHandler(this.FuncClick);
			// 
			// avg
			// 
			this.avg.Location = new System.Drawing.Point(12, 64);
			this.avg.Name = "avg";
			this.avg.Size = new System.Drawing.Size(75, 23);
			this.avg.TabIndex = 3;
			this.avg.Tag = "avg";
			this.avg.Text = "avg";
			this.avg.UseVisualStyleBackColor = true;
			this.avg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AvgMouseMove);
			this.avg.Click += new System.EventHandler(this.FuncClick);
			// 
			// min
			// 
			this.min.Location = new System.Drawing.Point(93, 35);
			this.min.Name = "min";
			this.min.Size = new System.Drawing.Size(75, 23);
			this.min.TabIndex = 2;
			this.min.Tag = "min";
			this.min.Text = "min";
			this.min.UseVisualStyleBackColor = true;
			this.min.Click += new System.EventHandler(this.FuncClick);
			// 
			// max
			// 
			this.max.Location = new System.Drawing.Point(12, 35);
			this.max.Name = "max";
			this.max.Size = new System.Drawing.Size(75, 23);
			this.max.TabIndex = 1;
			this.max.Tag = "max";
			this.max.Text = "max";
			this.max.UseVisualStyleBackColor = true;
			this.max.Click += new System.EventHandler(this.FuncClick);
			// 
			// sum
			// 
			this.sum.Location = new System.Drawing.Point(12, 6);
			this.sum.Name = "sum";
			this.sum.Size = new System.Drawing.Size(75, 23);
			this.sum.TabIndex = 0;
			this.sum.Tag = "sum";
			this.sum.Text = "sum";
			this.sum.UseVisualStyleBackColor = true;
			this.sum.Click += new System.EventHandler(this.FuncClick);
			// 
			// AgrChooseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(183, 121);
			this.ControlBox = false;
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.sum);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.max);
			this.Controls.Add(this.count);
			this.Controls.Add(this.min);
			this.Controls.Add(this.avg);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "AgrChooseForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Form1";
			this.TransparencyKey = System.Drawing.SystemColors.Control;
			this.Deactivate += new System.EventHandler(this.AgrChooseFormDeactivate);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.AgrChooseFormPaint);
			this.MouseCaptureChanged += new System.EventHandler(this.AgrChooseFormMouseCaptureChanged);
			this.MouseLeave += new System.EventHandler(this.AgrChooseFormMouseLeave);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button sum;
		private System.Windows.Forms.Button max;
		private System.Windows.Forms.Button min;
		private System.Windows.Forms.Button avg;
		private System.Windows.Forms.Button count;
	}
}
