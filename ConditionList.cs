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
	/// Description of ConditionList.
	/// </summary>
	public partial class ConditionList : UserControl
	{
		
		public condition[] conditionz
		{
			get
			{
				condition[] rez = new condition[itemCou];
				for(int i = 0; i < itemCou; i++)
				{
					rez[i] = items[i].data;
				}
				
				return rez;
			}
			
			set
			{
				condition[] input = value;
				int pop = (itemCou > input.Length) ? input.Length : itemCou;
				
				for(int i = 0; i < pop; i++)
				{
					items[i].data = input[i];
				}
				
				while(itemCou > input.Length)
				{
					items[itemCou - 1].Dispose();
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
			}
		}
		
//		int backupItemCou;
		int itmWidth;
		public bool useScroll;
		ConditionCtrl[] items;
		public int itemCou;
		public int ScrolWidth;
		ConditionCtrl sel;
		public event MethodInvoker activate;
		public ConditionCtrl selectedItem
		{
			get
			{
				return sel;
			}
			set
			{
				if ((sel!= null)&& (sel!= value))
				{
					sel.BackColor = Color.Transparent;
					sel.ConditionCtrlBackColorChanged(null , null);
					sel.OperandSiplify();
					
				}
				
				if((activate != null) && (value!= null))
				{
					activate();
				}
				
				sel = value;
			}
		}
		
		public ConditionList()
		{
			InitializeComponent();
			
			this.SetScrollState(ScrollStateHScrollVisible, false);
			
			itemCou = 0;
			items = new ConditionCtrl[300];
			useScroll = true;
			ScrolWidth = 22;
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
		
		
		public ConditionCtrl AddItem(condition c)
		{
			this.AutoScroll = false;
			
			ConditionCtrl itm = new ConditionCtrl(c);
//			itm.Size = new Size(this.Width- ScrolWidth, 20);
//			itm.Height = 20;
			itm.Width = itmWidth;
			itm.sizeInit();
			itm.Location =  new Point(3, (itemCou > 0)? itemCou *20 +2: 2);
			items[itemCou] = itm;
			
			itemCou++;
			Controls.Add(itm);
			
			this.AutoScroll = useScroll;
								
			return itm;
						
		}
		
		public void DeleteItem(ConditionCtrl c)
		{
			
			int idx = Array.IndexOf(items, c);
			if((idx >=0) && (idx < itemCou))
			{
				Point previous =  items[idx].Location;
				
				for(int i= idx +1; i < itemCou; i++)
				{
					Point current = items[i].Location;
					items[i].Location  = previous; // items[i-1].Location;
					previous = current;
					items[i-1] =  items[i];
				}
				itemCou--;
				c.Dispose();
			}
		}
		
		public void SizeUp()
		{
			ConditionListResize(null, null);		
		}
		
		public int SetPreferedSize()
		{
			if(itemCou > 0)
			{
				this.Height = items[itemCou -1].Bounds.Bottom + 2;
			} else
			{
				this.Height = 3;
			}
			
			this.AutoScroll = false;
			return this.Height;
		}
		
		public void SetEqSide(string s, bool side)
		{
			if ((selectedItem != null) && (selectedItem.data.type == ConditionType.equality))
			{
				s = s.ToLower();
				condition c = selectedItem.data;
				if(side)
				{
					c.operand2 = s;
				} else
				{
					c.operand1 = s;
				}
				selectedItem.data = c;
			}
		}
		
		void ConditionListResize(object sender, EventArgs e)
		{
			this.AutoScroll = false;
			
			this.SuspendLayout();
			
			itmWidth =  this.Width- ScrolWidth;
			for(int i = 0; i < itemCou; i++)
			{
				
//				items[i].Location =  new Point(3, (i > 0)? i *20 : 0);
//				items[i].Height = 20;
				items[i].Width = itmWidth;
				
				
			}
			
			this.ResumeLayout();
			
			this.AutoScroll = useScroll;
		}
		
		void ConditionListControlAdded(object sender, ControlEventArgs e)
		{
			((ConditionCtrl)e.Control).Width = itmWidth;
		}
		
		void ConditionListMouseClick(object sender, MouseEventArgs e)
		{
			ConditionCtrl.DragInterop();
		}
	
	
	}
}
