/*
 * Created by SharpDevelop.
 * User: igor_only
 * Date: 24.04.2010
 * Time: 12:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Glass;
 
namespace sql_constructor
{
	partial class QueryForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryForm));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.extractDbCtrl = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.ExpandAll = new System.Windows.Forms.Button();
			this.CollapseAll = new System.Windows.Forms.Button();
			this.moveTables = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.dbCtrl = new System.Windows.Forms.TreeView();
			this.dbimg = new System.Windows.Forms.ImageList(this.components);
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.TableRenameDelPanel = new System.Windows.Forms.Panel();
			this.removeTablCtrl = new System.Windows.Forms.Button();
			this.buttons = new System.Windows.Forms.ImageList(this.components);
			this.renameTablCtrl = new System.Windows.Forms.Button();
			this.SaveVirtToTemplCtrl = new System.Windows.Forms.Button();
			this.AddTemplateCtrl = new System.Windows.Forms.Button();
			this.TemplatesPanel = new System.Windows.Forms.Panel();
			this.deleteTemplCtrl = new System.Windows.Forms.PictureBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label12 = new System.Windows.Forms.Label();
			this.button10 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.checkTemplatesCtrl = new System.Windows.Forms.CheckedListBox();
			this.addVirtCtrl = new System.Windows.Forms.Button();
			this.VirtOpen = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.button8 = new System.Windows.Forms.Button();
			this.button9 = new System.Windows.Forms.Button();
			this.moveFields = new System.Windows.Forms.Button();
			this.tablesCtrl = new System.Windows.Forms.TreeView();
			this.queryTreeCtrl = new System.Windows.Forms.Panel();
			this.label13 = new System.Windows.Forms.Label();
			this.queryTreeViewCtrl = new System.Windows.Forms.TreeView();
			this.uParamCtrl = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.fieldsCtrl = new System.Windows.Forms.ListBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.splitContainer8 = new System.Windows.Forms.SplitContainer();
			this.label9 = new System.Windows.Forms.Label();
			this.tablesToJoinCtrl = new System.Windows.Forms.TreeView();
			this.expandAllJoinsCtrl = new System.Windows.Forms.Button();
			this.deleteJoinCtrl = new System.Windows.Forms.Button();
			this.JoinsCtrl = new sql_constructor.QueryJoinsCtrl();
			this.colapseAllCtrl = new System.Windows.Forms.Button();
			this.moveJoinDownCtrl = new System.Windows.Forms.Button();
			this.upJoinCtrl = new System.Windows.Forms.Button();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.label4 = new System.Windows.Forms.Label();
			this.AvalFields = new System.Windows.Forms.TreeView();
			this.splitContainer4 = new System.Windows.Forms.SplitContainer();
			this.label1 = new System.Windows.Forms.Label();
			this.GoupByCtrl = new System.Windows.Forms.ListBox();
			this.label6 = new System.Windows.Forms.Label();
			this.AggregateFldsCtrl = new System.Windows.Forms.ListBox();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitContainer7 = new System.Windows.Forms.SplitContainer();
			this.label14 = new System.Windows.Forms.Label();
			this.tablesList = new System.Windows.Forms.TreeView();
			this.addTextConditionCtrl = new System.Windows.Forms.Button();
			this.deleteConditionCtrl = new System.Windows.Forms.Button();
			this.addConditionCtrl = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.conditionListCtrl = new sql_constructor.ConditionList();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.splitContainer6 = new System.Windows.Forms.SplitContainer();
			this.deleteUnionCtrl = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.addUnionCtrl = new System.Windows.Forms.Button();
			this.unionListCtrl = new System.Windows.Forms.ListBox();
			this.uniEditCtrl = new System.Windows.Forms.ComboBox();
			this.unialiasTableCtrl = new sql_constructor.MyListView();
			this.aliasListCtrl = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.tabPage6 = new System.Windows.Forms.TabPage();
			this.splitContainer5 = new System.Windows.Forms.SplitContainer();
			this.label7 = new System.Windows.Forms.Label();
			this.orderAvalCtrl = new System.Windows.Forms.TreeView();
			this.label8 = new System.Windows.Forms.Label();
			this.orderByCtrl = new System.Windows.Forms.ListBox();
			this.button3 = new Glass.GlassButton();
			this.button2 = new Glass.GlassButton();
			this.button5 = new Glass.GlassButton();
			this.unionsCtrl = new System.Windows.Forms.TabControl();
			this.cancelCtrl = new Glass.GlassButton();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.showTreeCtrl = new System.Windows.Forms.Button();
			this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.TableRenameDelPanel.SuspendLayout();
			this.TemplatesPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.deleteTemplCtrl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.queryTreeCtrl.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.splitContainer8.Panel1.SuspendLayout();
			this.splitContainer8.Panel2.SuspendLayout();
			this.splitContainer8.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.splitContainer4.Panel1.SuspendLayout();
			this.splitContainer4.Panel2.SuspendLayout();
			this.splitContainer4.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.panel1.SuspendLayout();
			this.splitContainer7.Panel1.SuspendLayout();
			this.splitContainer7.Panel2.SuspendLayout();
			this.splitContainer7.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.splitContainer6.Panel1.SuspendLayout();
			this.splitContainer6.Panel2.SuspendLayout();
			this.splitContainer6.SuspendLayout();
			this.tabPage6.SuspendLayout();
			this.splitContainer5.Panel1.SuspendLayout();
			this.splitContainer5.Panel2.SuspendLayout();
			this.splitContainer5.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage6);
			this.tabControl1.Location = new System.Drawing.Point(1, 5);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(849, 533);
			this.tabControl1.TabIndex = 0;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.splitContainer1);
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
			this.tabPage1.Size = new System.Drawing.Size(841, 505);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Tables and fields";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(4, 4);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.splitContainer1.Panel1.Controls.Add(this.extractDbCtrl);
			this.splitContainer1.Panel1.Controls.Add(this.label5);
			this.splitContainer1.Panel1.Controls.Add(this.ExpandAll);
			this.splitContainer1.Panel1.Controls.Add(this.CollapseAll);
			this.splitContainer1.Panel1.Controls.Add(this.moveTables);
			this.splitContainer1.Panel1.Controls.Add(this.button7);
			this.splitContainer1.Panel1.Controls.Add(this.button6);
			this.splitContainer1.Panel1.Controls.Add(this.button4);
			this.splitContainer1.Panel1.Controls.Add(this.dbCtrl);
			this.splitContainer1.Panel1MinSize = 240;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(833, 497);
			this.splitContainer1.SplitterDistance = 285;
			this.splitContainer1.SplitterWidth = 3;
			this.splitContainer1.TabIndex = 2;
			// 
			// extractDbCtrl
			// 
			this.extractDbCtrl.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
			this.extractDbCtrl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
			this.extractDbCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.extractDbCtrl.ForeColor = System.Drawing.SystemColors.Control;
			this.extractDbCtrl.Image = ((System.Drawing.Image)(resources.GetObject("extractDbCtrl.Image")));
			this.extractDbCtrl.Location = new System.Drawing.Point(230, 4);
			this.extractDbCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.extractDbCtrl.Name = "extractDbCtrl";
			this.extractDbCtrl.Size = new System.Drawing.Size(39, 26);
			this.extractDbCtrl.TabIndex = 8;
			this.toolTip2.SetToolTip(this.extractDbCtrl, "Extract (extend current) from text");
			this.extractDbCtrl.UseVisualStyleBackColor = true;
			this.extractDbCtrl.Click += new System.EventHandler(this.ExtractDbCtrlClick);
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.label5.Location = new System.Drawing.Point(4, 7);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(86, 23);
			this.label5.TabIndex = 7;
			this.label5.Text = "DB structure";
			// 
			// ExpandAll
			// 
			this.ExpandAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ExpandAll.BackColor = System.Drawing.Color.Transparent;
			this.ExpandAll.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
			this.ExpandAll.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.ExpandAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ExpandAll.Font = new System.Drawing.Font("Lucida Console", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ExpandAll.ForeColor = System.Drawing.SystemColors.ScrollBar;
			this.ExpandAll.Location = new System.Drawing.Point(253, 36);
			this.ExpandAll.Margin = new System.Windows.Forms.Padding(4);
			this.ExpandAll.Name = "ExpandAll";
			this.ExpandAll.Size = new System.Drawing.Size(29, 38);
			this.ExpandAll.TabIndex = 6;
			this.ExpandAll.Text = "_-";
			this.ExpandAll.UseVisualStyleBackColor = false;
			this.ExpandAll.Click += new System.EventHandler(this.ExpandAllClick);
			// 
			// CollapseAll
			// 
			this.CollapseAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CollapseAll.BackColor = System.Drawing.Color.Transparent;
			this.CollapseAll.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
			this.CollapseAll.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.CollapseAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CollapseAll.Font = new System.Drawing.Font("Lucida Console", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.CollapseAll.ForeColor = System.Drawing.SystemColors.ScrollBar;
			this.CollapseAll.Location = new System.Drawing.Point(253, 81);
			this.CollapseAll.Margin = new System.Windows.Forms.Padding(4);
			this.CollapseAll.Name = "CollapseAll";
			this.CollapseAll.Size = new System.Drawing.Size(29, 38);
			this.CollapseAll.TabIndex = 5;
			this.CollapseAll.Text = "_+";
			this.CollapseAll.UseVisualStyleBackColor = false;
			this.CollapseAll.Click += new System.EventHandler(this.CollapseAllClick);
			// 
			// moveTables
			// 
			this.moveTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.moveTables.BackColor = System.Drawing.Color.Transparent;
			this.moveTables.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.moveTables.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.moveTables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.moveTables.ForeColor = System.Drawing.SystemColors.ScrollBar;
			this.moveTables.Location = new System.Drawing.Point(253, 154);
			this.moveTables.Margin = new System.Windows.Forms.Padding(4);
			this.moveTables.MinimumSize = new System.Drawing.Size(0, 70);
			this.moveTables.Name = "moveTables";
			this.moveTables.Size = new System.Drawing.Size(29, 137);
			this.moveTables.TabIndex = 4;
			this.moveTables.Text = ">       >       >";
			this.moveTables.UseVisualStyleBackColor = false;
			this.moveTables.Click += new System.EventHandler(this.MoveTablesClick);
			// 
			// button7
			// 
			this.button7.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
			this.button7.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
			this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button7.ForeColor = System.Drawing.SystemColors.Control;
			this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
			this.button7.Location = new System.Drawing.Point(183, 4);
			this.button7.Margin = new System.Windows.Forms.Padding(4);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(39, 26);
			this.button7.TabIndex = 3;
			this.toolTip2.SetToolTip(this.button7, "Save to file");
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.Button7Click);
			// 
			// button6
			// 
			this.button6.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
			this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
			this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button6.ForeColor = System.Drawing.SystemColors.Control;
			this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
			this.button6.Location = new System.Drawing.Point(140, 4);
			this.button6.Margin = new System.Windows.Forms.Padding(4);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(39, 26);
			this.button6.TabIndex = 2;
			this.toolTip2.SetToolTip(this.button6, "Open saved DB struct. from file");
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.Button6Click);
			// 
			// button4
			// 
			this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
			this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button4.ForeColor = System.Drawing.SystemColors.Control;
			this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
			this.button4.Location = new System.Drawing.Point(97, 4);
			this.button4.Margin = new System.Windows.Forms.Padding(4);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(39, 26);
			this.button4.TabIndex = 1;
			this.toolTip2.SetToolTip(this.button4, "Scan DB structure via OLE connection");
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// dbCtrl
			// 
			this.dbCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.dbCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dbCtrl.HideSelection = false;
			this.dbCtrl.ImageIndex = 0;
			this.dbCtrl.ImageList = this.dbimg;
			this.dbCtrl.Location = new System.Drawing.Point(2, 35);
			this.dbCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.dbCtrl.Name = "dbCtrl";
			this.dbCtrl.SelectedImageIndex = 0;
			this.dbCtrl.ShowNodeToolTips = true;
			this.dbCtrl.Size = new System.Drawing.Size(247, 459);
			this.dbCtrl.TabIndex = 0;
			this.dbCtrl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DbCtrlMouseDoubleClick);
			this.dbCtrl.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.DbCtrlAfterCollapse);
			this.dbCtrl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DbCtrlMouseClick);
			this.dbCtrl.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.DbCtrlBeforeExpand);
			// 
			// dbimg
			// 
			this.dbimg.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("dbimg.ImageStream")));
			this.dbimg.TransparentColor = System.Drawing.Color.Transparent;
			this.dbimg.Images.SetKeyName(0, "table2.bmp");
			this.dbimg.Images.SetKeyName(1, "fld.bmp");
			this.dbimg.Images.SetKeyName(2, "fld.bmp");
			this.dbimg.Images.SetKeyName(3, "fld.bmp");
			this.dbimg.Images.SetKeyName(4, "fld.bmp");
			this.dbimg.Images.SetKeyName(5, "tableVirt.bmp");
			this.dbimg.Images.SetKeyName(6, "tablehilight.bmp");
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.TableRenameDelPanel);
			this.splitContainer2.Panel1.Controls.Add(this.SaveVirtToTemplCtrl);
			this.splitContainer2.Panel1.Controls.Add(this.AddTemplateCtrl);
			this.splitContainer2.Panel1.Controls.Add(this.TemplatesPanel);
			this.splitContainer2.Panel1.Controls.Add(this.addVirtCtrl);
			this.splitContainer2.Panel1.Controls.Add(this.VirtOpen);
			this.splitContainer2.Panel1.Controls.Add(this.label2);
			this.splitContainer2.Panel1.Controls.Add(this.button8);
			this.splitContainer2.Panel1.Controls.Add(this.button9);
			this.splitContainer2.Panel1.Controls.Add(this.moveFields);
			this.splitContainer2.Panel1.Controls.Add(this.tablesCtrl);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.queryTreeCtrl);
			this.splitContainer2.Panel2.Controls.Add(this.uParamCtrl);
			this.splitContainer2.Panel2.Controls.Add(this.label3);
			this.splitContainer2.Panel2.Controls.Add(this.fieldsCtrl);
			this.splitContainer2.Size = new System.Drawing.Size(545, 497);
			this.splitContainer2.SplitterDistance = 274;
			this.splitContainer2.SplitterWidth = 3;
			this.splitContainer2.TabIndex = 0;
			// 
			// TableRenameDelPanel
			// 
			this.TableRenameDelPanel.Controls.Add(this.removeTablCtrl);
			this.TableRenameDelPanel.Controls.Add(this.renameTablCtrl);
			this.TableRenameDelPanel.Location = new System.Drawing.Point(177, 208);
			this.TableRenameDelPanel.Name = "TableRenameDelPanel";
			this.TableRenameDelPanel.Size = new System.Drawing.Size(45, 18);
			this.TableRenameDelPanel.TabIndex = 15;
			// 
			// removeTablCtrl
			// 
			this.removeTablCtrl.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.removeTablCtrl.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.removeTablCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.removeTablCtrl.ForeColor = System.Drawing.Color.Transparent;
			this.removeTablCtrl.ImageIndex = 1;
			this.removeTablCtrl.ImageList = this.buttons;
			this.removeTablCtrl.Location = new System.Drawing.Point(21, 0);
			this.removeTablCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.removeTablCtrl.Name = "removeTablCtrl";
			this.removeTablCtrl.Size = new System.Drawing.Size(23, 18);
			this.removeTablCtrl.TabIndex = 17;
			this.removeTablCtrl.UseVisualStyleBackColor = true;
			this.removeTablCtrl.MouseLeave += new System.EventHandler(this.RemoveTablCtrlMouseLeave);
			this.removeTablCtrl.Click += new System.EventHandler(this.RemoveTablCtrlClick);
			this.removeTablCtrl.MouseHover += new System.EventHandler(this.RemoveTablCtrlMouseHover);
			// 
			// buttons
			// 
			this.buttons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("buttons.ImageStream")));
			this.buttons.TransparentColor = System.Drawing.Color.Transparent;
			this.buttons.Images.SetKeyName(0, "delete_X.bmp");
			this.buttons.Images.SetKeyName(1, "delete_X_gray.bmp");
			this.buttons.Images.SetKeyName(2, "rename.bmp");
			this.buttons.Images.SetKeyName(3, "rename_gray.bmp");
			// 
			// renameTablCtrl
			// 
			this.renameTablCtrl.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.renameTablCtrl.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.renameTablCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.renameTablCtrl.ForeColor = System.Drawing.Color.Transparent;
			this.renameTablCtrl.ImageIndex = 3;
			this.renameTablCtrl.ImageList = this.buttons;
			this.renameTablCtrl.Location = new System.Drawing.Point(0, 0);
			this.renameTablCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.renameTablCtrl.Name = "renameTablCtrl";
			this.renameTablCtrl.Size = new System.Drawing.Size(22, 18);
			this.renameTablCtrl.TabIndex = 16;
			this.renameTablCtrl.UseVisualStyleBackColor = true;
			this.renameTablCtrl.MouseLeave += new System.EventHandler(this.RemoveTablCtrlMouseLeave);
			this.renameTablCtrl.Click += new System.EventHandler(this.RenameTablCtrlClick);
			this.renameTablCtrl.MouseHover += new System.EventHandler(this.RemoveTablCtrlMouseHover);
			// 
			// SaveVirtToTemplCtrl
			// 
			this.SaveVirtToTemplCtrl.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
			this.SaveVirtToTemplCtrl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
			this.SaveVirtToTemplCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SaveVirtToTemplCtrl.ForeColor = System.Drawing.SystemColors.Control;
			this.SaveVirtToTemplCtrl.Image = ((System.Drawing.Image)(resources.GetObject("SaveVirtToTemplCtrl.Image")));
			this.SaveVirtToTemplCtrl.Location = new System.Drawing.Point(193, 4);
			this.SaveVirtToTemplCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.SaveVirtToTemplCtrl.Name = "SaveVirtToTemplCtrl";
			this.SaveVirtToTemplCtrl.Size = new System.Drawing.Size(39, 26);
			this.SaveVirtToTemplCtrl.TabIndex = 14;
			this.toolTip2.SetToolTip(this.SaveVirtToTemplCtrl, "Save subquery to Templates");
			this.SaveVirtToTemplCtrl.UseVisualStyleBackColor = true;
			this.SaveVirtToTemplCtrl.Click += new System.EventHandler(this.SaveVirtToTemplCtrlClick);
			// 
			// AddTemplateCtrl
			// 
			this.AddTemplateCtrl.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
			this.AddTemplateCtrl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
			this.AddTemplateCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.AddTemplateCtrl.ForeColor = System.Drawing.SystemColors.Control;
			this.AddTemplateCtrl.Image = ((System.Drawing.Image)(resources.GetObject("AddTemplateCtrl.Image")));
			this.AddTemplateCtrl.Location = new System.Drawing.Point(151, 4);
			this.AddTemplateCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.AddTemplateCtrl.Name = "AddTemplateCtrl";
			this.AddTemplateCtrl.Size = new System.Drawing.Size(39, 26);
			this.AddTemplateCtrl.TabIndex = 13;
			this.toolTip2.SetToolTip(this.AddTemplateCtrl, "Add subquery from templates");
			this.AddTemplateCtrl.UseVisualStyleBackColor = false;
			this.AddTemplateCtrl.Click += new System.EventHandler(this.AddTemplateCtrlClick);
			// 
			// TemplatesPanel
			// 
			this.TemplatesPanel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.TemplatesPanel.Controls.Add(this.deleteTemplCtrl);
			this.TemplatesPanel.Controls.Add(this.pictureBox1);
			this.TemplatesPanel.Controls.Add(this.label12);
			this.TemplatesPanel.Controls.Add(this.button10);
			this.TemplatesPanel.Controls.Add(this.button1);
			this.TemplatesPanel.Controls.Add(this.checkTemplatesCtrl);
			this.TemplatesPanel.Location = new System.Drawing.Point(4, 37);
			this.TemplatesPanel.Name = "TemplatesPanel";
			this.TemplatesPanel.Size = new System.Drawing.Size(188, 145);
			this.TemplatesPanel.TabIndex = 12;
			this.TemplatesPanel.Visible = false;
			this.TemplatesPanel.VisibleChanged += new System.EventHandler(this.TemplatesPanelVisibleChanged);
			// 
			// deleteTemplCtrl
			// 
			this.deleteTemplCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.deleteTemplCtrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.deleteTemplCtrl.Image = ((System.Drawing.Image)(resources.GetObject("deleteTemplCtrl.Image")));
			this.deleteTemplCtrl.InitialImage = ((System.Drawing.Image)(resources.GetObject("deleteTemplCtrl.InitialImage")));
			this.deleteTemplCtrl.Location = new System.Drawing.Point(136, 0);
			this.deleteTemplCtrl.Name = "deleteTemplCtrl";
			this.deleteTemplCtrl.Size = new System.Drawing.Size(20, 19);
			this.deleteTemplCtrl.TabIndex = 5;
			this.deleteTemplCtrl.TabStop = false;
			this.deleteTemplCtrl.Click += new System.EventHandler(this.DeleteTemplCtrlClick);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
			this.pictureBox1.Location = new System.Drawing.Point(165, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(20, 19);
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox1MouseClick);
			// 
			// label12
			// 
			this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.label12.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.label12.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label12.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.label12.Location = new System.Drawing.Point(3, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(182, 17);
			this.label12.TabIndex = 3;
			this.label12.Text = "Templates list";
			// 
			// button10
			// 
			this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button10.Location = new System.Drawing.Point(119, 117);
			this.button10.Name = "button10";
			this.button10.Size = new System.Drawing.Size(66, 23);
			this.button10.TabIndex = 2;
			this.button10.Text = "Ca&ncel";
			this.button10.UseVisualStyleBackColor = true;
			this.button10.Click += new System.EventHandler(this.Button10Click);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Location = new System.Drawing.Point(3, 117);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(89, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Add &selected";
			this.toolTip1.SetToolTip(this.button1, "   ");
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// checkTemplatesCtrl
			// 
			this.checkTemplatesCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.checkTemplatesCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkTemplatesCtrl.FormattingEnabled = true;
			this.checkTemplatesCtrl.Location = new System.Drawing.Point(3, 20);
			this.checkTemplatesCtrl.Name = "checkTemplatesCtrl";
			this.checkTemplatesCtrl.Size = new System.Drawing.Size(182, 89);
			this.checkTemplatesCtrl.TabIndex = 0;
			this.checkTemplatesCtrl.MouseEnter += new System.EventHandler(this.CheckTemplatesCtrlMouseEnter);
			this.checkTemplatesCtrl.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.CheckTemplatesCtrlPreviewKeyDown);
			this.checkTemplatesCtrl.MouseLeave += new System.EventHandler(this.CheckTemplatesCtrlMouseLeave);
			this.checkTemplatesCtrl.SelectedValueChanged += new System.EventHandler(this.CheckTemplatesCtrlSelectedValueChanged);
			// 
			// addVirtCtrl
			// 
			this.addVirtCtrl.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
			this.addVirtCtrl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
			this.addVirtCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.addVirtCtrl.ForeColor = System.Drawing.SystemColors.Control;
			this.addVirtCtrl.Image = ((System.Drawing.Image)(resources.GetObject("addVirtCtrl.Image")));
			this.addVirtCtrl.Location = new System.Drawing.Point(109, 4);
			this.addVirtCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.addVirtCtrl.Name = "addVirtCtrl";
			this.addVirtCtrl.Size = new System.Drawing.Size(39, 26);
			this.addVirtCtrl.TabIndex = 11;
			this.toolTip2.SetToolTip(this.addVirtCtrl, "Add empty Subquery");
			this.addVirtCtrl.UseVisualStyleBackColor = true;
			this.addVirtCtrl.Click += new System.EventHandler(this.AddVirtCtrlClick);
			// 
			// VirtOpen
			// 
			this.VirtOpen.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
			this.VirtOpen.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
			this.VirtOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.VirtOpen.ForeColor = System.Drawing.SystemColors.Control;
			this.VirtOpen.Image = ((System.Drawing.Image)(resources.GetObject("VirtOpen.Image")));
			this.VirtOpen.Location = new System.Drawing.Point(67, 4);
			this.VirtOpen.Margin = new System.Windows.Forms.Padding(4);
			this.VirtOpen.Name = "VirtOpen";
			this.VirtOpen.Size = new System.Drawing.Size(39, 26);
			this.VirtOpen.TabIndex = 10;
			this.toolTip2.SetToolTip(this.VirtOpen, "Open subquery in new window");
			this.VirtOpen.UseVisualStyleBackColor = true;
			this.VirtOpen.Click += new System.EventHandler(this.VirtOpenClick);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.label2.Location = new System.Drawing.Point(4, 7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 23);
			this.label2.TabIndex = 9;
			this.label2.Text = "Tables";
			// 
			// button8
			// 
			this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button8.BackColor = System.Drawing.Color.Transparent;
			this.button8.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
			this.button8.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button8.Font = new System.Drawing.Font("Lucida Console", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button8.ForeColor = System.Drawing.SystemColors.ScrollBar;
			this.button8.Location = new System.Drawing.Point(242, 36);
			this.button8.Margin = new System.Windows.Forms.Padding(4);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(29, 38);
			this.button8.TabIndex = 8;
			this.button8.Text = "_-";
			this.button8.UseVisualStyleBackColor = false;
			this.button8.Click += new System.EventHandler(this.Button8Click);
			// 
			// button9
			// 
			this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button9.BackColor = System.Drawing.Color.Transparent;
			this.button9.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
			this.button9.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button9.Font = new System.Drawing.Font("Lucida Console", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button9.ForeColor = System.Drawing.SystemColors.ScrollBar;
			this.button9.Location = new System.Drawing.Point(242, 81);
			this.button9.Margin = new System.Windows.Forms.Padding(4);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(29, 38);
			this.button9.TabIndex = 7;
			this.button9.Text = "_+";
			this.button9.UseVisualStyleBackColor = false;
			this.button9.Click += new System.EventHandler(this.Button9Click);
			// 
			// moveFields
			// 
			this.moveFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.moveFields.BackColor = System.Drawing.Color.Transparent;
			this.moveFields.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.moveFields.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.moveFields.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.moveFields.ForeColor = System.Drawing.SystemColors.ScrollBar;
			this.moveFields.Location = new System.Drawing.Point(242, 154);
			this.moveFields.Margin = new System.Windows.Forms.Padding(4);
			this.moveFields.MinimumSize = new System.Drawing.Size(0, 70);
			this.moveFields.Name = "moveFields";
			this.moveFields.Size = new System.Drawing.Size(29, 137);
			this.moveFields.TabIndex = 5;
			this.moveFields.Text = ">       >       >";
			this.moveFields.UseVisualStyleBackColor = false;
			this.moveFields.Click += new System.EventHandler(this.MoveFieldsClick);
			// 
			// tablesCtrl
			// 
			this.tablesCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tablesCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tablesCtrl.HideSelection = false;
			this.tablesCtrl.ImageIndex = 0;
			this.tablesCtrl.ImageList = this.dbimg;
			this.tablesCtrl.LabelEdit = true;
			this.tablesCtrl.Location = new System.Drawing.Point(2, 35);
			this.tablesCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.tablesCtrl.Name = "tablesCtrl";
			this.tablesCtrl.PathSeparator = ".";
			this.tablesCtrl.SelectedImageIndex = 0;
			this.tablesCtrl.ShowNodeToolTips = true;
			this.tablesCtrl.Size = new System.Drawing.Size(235, 459);
			this.tablesCtrl.TabIndex = 0;
			this.tablesCtrl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TablesCtrlMouseDoubleClick);
			this.tablesCtrl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TablesCtrlMouseClick);
			this.tablesCtrl.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TablesCtrlAfterLabelEdit);
			this.tablesCtrl.Leave += new System.EventHandler(this.TablesCtrlLeave);
			this.tablesCtrl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TablesCtrlKeyUp);
			this.tablesCtrl.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TablesCtrlNodeMouseClick);
			this.tablesCtrl.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TablesCtrlBeforeLabelEdit);
			// 
			// queryTreeCtrl
			// 
			this.queryTreeCtrl.Controls.Add(this.label13);
			this.queryTreeCtrl.Controls.Add(this.queryTreeViewCtrl);
			this.queryTreeCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.queryTreeCtrl.Location = new System.Drawing.Point(0, 0);
			this.queryTreeCtrl.Name = "queryTreeCtrl";
			this.queryTreeCtrl.Size = new System.Drawing.Size(268, 497);
			this.queryTreeCtrl.TabIndex = 3;
			this.queryTreeCtrl.Visible = false;
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.label13.Location = new System.Drawing.Point(4, 7);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(198, 23);
			this.label13.TabIndex = 15;
			this.label13.Text = "Query tree  (saved items)";
			// 
			// queryTreeViewCtrl
			// 
			this.queryTreeViewCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.queryTreeViewCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
			this.queryTreeViewCtrl.ImageIndex = 0;
			this.queryTreeViewCtrl.ImageList = this.dbimg;
			this.queryTreeViewCtrl.Location = new System.Drawing.Point(4, 35);
			this.queryTreeViewCtrl.Name = "queryTreeViewCtrl";
			this.queryTreeViewCtrl.PathSeparator = "|";
			this.queryTreeViewCtrl.SelectedImageIndex = 0;
			this.queryTreeViewCtrl.Size = new System.Drawing.Size(256, 458);
			this.queryTreeViewCtrl.TabIndex = 0;
			this.queryTreeViewCtrl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.QueryTreeViewCtrlMouseDoubleClick);
			this.queryTreeViewCtrl.Leave += new System.EventHandler(this.QueryTreeViewCtrlLeave);
			// 
			// uParamCtrl
			// 
			this.uParamCtrl.ForeColor = System.Drawing.Color.Teal;
			this.uParamCtrl.Location = new System.Drawing.Point(208, 0);
			this.uParamCtrl.Name = "uParamCtrl";
			this.uParamCtrl.Size = new System.Drawing.Size(60, 23);
			this.uParamCtrl.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.label3.Location = new System.Drawing.Point(4, 7);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(50, 23);
			this.label3.TabIndex = 1;
			this.label3.Text = "Fields";
			// 
			// fieldsCtrl
			// 
			this.fieldsCtrl.AllowDrop = true;
			this.fieldsCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.fieldsCtrl.ColumnWidth = 50;
			this.fieldsCtrl.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.fieldsCtrl.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.fieldsCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.fieldsCtrl.IntegralHeight = false;
			this.fieldsCtrl.ItemHeight = 18;
			this.fieldsCtrl.Location = new System.Drawing.Point(2, 36);
			this.fieldsCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.fieldsCtrl.Name = "fieldsCtrl";
			this.fieldsCtrl.Size = new System.Drawing.Size(258, 457);
			this.fieldsCtrl.TabIndex = 0;
			this.fieldsCtrl.Tag = "fl";
			this.fieldsCtrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GoupByCtrlMouseUp);
			this.fieldsCtrl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FieldsCtrlMouseDoubleClick);
			this.fieldsCtrl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.FieldsCtrlDrawItem);
			this.fieldsCtrl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GoupByCtrlMouseMove);
			this.fieldsCtrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GoupByCtrlMouseDown);
			this.fieldsCtrl.MouseLeave += new System.EventHandler(this.GoupByCtrlMouseLeave);
			this.fieldsCtrl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GoupByCtrlKeyUp);
			this.fieldsCtrl.SelectedValueChanged += new System.EventHandler(this.FieldsCtrlSelectedValueChanged);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.splitContainer8);
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
			this.tabPage2.Size = new System.Drawing.Size(841, 505);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "   Relations     ";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.tabPage2.Leave += new System.EventHandler(this.TabPage2Leave);
			// 
			// splitContainer8
			// 
			this.splitContainer8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer8.Location = new System.Drawing.Point(4, 4);
			this.splitContainer8.Name = "splitContainer8";
			// 
			// splitContainer8.Panel1
			// 
			this.splitContainer8.Panel1.Controls.Add(this.label9);
			this.splitContainer8.Panel1.Controls.Add(this.tablesToJoinCtrl);
			// 
			// splitContainer8.Panel2
			// 
			this.splitContainer8.Panel2.Controls.Add(this.expandAllJoinsCtrl);
			this.splitContainer8.Panel2.Controls.Add(this.deleteJoinCtrl);
			this.splitContainer8.Panel2.Controls.Add(this.JoinsCtrl);
			this.splitContainer8.Panel2.Controls.Add(this.colapseAllCtrl);
			this.splitContainer8.Panel2.Controls.Add(this.moveJoinDownCtrl);
			this.splitContainer8.Panel2.Controls.Add(this.upJoinCtrl);
			this.splitContainer8.Size = new System.Drawing.Size(833, 497);
			this.splitContainer8.SplitterDistance = 235;
			this.splitContainer8.TabIndex = 16;
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.label9.Location = new System.Drawing.Point(25, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(130, 15);
			this.label9.TabIndex = 16;
			this.label9.Text = "Not joined tables";
			// 
			// tablesToJoinCtrl
			// 
			this.tablesToJoinCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tablesToJoinCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tablesToJoinCtrl.ImageIndex = 0;
			this.tablesToJoinCtrl.ImageList = this.dbimg;
			this.tablesToJoinCtrl.Location = new System.Drawing.Point(3, 18);
			this.tablesToJoinCtrl.Name = "tablesToJoinCtrl";
			this.tablesToJoinCtrl.SelectedImageIndex = 0;
			this.tablesToJoinCtrl.Size = new System.Drawing.Size(229, 473);
			this.tablesToJoinCtrl.TabIndex = 15;
			this.tablesToJoinCtrl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TablesToJoinCtrlMouseDoubleClick);
			// 
			// expandAllJoinsCtrl
			// 
			this.expandAllJoinsCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.expandAllJoinsCtrl.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
			this.expandAllJoinsCtrl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
			this.expandAllJoinsCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.expandAllJoinsCtrl.ForeColor = System.Drawing.SystemColors.Control;
			this.expandAllJoinsCtrl.Image = ((System.Drawing.Image)(resources.GetObject("expandAllJoinsCtrl.Image")));
			this.expandAllJoinsCtrl.Location = new System.Drawing.Point(463, 0);
			this.expandAllJoinsCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.expandAllJoinsCtrl.Name = "expandAllJoinsCtrl";
			this.expandAllJoinsCtrl.Size = new System.Drawing.Size(39, 19);
			this.expandAllJoinsCtrl.TabIndex = 17;
			this.toolTip2.SetToolTip(this.expandAllJoinsCtrl, "Expand all");
			this.expandAllJoinsCtrl.UseVisualStyleBackColor = true;
			this.expandAllJoinsCtrl.Click += new System.EventHandler(this.ExpandAllJoinsCtrlClick);
			// 
			// deleteJoinCtrl
			// 
			this.deleteJoinCtrl.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
			this.deleteJoinCtrl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
			this.deleteJoinCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.deleteJoinCtrl.ForeColor = System.Drawing.SystemColors.Control;
			this.deleteJoinCtrl.Image = ((System.Drawing.Image)(resources.GetObject("deleteJoinCtrl.Image")));
			this.deleteJoinCtrl.Location = new System.Drawing.Point(34, 0);
			this.deleteJoinCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.deleteJoinCtrl.Name = "deleteJoinCtrl";
			this.deleteJoinCtrl.Size = new System.Drawing.Size(39, 19);
			this.deleteJoinCtrl.TabIndex = 16;
			this.toolTip2.SetToolTip(this.deleteJoinCtrl, "Delete selected table");
			this.deleteJoinCtrl.UseVisualStyleBackColor = true;
			this.deleteJoinCtrl.Click += new System.EventHandler(this.DeleteJoinCtrlClick);
			// 
			// JoinsCtrl
			// 
			this.JoinsCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.JoinsCtrl.AutoScroll = true;
			this.JoinsCtrl.BackColor = System.Drawing.Color.Transparent;
			this.JoinsCtrl.joinz = new SqlBuilderClasses.join[0];
			this.JoinsCtrl.JoinzHost = null;
			this.JoinsCtrl.Location = new System.Drawing.Point(3, 21);
			this.JoinsCtrl.Name = "JoinsCtrl";
			this.JoinsCtrl.selectedItem = null;
			this.JoinsCtrl.Size = new System.Drawing.Size(588, 473);
			this.JoinsCtrl.TabIndex = 8;
			this.toolTip2.SetToolTip(this.JoinsCtrl, "Activate (click on table name) and double click its field name to add outer key o" +
						"f the table,   double click on field of other table will set corresponding outer" +
						" key");
			// 
			// colapseAllCtrl
			// 
			this.colapseAllCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.colapseAllCtrl.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
			this.colapseAllCtrl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
			this.colapseAllCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.colapseAllCtrl.ForeColor = System.Drawing.SystemColors.Control;
			this.colapseAllCtrl.Image = ((System.Drawing.Image)(resources.GetObject("colapseAllCtrl.Image")));
			this.colapseAllCtrl.Location = new System.Drawing.Point(510, 0);
			this.colapseAllCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.colapseAllCtrl.Name = "colapseAllCtrl";
			this.colapseAllCtrl.Size = new System.Drawing.Size(39, 19);
			this.colapseAllCtrl.TabIndex = 12;
			this.toolTip2.SetToolTip(this.colapseAllCtrl, "Collapse to table definitions");
			this.colapseAllCtrl.UseVisualStyleBackColor = true;
			this.colapseAllCtrl.Click += new System.EventHandler(this.ColapseAllCtrlClick);
			// 
			// moveJoinDownCtrl
			// 
			this.moveJoinDownCtrl.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
			this.moveJoinDownCtrl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
			this.moveJoinDownCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.moveJoinDownCtrl.ForeColor = System.Drawing.SystemColors.Control;
			this.moveJoinDownCtrl.Image = ((System.Drawing.Image)(resources.GetObject("moveJoinDownCtrl.Image")));
			this.moveJoinDownCtrl.Location = new System.Drawing.Point(249, 0);
			this.moveJoinDownCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.moveJoinDownCtrl.Name = "moveJoinDownCtrl";
			this.moveJoinDownCtrl.Size = new System.Drawing.Size(39, 19);
			this.moveJoinDownCtrl.TabIndex = 15;
			this.toolTip2.SetToolTip(this.moveJoinDownCtrl, "Move Down");
			this.moveJoinDownCtrl.UseVisualStyleBackColor = true;
			this.moveJoinDownCtrl.Click += new System.EventHandler(this.MoveJoinDownCtrlClick);
			// 
			// upJoinCtrl
			// 
			this.upJoinCtrl.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
			this.upJoinCtrl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
			this.upJoinCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.upJoinCtrl.ForeColor = System.Drawing.SystemColors.Control;
			this.upJoinCtrl.Image = ((System.Drawing.Image)(resources.GetObject("upJoinCtrl.Image")));
			this.upJoinCtrl.Location = new System.Drawing.Point(193, 0);
			this.upJoinCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.upJoinCtrl.Name = "upJoinCtrl";
			this.upJoinCtrl.Size = new System.Drawing.Size(39, 19);
			this.upJoinCtrl.TabIndex = 14;
			this.toolTip2.SetToolTip(this.upJoinCtrl, "Move Up");
			this.upJoinCtrl.UseVisualStyleBackColor = true;
			this.upJoinCtrl.Click += new System.EventHandler(this.UpJoinCtrlClick);
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.splitContainer3);
			this.tabPage3.Location = new System.Drawing.Point(4, 24);
			this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
			this.tabPage3.Size = new System.Drawing.Size(841, 505);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "   Grouping     ";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// splitContainer3
			// 
			this.splitContainer3.BackColor = System.Drawing.Color.Transparent;
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.splitContainer3.Location = new System.Drawing.Point(4, 4);
			this.splitContainer3.Name = "splitContainer3";
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.label4);
			this.splitContainer3.Panel1.Controls.Add(this.AvalFields);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
			this.splitContainer3.Size = new System.Drawing.Size(833, 497);
			this.splitContainer3.SplitterDistance = 234;
			this.splitContainer3.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.label4.Location = new System.Drawing.Point(25, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 15);
			this.label4.TabIndex = 1;
			this.label4.Text = "Available fields";
			// 
			// AvalFields
			// 
			this.AvalFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.AvalFields.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.AvalFields.Location = new System.Drawing.Point(3, 18);
			this.AvalFields.Name = "AvalFields";
			this.AvalFields.Size = new System.Drawing.Size(228, 476);
			this.AvalFields.TabIndex = 0;
			this.AvalFields.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AvalFieldsMouseDoubleClick);
			this.AvalFields.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AvalFieldsMouseUp);
			this.AvalFields.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AvalFieldsMouseMove);
			this.AvalFields.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AvalFieldsMouseDown);
			// 
			// splitContainer4
			// 
			this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer4.ForeColor = System.Drawing.SystemColors.Info;
			this.splitContainer4.Location = new System.Drawing.Point(0, 0);
			this.splitContainer4.Name = "splitContainer4";
			this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer4.Panel1
			// 
			this.splitContainer4.Panel1.Controls.Add(this.label1);
			this.splitContainer4.Panel1.Controls.Add(this.GoupByCtrl);
			// 
			// splitContainer4.Panel2
			// 
			this.splitContainer4.Panel2.Controls.Add(this.label6);
			this.splitContainer4.Panel2.Controls.Add(this.AggregateFldsCtrl);
			this.splitContainer4.Size = new System.Drawing.Size(595, 497);
			this.splitContainer4.SplitterDistance = 219;
			this.splitContainer4.SplitterWidth = 7;
			this.splitContainer4.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.PaleTurquoise;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.label1.Location = new System.Drawing.Point(36, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "Group BY";
			// 
			// GoupByCtrl
			// 
			this.GoupByCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.GoupByCtrl.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.GoupByCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.GoupByCtrl.FormattingEnabled = true;
			this.GoupByCtrl.IntegralHeight = false;
			this.GoupByCtrl.ItemHeight = 18;
			this.GoupByCtrl.Location = new System.Drawing.Point(3, 18);
			this.GoupByCtrl.Name = "GoupByCtrl";
			this.GoupByCtrl.Size = new System.Drawing.Size(591, 196);
			this.GoupByCtrl.TabIndex = 0;
			this.GoupByCtrl.Tag = "gr";
			this.GoupByCtrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GoupByCtrlMouseUp);
			this.GoupByCtrl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.GoupByCtrlDrawItem);
			this.GoupByCtrl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GoupByCtrlMouseClick);
			this.GoupByCtrl.SelectedIndexChanged += new System.EventHandler(this.ListBox1SelectedIndexChanged);
			this.GoupByCtrl.Leave += new System.EventHandler(this.AggregateFldsCtrlLeave);
			this.GoupByCtrl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GoupByCtrlMouseMove);
			this.GoupByCtrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GoupByCtrlMouseDown);
			this.GoupByCtrl.MouseLeave += new System.EventHandler(this.GoupByCtrlMouseLeave);
			this.GoupByCtrl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GoupByCtrlKeyUp);
			this.GoupByCtrl.SelectedValueChanged += new System.EventHandler(this.GoupByCtrlSelectedValueChanged);
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.label6.Location = new System.Drawing.Point(36, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(123, 17);
			this.label6.TabIndex = 1;
			this.label6.Text = "Agregate fields";
			this.toolTip2.SetToolTip(this.label6, "Double click to change function type");
			// 
			// AggregateFldsCtrl
			// 
			this.AggregateFldsCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.AggregateFldsCtrl.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.AggregateFldsCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.AggregateFldsCtrl.IntegralHeight = false;
			this.AggregateFldsCtrl.ItemHeight = 18;
			this.AggregateFldsCtrl.Location = new System.Drawing.Point(3, 20);
			this.AggregateFldsCtrl.Name = "AggregateFldsCtrl";
			this.AggregateFldsCtrl.Size = new System.Drawing.Size(589, 244);
			this.AggregateFldsCtrl.TabIndex = 0;
			this.toolTip2.SetToolTip(this.AggregateFldsCtrl, "Double click to change function type");
			this.AggregateFldsCtrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AggregateFldsCtrlMouseUp);
			this.AggregateFldsCtrl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AggregateFldsCtrlMouseDoubleClick);
			this.AggregateFldsCtrl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.AggregateFldsCtrlDrawItem);
			this.AggregateFldsCtrl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AggregateFldsCtrlMouseClick);
			this.AggregateFldsCtrl.Leave += new System.EventHandler(this.AggregateFldsCtrlLeave);
			this.AggregateFldsCtrl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.AggregateFldsCtrlKeyUp);
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.panel1);
			this.tabPage4.Location = new System.Drawing.Point(4, 24);
			this.tabPage4.Margin = new System.Windows.Forms.Padding(4);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(4);
			this.tabPage4.Size = new System.Drawing.Size(841, 505);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "   Conditions    ";
			this.tabPage4.UseVisualStyleBackColor = true;
			this.tabPage4.Leave += new System.EventHandler(this.TabPage4Leave);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.splitContainer7);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(833, 497);
			this.panel1.TabIndex = 0;
			// 
			// splitContainer7
			// 
			this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer7.Location = new System.Drawing.Point(0, 0);
			this.splitContainer7.Name = "splitContainer7";
			// 
			// splitContainer7.Panel1
			// 
			this.splitContainer7.Panel1.Controls.Add(this.label14);
			this.splitContainer7.Panel1.Controls.Add(this.tablesList);
			// 
			// splitContainer7.Panel2
			// 
			this.splitContainer7.Panel2.Controls.Add(this.addTextConditionCtrl);
			this.splitContainer7.Panel2.Controls.Add(this.deleteConditionCtrl);
			this.splitContainer7.Panel2.Controls.Add(this.addConditionCtrl);
			this.splitContainer7.Panel2.Controls.Add(this.groupBox1);
			this.splitContainer7.Size = new System.Drawing.Size(833, 497);
			this.splitContainer7.SplitterDistance = 234;
			this.splitContainer7.TabIndex = 15;
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.label14.Location = new System.Drawing.Point(25, 0);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(100, 15);
			this.label14.TabIndex = 15;
			this.label14.Text = "Available fields";
			this.toolTip2.SetToolTip(this.label14, "Right click - fill right side of equation; Left - set left side");
			// 
			// tablesList
			// 
			this.tablesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tablesList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tablesList.Location = new System.Drawing.Point(3, 18);
			this.tablesList.Name = "tablesList";
			this.tablesList.Size = new System.Drawing.Size(228, 469);
			this.tablesList.TabIndex = 14;
			this.toolTip2.SetToolTip(this.tablesList, "Right click - fill right side of equation; Left - set left side");
			this.tablesList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TablesListMouseDoubleClick);
			this.tablesList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TablesListMouseClick);
			this.tablesList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AvalFieldsMouseUp);
			this.tablesList.MouseEnter += new System.EventHandler(this.TablesListMouseEnter);
			this.tablesList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AvalFieldsMouseMove);
			this.tablesList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AvalFieldsMouseDown);
			this.tablesList.MouseLeave += new System.EventHandler(this.TablesListMouseLeave);
			// 
			// addTextConditionCtrl
			// 
			this.addTextConditionCtrl.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
			this.addTextConditionCtrl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
			this.addTextConditionCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.addTextConditionCtrl.ForeColor = System.Drawing.SystemColors.Control;
			this.addTextConditionCtrl.Image = ((System.Drawing.Image)(resources.GetObject("addTextConditionCtrl.Image")));
			this.addTextConditionCtrl.Location = new System.Drawing.Point(0, 73);
			this.addTextConditionCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.addTextConditionCtrl.Name = "addTextConditionCtrl";
			this.addTextConditionCtrl.Size = new System.Drawing.Size(24, 26);
			this.addTextConditionCtrl.TabIndex = 14;
			this.toolTip2.SetToolTip(this.addTextConditionCtrl, "Add raw text equality");
			this.addTextConditionCtrl.UseVisualStyleBackColor = true;
			this.addTextConditionCtrl.Click += new System.EventHandler(this.AddTextConditionCtrlClick);
			// 
			// deleteConditionCtrl
			// 
			this.deleteConditionCtrl.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
			this.deleteConditionCtrl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
			this.deleteConditionCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.deleteConditionCtrl.ForeColor = System.Drawing.SystemColors.Control;
			this.deleteConditionCtrl.Image = ((System.Drawing.Image)(resources.GetObject("deleteConditionCtrl.Image")));
			this.deleteConditionCtrl.Location = new System.Drawing.Point(0, 122);
			this.deleteConditionCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.deleteConditionCtrl.Name = "deleteConditionCtrl";
			this.deleteConditionCtrl.Size = new System.Drawing.Size(24, 26);
			this.deleteConditionCtrl.TabIndex = 13;
			this.deleteConditionCtrl.UseVisualStyleBackColor = true;
			this.deleteConditionCtrl.Click += new System.EventHandler(this.DeleteConditionCtrlClick);
			// 
			// addConditionCtrl
			// 
			this.addConditionCtrl.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
			this.addConditionCtrl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
			this.addConditionCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.addConditionCtrl.ForeColor = System.Drawing.SystemColors.Control;
			this.addConditionCtrl.Image = ((System.Drawing.Image)(resources.GetObject("addConditionCtrl.Image")));
			this.addConditionCtrl.Location = new System.Drawing.Point(0, 39);
			this.addConditionCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.addConditionCtrl.Name = "addConditionCtrl";
			this.addConditionCtrl.Size = new System.Drawing.Size(24, 26);
			this.addConditionCtrl.TabIndex = 12;
			this.toolTip2.SetToolTip(this.addConditionCtrl, "Add structured equality");
			this.addConditionCtrl.UseVisualStyleBackColor = true;
			this.addConditionCtrl.Click += new System.EventHandler(this.AddConditionCtrlClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.conditionListCtrl);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox1.Location = new System.Drawing.Point(24, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(568, 491);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Conditions (K.O.)";
			// 
			// conditionListCtrl
			// 
			this.conditionListCtrl.AutoScroll = true;
			this.conditionListCtrl.BackColor = System.Drawing.Color.Transparent;
			this.conditionListCtrl.conditionz = new SqlBuilderClasses.condition[0];
			this.conditionListCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.conditionListCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.conditionListCtrl.ForeColor = System.Drawing.SystemColors.ScrollBar;
			this.conditionListCtrl.Location = new System.Drawing.Point(3, 18);
			this.conditionListCtrl.Name = "conditionListCtrl";
			this.conditionListCtrl.selectedItem = null;
			this.conditionListCtrl.Size = new System.Drawing.Size(562, 470);
			this.conditionListCtrl.TabIndex = 3;
			this.conditionListCtrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ConditionListCtrlMouseUp);
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.splitContainer6);
			this.tabPage5.Location = new System.Drawing.Point(4, 24);
			this.tabPage5.Margin = new System.Windows.Forms.Padding(4);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Padding = new System.Windows.Forms.Padding(4);
			this.tabPage5.Size = new System.Drawing.Size(841, 505);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "Unions / Aliases";
			this.tabPage5.UseVisualStyleBackColor = true;
			// 
			// splitContainer6
			// 
			this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer6.Location = new System.Drawing.Point(4, 4);
			this.splitContainer6.Name = "splitContainer6";
			// 
			// splitContainer6.Panel1
			// 
			this.splitContainer6.Panel1.Controls.Add(this.deleteUnionCtrl);
			this.splitContainer6.Panel1.Controls.Add(this.label10);
			this.splitContainer6.Panel1.Controls.Add(this.addUnionCtrl);
			this.splitContainer6.Panel1.Controls.Add(this.unionListCtrl);
			// 
			// splitContainer6.Panel2
			// 
			this.splitContainer6.Panel2.Controls.Add(this.uniEditCtrl);
			this.splitContainer6.Panel2.Controls.Add(this.unialiasTableCtrl);
			this.splitContainer6.Panel2.Controls.Add(this.aliasListCtrl);
			this.splitContainer6.Size = new System.Drawing.Size(833, 497);
			this.splitContainer6.SplitterDistance = 184;
			this.splitContainer6.TabIndex = 2;
			// 
			// deleteUnionCtrl
			// 
			this.deleteUnionCtrl.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
			this.deleteUnionCtrl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
			this.deleteUnionCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.deleteUnionCtrl.ForeColor = System.Drawing.SystemColors.Control;
			this.deleteUnionCtrl.Image = ((System.Drawing.Image)(resources.GetObject("deleteUnionCtrl.Image")));
			this.deleteUnionCtrl.Location = new System.Drawing.Point(136, 7);
			this.deleteUnionCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.deleteUnionCtrl.Name = "deleteUnionCtrl";
			this.deleteUnionCtrl.Size = new System.Drawing.Size(39, 26);
			this.deleteUnionCtrl.TabIndex = 14;
			this.toolTip2.SetToolTip(this.deleteUnionCtrl, "Delete union");
			this.deleteUnionCtrl.UseVisualStyleBackColor = true;
			this.deleteUnionCtrl.Click += new System.EventHandler(this.DeleteUnionCtrlClick);
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.label10.Location = new System.Drawing.Point(3, 7);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(84, 23);
			this.label10.TabIndex = 13;
			this.label10.Text = "Unions list";
			// 
			// addUnionCtrl
			// 
			this.addUnionCtrl.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
			this.addUnionCtrl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
			this.addUnionCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.addUnionCtrl.ForeColor = System.Drawing.SystemColors.Control;
			this.addUnionCtrl.Image = ((System.Drawing.Image)(resources.GetObject("addUnionCtrl.Image")));
			this.addUnionCtrl.Location = new System.Drawing.Point(89, 7);
			this.addUnionCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.addUnionCtrl.Name = "addUnionCtrl";
			this.addUnionCtrl.Size = new System.Drawing.Size(39, 26);
			this.addUnionCtrl.TabIndex = 12;
			this.toolTip2.SetToolTip(this.addUnionCtrl, "Add Union");
			this.addUnionCtrl.UseVisualStyleBackColor = true;
			this.addUnionCtrl.Click += new System.EventHandler(this.AddUnionCtrlClick);
			// 
			// unionListCtrl
			// 
			this.unionListCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.unionListCtrl.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.unionListCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.unionListCtrl.FormattingEnabled = true;
			this.unionListCtrl.IntegralHeight = false;
			this.unionListCtrl.ItemHeight = 18;
			this.unionListCtrl.Items.AddRange(new object[] {
									" 1 hhhhhhhhhh",
									" 2 ooooooooo",
									" 3 rrrrrrrrrrrrrrrrr",
									" 4 jjjjjjjjjjjjjjjjjjjjjjjjj",
									" 5 vvvvvvvvvv",
									" 6 xxxxxxxxxxxx"});
			this.unionListCtrl.Location = new System.Drawing.Point(3, 37);
			this.unionListCtrl.Name = "unionListCtrl";
			this.unionListCtrl.Size = new System.Drawing.Size(178, 460);
			this.unionListCtrl.TabIndex = 1;
			this.unionListCtrl.Tag = "un";
			this.unionListCtrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GoupByCtrlMouseUp);
			this.unionListCtrl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.UnionListCtrlMouseDoubleClick);
			this.unionListCtrl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.GoupByCtrlDrawItem);
			this.unionListCtrl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.UnionListCtrlMouseClick);
			this.unionListCtrl.SelectedIndexChanged += new System.EventHandler(this.GoupByCtrlSelectedValueChanged);
			this.unionListCtrl.Leave += new System.EventHandler(this.AggregateFldsCtrlLeave);
			this.unionListCtrl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GoupByCtrlMouseMove);
			this.unionListCtrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GoupByCtrlMouseDown);
			this.unionListCtrl.MouseLeave += new System.EventHandler(this.GoupByCtrlMouseLeave);
			this.unionListCtrl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GoupByCtrlKeyUp);
			// 
			// uniEditCtrl
			// 
			this.uniEditCtrl.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.uniEditCtrl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			this.uniEditCtrl.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.uniEditCtrl.FormattingEnabled = true;
			this.uniEditCtrl.ItemHeight = 14;
			this.uniEditCtrl.Location = new System.Drawing.Point(3222, 368);
			this.uniEditCtrl.Name = "uniEditCtrl";
			this.uniEditCtrl.Size = new System.Drawing.Size(160, 22);
			this.uniEditCtrl.TabIndex = 1;
			this.uniEditCtrl.Leave += new System.EventHandler(this.UniEditCtrlLeave);
			this.uniEditCtrl.LocationChanged += new System.EventHandler(this.UniEditCtrlLocationChanged);
			// 
			// unialiasTableCtrl
			// 
			this.unialiasTableCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.unialiasTableCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.unialiasTableCtrl.GridLines = true;
			this.unialiasTableCtrl.Location = new System.Drawing.Point(167, 0);
			this.unialiasTableCtrl.MultiSelect = false;
			this.unialiasTableCtrl.Name = "unialiasTableCtrl";
			this.unialiasTableCtrl.ShowGroups = false;
			this.unialiasTableCtrl.Size = new System.Drawing.Size(475, 497);
			this.unialiasTableCtrl.TabIndex = 0;
			this.unialiasTableCtrl.Tag = "0";
			this.unialiasTableCtrl.UseCompatibleStateImageBehavior = false;
			this.unialiasTableCtrl.View = System.Windows.Forms.View.Details;
			this.unialiasTableCtrl.ClientSizeChanged += new System.EventHandler(this.ListView1ClientSizeChanged);
			this.unialiasTableCtrl.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ListView1Scroll);
			this.unialiasTableCtrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UnialiasTableCtrlMouseUp);
			this.unialiasTableCtrl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UnialiasTableCtrlMouseMove);
			this.unialiasTableCtrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListView1MouseUp);
			this.unialiasTableCtrl.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ListView1ColumnWidthChanging);
			// 
			// aliasListCtrl
			// 
			this.aliasListCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left)));
			this.aliasListCtrl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader1,
									this.columnHeader2});
			this.aliasListCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.aliasListCtrl.GridLines = true;
			this.aliasListCtrl.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.aliasListCtrl.LabelEdit = true;
			this.aliasListCtrl.Location = new System.Drawing.Point(3, 0);
			this.aliasListCtrl.Name = "aliasListCtrl";
			this.aliasListCtrl.Scrollable = false;
			this.aliasListCtrl.Size = new System.Drawing.Size(161, 497);
			this.aliasListCtrl.TabIndex = 2;
			this.aliasListCtrl.Tag = "0";
			this.aliasListCtrl.UseCompatibleStateImageBehavior = false;
			this.aliasListCtrl.View = System.Windows.Forms.View.Details;
			this.aliasListCtrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AliasListCtrlMouseUp);
			this.aliasListCtrl.MouseEnter += new System.EventHandler(this.AliasListCtrlMouseEnter);
			this.aliasListCtrl.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.AliasListCtrlColumnWidthChanging);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "wow, you are hacker :)";
			this.columnHeader1.Width = 0;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Alias";
			this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader2.Width = 159;
			// 
			// tabPage6
			// 
			this.tabPage6.Controls.Add(this.splitContainer5);
			this.tabPage6.Location = new System.Drawing.Point(4, 24);
			this.tabPage6.Margin = new System.Windows.Forms.Padding(4);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Padding = new System.Windows.Forms.Padding(4);
			this.tabPage6.Size = new System.Drawing.Size(841, 505);
			this.tabPage6.TabIndex = 5;
			this.tabPage6.Text = "   Order  ";
			this.tabPage6.UseVisualStyleBackColor = true;
			// 
			// splitContainer5
			// 
			this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer5.Location = new System.Drawing.Point(4, 4);
			this.splitContainer5.Name = "splitContainer5";
			// 
			// splitContainer5.Panel1
			// 
			this.splitContainer5.Panel1.Controls.Add(this.label7);
			this.splitContainer5.Panel1.Controls.Add(this.orderAvalCtrl);
			// 
			// splitContainer5.Panel2
			// 
			this.splitContainer5.Panel2.Controls.Add(this.label8);
			this.splitContainer5.Panel2.Controls.Add(this.orderByCtrl);
			this.splitContainer5.Size = new System.Drawing.Size(833, 497);
			this.splitContainer5.SplitterDistance = 233;
			this.splitContainer5.TabIndex = 0;
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.label7.Location = new System.Drawing.Point(25, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 15);
			this.label7.TabIndex = 2;
			this.label7.Text = "Available fields";
			// 
			// orderAvalCtrl
			// 
			this.orderAvalCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.orderAvalCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
			this.orderAvalCtrl.Location = new System.Drawing.Point(3, 18);
			this.orderAvalCtrl.Name = "orderAvalCtrl";
			this.orderAvalCtrl.Size = new System.Drawing.Size(227, 476);
			this.orderAvalCtrl.TabIndex = 0;
			this.orderAvalCtrl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OrderAvalCtrlMouseDoubleClick);
			this.orderAvalCtrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AvalFieldsMouseUp);
			this.orderAvalCtrl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AvalFieldsMouseMove);
			this.orderAvalCtrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AvalFieldsMouseDown);
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.label8.Location = new System.Drawing.Point(31, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 15);
			this.label8.TabIndex = 3;
			this.label8.Text = "Order BY";
			// 
			// orderByCtrl
			// 
			this.orderByCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.orderByCtrl.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.orderByCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
			this.orderByCtrl.FormattingEnabled = true;
			this.orderByCtrl.IntegralHeight = false;
			this.orderByCtrl.ItemHeight = 17;
			this.orderByCtrl.Location = new System.Drawing.Point(3, 18);
			this.orderByCtrl.Name = "orderByCtrl";
			this.orderByCtrl.Size = new System.Drawing.Size(590, 476);
			this.orderByCtrl.TabIndex = 0;
			this.orderByCtrl.Tag = "od";
			this.orderByCtrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GoupByCtrlMouseUp);
			this.orderByCtrl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.GoupByCtrlDrawItem);
			this.orderByCtrl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OrderByCtrlMouseClick);
			this.orderByCtrl.SelectedIndexChanged += new System.EventHandler(this.GoupByCtrlSelectedValueChanged);
			this.orderByCtrl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GoupByCtrlMouseMove);
			this.orderByCtrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GoupByCtrlMouseDown);
			this.orderByCtrl.MouseLeave += new System.EventHandler(this.GoupByCtrlMouseLeave);
			this.orderByCtrl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GoupByCtrlKeyUp);
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.BackColor = System.Drawing.Color.DimGray;
			this.button3.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button3.ForeColor = System.Drawing.Color.DarkOrange;
			this.button3.GlowColor = System.Drawing.Color.Transparent;
			this.button3.InnerBorderColor = System.Drawing.Color.LightSteelBlue;
			this.button3.Location = new System.Drawing.Point(817, 538);
			this.button3.Margin = new System.Windows.Forms.Padding(4);
			this.button3.Name = "button3";
			this.button3.OuterBorderColor = System.Drawing.Color.LavenderBlush;
			this.button3.Size = new System.Drawing.Size(33, 31);
			this.button3.TabIndex = 1;
			this.button3.Text = "I";
			this.button3.Visible = false;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.button2.ForeColor = System.Drawing.SystemColors.MenuText;
			this.button2.GlowColor = System.Drawing.Color.LimeGreen;
			this.button2.Location = new System.Drawing.Point(364, 538);
			this.button2.Margin = new System.Windows.Forms.Padding(4);
			this.button2.Name = "button2";
			this.button2.ShineColor = System.Drawing.SystemColors.MenuBar;
			this.button2.Size = new System.Drawing.Size(97, 30);
			this.button2.TabIndex = 2;
			this.button2.Text = "O &K";
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// button5
			// 
			this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button5.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.button5.ForeColor = System.Drawing.SystemColors.MenuText;
			this.button5.GlowColor = System.Drawing.Color.CornflowerBlue;
			this.button5.Location = new System.Drawing.Point(5, 538);
			this.button5.Margin = new System.Windows.Forms.Padding(4);
			this.button5.Name = "button5";
			this.button5.ShineColor = System.Drawing.SystemColors.MenuBar;
			this.button5.Size = new System.Drawing.Size(104, 30);
			this.button5.TabIndex = 3;
			this.button5.Text = "SQL &T E X T";
			this.button5.Click += new System.EventHandler(this.Button5Click);
			// 
			// unionsCtrl
			// 
			this.unionsCtrl.Alignment = System.Windows.Forms.TabAlignment.Right;
			this.unionsCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.unionsCtrl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
			this.unionsCtrl.Location = new System.Drawing.Point(853, 26);
			this.unionsCtrl.Multiline = true;
			this.unionsCtrl.Name = "unionsCtrl";
			this.unionsCtrl.SelectedIndex = 0;
			this.unionsCtrl.Size = new System.Drawing.Size(26, 538);
			this.unionsCtrl.TabIndex = 1;
			this.unionsCtrl.SelectedIndexChanged += new System.EventHandler(this.UnionsCtrlSelectedIndexChanged);
			// 
			// cancelCtrl
			// 
			this.cancelCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelCtrl.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.cancelCtrl.ForeColor = System.Drawing.SystemColors.MenuText;
			this.cancelCtrl.GlowColor = System.Drawing.Color.OrangeRed;
			this.cancelCtrl.Location = new System.Drawing.Point(478, 538);
			this.cancelCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.cancelCtrl.Name = "cancelCtrl";
			this.cancelCtrl.ShineColor = System.Drawing.SystemColors.MenuBar;
			this.cancelCtrl.Size = new System.Drawing.Size(99, 30);
			this.cancelCtrl.TabIndex = 4;
			this.cancelCtrl.Text = "CA&NCEL";
			this.cancelCtrl.Click += new System.EventHandler(this.CancelCtrlClick);
			// 
			// toolTip1
			// 
			this.toolTip1.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.toolTip1.ToolTipTitle = "Description";
			// 
			// showTreeCtrl
			// 
			this.showTreeCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.showTreeCtrl.BackColor = System.Drawing.Color.Transparent;
			this.showTreeCtrl.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
			this.showTreeCtrl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.showTreeCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.showTreeCtrl.ForeColor = System.Drawing.SystemColors.Control;
			this.showTreeCtrl.Image = ((System.Drawing.Image)(resources.GetObject("showTreeCtrl.Image")));
			this.showTreeCtrl.Location = new System.Drawing.Point(853, 2);
			this.showTreeCtrl.Margin = new System.Windows.Forms.Padding(4);
			this.showTreeCtrl.Name = "showTreeCtrl";
			this.showTreeCtrl.Size = new System.Drawing.Size(26, 25);
			this.showTreeCtrl.TabIndex = 15;
			this.toolTip2.SetToolTip(this.showTreeCtrl, "Subqueries Tree");
			this.showTreeCtrl.UseVisualStyleBackColor = false;
			this.showTreeCtrl.Click += new System.EventHandler(this.ShowTreeCtrlClick);
			// 
			// toolTip2
			// 
			this.toolTip2.AutoPopDelay = 9000;
			this.toolTip2.InitialDelay = 500;
			this.toolTip2.ReshowDelay = 100;
			// 
			// QueryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(879, 572);
			this.Controls.Add(this.showTreeCtrl);
			this.Controls.Add(this.cancelCtrl);
			this.Controls.Add(this.unionsCtrl);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.button3);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(150, 150);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "QueryForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Query constructor";
			this.Shown += new System.EventHandler(this.QueryFormShown);
			this.Activated += new System.EventHandler(this.QueryFormActivated);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.QueryFormFormClosed);
			this.MouseLeave += new System.EventHandler(this.QueryFormMouseLeave);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.ResumeLayout(false);
			this.TableRenameDelPanel.ResumeLayout(false);
			this.TemplatesPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.deleteTemplCtrl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.queryTreeCtrl.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.splitContainer8.Panel1.ResumeLayout(false);
			this.splitContainer8.Panel2.ResumeLayout(false);
			this.splitContainer8.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			this.splitContainer3.ResumeLayout(false);
			this.splitContainer4.Panel1.ResumeLayout(false);
			this.splitContainer4.Panel2.ResumeLayout(false);
			this.splitContainer4.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.splitContainer7.Panel1.ResumeLayout(false);
			this.splitContainer7.Panel2.ResumeLayout(false);
			this.splitContainer7.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tabPage5.ResumeLayout(false);
			this.splitContainer6.Panel1.ResumeLayout(false);
			this.splitContainer6.Panel2.ResumeLayout(false);
			this.splitContainer6.ResumeLayout(false);
			this.tabPage6.ResumeLayout(false);
			this.splitContainer5.Panel1.ResumeLayout(false);
			this.splitContainer5.Panel2.ResumeLayout(false);
			this.splitContainer5.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ToolTip toolTip2;
		private System.Windows.Forms.Button extractDbCtrl;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button expandAllJoinsCtrl;
		private System.Windows.Forms.Button deleteJoinCtrl;
		private System.Windows.Forms.TreeView tablesToJoinCtrl;
		private System.Windows.Forms.SplitContainer splitContainer8;
		private System.Windows.Forms.Button moveJoinDownCtrl;
		private System.Windows.Forms.Button upJoinCtrl;
		private sql_constructor.QueryJoinsCtrl JoinsCtrl;
		private System.Windows.Forms.Button colapseAllCtrl;
		private System.Windows.Forms.Button addTextConditionCtrl;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.SplitContainer splitContainer7;
		private System.Windows.Forms.TreeView tablesList;
		private System.Windows.Forms.Button deleteConditionCtrl;
		private System.Windows.Forms.Button addConditionCtrl;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ImageList buttons;
		private System.Windows.Forms.Button renameTablCtrl;
		private System.Windows.Forms.Button removeTablCtrl;
		private System.Windows.Forms.Panel TableRenameDelPanel;
		private sql_constructor.ConditionList conditionListCtrl;
		private System.Windows.Forms.Button deleteUnionCtrl;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TreeView queryTreeViewCtrl;
		private System.Windows.Forms.Panel queryTreeCtrl;
		private System.Windows.Forms.Button showTreeCtrl;
		private System.Windows.Forms.PictureBox deleteTemplCtrl;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button SaveVirtToTemplCtrl;
		private System.Windows.Forms.Button AddTemplateCtrl;
		private System.Windows.Forms.Panel TemplatesPanel;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button addUnionCtrl;
		private System.Windows.Forms.ListBox unionListCtrl;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ListView aliasListCtrl;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private sql_constructor.MyListView unialiasTableCtrl;
		private System.Windows.Forms.Label uParamCtrl;
		private System.Windows.Forms.Button addVirtCtrl;
		private System.Windows.Forms.SplitContainer splitContainer6;
		private System.Windows.Forms.ComboBox uniEditCtrl;
		private System.Windows.Forms.ListBox orderByCtrl;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TreeView orderAvalCtrl;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.SplitContainer splitContainer5;
		private Glass.GlassButton cancelCtrl;
		private System.Windows.Forms.Button VirtOpen;
		private System.Windows.Forms.TabControl unionsCtrl;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ListBox AggregateFldsCtrl;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TreeView AvalFields;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox GoupByCtrl;
		private System.Windows.Forms.SplitContainer splitContainer4;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button CollapseAll;
		private System.Windows.Forms.Button ExpandAll;
		private System.Windows.Forms.Button moveFields;
		private System.Windows.Forms.Button moveTables;
		private System.Windows.Forms.ImageList dbimg;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private Glass.GlassButton button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.TreeView tablesCtrl;
		private System.Windows.Forms.TreeView dbCtrl;
		private System.Windows.Forms.ListBox fieldsCtrl;
		private  Glass.GlassButton button3;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.SplitContainer splitContainer2;
//		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.CheckedListBox checkTemplatesCtrl;
		private  Glass.GlassButton button2;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
		
		
		
		void TableLayoutPanel1Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			
		}
		
		void ListBox1SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
		}
	}
	}
