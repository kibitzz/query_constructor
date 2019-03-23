/*
 * Created by SharpDevelop.
 * User: igor_only
 * Date: 08.10.2010
 * Time: 13:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace sql_constructor
{
	partial class JoinCtrl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
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
			this.conditionCtrlz = new sql_constructor.ConditionList();
			this.tablenameCtrl = new System.Windows.Forms.Label();
			this.JoinTypeCtrl = new System.Windows.Forms.ComboBox();
			this.outerKeyz = new System.Windows.Forms.TextBox();
			this.shrinkCtrl = new System.Windows.Forms.Label();
			this.conditionTicket = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// conditionCtrlz
			// 
			this.conditionCtrlz.AutoScroll = true;
			this.conditionCtrlz.BackColor = System.Drawing.Color.White;
			this.conditionCtrlz.conditionz = new SqlBuilderClasses.condition[0];
			this.conditionCtrlz.ForeColor = System.Drawing.SystemColors.ScrollBar;
			this.conditionCtrlz.Location = new System.Drawing.Point(12, 42);
			this.conditionCtrlz.Name = "conditionCtrlz";
			this.conditionCtrlz.selectedItem = null;
			this.conditionCtrlz.Size = new System.Drawing.Size(388, 81);
			this.conditionCtrlz.TabIndex = 4;
			this.conditionCtrlz.activate += new System.Windows.Forms.MethodInvoker(this.SetFocusinList);
			// 
			// tablenameCtrl
			// 
			this.tablenameCtrl.AutoSize = true;
			this.tablenameCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tablenameCtrl.ForeColor = System.Drawing.Color.DarkGreen;
			this.tablenameCtrl.Location = new System.Drawing.Point(110, 3);
			this.tablenameCtrl.Name = "tablenameCtrl";
			this.tablenameCtrl.Size = new System.Drawing.Size(43, 16);
			this.tablenameCtrl.TabIndex = 1;
			this.tablenameCtrl.Text = "table";
			this.tablenameCtrl.Click += new System.EventHandler(this.TablenameCtrlClick);
			this.tablenameCtrl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TablenameCtrlMouseDoubleClick);
			// 
			// JoinTypeCtrl
			// 
			this.JoinTypeCtrl.BackColor = System.Drawing.SystemColors.Control;
			this.JoinTypeCtrl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.JoinTypeCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.JoinTypeCtrl.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.JoinTypeCtrl.ForeColor = System.Drawing.Color.DarkGreen;
			this.JoinTypeCtrl.FormattingEnabled = true;
			this.JoinTypeCtrl.Items.AddRange(new object[] {
									"    JOIN",
									"INNER JOIN",
									"   LEFT JOIN",
									"RIGHT JOIN"});
			this.JoinTypeCtrl.Location = new System.Drawing.Point(12, -2);
			this.JoinTypeCtrl.Name = "JoinTypeCtrl";
			this.JoinTypeCtrl.Size = new System.Drawing.Size(96, 24);
			this.JoinTypeCtrl.TabIndex = 2;
			this.JoinTypeCtrl.TabStop = false;
			this.JoinTypeCtrl.TextChanged += new System.EventHandler(this.JoinTypeCtrlTextChanged);
			// 
			// outerKeyz
			// 
			this.outerKeyz.BackColor = System.Drawing.SystemColors.Control;
			this.outerKeyz.Cursor = System.Windows.Forms.Cursors.Default;
			this.outerKeyz.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.outerKeyz.HideSelection = false;
			this.outerKeyz.Location = new System.Drawing.Point(56, 21);
			this.outerKeyz.Name = "outerKeyz";
			this.outerKeyz.ReadOnly = true;
			this.outerKeyz.Size = new System.Drawing.Size(346, 22);
			this.outerKeyz.TabIndex = 1;
			this.outerKeyz.Text = "First | second | third | fourth | fifth | six | eight ";
			this.outerKeyz.MouseLeave += new System.EventHandler(this.OuterKeyzMouseLeave);
			this.outerKeyz.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TextBox1MouseMove);
			this.outerKeyz.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OuterKeyzMouseDoubleClick);
			this.outerKeyz.Enter += new System.EventHandler(this.OuterKeyzEnter);
			// 
			// shrinkCtrl
			// 
			this.shrinkCtrl.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.shrinkCtrl.BackColor = System.Drawing.SystemColors.Control;
			this.shrinkCtrl.Location = new System.Drawing.Point(376, 3);
			this.shrinkCtrl.Name = "shrinkCtrl";
			this.shrinkCtrl.Size = new System.Drawing.Size(21, 15);
			this.shrinkCtrl.TabIndex = 4;
			this.shrinkCtrl.Text = "<<";
			this.shrinkCtrl.Click += new System.EventHandler(this.ShrinkCtrlClick);
			// 
			// conditionTicket
			// 
			this.conditionTicket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.conditionTicket.BackColor = System.Drawing.Color.Transparent;
			this.conditionTicket.Location = new System.Drawing.Point(333, 3);
			this.conditionTicket.Name = "conditionTicket";
			this.conditionTicket.Size = new System.Drawing.Size(37, 17);
			this.conditionTicket.TabIndex = 5;
			// 
			// JoinCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.conditionTicket);
			this.Controls.Add(this.shrinkCtrl);
			this.Controls.Add(this.outerKeyz);
			this.Controls.Add(this.JoinTypeCtrl);
			this.Controls.Add(this.tablenameCtrl);
			this.Controls.Add(this.conditionCtrlz);
			this.Name = "JoinCtrl";
			this.Size = new System.Drawing.Size(415, 150);
			this.Load += new System.EventHandler(this.JoinCtrlLoad);
			this.BackColorChanged += new System.EventHandler(this.JoinCtrlBackColorChanged);
			this.SizeChanged += new System.EventHandler(this.JoinCtrlSizeChanged);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label conditionTicket;
		private System.Windows.Forms.Label shrinkCtrl;
		private System.Windows.Forms.TextBox outerKeyz;
		private System.Windows.Forms.ComboBox JoinTypeCtrl;
		private System.Windows.Forms.Label tablenameCtrl;
		private sql_constructor.ConditionList conditionCtrlz;
	}
}
