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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SqlBuilderClasses;

namespace sql_constructor
{
	/// <summary>
	/// Description of ConditionCtrl.
	/// </summary>
	public partial class ConditionCtrl : UserControl
	{
		
		public string message;
		condition dat;
		public condition data
		{
			get
			{
				return dat;
			}
			
			set
			{
				dat = value;
				
				noResponse = true;
				
				#region isolate err Message and visualise it
				
				if((dat.type == ConditionType.err))
				{
					message = dat.operand1;
					dat.operand1 = "";
					if(dat.text.Trim().Length == 0)
					{
						dat.type = ConditionType.equality;
						warningCtrl.Text = "?";
					} else
					{
						warningCtrl.Text = "!";
					}
					
				} else
				{
					warningCtrl.Text = "";
					message = "";
					if((dat.type == ConditionType.urest) && string.IsNullOrEmpty(dat.cOperator.Trim()))
					{
						warningCtrl.Text = "?";
						message = dat.operand1;
					}
				}
				#endregion
				
				operand1.Text = dat.operand1;
				operand2.Text = dat.operand2;
				operand3.Text = dat.operand3;
				
				NotFlagClick(null, null);
				noResponse = false;
				
				#region Operator formatting to typeBox
				
				if(dat.cOperator != null)
				{
					dat.cOperator = QueryParser.NormalizeSpaces(dat.cOperator);
					string tab = (dat.cOperator.Trim().Length < 5) ? "   " : "";
					string shortOp = dat.cOperator.Trim();
					if(dat.cOperator.Trim().Length >= 4)
					{
						if(dat.cOperator.ToLower().Contains("between"))
						{
							shortOp = "between";
						}
						if(dat.cOperator.ToLower().Contains("like"))
						{
							shortOp = "like";
							tab = "   " ;
						}
					}
					int idx = typeBox.Items.IndexOf(tab + shortOp.ToUpper());
					if(idx < 0)
					{
						typeBox.Items.Add(tab + shortOp.ToUpper());
						idx = typeBox.Items.Count - 1;
					}
					
					typeBox.SelectedIndex = idx;
				} else
					dat.cOperator = "";
				
				#endregion
				
				
				if((dat.type == ConditionType.urest) || (dat.type == ConditionType.err))
				{
					basicEquality.Visible = false;
					uresTextCtrl.Visible = true;
					notFlag.Visible = false;
					
					noResponse = true;
					if(string.IsNullOrEmpty(dat.text.Trim()))
					{
						uresTextCtrl.Text = dat.textForm;
					} else
					{
						uresTextCtrl.Text = dat.text;
					}
					noResponse = false;
					
				} else
				{
					basicEquality.Visible = true;
					uresTextCtrl.Visible = false;
					notFlag.Visible = true;
				}
				
				
				
//				UresTextCtrlTextChanged(null, null);
				
			}
		}
		int offset;
		bool noResponse;
		int dragPos;
		public static InterCrtlDragProc DragInterop;
		
		public ConditionCtrl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			data = new condition();
			
		}
		
		public ConditionCtrl(condition c)
		{
			
			InitializeComponent();
			data = c;
		}
		
		void TypeBoxLocationChanged(object sender, EventArgs e)
		{
		}
		
		public void OperandSiplify()
		{
			this.typeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			urestSwitch.ForeColor = Color.LightGray;
			selfDelCtrl.ForeColor = Color.LightGray;
		}
		
		#region SIZING
		
		// size up structured Block of ctrl and keep proportions
		void BasicEqualitySizeChanged(object sender, EventArgs e)
		{
			if(noResponse){ return;}
			offset = 0;
			
			int newWidth = (basicEquality.Width - (typeBox.Width +6)) / 2;
			if((dat.cOperator != null) && (dat.cOperator.ToLower().Contains("between") || dat.cOperator.ToLower().Contains("like")))
			{
				offset = newWidth/2;
				operand3.Width  = offset-1;
			}
			operand1.Width = newWidth;
			operand2.Width = newWidth - offset - 1;
			
			typeBox.Location  = new Point(operand1.Location.X + operand1.Width +1, 2);
			operand2.Location = new Point(typeBox.Location.X + typeBox.Width +1, 1);
			if(offset > 0)
			{
				operand3.Visible = true;
				operand3.Location = new Point(operand2.Location.X + operand2.Width +1, 1);
			} else
			{
				operand3.Text = "";
				operand3.Visible = false;				
			}
			
		}
		
