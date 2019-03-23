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
	/// Description of JoinCtrl.
	/// </summary>
	public partial class JoinCtrl : UserControl
	{
		join dat;
		join datConstr;
		table tab;
		string selColumn;
		bool minimized;
		bool isFocused;
		int lidx;
		public int ListIdx
		{
			get
			{
				return lidx;
			}
			
			set
			{
				
				this.BackColorDef = ((value % 2) == 0) ?  Color.AliceBlue : Color.FloralWhite;
				if((lidx != value) &&(!isFocused))
				{
					this.BackColor = this.BackColorDef;
				}
				
				lidx = value;
			}
		}
		public Color BackColorDef;
		
		public table tabInfo
		{
			get
			{
				return tab;
			}
			
			set
			{
				tab = value;
				outerKeyz.Text = GetTableStruct();
			}
		}
		public join data
		{
			get
			{
				dat.ConditionzA = conditionCtrlz.conditionz;
				return dat;
			}
			
			set
			{
				
				dat = value;
				selColumn = "";
				
				tablenameCtrl.Text = dat.rightTable.ToUpper();
				conditionCtrlz.conditionz = dat.ConditionzA;
				
				if(minimized)
				{
					conditionTicket.Text = "( " + conditionCtrlz.GetItemCou().ToString() + " )";
				}
				outerKeyz.Text = GetTableStruct();
				
				if((dat.type == null) ||(dat.type.Trim() == "")) {dat.type = "join";}
				
				if(dat.leftTable != "")
				{
					JoinTypeCtrl.SelectedIndex = findListIdx(JoinTypeCtrl, dat.type);
				} else
				{
					minimized = true;
				}
			}
		}
		
		void Suko()
		{
			minimized = false;
			isFocused= false;
			conditionCtrlz.useScroll = false;
			conditionCtrlz.ScrolWidth = 4;
//			conditionCtrlz.SizeUp();
			tabInfo = new table("","",0);
			ListIdx = -1;
		}
		
		int findListIdx(ComboBox cbCtrl, string find)
		{
			int rez = -1;
			int cou = -1;
			if(find == "") {find = "join";}
			
			foreach(string s in cbCtrl.Items)
			{
				cou++;
				if(QueryParser.ContainsExpr(s.ToLower(), find))
				{
					rez = cou;
					break;
				}
			}
			
			return rez;
		}
		
		public JoinCtrl()
		{
			InitializeComponent();
			Suko();
		}
		
		public JoinCtrl(join j)
		{
			
			InitializeComponent();
			
			Suko();
			datConstr = j;
			
		}
		
		public string GetTableStruct()
		{
			string rez = "";
			
			foreach(field f in tab.Fields)
			{
				rez += f.alias.Trim() + " | ";
			}
			
			return rez ;
		}
		
		#region Focusing
		
		public void Unfocus()
		{
			conditionCtrlz.selectedItem = null;
			this.BackColor = BackColorDef;
			isFocused = false;
//			SizeUp();
		}
		
		public void SetFocusinList()
		{
			isFocused = true;
			((QueryJoinsCtrl)Parent).selectedItem = this;
		}
		
		#endregion
		
		#region Sizing 
		
		public void SizeUp()
		{
			conditionCtrlz.SetPreferedSize();
			this.Height = (minimized) ? (outerKeyz.Bounds.Bottom + 3) : (conditionCtrlz.Bounds.Bottom + 5);
		}
		
		public void SizeUp(int width)
		{
			conditionCtrlz.SetPreferedSize();
			
			this.Size = new Size(width, (minimized) ? (outerKeyz.Bounds.Bottom + 3) : (conditionCtrlz.Bounds.Bottom + 5));
//			this.Height =  conditionCtrlz.Bounds.Bottom + 9;
		}
		
		public void Minimize()
		{
			minimized = false;
			ShrinkCtrlClick(null , null);
		}
		
		public void Maximize()
		{
			minimized = true;
			ShrinkCtrlClick(null , null);
		}
		
		void JoinCtrlSizeChanged(object sender, EventArgs e)
		{
			conditionCtrlz.Width =  this.Width - 24;
			outerKeyz.Width = conditionCtrlz.Width - outerKeyz.Left;
			shrinkCtrl.Location = new Point(this.Width - 30, shrinkCtrl.Location.Y);
			conditionTicket.Location = new Point(shrinkCtrl.Location.X - conditionTicket.Width - 3, shrinkCtrl.Location.Y);
//			conditionCtrlz.Bounds.Bottom = this.Bounds.Right - 6;
		}
		
		void ShrinkCtrlClick(object sender, EventArgs e)
		{
			minimized = !minimized;
			
			conditionCtrlz.Visible = !minimized;
			
			if(minimized)
			{
				this.Height = outerKeyz.Bounds.Bottom + 3;
				shrinkCtrl.Text = ">>";
				conditionTicket.Text = "( " + conditionCtrlz.GetItemCou().ToString() + " )";
				
//				if(((QueryJoinsCtrl)Parent).selectedItem == this)
//				{
//					((QueryJoinsCtrl)Parent).selectedItem = null;
//				}
			} else
			{
				SizeUp();
				shrinkCtrl.Text = "<<";
				conditionTicket.Text = " ";
			}
			
//			this.outerKeyz.Focus();
			((QueryJoinsCtrl)Parent).HeighFit();
			
		}
		
		
		#endregion
		
		public void InsertOutField(string fld)
		{
			conditionCtrlz.SetEqSide(fld, true);
		}
		
		public void JoinTo(string table)
		{
			dat.leftTable = table;
			if(table == "") {JoinTypeCtrl.SelectedIndex = -1;}
			else
			{
				JoinTypeCtrl.SelectedIndex = findListIdx(JoinTypeCtrl, dat.type);
			}
		}
		
		#region GUI interact behaviour
		
		void TextBox1MouseMove(object sender, MouseEventArgs e)
		{
			int chIdx = ((TextBox)sender).GetCharIndexFromPosition(e.Location);
			
			segmCoord sc = QueryParser.GetSubstrSegmByCharNum(((TextBox)sender).Text, chIdx);
			if(!sc.isEmpty)
			{
				outerKeyz.SelectionStart = sc.start;
				outerKeyz.SelectionLength =  sc.fin - sc.start +1;
			}
			
			selColumn = outerKeyz.SelectedText;
		}
		
		void OuterKeyzMouseLeave(object sender, EventArgs e)
		{
			outerKeyz.SelectionLength = 0;
		}
		
		void TablenameCtrlMouseDoubleClick(object sender, MouseEventArgs e)
		{
			ShrinkCtrlClick(null , null);
		}
		
		void TablenameCtrlClick(object sender, EventArgs e)
		{
			SetFocusinList();
		}
		
		void JoinTypeCtrlTextChanged(object sender, EventArgs e)
		{
			dat.type = JoinTypeCtrl.Text.Trim().ToLower();
			
			if(dat.leftTable == "")
			{
				JoinTypeCtrl.SelectedIndex = -1;
			}
			outerKeyz.Focus();
			outerKeyz.SelectionLength = 0;
		}
		
		void JoinCtrlBackColorChanged(object sender, EventArgs e)
		{
			outerKeyz.BackColor = this.BackColor;
			JoinTypeCtrl.BackColor = this.BackColor;
		}
		
		//		ConditionCtrl.DragInterop();
		#endregion
		
		string GetSelFieldFullPath()
		{
			return tablenameCtrl.Text.ToLower().Trim()+ "."+ selColumn.Trim();
		}
		
		void AddEmptyTypedCondition( ConditionType ct)
		{
			condition c = new condition();
			c.text = "";
			c.type = ct;
			c.operand1 = GetSelFieldFullPath();
			c.cOperator = "=";
			conditionCtrlz.AddItem(c).SetFocusInList();
		}
		
		bool CheckSelCondOpr1Fill()
		{
			bool rez = true;
			if(conditionCtrlz.selectedItem != null)
			{
				condition c = conditionCtrlz.selectedItem.data;
				
				if((c.type == ConditionType.equality) && (c.operand1.Trim() == ""))
				{
					rez = false;
					c.operand1 = GetSelFieldFullPath();
					conditionCtrlz.selectedItem.data = c;
				}
			}
			
			return rez;
		}
		
		void OuterKeyzMouseDoubleClick(object sender, MouseEventArgs e)
		{
			JoinCtrl sel = ((QueryJoinsCtrl)Parent).selectedItem;
			
			if((isFocused || (sel == null)))
			{
				if(dat.leftTable.Trim() != "")
				{
					if (CheckSelCondOpr1Fill())
					{
						AddEmptyTypedCondition(ConditionType.equality);
						SizeUp();
						minimized = true;
						ShrinkCtrlClick(null , null);
						if(!isFocused){	SetFocusinList();}
					}
				}
				
			} else
			{
				if(sel != null)
				{
					if(sel.ListIdx > this.ListIdx)
					{
						sel.InsertOutField( GetSelFieldFullPath() );
					} else
					{
						SetFocusinList();
//						MessageBox.Show("It is not allowed by the RULES \n You must move down joined table (so it will COME AFTER the table join to)");
					}
				}
			}

		}
		
		
		void OuterKeyzEnter(object sender, EventArgs e)
		{
		}
		
		// set hosted ctrls parameters after object fully initialised
		void JoinCtrlLoad(object sender, EventArgs e)
		{
			data = datConstr;
		}
	}
}
