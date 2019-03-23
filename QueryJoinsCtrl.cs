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
	/// Description of QueryJoinsCtrl.
	/// </summary>
	public partial class QueryJoinsCtrl : UserControl
	{
		string hostHandle;
		Query Box;
		public Query JoinzHost
		{
			get
			{
				return Box;
			}
			
			set
			{
				Box = value;
				
				
				if(Box != null)
				{
					if(hostHandle != Box.handle)
					{
						selectedItem = null;
						hostHandle = Box.handle;
					}
					
					joinz = Box.joinZ;
				}

			}
		}
		
		int backupItemCou;
		int itemCou;
		JoinCtrl[] items;
		
		JoinCtrl sel;
		public JoinCtrl selectedItem
		{
			get
			{
				return sel;
			}
			set
			{
				if((sel!= null)&&(sel != value))
				{
					sel.Unfocus();
//					sel.ConditionCtrlBackColorChanged(null , null);
//					sel.OperandSiplify();
					
				}
				
				sel = value;
				
				if(sel!= null)
				{
					sel.BackColor = Color.PeachPuff;
				}
			}
		}
		
		public join[] joinz
		{
			get
			{
				if((itemCou == 0))
				{
					return new join[0];
				}
				if((itemCou == 1) )
				{
					join[] rezH = new join[itemCou];
					rezH[0] = items[0].data;
					return rezH;
				}
				
				join[] rez = new join[itemCou - 1];
				for(int i = 1; i < itemCou; i++)
				{
					if(i > 0)
					{
						items[i].JoinTo(items[i - 1].data.rightTable);
					}
					rez[i -1] = items[i].data;
				}
				
				return rez;
			}
			
			set
			{
				join[] input = value;
				int pop = (backupItemCou > input.Length) ? input.Length : backupItemCou;
				
				for(int i = 0; i < pop; i++)
				{
					items[i].Visible = true;
					items[i].data = input[i];
					items[i].tabInfo =  Box.GetTableByName(input[i].rightTable);
					items[i].SizeUp();
				}
				
				itemCou = backupItemCou;
				
				while(itemCou > input.Length)
				{
					items[itemCou - 1].Visible = false;
					itemCou--;
				}
				
				this.AutoScroll = false;
				
				this.SuspendLayout();

				for(int i = itemCou; i < input.Length; i++)
				{
					AddItem(input[i]);
				}
				this.ResumeLayout(); //false

				this.AutoScroll = useScroll;
				
				if(backupItemCou < itemCou)
				{
					backupItemCou = itemCou;
				}
				
				if((itemCou >= 0) &&(items[0] != null))
				{
					items[0].JoinTo("");
				}
				
				HeighFit();
				this.ScrollControlIntoView(selectedItem);
			}
		}
		
		public int ScrolWidth;
		public bool useScroll;
		bool groupMan;
		
		
		public QueryJoinsCtrl()
		{
			InitializeComponent();
			
			itemCou = 0;
			items = new JoinCtrl[300];
			useScroll = true;
			ScrolWidth = 22;
			groupMan = false;
		}
		
		public void Save()
		{
			if(itemCou >0)
			{
				Box.joinZ = joinz;
			}
		}
		
		public void UpdateList()
		{
			joinz = Box.joinZ;
		}
		
		int GetTotalItemsHeigh(int start, int end)
		{
			int rez = 0;
			
			for(int i = start; i < end; i++)
			{
				rez += items[i].Height;
			}
			
//			rez += 2;
			
			return rez;
		}
		
		public void HeighFit()
		{
			if(groupMan) {return;}
			
			int scrollOffset = 0;
			if(itemCou > 0)
			{
				scrollOffset = items[0].Location.Y;
				if(scrollOffset > 0)
				{
					scrollOffset = 0;
				}
			}
			
			for(int i = 0; i < itemCou; i++)
			{
				items[i].Location =  new Point(3, ((i > 0)? GetTotalItemsHeigh(0, i)  + scrollOffset : scrollOffset));
				items[i].ListIdx = i;
				
//				items[i].BackColor = ((i % 2) == 0) ?  Color.AliceBlue : Color.FloralWhite;
//				items[i].BackColorDef = itm.BackColor;
			}

		}
		
		public int GetItemCou()
		{
			return itemCou;
		}
		
		public void ClearItems()
		{
			itemCou = 0;
			Controls.Clear();
		}
		
		public void AddItem(join j)
		{
			JoinCtrl itm = new JoinCtrl(j);
//		/	itm.data = j;
			itm.tabInfo = Box.GetTableByName(j.rightTable);
//			itm.SizeUp(this.Width-15);
			itm.Width = this.Width-15;
			
			itm.BackColor = ((itemCou % 2) == 0) ?  Color.AliceBlue : Color.FloralWhite;
			itm.BackColorDef = itm.BackColor;
//			itm.Height;
//			itm.Width = this.Width-15;
			
//			itm.Location =  new Point(3, (itemCou > 0)? items[itemCou -1].Bounds.Bottom +2 : 2);
			itm.Location =  new Point(3, (itemCou > 0)? GetTotalItemsHeigh(0, itemCou) : 2);
			items[itemCou] = itm;
			
			itemCou++;
			Controls.Add(itm);
		}
		
		public void JoinTable(string tName)
		{
			JoinTable(tName, -1);
		}
		
		public void JoinTable(string tName, int idx)
		{
			Save();
			tName = tName.Trim().ToLower();
			join j = new join(0);
			int leftIdx = (idx > 0)? (idx -1) : (itemCou -1);
			
			j.rightTable = tName;
			j.type = "join";
			
			if(leftIdx >= 0)
			{
				j.leftTable = items[leftIdx].data.rightTable;
			} else
			{
				j.leftTable = "";
			}
			
//			j.leftTable = items[leftIdx].data.rightTable;
			if(leftIdx == 0)
			{
				Box.joins[0] = j;
			} else
			{
				Box.AddJoin(j);
			}
			
			UpdateList();
		}
		
		public void MoveItem(int idx, int newIdx)
		{
			if((newIdx >= itemCou)||(idx >= itemCou)){return;}
			
			if(LoseJoinsStabilityOnMove(idx, newIdx))
			{
				MessageBox.Show("Affected table(s) depend on moved item, operation cancelled ");
				return;
			}
			
			JoinCtrl o = items[idx];
			
			
			if (idx > newIdx)
			{
				items[idx -1].JoinTo(items[idx].data.leftTable);
				for (int i =idx ; i > newIdx ; i--)
				{
					items[i] = items[i -1];
				}
				
			} else
			{
				items[idx +1].JoinTo(items[idx].data.leftTable);
				for (int i =idx; i < newIdx; i++)
				{
					items[i] = items[i +1];
				}
			}
			
			items[newIdx] = o;
			
			if(newIdx > 0)
			{
				items[newIdx].JoinTo(items[newIdx -1].data.rightTable);
				items[0].JoinTo("");
			} else
			{
				items[newIdx].JoinTo("");
			}
			
			HeighFit();
			this.ScrollControlIntoView(o);
		}
		
		public bool LoseJoinsStabilityOnMove(int idx, int newIdx)
		{
			bool rez = false;
			
			int max = (idx > newIdx)? idx : newIdx;
			int min = (idx < newIdx)? idx : newIdx;
			
			bool[] filter;
															
			if(idx == min )
			{
				string tabref = items[idx].data.rightTable + ".";
				string[] tmp = new string[max - min];
				min = min+1;
				
				for(int i = min; i <= max; i++)
				{
					tmp[i - min] = items[i].data.ToStringWH();
				}
				
				filter = Query.GetModifyIdxArr(tmp, tabref, 0);
				
			} else
			{				
				string[] tmp = new string[1];
				filter = new bool[1];
				tmp[0] = items[idx].data.ToStringWH();
				
				for(int i = min; i < max; i++)
				{
					Query.GetModifyIdxArr(tmp, ref filter, items[i].data.rightTable +"." , 0);
				}
								
			}
			
			foreach(bool b in filter)
			{
				rez = (rez == false) ? b : true;
			}
			
			return rez;
		}
		
		public void DeleteItem()
		{
			
			int idx = (selectedItem != null) ? selectedItem.ListIdx : -1;
			if(idx < 0)
			{
				return;
			}
			Save();
			Box.DeleteJoing( (idx == 0)? 0 : (idx -1) );
			UpdateList();
		}
		
		public void ExpandCollapseAll(bool exp)
		{
			int unaffected = (selectedItem != null) ? selectedItem.ListIdx : -1;
			
			this.SuspendLayout();
			groupMan = true;
			
			for(int i = 1; i < itemCou; i++)
			{
				if(!exp)
				{
					items[i].Maximize();
				} else
				{
					if(i != unaffected)
					{
						items[i].Minimize();
					}
				}
			}
			
			groupMan = false;
			HeighFit();
			
			this.ResumeLayout();
			
			this.ScrollControlIntoView(items[0]);
			this.ScrollControlIntoView(selectedItem);
		}
		
		void QueryJoinsCtrlSizeChanged(object sender, EventArgs e)
		{
			this.AutoScroll = false;
			
			for(int i = 0; i < itemCou; i++)
			{
				items[i].Width = this.Width- ScrolWidth;
			}
			
			this.AutoScroll = useScroll;
		}
		
		void QueryJoinsCtrlControlAdded(object sender, ControlEventArgs e)
		{
			((JoinCtrl)e.Control).SizeUp();
		}
	}
}