		public void sizeInit()
		{
			ConditionCtrlSizeChanged(null, null);
		}
		
		// Top level ctrls locate and fit to container size
		void ConditionCtrlSizeChanged(object sender, EventArgs e)
		{
//			 selfDelCtrl
			basicEquality.Location = new Point(45, -1);
			
			urestSwitch.Location = new Point(this.Width -  urestSwitch.Width - 3 - selfDelCtrl.Width, 1);
			selfDelCtrl.Location =  new Point(urestSwitch.Bounds.Right+ 1, selfDelCtrl.Location.Y);
			basicEquality.Width = this.Width -  urestSwitch.Width - selfDelCtrl.Width - 10 - basicEquality.Location.X;
			uresTextCtrl.Width =  basicEquality.Width - 5;
			
			uresTextCtrl.Location = new Point(48, 0);
			
//			BasicEqualitySizeChanged(null, null);
		}
		
		void ConditionCtrlVisibleChanged(object sender, EventArgs e)
		{
//			ConditionCtrlSizeChanged(null, null);
		}
		
		#endregion
		
		// negation visualization
		void NotFlagClick(object sender, EventArgs e)
		{
			if(!noResponse){dat.negation = !dat.negation;}
			
			if(dat.negation)
			{
				notFlag.ForeColor = Color.Tomato ;
				notFlag.Text = notFlag.Text.ToUpper();
				notFlag.Top = 4;
			} else
			{
				notFlag.ForeColor = Color.LightGray;
				notFlag.Text = notFlag.Text.ToLower();
				notFlag.Top = 8;
			}
		}
		
		// Operator
		void TypeBoxTextChanged(object sender, EventArgs e)
		{
			if(noResponse){ return;}
			if(typeBox.Text.Trim() == "") {return;}
			
			dat.cOperator = typeBox.Text.ToLower();
			if(dat.cOperator.ToLower().Contains("between"))
			{
				dat.cOperator = "between and";
			}
			if(dat.cOperator.ToLower().Contains("like") && (dat.operand3.Trim().Length > 0))
			{
				dat.cOperator = "like escape";
			}
			
			BasicEqualitySizeChanged(null, null);
//			ConditionCtrlSizeChanged(null, null);
		}
		
		#region operands edit
		
		void Operand1TextChanged(object sender, EventArgs e)
		{
			dat.operand1 = operand1.Text;
			
		}
		
		void Operand2TextChanged(object sender, EventArgs e)
		{
			dat.operand2 = operand2.Text;
		}
		
		void Operand3TextChanged(object sender, EventArgs e)
		{
			dat.operand3 = operand3.Text;
			TypeBoxTextChanged(null, null);
		}
		
		#endregion
				
		// Switch managed / UNmanaged condition text presentation
		void UrestSwitchClick(object sender, EventArgs e)
		{
			if((dat.type == ConditionType.urest) || (dat.type == ConditionType.err))
			{
				dat.type = ConditionType.equality;
				dat.text = (dat.text.Trim() == "") ? dat.textForm : dat.text;
				
				if(((dat.type == ConditionType.urest) || (dat.type == ConditionType.err)) && (dat.text.Trim().Length > 0))
				{
					if (QueryForm.InputBox("Change condition type", "This text can not be converted to managed equality, \n all data will be removed \n         Proceed ?") == DialogResult.OK)
					{
						dat.Clear();
						dat.type = ConditionType.equality;
					}
				}
				
			} else
			{
				dat.text = dat.textForm;
				dat.type = ConditionType.urest;
			}
			
			data = dat;
			
		}
		
		// UNmanaged text verify
		void UresTextCtrlTextChanged(object sender, EventArgs e)
		{
			if(noResponse){return;}
			
			dat.text = uresTextCtrl.Text;
			
			bool notOk = (dat.type == ConditionType.err);
			
			if((dat.text.Trim().Length == 0) || notOk)
			{
				message = dat.operand1;
				warningCtrl.Text = (dat.text.Trim().Length == 0) ? "?" : "!";
			} else
			{
				message = "";
				warningCtrl.Text = "";
			}
			
			dat.type = ConditionType.urest;
		}
		
		#region Warning mesage and flag  visualization
		
		void WarningCtrlTextChanged(object sender, EventArgs e)
		{
			if(warningCtrl.Text.Trim().Length >0)
			{
				warningCtrl.BackColor = System.Drawing.SystemColors.Info;
				if(warningCtrl.Text.Contains("!"))
				{
					warningCtrl.ForeColor = Color.Red;
				} else
				{
					warningCtrl.ForeColor = Color.GreenYellow;
				}
			} else
			{
				warningCtrl.BackColor = Color.Transparent;
			}
		}
		
