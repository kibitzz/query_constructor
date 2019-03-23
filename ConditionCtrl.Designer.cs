/*
 * Created by SharpDevelop.
 * User: igor_only
 * Date: 14.09.2010
 * Time: 14:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace sql_constructor
{
	partial class ConditionCtrl
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
			this.typeBox = new System.Windows.Forms.ComboBox();
			this.notFlag = new System.Windows.Forms.Label();
			this.operand1 = new System.Windows.Forms.TextBox();
			this.operand2 = new System.Windows.Forms.TextBox();
			this.operand3 = new System.Windows.Forms.TextBox();
			this.urestSwitch = new System.Windows.Forms.Label();
			this.basicEquality = new System.Windows.Forms.Panel();
			this.uresTextCtrl = new System.Windows.Forms.TextBox();
			this.warningCtrl = new System.Windows.Forms.Label();
			this.selfDelCtrl = new System.Windows.Forms.Label();
			this.basicEquality.SuspendLayout();
			this.SuspendLayout();
			// 
			// typeBox
			// 
			this.typeBox.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.typeBox.BackColor = System.Drawing.SystemColors.Window;
			this.typeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			this.typeBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.typeBox.FormattingEnabled = true;
			this.typeBox.Items.AddRange(new object[] {
									"   =",
									"   >",
									"   <",
									"   >=",
									"   <=",
									"   <>",
									"   !>",
									"   !<",
									"   !=",
									"BETWEEN",
									"   LIKE",
									"   IN",
									"IS NULL"});
			this.typeBox.Location = new System.Drawing.Point(67, -1);
			this.typeBox.MaxDropDownItems = 16;
			this.typeBox.Name = "typeBox";
			this.typeBox.Size = new System.Drawing.Size(76, 23);
			this.typeBox.TabIndex = 6;
			this.typeBox.TextChanged += new System.EventHandler(this.TypeBoxTextChanged);
			// 
			// notFlag
			// 
			this.notFlag.ForeColor = System.Drawing.Color.LightGray;
			this.notFlag.Location = new System.Drawing.Point(14, 8);
			this.notFlag.Name = "notFlag";
			this.notFlag.Size = new System.Drawing.Size(33, 12);
			this.notFlag.TabIndex = 1;
			this.notFlag.Text = "not";
			this.notFlag.Click += new System.EventHandler(this.NotFlagClick);
			// 
			// operand1
			// 
			this.operand1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.operand1.Location = new System.Drawing.Point(3, 1);
			this.operand1.Name = "operand1";
			this.operand1.Size = new System.Drawing.Size(100, 22);
			this.operand1.TabIndex = 2;
			this.operand1.TextChanged += new System.EventHandler(this.Operand1TextChanged);
			this.operand1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Operand1MouseMove);
			this.operand1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Operand1MouseDown);
			this.operand1.Enter += new System.EventHandler(this.Operand1Enter);
			this.operand1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Operand1MouseUp);
			// 
			// operand2
			// 
			this.operand2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.operand2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.operand2.Location = new System.Drawing.Point(107, 1);
			this.operand2.Name = "operand2";
			this.operand2.Size = new System.Drawing.Size(100, 22);
			this.operand2.TabIndex = 3;
			this.operand2.TextChanged += new System.EventHandler(this.Operand2TextChanged);
			this.operand2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Operand1MouseMove);
			this.operand2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Operand1MouseUp);
			// 
			// operand3
			// 
			this.operand3.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.operand3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.operand3.Location = new System.Drawing.Point(263, 1);
			this.operand3.Name = "operand3";
			this.operand3.Size = new System.Drawing.Size(76, 22);
			this.operand3.TabIndex = 4;
			this.operand3.TextChanged += new System.EventHandler(this.Operand3TextChanged);
			this.operand3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Operand1MouseMove);
			this.operand3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Operand1MouseUp);
			// 
			// urestSwitch
			// 
			this.urestSwitch.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.urestSwitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.urestSwitch.ForeColor = System.Drawing.Color.LightGray;
			this.urestSwitch.Location = new System.Drawing.Point(476, 1);
			this.urestSwitch.Name = "urestSwitch";
			this.urestSwitch.Size = new System.Drawing.Size(21, 21);
			this.urestSwitch.TabIndex = 6;
			this.urestSwitch.Text = "T";
			this.urestSwitch.MouseLeave += new System.EventHandler(this.UrestSwitchMouseLeave);
			this.urestSwitch.Click += new System.EventHandler(this.UrestSwitchClick);
			this.urestSwitch.MouseEnter += new System.EventHandler(this.UrestSwitchMouseEnter);
			// 
			// basicEquality
			// 
			this.basicEquality.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.basicEquality.BackColor = System.Drawing.Color.Transparent;
			this.basicEquality.Controls.Add(this.typeBox);
			this.basicEquality.Controls.Add(this.operand1);
			this.basicEquality.Controls.Add(this.operand2);
			this.basicEquality.Controls.Add(this.operand3);
			this.basicEquality.Location = new System.Drawing.Point(39, -3);
			this.basicEquality.Name = "basicEquality";
			this.basicEquality.Size = new System.Drawing.Size(306, 23);
			this.basicEquality.TabIndex = 7;
			this.basicEquality.SizeChanged += new System.EventHandler(this.BasicEqualitySizeChanged);
			// 
			// uresTextCtrl
			// 
			this.uresTextCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.uresTextCtrl.Location = new System.Drawing.Point(351, -2);
			this.uresTextCtrl.Name = "uresTextCtrl";
			this.uresTextCtrl.Size = new System.Drawing.Size(100, 22);
			this.uresTextCtrl.TabIndex = 8;
			this.uresTextCtrl.TextChanged += new System.EventHandler(this.UresTextCtrlTextChanged);
			this.uresTextCtrl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Operand1MouseMove);
			this.uresTextCtrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Operand1MouseUp);
			// 
			// warningCtrl
			// 
			this.warningCtrl.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.warningCtrl.ForeColor = System.Drawing.Color.GreenYellow;
			this.warningCtrl.Location = new System.Drawing.Point(3, 3);
			this.warningCtrl.Name = "warningCtrl";
			this.warningCtrl.Size = new System.Drawing.Size(12, 16);
			this.warningCtrl.TabIndex = 9;
			this.warningCtrl.TextChanged += new System.EventHandler(this.WarningCtrlTextChanged);
			this.warningCtrl.Click += new System.EventHandler(this.WarningCtrlClick);
			// 
			// selfDelCtrl
			// 
			this.selfDelCtrl.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.selfDelCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.selfDelCtrl.ForeColor = System.Drawing.Color.LightGray;
			this.selfDelCtrl.Location = new System.Drawing.Point(512, 2);
			this.selfDelCtrl.Name = "selfDelCtrl";
			this.selfDelCtrl.Size = new System.Drawing.Size(21, 21);
			this.selfDelCtrl.TabIndex = 10;
			this.selfDelCtrl.Text = "X";
			this.selfDelCtrl.MouseLeave += new System.EventHandler(this.UrestSwitchMouseLeave);
			this.selfDelCtrl.Click += new System.EventHandler(this.SelfDelCtrlClick);
			this.selfDelCtrl.MouseEnter += new System.EventHandler(this.SelfDelCtrlMouseEnter);
			// 
			// ConditionCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.selfDelCtrl);
			this.Controls.Add(this.warningCtrl);
			this.Controls.Add(this.uresTextCtrl);
			this.Controls.Add(this.basicEquality);
			this.Controls.Add(this.urestSwitch);
			this.Controls.Add(this.notFlag);
			this.Name = "ConditionCtrl";
			this.Size = new System.Drawing.Size(543, 20);
			this.VisibleChanged += new System.EventHandler(this.ConditionCtrlVisibleChanged);
			this.Leave += new System.EventHandler(this.ConditionCtrlLeave);
			this.Enter += new System.EventHandler(this.ConditionCtrlEnter);
			this.SizeChanged += new System.EventHandler(this.ConditionCtrlSizeChanged);
			this.basicEquality.ResumeLayout(false);
			this.basicEquality.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label selfDelCtrl;
		private System.Windows.Forms.Label warningCtrl;
		private System.Windows.Forms.TextBox uresTextCtrl;
		private System.Windows.Forms.Panel basicEquality;
		private System.Windows.Forms.TextBox operand3;
		private System.Windows.Forms.TextBox operand1;
		private System.Windows.Forms.TextBox operand2;
		private System.Windows.Forms.Label urestSwitch;
		private System.Windows.Forms.ComboBox typeBox;
		private System.Windows.Forms.Label notFlag;
	}
}
