/*

 * Unit contain class that represent GUI for SqlBuilderClasses.Query
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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SqlBuilderClasses;
using System.Text;
using System.IO;


namespace sql_constructor
{
	
	public partial class QueryForm : Form
	{
		Query TQ;
		public Query ThisQuery
		{
			set
			{
				TQ = value;
				//label11.Text = "["+ TQ.ObjIdx +"] " + TQ.handle;
			}
			
			get
			{
				return TQ;
			}
		}
		public Query RootUnionObject;
		public Query[] RefToRootUnionArray;
		
		static QueryForm[] OpenList;
		static QueryTemplatesManager tmpman;
		public static int activeOpenListID;
		public static bool refreshOnActivate;
		int ThisOpenListID;
		
		bool isRoot;
		bool isUnioned;
		bool updating;
		bool listshow;
		bool flag;
		bool doTablesNodeEdit;
		
		bool mouseDrag;
		bool md
		{
			set
			{
				mouseDrag = value;
				if (!mouseDrag)
				{
					this.Cursor = Cursors.Arrow;
					movedItm = -1;
					mdList = null;
				}
				
			}
			get
			{
				return mouseDrag;
			}
		}
		
		string movedStr;
		int movedItm;
		int parallelNum;
		int prevTab;
		
		PrevValGetter fieldPrev;
		string DBConnectionStr;
		string[] undoroot;
		string[] aliasesUnionList;
		int activeSide;
		int aliasScroll;
		int clickCount;
		Point aliGridPoi;
		
		object mdList;
		
		public string[] QtextA
		{
			set
			{
				if(isRoot)
				{
					undoroot = value;
				}
				if (value == null) {return;}
				
				QueryParser qp = new QueryParser();
				qp.Text = value;
				qp.ParseRootSql(0);
				if (qp.errMessages != "")
				{
					MessageBox.Show(qp.errMessages);

				}else
				{
					Query.QueryObjects[qp.root].CopyTO(ThisQuery);
					ThisQuery.SelfCheck();
					
					parallelNum = 0;
					if(!isUnioned) // if parsed another union we ignore it, to existing union set adding only on spec tab
					{
						isUnioned = ThisQuery.IsUnioned;
						RefToRootUnionArray = ThisQuery.QueryUnions;
//						RootUnionObject = ThisQuery;
						UpdateUnions(ThisQuery.GetUnionedQId());
					}
					
				}
				
				TabControl1SelectedIndexChanged(null, null);
			}
		}
		
		public string Qtext
		{
			set
			{
				MessageBox.Show("Stub");
			}
			get
			{
				SyncGuiAndQuery(-1);
				return ThisQuery.GetSQLText(isRoot);
			}
		}
		public string connection
		{
			set
			{
				DBConnectionStr = value;
				Query.dbTables = DBEngine.GetDBStruct(DBConnectionStr);
				UpdateDBtree();
			}
			get
			{
				return	DBConnectionStr;
			}
		}
		
		string AllFieldsText = "All fields";
		
		#region forms list management
		
		static public int GetFreeOpenListIndex()
		{
			int rez = 0;
			while ((OpenList[rez] != null) && (rez < Query.n))
			{
				rez++;
			}
			return rez;
		}
		
		static	public int IsAlreadyOpened(string path)
		{
			int rez = -1;
			path = path.ToLower();
			
			for (int i = 0; i < OpenList.Length; i++)
			{
				if ((OpenList[i] != null) && (OpenList[i].Text.ToLower() == path))
				{
					rez = i;
					break;
				}
			}
			return rez;
		}
		
		#endregion
		
		public QueryForm()
		{
			InitializeComponent();
			
//			ConditionCtrl.DragInterop = this.GetDragString;
			button3.Visible = true;
			
			OpenList = new QueryForm[Query.n];
			tmpman = new QueryTemplatesManager();
			tmpman.ScanAndLoadTemplates();
			
			movedStr = "";
			isRoot = true;
			int id =  SqlBuilderClasses.Query.AddListObject();
			ThisQuery = Query.QueryObjects[id];
			ThisQuery.SelfCheck();
			RefToRootUnionArray = ThisQuery.QueryUnions;
			
//			RootUnionObject = ThisQuery;
			refreshOnActivate = true;
			
			object o = QueryTemplatesManager.LoadAppSettings("SizePosition");
			if(o != null)
			{
				string[] OpArr;
				QueryParser.GetExpresionStruct(o.ToString(), out OpArr);
				this.Bounds = new Rectangle( Convert.ToInt32(OpArr[1]) , Convert.ToInt32(OpArr[3]), Convert.ToInt32(OpArr[5]), Convert.ToInt32(OpArr[7]) );
			}
			
			o = QueryTemplatesManager.LoadAppSettings("lastOpenedDB");
			if(o != null)
			{
				QueryTemplatesManager.lastOpenedDB = o.ToString();
				if (!tmpman.CheckFreeName(QueryTemplatesManager.lastOpenedDB, false))
				{
					table[] tmp = QueryTemplatesManager.LoadDBStruct(QueryTemplatesManager.lastOpenedDB);
					if (tmp.Length > 0)
					{
						Query.dbTables = tmp;
					}
				}
			}
		}
		
		public QueryForm(Query q, string qname)
		{
			InitializeComponent();
			
			movedStr = "";
			this.ShowInTaskbar = false;
			this.Location =  OpenList[0].Location;
			this.Size = OpenList[0].Size;
			
			isRoot = false;
			ThisQuery = q;
			
			this.isUnioned = ThisQuery.IsUnioned;
			
//			RootUnionObject = ThisQuery;
			
			ThisQuery.SelfCheck();
			RefToRootUnionArray = ThisQuery.QueryUnions;
			ThisQuery.inEdit = true;
			
			this.Text = qname;
		}
		
		#region GUI data updating
		
		void UpdateUnions(int[] ba)
		{
			parallelNum = this.unionsCtrl.SelectedIndex;
			
			updating = true;
			this.unionsCtrl.TabPages.Clear();
			updating = false;
			#region path for delete case(to ensure that correct union deleted)
			object[] prev = new object[this.unionListCtrl.Items.Count];
			this.unionListCtrl.Items.CopyTo(prev, 0);
			#endregion
			this.unionListCtrl.Items.Clear();
			// updating unioned state of this query
			isUnioned = RefToRootUnionArray[0].IsUnioned;
			if (ba.Length <= 1)
			{
				this.unionListCtrl.Items.Add("Query 1");
				
				parallelNum = 0;
				UnionsCtrlSelectedIndexChanged(null, null);
				
				return;
			}
			
			for(int i = 0; i < ba.Length; i++)
			{
				string uMdf = string.IsNullOrEmpty(RefToRootUnionArray[i].UnionParam.Trim()) ? " ___" : (" "+ RefToRootUnionArray[i].UnionParam);
				this.unionsCtrl.TabPages.Add("Query "+(i + 1).ToString());
				if(flag)
				{
					this.unionListCtrl.Items.Add(prev[i]);
				} else
				{
					this.unionListCtrl.Items.Add("Query "+(i + 1).ToString() + uMdf );
				}
			}
			if (ba.Length >0)
			{
				this.unionsCtrl.SelectedIndex = ((parallelNum > 0) && (parallelNum < unionsCtrl.TabPages.Count)) ? parallelNum : 0;
			}
			parallelNum = this.unionsCtrl.SelectedIndex;
			
		}
		
		void UpdateDBtree()
		{
			this.dbCtrl.Nodes.Clear();
			if (Query.dbTables == null)
				return;
			
			
			foreach (table t in Query.dbTables)
			{
				if(t.alias == null) {continue;}
				TreeNode tableNode = new TreeNode(t.Name.ToUpper());
				tableNode.Tag = "dbl";
				
				field[] flds = t.Fields;
				
				foreach(field f in flds)
				{
					TreeNode fieldNode = new TreeNode(f.alias);
					tableNode.ToolTipText += "   " + f.alias + "      \n";
					tableNode.Name = f.alias;
					
					#region Type visualization
					if (!string.IsNullOrEmpty(f.type))
					{
						switch (f.type)
						{
							case "5":   // float
								case "131": fieldNode.ImageIndex = 1; // number types
								break;
								case "130":	fieldNode.ImageIndex = 2; // string
								break;
								case "135":	fieldNode.ImageIndex = 4; // date
								break;
								
								
							default:
								fieldNode.ImageIndex = 3;
								break;
						}
//						fieldNode.ToolTipText = f.type + "   ";
					} else
					{
						fieldNode.ImageIndex = 3;
					}
					
					fieldNode.SelectedImageIndex = fieldNode.ImageIndex;
					#endregion
					
					tableNode.Nodes.Add(fieldNode);
				}
				
				if ((!string.IsNullOrEmpty(t.Name)) && (!t.Name.Contains("$")))
				{
					this.dbCtrl.Nodes.Add(tableNode);
				}

			}
		}
		
		void UpdateTablesTree(table[] Tables)
		{
			this.tablesCtrl.Nodes.Clear();
			TableRenameDelPanel.Visible = false;
			
			foreach(table t in Tables)
			{
				TreeNode tableNode = new TreeNode(t.alias.ToUpper());
				tableNode.ToolTipText = t.Name;
				
				field[] flds = t.Fields;
				
				foreach(field f in flds)
				{
					TreeNode fieldNode = new TreeNode(f.alias);
					
					#region Type visualization
					if (!string.IsNullOrEmpty(f.type))
					{
						switch (f.type)
						{
							case "5":   // float
								case "131": fieldNode.ImageIndex = 1; // number types
								break;
								case "130":	fieldNode.ImageIndex = 2; // string
								break;
								case "135":	fieldNode.ImageIndex = 4; // date
								break;
								
								
							default:
								fieldNode.ImageIndex = 3;
								break;
						}
						
					} else
					{
						fieldNode.ImageIndex = 3;
					}
					
					fieldNode.SelectedImageIndex = fieldNode.ImageIndex;
					#endregion
					
					tableNode.Nodes.Add(fieldNode);
				}
				
				if ((!string.IsNullOrEmpty(t.alias)) && (!t.Name.Contains("$")))
				{
					if (t.queryId != -1)
					{
						tableNode.Tag = "VirtualTableDa";
						tableNode.ImageIndex = 5;
						tableNode.SelectedImageIndex = tableNode.ImageIndex;
					}
					this.tablesCtrl.Nodes.Add(tableNode);
				}

			}
		}
		
		void UpdateFieldsList(string[] sa)
		{
			// stub
			this.fieldsCtrl.Items.Clear();
			foreach(string s in sa)
			{
				string item = s;
				if (string.IsNullOrEmpty(s)) {item = "null";}
				this.fieldsCtrl.Items.Add(item);
			}
		}
		
		void UpdateGroupByList(string[] sa)
		{
			this.GoupByCtrl.Items.Clear();
			foreach(string s in sa)
			{
				this.GoupByCtrl.Items.Add(s);
			}
		}
		
		void UpdateAvalList(string[] sa)
		{
			bool exp = false;
			if (this.AvalFields.Nodes.Count > 0)
			{
				exp = this.AvalFields.Nodes[0].IsExpanded;
			}
			this.AvalFields.Nodes.Clear();
			TreeNode allFields = new TreeNode();
			allFields.Text = AllFieldsText;
			
			foreach(TreeNode tn in this.tablesCtrl.Nodes)
			{
				if (tn.Nodes.Count > 0)
				{
					foreach(TreeNode tnf in tn.Nodes)
					{
						allFields.Nodes.Add(tnf.FullPath);
					}
				}
			}
			
			if (allFields.Nodes.Count > 0)
			{
				this.AvalFields.Nodes.Add(allFields);
			}
			
			foreach(string s in sa)
			{
				this.AvalFields.Nodes.Add(s);
			}
			
			if (exp)
			{
				this.AvalFields.ExpandAll();
			}
		}
		
		void UpdateAgregateList(string[] sa)
		{
			int sel = this.AggregateFldsCtrl.SelectedIndex;
			this.AggregateFldsCtrl.Items.Clear();
			foreach(string s in sa)
			{
				this.AggregateFldsCtrl.Items.Add(s);
			}
			if (sel < this.AggregateFldsCtrl.Items.Count)
			{
				this.AggregateFldsCtrl.SelectedIndex = sel;
			}
		}
		
		void UpdateOrderAvalList(string[] sa)
		{
			bool exp = false;
			if (this.orderAvalCtrl.Nodes.Count > 0)
			{
				exp = this.orderAvalCtrl.Nodes[0].IsExpanded;
			}
			this.orderAvalCtrl.Nodes.Clear();
			TreeNode allFields = new TreeNode();
			allFields.Text = AllFieldsText;
			
			foreach(TreeNode tn in this.tablesCtrl.Nodes)
			{
				if (tn.Nodes.Count > 0)
				{
					foreach(TreeNode tnf in tn.Nodes)
					{
						allFields.Nodes.Add(tnf.FullPath);
					}
				}
			}
			
			if (allFields.Nodes.Count > 0)
			{
				this.orderAvalCtrl.Nodes.Add(allFields);
			}
			
			foreach(string s in sa)
			{
				this.orderAvalCtrl.Nodes.Add(s);
			}
			
			if (exp)
			{
				this.orderAvalCtrl.ExpandAll();
			}
		}
		
		void UpdateOrderByList(string[] sa)
		{
			this.orderByCtrl.Items.Clear();
			foreach(string s in sa)
			{
				this.orderByCtrl.Items.Add(s);
			}
		}
		
		void UpdateAliasUnionTable()
		{
			aliasListCtrl.Items.Clear();
			
			string[,] tbl;
			int rowcou;

			tbl = RefToRootUnionArray[0].GetUnionAliasTable();
//			tbl = RootUnionObject.GetUnionAliasTable();
			rowcou = RefToRootUnionArray[0].maxUFldCou;


			int colMax = tbl.GetLength(1);
			int colPrev = unialiasTableCtrl.Columns.Count;
			aliasesUnionList = new string[rowcou];
			
			#region Columns in table Query_1 ..Query_n
			
			for(int i = colPrev; i >= colMax; i--)
			{
				unialiasTableCtrl.Columns.Remove(unialiasTableCtrl.Columns[i-1]);
			}
			
			for(int i = (colPrev+1); i < colMax; i++)
			{
				unialiasTableCtrl.Columns.Add("Query "+ i.ToString(), 200, HorizontalAlignment.Left);
			}
			#endregion
			
			while(rowcou < unialiasTableCtrl.Items.Count)
			{
				unialiasTableCtrl.Items.RemoveAt(unialiasTableCtrl.Items.Count - 1);
			}
			
			for(int i = 0; i < rowcou; i++)
			{
				aliasListCtrl.Items.Add(tbl[i, 0]);
				aliasListCtrl.Items[i].SubItems.Add(tbl[i, 0]);
				
				#region lines in table
				if(unialiasTableCtrl.Items.Count > i)
				{
					string s = tbl[i, 1];
					unialiasTableCtrl.Items[i].SubItems.Clear();
					unialiasTableCtrl.Items[i].Text = tbl[i, 1];
				}else
				{
					unialiasTableCtrl.Items.Add(tbl[i, 1]);
				}
				#endregion
				
				aliasesUnionList[i] = tbl[i, 0];
				
				for(int j = 2; j < colMax; j++)
				{
					unialiasTableCtrl.Items[i].SubItems.Add(tbl[i, j]);
				}
			}
			
			ListView1Scroll(null, null);
			ScrollUAliasStripe(aliasScroll);
			
		}
		
		void ScrollUAliasStripe(int start)
		{
			aliasListCtrl.Items.Clear();
			int cou = 0;
			aliasScroll = start;
			for(int i = start; i < aliasesUnionList.Length; i++)
			{
				aliasListCtrl.Items.Add(aliasesUnionList[i]);
				aliasListCtrl.Items[cou].SubItems.Add(aliasesUnionList[i]);
				cou++;
			}
		}
		
		void UpdateConditionAvalList(string[] sa) // TODO : refactor for conditions
		{
			bool exp = false;
			if (this.orderAvalCtrl.Nodes.Count > 0)
			{
				exp = this.orderAvalCtrl.Nodes[0].IsExpanded;
			}
			this.orderAvalCtrl.Nodes.Clear();
			TreeNode allFields = new TreeNode();
			allFields.Text = AllFieldsText;
			
			foreach(TreeNode tn in this.tablesCtrl.Nodes)
			{
				if (tn.Nodes.Count > 0)
				{
					foreach(TreeNode tnf in tn.Nodes)
					{
						allFields.Nodes.Add(tnf.FullPath);
					}
				}
			}
			
			if (allFields.Nodes.Count > 0)
			{
				this.orderAvalCtrl.Nodes.Add(allFields);
			}
			
			foreach(string s in sa)
			{
				this.orderAvalCtrl.Nodes.Add(s);
			}
			
			if (exp)
			{
				this.orderAvalCtrl.ExpandAll();
			}
		}
		
		void UpdateJoinAvalTables()
		{
			this.tablesToJoinCtrl.Nodes.Clear();
			
			if (this.tablesCtrl.Nodes.Count <= 0)
			{
				return;
			}
			
			string except = (ThisQuery.joinCou > 0) ? ThisQuery.joins[0].leftTable.Trim().ToLower() : "";
			
			foreach(TreeNode tn in this.tablesCtrl.Nodes)
			{
				if((ThisQuery.IsTableJoined(tn.Text.Trim().ToLower()) < 0) && (tn.Text.Trim().ToLower() != except))
				{
					tablesToJoinCtrl.Nodes.Add(tn.Text.Trim());
				}
			}
		}
		
		void SyncGuiAndQuery(int tabNum)
		{
			int check = (tabNum == -1) ? this.tabControl1.SelectedIndex : prevTab;
				
			if (check == 1)
			{
				JoinsCtrl.Save();
			}
			
			if (check == 3)
			{
				ThisQuery.whereZ = conditionListCtrl.conditionz;
			}
			
			prevTab = this.tabControl1.SelectedIndex;
		}
		
		#endregion
		
		#region Dialogs
		
		// Open DB Connection Form
		void Button4Click(object sender, EventArgs e)
		{
			DBConnectionForm frm = new DBConnectionForm(this);
			frm.Show(this);
		}
		
		// Open Query Text Box
		void Button5Click(object sender, EventArgs e)
		{
			QueryTextBox frm = new QueryTextBox(this);
			frm.text = Qtext;
			frm.Show(this);
		}
		
		#region Input Box
		
		public static DialogResult InputBox(string title, string promptText)
		{
			string stub = "";
			return InputBox(2, title, null, promptText, ref stub);
		}
		
		public static DialogResult InputBox(string title, string promptText, ref string value)
		{
			return InputBox(0, title, null, promptText, ref value);
		}
		
		public static DialogResult InputBox(string title, string[] list, string promptText, ref string value)
		{
			return InputBox(1, title, list, promptText, ref value);
		}
		
		public static DialogResult InputBox(byte type, string title, string[] list, string promptText, ref string value)
		{
			
			Form form = new Form();
			Label label = new Label();
			Control textBox = null;
			if((type==0) || (type== 2))
			{
				textBox = new TextBox();
			}
			if(type==1)
			{
				textBox = new ComboBox();
				((ComboBox)textBox).Items.AddRange(list);
				((ComboBox)textBox).DropDownStyle = ComboBoxStyle.DropDownList;
			}
			
			if((type== 2))
			{
				textBox.Visible = false;
				refreshOnActivate = false;
			}
			
			Glass.GlassButton buttonOk = new Glass.GlassButton();
			Glass.GlassButton buttonCancel = new Glass.GlassButton();;

			form.Text = title;
			label.Text = promptText;
			textBox.Text = value;

			buttonOk.Text = "OK";
			buttonCancel.Text = "Cancel";
			buttonOk.DialogResult = DialogResult.OK;
			buttonCancel.DialogResult = DialogResult.Cancel;

			label.SetBounds(9, 20, 372, 13);
			textBox.SetBounds(12, 36, 372, 20);
			buttonOk.SetBounds(228, 72, 75, 23);
			buttonCancel.SetBounds(309, 72, 75, 23);

			label.AutoSize = true;
			textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
			buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			
			buttonOk.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			buttonOk.ForeColor = System.Drawing.SystemColors.MenuText;
			buttonOk.GlowColor = System.Drawing.SystemColors.GradientActiveCaption;
			buttonOk.ShineColor = System.Drawing.SystemColors.ButtonFace;
			
			buttonCancel.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			buttonCancel.ForeColor = System.Drawing.SystemColors.MenuText;
			buttonCancel.GlowColor = System.Drawing.SystemColors.GradientActiveCaption;
			buttonCancel.ShineColor = System.Drawing.SystemColors.ButtonFace;

			form.ClientSize = new Size(396, 107);
			form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
			form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
			form.FormBorderStyle = FormBorderStyle.FixedDialog;
			form.StartPosition = FormStartPosition.CenterScreen;
			form.MinimizeBox = false;
			form.MaximizeBox = false;
			form.AcceptButton = buttonOk;
			form.CancelButton = buttonCancel;

			DialogResult dialogResult = form.ShowDialog();
			value = textBox.Text;
			return dialogResult;
		}
		
		#endregion
		
		// Load DB structure
		void Button6Click(object sender, EventArgs e)
		{
			tmpman.ScanAvailableDBStruct();
			string name = tmpman.dbNames.Length > 0 ? tmpman.dbNames[0] : "";
			if ((InputBox("Load DB structure", tmpman.dbNames, "Choose DB structure to load:", ref name) == DialogResult.OK) && (name != ""))
			{
				table[] tmp = QueryTemplatesManager.LoadDBStruct(Environment.CurrentDirectory + @"\DB Structure\"+ name);
				if (tmp.Length > 0)
				{
					Query.dbTables = tmp;
					UpdateDBtree();
				}
			}
		}
		
		// Save DB structure
		void Button7Click(object sender, EventArgs e)
		{
			string name = "DB structure";
			if ((InputBox("Save DB structure", "File name:", ref name) == DialogResult.OK) && (name != ""))
			{
				if ((Query.dbTables != null) && (Query.dbTables.Length > 0))
				{
					string tmp = Environment.CurrentDirectory + @"\DB Structure\"+ name;
					
					if(!tmpman.CheckFreeName(tmp+ ".txt", false))
					{
						int i = 1;
						while(!tmpman.CheckFreeName(tmp+"_"+ i.ToString() + ".txt", false))
						{
							i++;
						}
						tmp = tmp+"_"+ i.ToString();
					}
					QueryTemplatesManager.saveDBStruct(tmp+ ".txt");
				}
			}
		}
		
		// open new form for virtual table
		void OpenVirtTableForm(TreeNode tmp)
		{
			if (tmp == null) {return;}
			if((tmp.Tag != null)&&(tmp.Tag.ToString() == "VirtualTableDa"))
			{
				int idx = ThisQuery.HaveSuchTable(tmp.Text, true);
				if (idx != -1)
				{
					table[] t = ThisQuery.GetTables();
					Query QU = new Query();
					Query.QueryObjects[ t[idx].queryId ].CopyTO(QU);
					
					string formPath = tmp.Text + ((RefToRootUnionArray[0].IsUnioned ) ? " /Query "+ (parallelNum +1).ToString() : "") + " |"+ this.Text;

					int openID = IsAlreadyOpened(formPath);
					if(openID >= 0)
					{
						OpenList[openID].BringToFront();
						return;
					} else
					{
						QueryForm frm = new QueryForm(QU, formPath);
						frm.Show();
					}
				}
			}
		}
		
		#endregion
		
		//  before form open
		void QueryFormShown(object sender, EventArgs e)
		{
			if (this.Left == 0)
			{
				this.Left = 200;
				this.Top= 150;
			}
			
			ThisOpenListID = GetFreeOpenListIndex();
			OpenList[ThisOpenListID] = this;
			QueryForm.activeOpenListID = ThisOpenListID;
			
			fieldPrev = new PrevValGetter();
			aliGridPoi = new Point();
			prevTab = -1;
			
			TabControl1SelectedIndexChanged(null, null);
			
			if (ThisQuery != null)
			{
				UpdateUnions(ThisQuery.GetUnionedQId());
			}
		}
		
		#region Activate, Navigation on opened formz
		
		// show list of opened forms
		void QueryFormMouseLeave(object sender, EventArgs e)
		{
			
			Rectangle area = this.Bounds;
			area.Inflate(-150, 0);
			area.Height = 30;
			if(area.Contains(Control.MousePosition))
			{
				listshow = true;
				OpenQueryFormsNAvigator frm = new OpenQueryFormsNAvigator(OpenList);
				frm.Location = new Point(this.Location.X+ 1, this.Location.Y + 27);
				if(frm.FormCount > 1)
				{
					frm.Show();
				}
			}
		}
		
		// delete closed form from the list
		void QueryFormFormClosed(object sender, FormClosedEventArgs e)
		{
			OpenList[ThisOpenListID] = null;
			
			if(isRoot)
			{
				QueryTemplatesManager.SaveAppSettings("SizePosition", this.Bounds);
				QueryTemplatesManager.SaveAppSettings("lastOpenedDB", QueryTemplatesManager.lastOpenedDB);
			}
		}
		
		// open top form after switch from other applications
		void QueryFormActivated(object sender, EventArgs e)
		{
			if((!listshow) && (refreshOnActivate))
			{				
				SyncGuiAndQuery(-1);
				TabControl1SelectedIndexChanged(null,null);				
			}
			ConditionCtrl.DragInterop = this.GetDragString;
			
			listshow = false;
			refreshOnActivate = true;
			
			if (isRoot)
			{
				if((OpenList[activeOpenListID] != null) && (activeOpenListID != this.ThisOpenListID))
				{
					OpenList[activeOpenListID].BringToFront();
				}
			} else
			{				
//					activeOpenListID = this.ThisOpenListID;
			}
			
//			label11.Text = "Activated  " + activeOpenListID.ToString() + " self "+ this.ThisOpenListID.ToString();
			
		}
		
		#endregion
		
		//  OK
		void Button2Click(object sender, EventArgs e)
		{
			
			SyncGuiAndQuery(-1);
			if (isUnioned)
			{
				RefToRootUnionArray[0].Save();

			} else
			{
				ThisQuery.Save();
			}
			
			if(!isRoot)
			{
				this.Close();
			} else
			{
				Button5Click(null , null);
			}
		}
		
		// CANCELL
		void CancelCtrlClick(object sender, EventArgs e)
		{
//			if(!isRoot)
//			{
			this.Close();
//			} else
//			{
//				QtextA = undoroot;
//			}
			
//			if(isRoot)
//			{
//				QueryTemplatesManager.SaveAppSettings("SizePosition", this.Bounds);
//				QueryTemplatesManager.SaveAppSettings("lastOpenedDB", QueryTemplatesManager.lastOpenedDB);
//			}
		}
		
		// switch UNIONS
		void UnionsCtrlSelectedIndexChanged(object sender, EventArgs e)
		{
			if((!isUnioned) || (updating)) {return;}
			if(this.unionsCtrl.SelectedIndex >= 0)
			{
				parallelNum = this.unionsCtrl.SelectedIndex;
			}
			ThisQuery.inEdit = false;
			
			SyncGuiAndQuery(-1);
			
			ThisQuery = RefToRootUnionArray[ parallelNum ];
			ThisQuery.SelfCheck();
			
			ThisQuery.inEdit = true;
			
			if(isRoot && parallelNum ==0){ThisQuery.inEdit = false;} // to show whole query text, not only header union
			TabControl1SelectedIndexChanged(null, null);
		}
		
		// UPDATING Sheets
		void TabControl1SelectedIndexChanged(object sender, EventArgs e)
		{			
			
			if (this.tabControl1.SelectedIndex == 0)
			{
				ThisQuery.SelfCheck();
				if(!string.IsNullOrEmpty(ThisQuery.UnionParam)){
					uParamCtrl.Text = "U " + ThisQuery.UnionParam;} else
					uParamCtrl.Text = "" ;
				UpdateTablesTree(ThisQuery.GetTables());
				UpdateFieldsList(ThisQuery.FldStrings());
				UpdateDBtree();
			} else
			{
				ThisQuery.SelfCheck();
				UpdateTablesTree(ThisQuery.GetTables()); // other available item trees (condition, group, order) use this tree nodes
			}
			
			if (this.tabControl1.SelectedIndex == 1)
			{
				UpdateJoinAvalTables();
				JoinsCtrl.JoinzHost = ThisQuery;
				
//				JoinsCtrl.Save();
			}
			
			if (this.tabControl1.SelectedIndex == 2)
			{
				UpdateGroupByList(ThisQuery.groups);
				UpdateAvalList(ThisQuery.GetGroupingAvailableItems());
				UpdateAgregateList(ThisQuery.GetAgregateFields());
			}
			
			if (this.tabControl1.SelectedIndex == 3)
			{
				tablesList.Nodes.Clear();				
				
				foreach(TreeNode tn in this.tablesCtrl.Nodes)
				{
					tablesList.Nodes.Add( (TreeNode) tn.Clone());
				}
				
				conditionListCtrl.conditionz = ThisQuery.whereZ;
				
			}
			
			if (this.tabControl1.SelectedIndex == 4)
			{
				if (!isUnioned && ThisQuery.isHeader)
				{
					RefToRootUnionArray = ThisQuery.QueryUnions;
				}
				UpdateAliasUnionTable();
				unionListCtrl.SelectedIndex = -1;
//				UpdateUnions(RootUnionObject.GetUnionedQId());
			}
			
			if (this.tabControl1.SelectedIndex == 5)
			{
				UpdateOrderAvalList(ThisQuery.GetOrderingAvailableItems());
				UpdateOrderByList(ThisQuery.orders);
			}						
				
			SyncGuiAndQuery(prevTab);
			
			
			Button10Click(null, null); // hide templates list
			fieldPrev = new PrevValGetter();
			md = false;
			aliasScroll = 0;
			movedStr = "";
		}
		
		// TEST PARSE
		void Button3Click(object sender, EventArgs e)
		{
			// About Show
			AboutBox f = new AboutBox();
			f.ShowDialog();
		}
		
		public string GetDragString()
		{
			string	rez = movedStr;
			movedStr = "";
			md = false;
			return rez;
		}
		
		#region DB tree regulation
		
		void DbCtrlMouseDoubleClick(object sender, MouseEventArgs e)
		{
			TreeNode fieldNode = this.dbCtrl.SelectedNode;
			if ((fieldNode == null) || (fieldNode != this.dbCtrl.GetNodeAt(new Point(e.X, e.Y))))
			{
				return;
			}
			
			if (!fieldNode.IsExpanded)
			{
				fieldNode.Tag = "dbl";
				AddQueryTable(fieldNode);
				
			} else
			{
				fieldNode.Tag = "";
			}
			
		}
		
		void DbCtrlBeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			e.Cancel = (e.Node.Tag.ToString() == "dbl");
			e.Node.Tag = "";
		}
		
		void DbCtrlMouseClick(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				TreeNode fieldNode = this.dbCtrl.GetNodeAt(new Point(e.X, e.Y));
				if (fieldNode != null)
				{
					this.dbCtrl.SelectedNode = fieldNode;
					if (!fieldNode.IsExpanded)
					{
						fieldNode.Tag = "";
						fieldNode.Expand();
					} else
					{
						fieldNode.Tag = "dbl";
						fieldNode.Collapse();
					}
				}
			}
		}
		
		void DbCtrlAfterCollapse(object sender, TreeViewEventArgs e)
		{
			e.Node.Tag =  "dbl";
		}
		
		#endregion
		
		#region Collapse / Expand Tables
		
		void MoveTablesClick(object sender, EventArgs e)
		{
			if	(this.dbCtrl.SelectedNode != null)
			{
				AddQueryTable(this.dbCtrl.SelectedNode);
			}
		}
		
		void CollapseAllClick(object sender, EventArgs e)
		{
			this.dbCtrl.CollapseAll();
		}
		
		void ExpandAllClick(object sender, EventArgs e)
		{
			this.dbCtrl.ExpandAll();
			this.dbCtrl.ExpandAll();
		}
		
		void TablesCtrlBeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			if(doTablesNodeEdit)
			{
				e.CancelEdit = (e.Node.Parent != null);
				TableRenameDelPanel.Visible = false;
			} else e.CancelEdit = true;
			
			
		}
		
		void Button9Click(object sender, EventArgs e)
		{
			this.tablesCtrl.CollapseAll();
		}
		
		void Button8Click(object sender, EventArgs e)
		{
			this.tablesCtrl.ExpandAll();
		}
		
		#endregion
		
		#region Tables manipulation
		
		// Add DB teble
		void AddQueryTable(TreeNode fieldNode)
		{
			TreeNode tmp;
			if(fieldNode.Parent!= null)
			{
				tmp = (TreeNode)fieldNode.Parent.Clone();
			} else
			{
				tmp = (TreeNode)fieldNode.Clone();
			}
			tmp.ToolTipText = tmp.Text;
			tmp.Text = ThisQuery.AddDBTable(tmp.Text);
			this.tablesCtrl.Nodes.Add(tmp);
		}
		
		// Add Virtual teble
		void AddVirtCtrlClick(object sender, EventArgs e)
		{
			ThisQuery.AddVirtTable();
			ThisQuery.SelfCheck();
			UpdateTablesTree(ThisQuery.GetTables());
		}
		
		// Delete Table
		void TablesCtrlKeyUp(object sender, KeyEventArgs e)
		{
			if	((e.KeyCode == Keys.Delete) &&(this.tablesCtrl.SelectedNode != null) && (this.tablesCtrl.SelectedNode.Parent == null))
			{
				ThisQuery.DeleteTable(this.tablesCtrl.SelectedNode.Text);
				this.tablesCtrl.Nodes.Remove(this.tablesCtrl.SelectedNode);
				UpdateFieldsList(ThisQuery.FldStrings());
			}
		}
		
		// Change Table alias
		void TablesCtrlAfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			if (e.Label == null)
			{
				return;
			}
			string rezult = ThisQuery.ChangeAlias(e.Node.Text, e.Label);
			
			if (rezult != "")
			{
				e.CancelEdit = true;
				MessageBox.Show(rezult);
			}
			e.Node.Text = e.Label.Replace(' ','_');
			UpdateFieldsList(ThisQuery.FldStrings());
			e.CancelEdit = true;
		}
		
		// highlight selected fields from curr table
		void TablesCtrlNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node.Parent == null)
			{
				doTablesNodeEdit = false;
				ThisQuery.MarkFldFromTable(e.Node.Text.Trim());
				TableRenameDelPanel.Visible = true;
				tablesCtrl.SelectedNode = e.Node;
				TableRenameDelPanel.Location = new Point(e.Node.Bounds.X + e.Node.Bounds.Width +10, e.Node.Bounds.Y + tablesCtrl.Location.Y+ 2);
			} else
			{
				TableRenameDelPanel.Visible = false;
				ThisQuery.MarkFldFromTable(e.Node.Parent.Text.Trim());
			}
			
			
			this.fieldsCtrl.Refresh();
		}
		
		// right-click open virtual table form
		void TablesCtrlMouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				TreeNode tmp = this.tablesCtrl.GetNodeAt(new Point(e.X, e.Y));
				OpenVirtTableForm( tmp);
			} else
			{
				TablesListMouseClick(sender, e);
			}
		}
		
		// button open virtual table form
		void VirtOpenClick(object sender, EventArgs e)
		{
			OpenVirtTableForm(this.tablesCtrl.SelectedNode);
		}
		
		// retrieve all database structure info from query
		void GetDBFromQuery()
		{
			int startIdx = -1;
			int host = -1;
			string[] nameTree = new string[300];
			int[] idTree  = new int[300];
			int[] hostTree = new int[300];
			if(isRoot)
			{
				RefToRootUnionArray[0].Save();
			}
			Query.extendDb = true;
//			Query.dbTables = new table[0];
			
			this.tabControl1.SelectedIndex = 0;
			OpenList[0].RefToRootUnionArray[0].TraceTree(ref  startIdx,  host,  "Root",  ref  nameTree, ref  idTree, ref hostTree);
			
			Query.extendDb = false;
			
			UpdateDBtree();
		}
		
		#endregion
		
		#region Fields manipulation
		
		// add field standalone proc
		void AddField(TreeNode tmp)
		{
			if (tmp.Parent != null)
			{
				this.fieldsCtrl.Items.Add(tmp.FullPath);
				ThisQuery.AddField(tmp.FullPath);
				
				ThisQuery.MarkFldFromTable(tmp.Parent.Text.Trim());
				UpdateFieldsList(ThisQuery.FldStrings());
			}
		}
		
		// add field from Table
		void TablesCtrlMouseDoubleClick(object sender, MouseEventArgs e)
		{
			TreeNode tmp = this.tablesCtrl.GetNodeAt(new Point(e.X, e.Y));
			AddField(tmp);
		}
		
		// field list coloring
		void FieldsCtrlDrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index == -1)
			{
				return;
			}
			e.DrawBackground();
			Brush myBrush = Brushes.Black;
			
			if (ThisQuery.IsMArkedFld(e.Index))
			{
				myBrush = Brushes.Green;
			}
			
			if (((fieldsCtrl.SelectedIndex == e.Index) && (!md) ) )
			{
				SolidBrush mBrush = new SolidBrush(Color.FromArgb(192, 255, 192));
				e.Graphics.FillRectangle(mBrush, e.Bounds);
			}
			
			if (((movedItm == e.Index)&& (md)))
			{
				myBrush = Brushes.Teal;
				SolidBrush mBrush = new SolidBrush(Color.LightCyan);
				e.Graphics.FillRectangle(mBrush, e.Bounds);
			}
			
			e.Graphics.DrawString(fieldsCtrl.Items[e.Index].ToString(), e.Font, myBrush,e.Bounds,StringFormat.GenericDefault);

			if ((!fieldPrev.isBothEqual)&&(fieldPrev.Previous != -1)&&(fieldPrev.Previous < fieldsCtrl.Items.Count))
			{
				Rectangle r =fieldsCtrl.GetItemRectangle(fieldPrev.Previous);
				fieldsCtrl.Invalidate(r);
			}
		}
		
		void FieldsCtrlMouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.Cursor = Cursors.NoMoveVert;
				md = true;
			}
		}
		
		// invalidate previous colored item
		void FieldsCtrlSelectedValueChanged(object sender, EventArgs e)
		{
			if ((fieldPrev.Previous >= 0)&&(fieldPrev.Previous < fieldsCtrl.Items.Count))
			{
				Rectangle r =fieldsCtrl.GetItemRectangle(fieldPrev.Previous);
				fieldsCtrl.Invalidate(r);
			}
			fieldPrev.Current = this.fieldsCtrl.SelectedIndex;
		}
		
		// add field to select list
		void MoveFieldsClick(object sender, EventArgs e)
		{
			TreeNode tmp = this.tablesCtrl.SelectedNode;
			if	(tmp != null)
			{
				if (tmp.Parent != null)
				{
					AddField(tmp);
				} else
				{
					foreach(TreeNode tn in tmp.Nodes)
					{
						AddField(tn);
					}
				}
			}
		}
		
		#endregion Fields
		
		#region multi-tab Drag n drop functionality (based on Grouping functionality chema)
		
		void GoupByCtrlMouseUp(object sender, MouseEventArgs e)
		{
			int sel = ((ListBox)sender).IndexFromPoint(e.X, e.Y);
			string ListKind = ((ListBox)sender).Tag.ToString();
			
			if ((sel >= 0) && (string.IsNullOrEmpty(movedStr)) && (movedItm >=0) )
			{
				if (!md) {return;}
				object o =  ((ListBox)sender).Items[movedItm];

				#region group / order processing
				if (ListKind == "gr")
				{
					ThisQuery.GroupingMoveTo(movedItm, sel);
				}
				if (ListKind == "od")
				{
					ThisQuery.OrderMoveTo(movedItm, sel);
				}
				
				if (ListKind == "fl")
				{
					ThisQuery.FieldMoveTo(movedItm, sel);;
				}
				
				#endregion
				
				if (movedItm != sel)
				{
					((ListBox)sender).Items.RemoveAt(movedItm);
					((ListBox)sender).Items.Insert(sel, o);
				}
				
				((ListBox)sender).SelectedIndex = sel;
				movedItm = -1;
			}
			
			//============================================
			if((md)&& (movedItm != -1) && (movedStr == "") && (sel == -1) && (e.X < 0)) //&&(((ListBox)sender).Bounds.X >)
			{
				((ListBox)sender).SelectedIndex = movedItm;
				GoupByCtrlKeyUp(mdList, new KeyEventArgs(Keys.Delete));
			}
			//============================================
			
			#region dragging outside object (string) to add to list
			
			if ((md) && (!string.IsNullOrEmpty(movedStr)))
			{
				
				int idx = ((ListBox)sender).IndexFromPoint(new Point(e.X, e.Y));
				if (idx != ListBox.NoMatches)
				{
					((ListBox)sender).Items.Insert(idx, movedStr);
					#region group / order processing
					if (ListKind == "gr")
					{
						ThisQuery.AddGrouping(movedStr, idx);
					}
					if (ListKind == "od")
					{
						ThisQuery.AddOrder(movedStr, idx);
					}
					#endregion
					
				} else
				{
					((ListBox)sender).Items.Add(movedStr);
					#region group / order processing
					if (ListKind == "gr")
					{
						ThisQuery.AddGrouping(movedStr);
					}
					
					if (ListKind == "od")
					{
						ThisQuery.AddOrder(movedStr);
					}
					#endregion
				}
				movedStr = "";
				
				if (ListKind == "gr")
				{
					UpdateAvalList(ThisQuery.GetGroupingAvailableItems());
				}
				if (ListKind == "od")
				{
					UpdateOrderAvalList(ThisQuery.GetOrderingAvailableItems());
				}
			}
			#endregion
			
			md = false;
		}
		
		void GoupByCtrlMouseDown(object sender, MouseEventArgs e)
		{
			movedItm = ((ListBox)sender).SelectedIndex;
			movedStr = "";
			fieldPrev.Current = movedItm;
			if ((fieldPrev.Previous >= 0) && (fieldPrev.Previous < ((ListBox)sender).Items.Count))
			{
				Rectangle r =((ListBox)sender).GetItemRectangle(fieldPrev.Previous);
				((ListBox)sender).Invalidate(r);
			}
			
			mdList = sender;
		}
		
		void GoupByCtrlMouseLeave(object sender, EventArgs e)
		{
			md = false;
		}
		
		void GoupByCtrlMouseMove(object sender, MouseEventArgs e)
		{
			int sel = ((ListBox)sender).IndexFromPoint(e.X, e.Y);
			if (fieldPrev.Current != sel)
			{
				if ((e.Button == MouseButtons.Left) && (!md))
				{
					movedStr = "";
					// because moved item is leaved and no auto redraw
					Rectangle r;
					if(movedItm >=0)
					{
						r =((ListBox)sender).GetItemRectangle(movedItm);
						((ListBox)sender).Invalidate(r);
					}
					// redraw is invoking earlier than MouseMove (first near item colored as selected item in NO drag mode) so it needed to redraw
					if(sel >=0)
					{
						r =((ListBox)sender).GetItemRectangle(sel);
						((ListBox)sender).Invalidate(r);
					}
					
					if(e.X >0){
						this.Cursor = Cursors.NoMoveVert;}
//					else{this.Cursor = Cursors.PanWest;}
					
					md = true;
				}
				
				if((e.X >0) && md){
					this.Cursor = Cursors.NoMoveVert;}
			}
			else
			{
//				if((movedItm >=0) && (movedItm >=0)){
//					this.Cursor = Cursors.PanWest;
//				}
			}
			
			if((movedItm >=0) && (e.X < 0)){
				this.Cursor = Cursors.PanWest;
			}
		}
		
		// item DRAWING style in list
		void GoupByCtrlDrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index == -1)
			{
				return;
			}
			e.DrawBackground();
			Brush myBrush = Brushes.Black;
			
			//if(((ListBox)sender).SelectedIndex != e.Index)
			if((e.State & DrawItemState.Focus) != DrawItemState.Focus)
			{
				SolidBrush zebraBrush = new SolidBrush(((e.Index % 2) == 0) ? Color.Transparent : Color.MintCream);
				e.Graphics.FillRectangle(zebraBrush, e.Bounds);
			}
			
			if (((((ListBox)sender).SelectedIndex == e.Index) && (!md) ) )
			{
				SolidBrush mBrush = new SolidBrush(Color.FromArgb(192, 255, 192));
				e.Graphics.FillRectangle(mBrush, e.Bounds);
			}
			
			if (((movedItm == e.Index)&& (md)))
			{
				myBrush = Brushes.Teal;
				SolidBrush mBrush = new SolidBrush(Color.LightCyan);
				e.Graphics.FillRectangle(mBrush, e.Bounds);
			}
			
			string itmText =((ListBox)sender).Items[e.Index].ToString().TrimEnd();
			e.Graphics.DrawString( QueryParser.TruncOrderMdf(itmText), e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);

			if (QueryParser.IsSortExplicit(itmText))
			{
				string mdf = itmText.Substring(itmText.Length -4, 4).TrimStart();
				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Far;
				
				mdf = (mdf == "___") ? "__" : mdf;
				
				Size preferredSize = e.Graphics.MeasureString(mdf, e.Font).ToSize();
				
				e.Graphics.FillRectangle(Brushes.Lavender, e.Bounds.Right  - preferredSize.Width , e.Bounds.Y, preferredSize.Width, preferredSize.Height);
				e.Graphics.DrawString( mdf, e.Font, myBrush, e.Bounds, sf);
			}
			
			if ((!fieldPrev.isBothEqual)&&(fieldPrev.Previous != -1)&&(fieldPrev.Previous < ((ListBox)sender).Items.Count))
			{
				Rectangle r =((ListBox)sender).GetItemRectangle(fieldPrev.Previous);
				((ListBox)sender).Invalidate(r);
			}
		}
		
		void GoupByCtrlSelectedValueChanged(object sender, EventArgs e)
		{
			if ((fieldPrev.Previous >= 0)&&(fieldPrev.Previous < ((ListBox)sender).Items.Count))
			{
				Rectangle r =((ListBox)sender).GetItemRectangle(fieldPrev.Previous);
				((ListBox)sender).Invalidate(r);
			}
			fieldPrev.Current = ((ListBox)sender).SelectedIndex;
		}
		
		string DefineNodeDragText(TreeNode tn)
		{
			string rez;
			
			if ((tn == null) || (tn.Nodes.Count > 0))  {return "";}
			
			if ((tn.Parent != null) && (tn.Parent.Text != AllFieldsText))
			{
				rez = tn.Parent.Text + "." + tn.Text;
			} else
			{
				rez = tn.Text;
			}
			
			return rez;
		}
		
		// dragged text assign
		void AvalFieldsMouseDown(object sender, MouseEventArgs e)
		{
			md = false;
			movedStr = "";
			TreeNode tmp = ((TreeView)sender).GetNodeAt(new Point(e.X, e.Y));
			if ((tmp != null) && (tmp.Nodes.Count == 0))
			{
				movedStr = DefineNodeDragText(tmp);
//				if ((tmp.Parent != null) && (tmp.Parent.Text != AllFieldsText))
//				{
//					movedStr = tmp.Parent.Text + "." + tmp.Text;
//				} else
//				{
//					movedStr = tmp.Text;
//				}
				
				movedStr = movedStr.ToLower();
				movedItm = -1;
				((TreeView)sender).SelectedNode = tmp;
			}
			
		}
		
		void AvalFieldsMouseUp(object sender, MouseEventArgs e)
		{
			md = false;
		}
		
		void AvalFieldsMouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.Cursor = Cursors.PanEast;
				md = true;
			} else md = false;
		}
		
		#endregion
		
		// Delete 'group by' list item
		void GoupByCtrlKeyUp(object sender, KeyEventArgs e)
		{

			if((e.KeyData == Keys.Delete) && (((ListBox)sender).SelectedIndex != -1))
			{
				string ListKind = ((ListBox)sender).Tag.ToString();
				if (ListKind == "od")
				{
					ThisQuery.DeleteOrder(((ListBox)sender).Items[((ListBox)sender).SelectedIndex].ToString());
				}
				if (ListKind == "gr")
				{
					ThisQuery.DeleteGrouping(((ListBox)sender).Items[((ListBox)sender).SelectedIndex].ToString());
				}
				
				int sel = ((ListBox)sender).SelectedIndex;
				if (ListKind == "un")
				{
					if(this.unionsCtrl.SelectedIndex == sel && sel > 0) {this.unionsCtrl.SelectedIndex = sel-1;}
					RefToRootUnionArray[0].DeleteUnionedQuery(sel);
					if(sel > 0){
						((ListBox)sender).Items.RemoveAt(sel);}

					flag = true;
					UpdateUnions(RefToRootUnionArray[0].GetUnionedQId());
					flag = false;
				}
				
				if (ListKind == "fl")
				{
					ThisQuery.DeleteField(((ListBox)sender).Items[sel].ToString());
					((ListBox)sender).Items.RemoveAt(sel);
					
					UpdateFieldsList(ThisQuery.FldStrings());
					((ListBox)sender).SelectedIndex = sel -1;
				}
				
				TabControl1SelectedIndexChanged(null, null);
			}
		}
		
		#region Aggregate fields manipulations in Grouping tab
		
		void AggregateFldsCtrlMouseUp(object sender, MouseEventArgs e)
		{
			if ((md) && (!string.IsNullOrEmpty(movedStr)))
			{
				ThisQuery.MakeFieldAggregate(movedStr, "sum");
				UpdateAvalList(ThisQuery.GetGroupingAvailableItems());
				UpdateAgregateList(ThisQuery.GetAgregateFields());
			}
			md = false;
		}
		
		void AggregateFldsCtrlKeyUp(object sender, KeyEventArgs e)
		{
			if((e.KeyCode == Keys.Delete) && (this.AggregateFldsCtrl.SelectedIndex != -1))
			{
				ThisQuery.MakeFieldAggregate(this.AggregateFldsCtrl.Items[this.AggregateFldsCtrl.SelectedIndex].ToString(), "");
				TabControl1SelectedIndexChanged(null, null);
			}
			
		}
		
		void AggregateFldsCtrlMouseDoubleClick(object sender, MouseEventArgs e)
		{
			if(this.AggregateFldsCtrl.SelectedIndex != -1)
			{
				listshow = true;
				QueryForm.refreshOnActivate = false;
				AgrChooseForm frm = new AgrChooseForm();
				frm.Location = this.AggregateFldsCtrl.PointToScreen(new Point(e.X-4, e.Y-3));
				int sel = this.AggregateFldsCtrl.SelectedIndex;
				frm.ShowDialog();
				if (frm.ChoosedFunc != "")
				{
					ThisQuery.MakeFieldAggregate(this.AggregateFldsCtrl.Items[sel].ToString(), frm.ChoosedFunc);
					UpdateAgregateList(ThisQuery.GetAgregateFields());
					UpdateAvalList(ThisQuery.GetGroupingAvailableItems());
				}
			}
			
		}
		
		#endregion
		
		void AggregateFldsCtrlLeave(object sender, EventArgs e)
		{
//			((ListBox)sender).SelectedIndex = -1;
		}
		
		#region UNION table grid
		
		int GetListLineFromPoint(object obj, int x, int y)
		{
			int rez = 0;
			bool finded = false;
			while((rez < ((ListView)obj).Items.Count) && (!finded))
			{
				finded = ((ListView)obj).Items[rez].Bounds.Contains(x, y);
				rez++;
			}
			rez--;
			return rez;
		}
		
		void DrawHiligthRectangle(object obj, string text, Rectangle hiligthArea)
		{
			object ot = ((ListView)obj).Tag;
			int idx = Convert.ToInt32(ot);
			int newIdx = GetListLineFromPoint(obj, hiligthArea.X +3, hiligthArea.Y +3);
			if ((idx != newIdx) && (idx < ((ListView)obj).Items.Count))
			{
				((ListView)obj).RedrawItems(idx, idx, true);
			}else
			{
				((ListView)obj).Refresh();
			}
			((ListView)obj).Tag = (object)newIdx;
			
			Graphics g = ((ListView)obj).CreateGraphics();
			SolidBrush mBrush = new SolidBrush(Color.FromArgb(192, 255, 192));
			g.FillRectangle(mBrush, hiligthArea);
			Brush myBrush = Brushes.Black;
			
			StringFormat sf = new StringFormat();
			if(((ListView)obj).Name == "aliasListCtrl")
			{
				sf.Alignment = StringAlignment.Far;
				hiligthArea.Width -= 6;
			}
			g.DrawString(text, ((ListView)obj).Font, myBrush, hiligthArea, sf);
			//g.DrawString(text, ((ListView)obj).Font, myBrush, hiligthArea, StringFormat.GenericDefault);
			
			g.Dispose();
		}
		
		// get cell coordinates and run edit in union grid (3 clicks needed)
		void ListView1MouseUp(object sender, MouseEventArgs e)
		{
			if((this.unialiasTableCtrl.Items.Count == 0)) {return;}

			int i = 0;
			int col = 0;
			int wdh = 0;
			int wdhCurr = 0;
			clickCount++;
			md = false;
			
			#region define_ coordinates of chosen cell
			
			i = GetListLineFromPoint(this.unialiasTableCtrl, e.X, e.Y);
			
			wdh = this.unialiasTableCtrl.Items[0].Bounds.X; // scrolling offset
			
			// define current column width and  X client position
			while((col < this.unialiasTableCtrl.Columns.Count) && (wdh < e.X))
			{
				wdhCurr = this.unialiasTableCtrl.Columns[col].Width;
				wdh+= wdhCurr;
				col++;
			}
			col--;
			
			// init global var about current coordinates in grid
			if ((aliGridPoi.X != col) ||(aliGridPoi.Y != i))
			{
				aliGridPoi.X = col;
				aliGridPoi.Y = i;
				clickCount = 1;
			}
			
			#endregion

			if((col < this.unialiasTableCtrl.Columns.Count) && (wdh > e.X))
			{
				#region init  highlight and edit  Rectangle's
				Point pt = new Point(this.unialiasTableCtrl.Location.X + wdh - wdhCurr +1, this.unialiasTableCtrl.Location.Y + this.unialiasTableCtrl.Items[i].Bounds.Y +2 );
				Rectangle editarea = new Rectangle(pt.X , pt.Y , wdhCurr, this.unialiasTableCtrl.Items[i].Bounds.Height);
				
				pt = new Point( wdh - wdhCurr , this.unialiasTableCtrl.Items[i].Bounds.Y );
				Rectangle hiligthArea = new Rectangle(pt.X , pt.Y , wdhCurr, this.unialiasTableCtrl.Items[i].Bounds.Height);
				
				hiligthArea.Inflate(-1, 0);
				editarea.Inflate(0,1);
				#endregion
				
				string currText;
				#region Highlight chosen cell;  init currText
				if ((this.unialiasTableCtrl.Items[i].SubItems != null)&& (this.unialiasTableCtrl.Items[i].SubItems.Count >col))
				{
					currText = this.unialiasTableCtrl.Items[i].SubItems[col].Text;
					DrawHiligthRectangle(unialiasTableCtrl, currText, hiligthArea);
				} else {currText = "";}//
				#endregion
				
				#region run cell edit in 3 clicks
				if (clickCount > 2)
				{
					uniEditCtrl.Bounds = editarea;
					uniEditCtrl.Text = currText;
					uniEditCtrl.Tag = (object)aliGridPoi;
				} else
				{
					ListView1ColumnWidthChanging(null,null);
				}
				#endregion
				
				movedStr = currText;

			} else // move edit away
			{
				ListView1ColumnWidthChanging(null,null);
			}
			
		}
		
		#region scroll aliases list and hide edit when scroll grid
		
		void ListView1ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			uniEditCtrl.Location = new Point(2010, 15);
		}
		
		void ListView1ClientSizeChanged(object sender, EventArgs e)
		{
			ListView1ColumnWidthChanging(null,null);
		}
		
		void ListView1Scroll(object sender, ScrollEventArgs e)
		{
			if (this.unialiasTableCtrl.Items.Count <= 0){return;}
			ListView1ColumnWidthChanging(null,null);
			int h = this.unialiasTableCtrl.Items[0].Bounds.Height;
			
			int i = 0;
			i = GetListLineFromPoint(this.unialiasTableCtrl, 6, h+3);
			
			ScrollUAliasStripe(i);
		}
		
		// path hilighting union table
		void UnialiasTableCtrlMouseUp(object sender, MouseEventArgs e)
		{
			if(uniEditCtrl.Text != " ")
			{
				uniEditCtrl.Focus();
			}
		}
		
		#endregion scroll grid
		
		#region union table sorting and edit
		
		// drag 'n drop func in union table
		void UnialiasTableCtrlMouseMove(object sender, MouseEventArgs e)
		{
			int a = GetListLineFromPoint((MyListView)sender, e.X, e.Y); // - aliasScroll;
			int i = a - aliasScroll;
			if((i < 0) || (aliasListCtrl.Items.Count <= i)) {return;}
			
			ListViewItem itm = aliasListCtrl.Items[i];
			int idx = Convert.ToInt32(aliasListCtrl.Tag);
			
			if(i != idx)
			{
				if ((e.Button == MouseButtons.Left)&& !md)
				{
					FieldsCtrlMouseMove( sender, e);
					movedItm = idx;
					unialiasTableCtrl.lastMsg = -7;
				}
				clickCount = 0;
				DrawHiligthRectangle((object)aliasListCtrl, itm.Text, itm.Bounds);
			}
			
			if ((e.Button == MouseButtons.None)&& md)
			{
				// x - column y - field idx
				int uniNum = aliGridPoi.X;
				RefToRootUnionArray[uniNum].FieldReplace(movedItm + aliasScroll, a, aliasesUnionList);
				
				UpdateAliasUnionTable();
				md = false;
			}
//	       delay needed here
//			stackSaver = (stackSaver == 1)? 0 : 1;
//
//			if((md) &&(stackSaver == 1))
//			{
//				UnialiasTableCtrlMouseMove( sender,  e);
//			}
		}
		
		// update edited data in table or alias list
		void UniEditCtrlLocationChanged(object sender, EventArgs e)
		{
			if (md){return;}
			if((uniEditCtrl.Tag == null) ||(uniEditCtrl.Text == " ")) {return;}
			Point p = (Point)uniEditCtrl.Tag;
			// x - column y - field idx
			
			if(p.X == -7)
			{
				for(int i = 0; i < RefToRootUnionArray[0].unionsCount; i++)
				{
					if(RefToRootUnionArray[i].fieldsCount > p.Y)
					{
						RefToRootUnionArray[i].FieldChangeAlias(p.Y, uniEditCtrl.Text);
					}
				}

				aliasesUnionList[p.Y] = uniEditCtrl.Text;
				ScrollUAliasStripe(aliasScroll);
			} else
			{
				if ((this.unialiasTableCtrl.Items[p.Y].SubItems != null)&& (this.unialiasTableCtrl.Items[p.Y].SubItems.Count > p.X))
				{
					this.unialiasTableCtrl.Items[p.Y].SubItems[p.X].Text = uniEditCtrl.Text;
					RefToRootUnionArray[p.X].UpdateField(p.Y, uniEditCtrl.Text);
				}
			}
			uniEditCtrl.Text = " "; // set a space because empty edit is not focusing caret for input
		}
		
		// edit aliases in one click
		void AliasListCtrlMouseUp(object sender, MouseEventArgs e)
		{
			int i = GetListLineFromPoint(sender, e.X, e.Y);
			if(i < 0){return;}
			ListViewItem itm = aliasListCtrl.Items[i];
			
			Rectangle editArea = itm.Bounds;
			editArea.X +=3;
			editArea.Y +=2;
			
			uniEditCtrl.Bounds = editArea;
			uniEditCtrl.Text = itm.Text;
			uniEditCtrl.Focus();
			aliGridPoi.X = -7;
			aliGridPoi.Y = i + aliasScroll;
			uniEditCtrl.Tag = aliGridPoi;
		}
		
		
		// Alias List Refresh
		void AliasListCtrlMouseEnter(object sender, EventArgs e)
		{
			aliasListCtrl.Refresh();
		}
		
		void UniEditCtrlLeave(object sender, EventArgs e)
		{
			ListView1ColumnWidthChanging(null,null);
		}
		
		#endregion sorting and edit
		
		
		#endregion union table grid
		
		void FieldsCtrlMouseDoubleClick(object sender, MouseEventArgs e)
		{
//			MessageBox.Show("There can be edit box opening");movedStr
		}
		
		void AliasListCtrlColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			e.Cancel = true;
		}
		
		void OrderAvalCtrlMouseDoubleClick(object sender, MouseEventArgs e)
		{
			TreeNode tmp = this.orderAvalCtrl.GetNodeAt(new Point(e.X, e.Y));
			if ((tmp != null) && (tmp.Nodes.Count == 0))
			{
				ThisQuery.AddOrder(tmp.Text);
				this.orderAvalCtrl.Nodes.Remove(tmp);
				UpdateOrderByList(ThisQuery.orders);
			}
		}
		
		void GoupByCtrlMouseClick(object sender, MouseEventArgs e)
		{
			label1.BackColor = Color.PaleTurquoise;
			label6.BackColor = Color.Transparent;
			activeSide = 0;
		}
		
		void AggregateFldsCtrlMouseClick(object sender, MouseEventArgs e)
		{
			label1.BackColor = Color.Transparent;
			label6.BackColor = Color.PaleTurquoise;
			activeSide = 1;
		}
		
		void AvalFieldsMouseDoubleClick(object sender, MouseEventArgs e)
		{
			TreeNode tmp = this.AvalFields.GetNodeAt(new Point(e.X, e.Y));
			if ((tmp != null) && (tmp.Nodes.Count == 0))
			{
				if(activeSide == 0)
				{
					ThisQuery.AddGrouping(tmp.Text);
					UpdateGroupByList(ThisQuery.groups);
				} else
				{
					ThisQuery.MakeFieldAggregate(tmp.Text, "sum");
					UpdateAgregateList(ThisQuery.GetAgregateFields());
				}
				this.AvalFields.Nodes.Remove(tmp);
			}
		}
		
		void AggregateFldsCtrlDrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index == -1)
			{
				return;
			}
			e.DrawBackground();
			Brush myBrush = Brushes.Black;
			
			if(((e.State & DrawItemState.Focus) != DrawItemState.Focus) && ((e.State & DrawItemState.Selected) != DrawItemState.Selected ))
			{
				SolidBrush zebraBrush = new SolidBrush(((e.Index % 2) == 0) ? Color.Transparent : Color.MintCream);
				e.Graphics.FillRectangle(zebraBrush, e.Bounds);
			} else
			{
//				SolidBrush zebraBrush = new SolidBrush(Color.PaleGreen);
				SolidBrush zebraBrush = new SolidBrush(Color.FromArgb(192, 255, 192));
				e.Graphics.FillRectangle(zebraBrush, e.Bounds);
			}
			
			e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, myBrush,e.Bounds,StringFormat.GenericDefault);

		}
		
		#region union list manage
		
		// ADD new union object
		void AddUnionCtrlClick(object sender, EventArgs e)
		{
			isUnioned = true;
			RefToRootUnionArray[0].AddUnionQuery();
			
			UpdateUnions(RefToRootUnionArray[0].GetUnionedQId());
			RefToRootUnionArray[0].SelfCheck();
			TabControl1SelectedIndexChanged(null, null);
		}
		// goto selected union
		void UnionListCtrlMouseDoubleClick(object sender, MouseEventArgs e)
		{
			unionsCtrl.SelectedIndex = ((ListBox)sender).SelectedIndex;
			tabControl1.SelectedIndex = 0;
		}
		
		void DeleteUnionCtrlClick(object sender, EventArgs e)
		{
			GoupByCtrlKeyUp((object) unionListCtrl, new KeyEventArgs(Keys.Delete));
		}
		
		#endregion
		
		#region Templates panel movements
		
		void Button10Click(object sender, EventArgs e)
		{
			TemplatesPanel.Visible = false;
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			int iCou = 0;
			int[] idxArr = new int[checkTemplatesCtrl.CheckedItems.Count];
			foreach(object itemChecked in checkTemplatesCtrl.CheckedItems)
			{
				idxArr[iCou] = checkTemplatesCtrl.Items.IndexOf(itemChecked);
				iCou++;
			}
			
			foreach(int idx in idxArr)
			{
				string itemtext = checkTemplatesCtrl.Items[idx].ToString();
				checkTemplatesCtrl.Items[idx] = "-> " +itemtext;
				checkTemplatesCtrl.Update();
				ThisQuery.AddExisingVirtTable(tmpman.TemplNames[idx], tmpman.LoadTemplate(idx));
				checkTemplatesCtrl.Items[idx] = "* " +itemtext;
			}
			
			TemplatesPanel.Visible = false;
			TabControl1SelectedIndexChanged(null, null);
		}
		
		void AddTemplateCtrlClick(object sender, EventArgs e)
		{
			toolTip1.Active = true;
			checkTemplatesCtrl.Items.Clear();
			checkTemplatesCtrl.Items.AddRange(tmpman.TemplNames);
			string[] tmp = tmpman.TemplNames;
			string maxLen = "";
			foreach (string s in tmp)
			{
				if (maxLen.Length < s.Length)
				{
					maxLen = s;
				}
			}
			TemplatesPanel.Width = maxLen.Length *11;
			if (TemplatesPanel.Width < 188) {TemplatesPanel.Width = 188;}
			TemplatesPanel.Height = tmp.Length * 16+ 88;
			if(TemplatesPanel.Height > tablesCtrl.Height) {TemplatesPanel.Height = tablesCtrl.Height - 3;}
			TemplatesPanel.Visible = true;
			checkTemplatesCtrl.Focus();
		}
		
		void SaveVirtToTemplCtrlClick(object sender, EventArgs e)
		{
			SaveVirtTableTempl(this.tablesCtrl.SelectedNode);
		}
		
		void SaveVirtTableTempl(TreeNode tmp)
		{
			if (tmp == null) {return;}
			if((tmp.Tag != null)&&(tmp.Tag.ToString() == "VirtualTableDa"))
			{
				int idx = ThisQuery.HaveSuchTable(tmp.Text, true);
				if (idx != -1)
				{
					string Info = "";
					if(InputBox("New template ["+ tmp.Text +"]", "Enter some description (or not)", ref Info) == DialogResult.OK)
					{
						table[] t = ThisQuery.GetTables();
						string textToSave = Query.QueryObjects[ t[idx].queryId ].GetSQLText(false);
						if(Info.TrimStart().Length > 0){
							Info = Info.TrimStart().ToUpper()[0] + Info.TrimStart().Substring(1);}
						tmpman.SaveTemplate(tmp.Text, textToSave, "    DB scheme: "+ QueryTemplatesManager.Extractfilename(QueryTemplatesManager.lastOpenedDB) + "\n --" +Info);
					}
				}
			}
		}
		
		void PictureBox1MouseClick(object sender, MouseEventArgs e)
		{
			tmpman.ScanAndLoadTemplates();
			AddTemplateCtrlClick(null, null);
		}
		
		void CheckTemplatesCtrlPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
			{
				TemplatesPanel.Visible = false;
			}
		}
		
		void CheckTemplatesCtrlSelectedValueChanged(object sender, EventArgs e)
		{
			int sel = checkTemplatesCtrl.SelectedIndex;
			toolTip1.Hide(checkTemplatesCtrl);
			if(sel >= 0)
			{
				if(tmpman.GetInfo(sel) != "")
				{
					Point mpos = Control.MousePosition;
					mpos = checkTemplatesCtrl.PointToClient(mpos);
					mpos.X += checkTemplatesCtrl.Width / 2;
					toolTip1.ToolTipTitle = tmpman.TemplNames[sel];
					toolTip1.Show(tmpman.GetInfo(sel), checkTemplatesCtrl, mpos, 15000);
				}
			}
		}
		
		void TemplatesPanelVisibleChanged(object sender, EventArgs e)
		{
			toolTip1.Hide(checkTemplatesCtrl);
		}
		
		void DeleteTemplCtrlClick(object sender, EventArgs e)
		{
			int sel = checkTemplatesCtrl.SelectedIndex;
			toolTip1.Hide(checkTemplatesCtrl);
			if(sel >= 0)
			{
				tmpman.DeleteTemplate(sel);
				AddTemplateCtrlClick(null, null);
			}
		}
		
		void CheckTemplatesCtrlMouseLeave(object sender, EventArgs e)
		{
			toolTip1.Hide(checkTemplatesCtrl);
			toolTip1.Active = false;
			
		}
		
		void CheckTemplatesCtrlMouseEnter(object sender, EventArgs e)
		{
			toolTip1.Active = true;
		}
		
		#endregion
		
		#region Query Tree panel
		
		void ShowTreeCtrlClick(object sender, EventArgs e)
		{
			int startIdx = -1;
			int host = -1;
			string[] nameTree = new string[300];
			int[] idTree  = new int[300];
			int[] hostTree = new int[300];
			if(isRoot)
			{
				RefToRootUnionArray[0].Save();
			}
			
			this.tabControl1.SelectedIndex = 0;
			OpenList[0].RefToRootUnionArray[0].TraceTree(ref  startIdx,  host,  "Root",  ref  nameTree, ref  idTree, ref hostTree);
			
			TreeNode[] nodeTree = new TreeNode[300];
			
//			richTextBox2.Lines = nameTree;
			this.queryTreeViewCtrl.Nodes.Clear();
			
			for(int i = 0; i <= startIdx; i++)
			{
				TreeNode hostNode = new TreeNode(nameTree[i]);
				hostNode.Tag = idTree[i];
				nodeTree[i] = hostNode;
				
				if(i>0)
				{
					nodeTree[hostTree[i]].Nodes.Add(hostNode);
				}
			}
			
			this.queryTreeViewCtrl.Nodes.Add(nodeTree[0]);
			
			queryTreeCtrl.Visible = true;
			this.queryTreeViewCtrl.Focus();
			
			int thisIdx = Array.IndexOf(idTree, ThisQuery.ObjIdx);
			if(thisIdx >=0)
			{
				this.queryTreeViewCtrl.SelectedNode = nodeTree[thisIdx];
				nodeTree[thisIdx].ImageIndex = 6;
				nodeTree[thisIdx].SelectedImageIndex = 6;
				nodeTree[thisIdx].Expand();
			}
			
		}
		
		void QueryTreeViewCtrlLeave(object sender, EventArgs e)
		{
			queryTreeCtrl.Visible = false;
		}
		
		void OpenForm(TreeNode sel)
		{
//			TreeNode sel = this.queryTreeViewCtrl.SelectedNode;
			if	((sel != null) )
			{
				int qid = (int)sel.Tag;
				
				Query QU = new Query();
				Query.QueryObjects[ qid ].CopyTO(QU);
				
				string formPath = QueryParser.LeftToRightPath(sel.FullPath);//tmp.Text + ((RootUnionObject.IsUnioned ) ? " /Query "+ (parallelNum +1).ToString() : "") + " |"+ this.Text;

				int openID = IsAlreadyOpened(formPath);
				if(openID >= 0)
				{
					OpenList[openID].BringToFront();
					return;
				} else
				{
					QueryForm frm = new QueryForm(QU, formPath);
					frm.Show();
				}
			}
		}
		
		void QueryTreeViewCtrlMouseDoubleClick(object sender, MouseEventArgs e)
		{
			TreeNode tmp = this.queryTreeViewCtrl.GetNodeAt(new Point(e.X, e.Y));
//				OpenVirtTableForm( tmp);
			OpenForm(tmp);
		}
		
		#endregion
		
		
		#region personal Rename/Remove buttons in tables tree
		
		void RemoveTablCtrlClick(object sender, EventArgs e)
		{
			TablesCtrlKeyUp(null, new KeyEventArgs(Keys.Delete));
			TableRenameDelPanel.Visible = false;
		}
		
		void RenameTablCtrlClick(object sender, EventArgs e)
		{
			doTablesNodeEdit = true;
			this.tablesCtrl.SelectedNode.BeginEdit();
		}
		
		void TablesCtrlLeave(object sender, EventArgs e)
		{
			TableRenameDelPanel.Visible = TableRenameDelPanel.ContainsFocus;
		}
		
		void RemoveTablCtrlMouseHover(object sender, EventArgs e)
		{
			int imId = ((Button)sender).ImageIndex;
			int newImg = imId;
			if(imId == 1) {newImg = 0;}
			if(imId == 3) {newImg = 2;}
			
			((Button)sender).ImageIndex = newImg;
		}
		
		void RemoveTablCtrlMouseLeave(object sender, EventArgs e)
		{
			int imId = ((Button)sender).ImageIndex;
			int newImg = imId;
			if(imId == 0) {newImg = 1;}
			if(imId == 2) {newImg = 3;}
			
			((Button)sender).ImageIndex = newImg;
		}
		
		#endregion
		
		#region modifiers click
		
		void OrderByCtrlMouseClick(object sender, MouseEventArgs e)
		{
			if(e.X > ((ListBox)sender).Bounds.Right - 40)
			{
				int sel = ((ListBox)sender).IndexFromPoint(e.X, e.Y);
				if (sel >=0)
				{
					((ListBox)sender).Items[sel] = ThisQuery.SwitchOrderSorting(sel);
				}
			}
		}
		
		void UnionListCtrlMouseClick(object sender, MouseEventArgs e)
		{
			if((e.X > ((ListBox)sender).Bounds.Right - 30) && isUnioned)
			{
				int sel = ((ListBox)sender).IndexFromPoint(e.X, e.Y);
				if (sel >=0)
				{
					((ListBox)sender).Items[sel] =	QueryParser.TruncOrderMdf(((ListBox)sender).Items[sel].ToString()) + RefToRootUnionArray[sel].ChangeUnionModifier();
				}
			}
		}
		
		#endregion
		
		#region Condition manipulations
		
		void AddEmptyTypedCondition( ConditionType ct)
		{
			condition c = new condition();
			c.text = "";
			c.type = ct;
			c.operand1 = movedStr.Trim();
			c.cOperator = "=";
			conditionListCtrl.AddItem(c).SetFocusInList();
		}
		
		void AddConditionCtrlClick(object sender, EventArgs e)
		{
			AddEmptyTypedCondition(ConditionType.equality);
		}
		
		void AddTextConditionCtrlClick(object sender, EventArgs e)
		{
			AddEmptyTypedCondition(ConditionType.urest);
		}
		
		void DeleteConditionCtrlClick(object sender, EventArgs e)
		{
			conditionListCtrl.DeleteItem(conditionListCtrl.selectedItem);
		}
		// tranfer modifications to query object
		void TabPage4Leave(object sender, EventArgs e)
		{
//			ThisQuery.whereZ = conditionListCtrl.conditionz;
		}
		
		#endregion
		
		void TablesListMouseEnter(object sender, EventArgs e)
		{
			movedStr = "";
		}
		
		void ConditionListCtrlMouseUp(object sender, MouseEventArgs e)
		{
			if((md) && (movedStr != ""))
			{
				AddConditionCtrlClick(null , null);
				movedStr = "";
				md = false;
			}
		}
		
		void TablesListMouseClick(object sender, MouseEventArgs e)
		{
			if(e.X < 20 ) {return;}
			
			TreeNode tn = ((TreeView)sender).GetNodeAt(new Point(e.X, e.Y));
			if(tn != null)
			{
				if(tn.IsExpanded)
				{
					tn.Collapse();
				} else
					tn.Expand();
			}
		}
		
		void TablesListMouseDoubleClick(object sender, MouseEventArgs e)
		{
			string pasteStr;
			TreeNode tmp = ((TreeView)sender).GetNodeAt(new Point(e.X, e.Y));
			
			pasteStr = DefineNodeDragText(tmp);
			
			if(pasteStr != "")
			{
				conditionListCtrl.SetEqSide(pasteStr, (e.Button == MouseButtons.Right));
			}
		}
		
		#region	Join tables manipulations
		
		void ColapseAllCtrlClick(object sender, EventArgs e)
		{
			JoinsCtrl.ExpandCollapseAll(true);
		}
		
		void ExpandAllJoinsCtrlClick(object sender, EventArgs e)
		{
			JoinsCtrl.ExpandCollapseAll(false);
		}
		
		// Add Join
		void TablesToJoinCtrlMouseDoubleClick(object sender, MouseEventArgs e)
		{
			TreeNode tn = ((TreeView)sender).GetNodeAt(new Point(e.X, e.Y));
			if(tn != null)
			{
				JoinsCtrl.JoinTable(tn.Text.Trim());
				((TreeView)sender).Nodes.Remove(tn);
			}
		}
		
		void AddJoinCtrlClick(object sender, EventArgs e)
		{
			JoinsCtrl.JoinTable("test____");
		}
		// UP
		void UpJoinCtrlClick(object sender, EventArgs e)
		{
			int idx = (JoinsCtrl.selectedItem == null) ? -1 : JoinsCtrl.selectedItem.ListIdx;
			if(idx > 0)
			{
				JoinsCtrl.MoveItem(idx, idx -1);
			}
		}
		// Down
		void MoveJoinDownCtrlClick(object sender, EventArgs e)
		{
			int idx = (JoinsCtrl.selectedItem == null) ? -1 : JoinsCtrl.selectedItem.ListIdx;
			if(idx >= 0)
			{
				JoinsCtrl.MoveItem(idx, idx +1);
			}
		}
		// Save
		void TabPage2Leave(object sender, EventArgs e)
		{
//			JoinsCtrl.Save();
		}
		
		void DeleteJoinCtrlClick(object sender, EventArgs e)
		{
			JoinsCtrl.DeleteItem();
			UpdateJoinAvalTables();
		}
		
		#endregion
		
		
		void TablesListMouseLeave(object sender, EventArgs e)
		{
			if(!md)
			{
				movedStr = "";
			}
		}
					
		void ExtractDbCtrlClick(object sender, EventArgs e)
		{
			GetDBFromQuery();
		}
	}


	class MyListView : ListView {
		
		public int lastMsg;

		public event ScrollEventHandler Scroll;
		protected virtual void OnScroll(ScrollEventArgs e) {
			ScrollEventHandler handler = this.Scroll;
			if (handler != null) handler(this, e);
		}
		protected override void WndProc(ref Message m)
		{
			lastMsg = m.Msg;
			base.WndProc(ref m);
			if ((m.Msg == 0x115) || (m.Msg == 0x114)  || (m.Msg == 522)) { // Trap WM_VSCROLL WM_HSCROLL
				OnScroll(new ScrollEventArgs((ScrollEventType)(m.WParam.ToInt32() & 0xffff), 0));
			}
			
//			if (((m.Msg == 4114)||(m.Msg == 4115)||(m.Msg == 4113)) && (lastMsg !=0) )
//			if (((m.Msg == 4114)||(m.Msg == 4115)) && (lastMsg ==-7) )
//			{
//				if (Invokesome != null) Invokesome();
//			}
		}
		
	}

}