		void WarningCtrlClick(object sender, EventArgs e)
		{
			if(!string.IsNullOrEmpty(message))
			{
				QueryForm.refreshOnActivate = false;
				MessageBox.Show(message);
			}
		}
		
		#endregion
		
		
		void Operand1Enter(object sender, EventArgs e)
		{
			operand1.SelectionLength = 0;
		}
		
		#region focus updating
		
		public void SetFocusInList()
		{
			operand2.Focus();
			ConditionCtrlEnter(null , null);
		}
		
		void ConditionCtrlLeave(object sender, EventArgs e)
		{
			urestSwitch.ForeColor =  Color.LightGray;
			selfDelCtrl.ForeColor =  Color.LightGray;
			
//			if((this.BackColor == Color.Honeydew))
//			{this.BackColor = Color.Transparent;  ConditionCtrlBackColorChanged(null , null);}
			
		}
		
		void ConditionCtrlEnter(object sender, EventArgs e)
		{
			urestSwitch.ForeColor =  Color.Black;
			selfDelCtrl.ForeColor =  Color.Red;
			this.typeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			
			this.BackColor = Color.FromArgb(192, 255, 192); //Color.Honeydew;
			((ConditionList)this.Parent).selectedItem = this;
			ConditionCtrlBackColorChanged(null , null);
		}
		
		#endregion
		
		#region highlight , selectedItem change (invoke from other functions, NOT a handlers of event)
		
		void ConditionCtrlPaint(object sender, PaintEventArgs e)
		{
//			if((this.ContainsFocus) && (this.BackColor != Color.Honeydew))
//			{
//				this.BackColor = Color.Honeydew;
//				((ConditionList)this.Parent).selectedItem = this;
//				ConditionCtrlBackColorChanged(null , null);
//			}
//			
//			if((!this.ContainsFocus) && (this.BackColor == Color.Honeydew))
//			{this.BackColor = Color.Transparent;  ConditionCtrlBackColorChanged(null , null);}
			
		}
		
		public void ConditionCtrlBackColorChanged(object sender, EventArgs e)
		{
			foreach(Control c in this.Controls)
			{
				c.BackColor = (this.BackColor == Color.Transparent) ? Color.White : this.BackColor;
				
				foreach(Control sub in c.Controls)
				{
					sub.BackColor = c.BackColor;
				}
			}
		}
		
		// commands highlight
		void UrestSwitchMouseEnter(object sender, EventArgs e)
		{
			urestSwitch.ForeColor =  Color.Black;
		}
		
		void UrestSwitchMouseLeave(object sender, EventArgs e)
		{
			if(!this.ContainsFocus)
			{
				((Label)sender).ForeColor = Color.LightGray;
			}
		}
		
		void SelfDelCtrlMouseEnter(object sender, EventArgs e)
		{
			selfDelCtrl.ForeColor =  Color.Red;
		}
		
		#endregion
		
		#region Drag n drop functionality
		
		void Operand1MouseMove(object sender, MouseEventArgs e)
		{
//			GetCharIndexFromPosition (	Point pt)
			if(e.Button == MouseButtons.Left)
			{
				dragPos = ((TextBox)sender).GetCharIndexFromPosition(e.Location);
			} //else dragPos = -1;
		}
		
		public string DragString()
		{
			string rez = "";
			if(DragInterop != null)
			{
				rez = DragInterop();
			}
			if(rez == null){rez = "";}
			return rez.Trim();
		}
		
		void Operand1MouseUp(object sender, MouseEventArgs e)
		{
			string ds = DragString();			
			if( (dragPos != -1) && (ds != ""))
			{
				int sel = ((TextBox)sender).SelectionStart;
				((TextBox)sender).Text = QueryParser.InserText(((TextBox)sender).Text, ds, dragPos);
				((TextBox)sender).SelectionStart = sel;
			}
		}
		
		void Operand1MouseDown(object sender, MouseEventArgs e)
		{
//			dragPos = ((TextBox)sender).SelectionStart;
		}
		
		#endregion
		
		// Delete command
		void SelfDelCtrlClick(object sender, EventArgs e)
		{
			((ConditionList)this.Parent).DeleteItem(this);
		}
		
		
	}
}
