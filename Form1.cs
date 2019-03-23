/*
 * Created by SharpDevelop.
 * User: Proskochilo_I_Y
 * Date: 24.06.2010
 * Time: 11:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace sql_constructor
{
	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class AgrChooseForm : Form
	{
		public AgrChooseForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			
			func ="";
		}
		string func;
		
		
		public string ChoosedFunc
		{
			get
			{
				return func;
			}
		}
		
		void FuncClick(object sender, EventArgs e)
		{
			func = ((System.Windows.Forms.Button)sender).Tag.ToString();
			this.Close();
		}
		
		
		void AgrChooseFormDeactivate(object sender, EventArgs e)
		{
			this.Close();
		}
		
		
		void AgrChooseFormLeave(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void AgrChooseFormMouseLeave(object sender, EventArgs e)
		{
			Point p = Control.MousePosition;
			Rectangle area = this.Bounds;
//			area.Inflate(-10, -10);
			if(!area.Contains(p))
			{
				this.Close();
			}
		}
		
		void AgrChooseFormMouseDown(object sender, MouseEventArgs e)
		{
//			this.Close();
		}
		
		void Panel1MouseLeave(object sender, EventArgs e)
		{
//			bool notleave = false;
//			notleave =this.Capture;
//			foreach(Control ctrl in this.panel1.Controls)
//			{
			////				if (ctrl.)
			////				{
			////					notleave = true;
//				}
//			}
//			if (!notleave)
//			{
//				this.Close();
//			}
		}
		
		
		
		void AvgMouseMove(object sender, MouseEventArgs e)
		{
		}
		
		
		void AgrChooseFormMouseCaptureChanged(object sender, EventArgs e)
		{
			bool notleave =this.Capture;
			this.Close();
		}
		
		void AgrChooseFormMouseUp(object sender, MouseEventArgs e)
		{
			
		}
		
		void AgrChooseFormPaint(object sender, PaintEventArgs e)
		{
			using (Brush brush = new HatchBrush(HatchStyle.Percent70, this.BackColor, System.Drawing.SystemColors.AppWorkspace))
			{
				e.Graphics.FillRectangle(brush, this.ClientRectangle);
			}
		}
	}
	
}
