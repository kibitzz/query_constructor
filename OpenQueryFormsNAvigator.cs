/*
 * Copyright (C) 2010  Igor Proskochilo

 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.

 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 * 
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace sql_constructor
{
	/// <summary>
	/// Description .
	/// </summary>
	public partial class OpenQueryFormsNAvigator : Form
	{
		QueryForm[] displayList;
		int[] idxArray;
		int formCount;
		bool here;
		public int FormCount
		{
			get
			{
				return formCount;
			}
		}
		
		public OpenQueryFormsNAvigator(QueryForm[] List)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();			
			
			displayList = List;
			formCount = 0;
			idxArray = new int[displayList.Length];
			
			Label lab = new Label();
			string maxName = "";
			
			for(int i = 0; i < displayList.Length; i++)
			{
				if(displayList[i] != null)
				{
					lab = new Label();
					lab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
					lab.BackColor = System.Drawing.SystemColors.ActiveCaption;
					lab.Font = new System.Drawing.Font("Comic Sans MS", 9.75F/*11.25F*/, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
					lab.Location = new System.Drawing.Point(12 , 1 + (formCount * 20)+ 4);
					lab.Name = "label"+ formCount.ToString();
					lab.Size = new System.Drawing.Size(this.Width -25, 20);
					lab.Text = " "+ displayList[i].Text;					
					lab.MouseLeave += new System.EventHandler(this.Label1MouseLeave);
					lab.Paint += new System.Windows.Forms.PaintEventHandler(this.Label1Paint);
					lab.MouseHover += new System.EventHandler(this.Label1MouseHover);
					lab.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Label1MouseClick);
					
					idxArray[formCount] = i;
					formCount++;
					this.Controls.Add(lab);
					
					if (maxName.Length < lab.Text.Length)
					{
						maxName = lab.Text;
					}
				}
			}
			
			Graphics g = this.CreateGraphics();		
			Size preferredSize = g.MeasureString(maxName, lab.Font).ToSize();

			
			this.ClientSize = new System.Drawing.Size(preferredSize.Width + 26, 20 * formCount + 12);
						
		}
		
		void OpenQueryFormsNAvigatorPaint(object sender, PaintEventArgs e)
		{
			// LargeConfetti  NarrowHorizontal
			using (Brush brush = new HatchBrush(HatchStyle.Percent80, this.BackColor, System.Drawing.SystemColors.AppWorkspace))
			{
				e.Graphics.FillRectangle(brush, this.ClientRectangle);
			}
		}
		
		void OpenQueryFormsNAvigatorMouseLeave(object sender, EventArgs e)
		{
			Point p = Control.MousePosition;
			Rectangle area = this.Bounds;
//			area.Inflate(-10, -10);
			if(!area.Contains(p))
			{
				this.Close();
			}
		}
		
		void OpenQueryFormsNAvigatorMouseHover(object sender, EventArgs e)
		{
			this.Text = "here";
		}
		
		void Label1MouseHover(object sender, EventArgs e)
		{
			this.Text = "here";
			((Control)sender).Tag = "Hover";
			((Control)sender).Refresh();
			
		}
		
		void Label1MouseLeave(object sender, EventArgs e)
		{
			((Control)sender).Tag = "Leave";
			((Control)sender).Refresh();
		}
		
		void Label1Paint(object sender, PaintEventArgs e)
		{			
			string tagOfObject = (((Control)sender).Tag != null)? ((Control)sender).Tag.ToString() : "";
			using (Brush brush = new HatchBrush((this.Text == "here") ? (tagOfObject == "Hover") ? HatchStyle.Percent05 : HatchStyle.Percent10 : HatchStyle.ZigZag, this.BackColor, System.Drawing.SystemColors.AppWorkspace))
			{
				e.Graphics.FillRectangle(brush, ((Control)sender).ClientRectangle);
			}
			
			StringFormat sf = new StringFormat();			
			sf.Alignment = StringAlignment.Far;
			
			e.Graphics.DrawString(((Control)sender).Text, ((Control)sender).Font,  new SolidBrush((tagOfObject == "Hover") ? Color.LightGreen : Color.WhiteSmoke), ((Control)sender).ClientRectangle, sf);
		}
		
		
		void OpenQueryFormsNAvigatorDeactivate(object sender, EventArgs e)
		{			
			here = !here;
			if (here)
			{
				this.Close();
			}
		}
		
		void Label1MouseClick(object sender, MouseEventArgs e)
		{
			int idx = this.Controls.IndexOf(((Control)sender));
			this.Close();
		
			QueryForm.activeOpenListID = idxArray[idx];
			displayList[idxArray[idx]].BringToFront();
//			QueryForm.bringToFr = idxArray[idx];
		}
		
		void OpenQueryFormsNAvigatorShown(object sender, EventArgs e)
		{
			if (formCount == 1) {this.Close();}
		}
	}
}
