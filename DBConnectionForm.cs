/*
 * Created by SharpDevelop.
 * User: Proskochilo_I_Y
 * Date: 11.06.2010
 * Time: 14:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;


namespace sql_constructor
{
	/// <summary>
	/// Description of DBConnectionForm.
	/// </summary>
	public partial class DBConnectionForm : Form
	{
		QueryForm hostForm;
		string Provider;
		string DataSource;
		string UserId;
		string Password;
		
		public DBConnectionForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
						
		}
		
		public DBConnectionForm(QueryForm f)
		{
			
			InitializeComponent();
			hostForm = f;
					
			SqlBuilderClasses.DBEngine.GetOleDbProviersInfo();
			for(int i = 0; i < SqlBuilderClasses.DBEngine.provCount; i++)
			{
				oleProvCtrl.Items.Add(SqlBuilderClasses.DBEngine.ProvNames[i]);
			}	
			
			UpdateConnStrBox();									
		}
		
		// form closing: save recently used connection strings
		//        protected override void OnFormClosing(FormClosingEventArgs e)
		//        {
		////            hostForm.connection = this.ConnStrBox.Text;
		//            base.OnFormClosing(e);
		//        }
		
		void UpdateConnStrBox()
		{			
			this.ConnStrBox.Text = "Provider=" + Provider+ ";Data Source=" + DataSource+ ";User Id=" + UserId+ ";Password=" + Password+ ";";
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			hostForm.connection = this.ConnStrBox.Text;
			this.Close();
		}
		
		void Label1Click(object sender, EventArgs e)
		{
		
		}
		
		void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process linkLabelProc= new Process();
			linkLabelProc.StartInfo.FileName = @"iexplore.exe";
			linkLabelProc.StartInfo.Arguments = "http://www.connectionstrings.com/";//e.Link.LinkData.ToString();
			linkLabelProc.Start();
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void OleProvCtrlMouseDoubleClick(object sender, MouseEventArgs e)
		{
			Provider = SqlBuilderClasses.DBEngine.ProvAlias[oleProvCtrl.SelectedIndex];
			UpdateConnStrBox();		
		}
		
		void Label4Click(object sender, EventArgs e)
		{
			
		}
		
		void SuorceCtrlTextChanged(object sender, EventArgs e)
		{
			DataSource = suorceCtrl.Text;
			UpdateConnStrBox();	
		}
		
		void UsrNameCtrlTextChanged(object sender, EventArgs e)
		{
			UserId = usrNameCtrl.Text;
			UpdateConnStrBox();	
		}
		
		void PwdCtrlTextChanged(object sender, EventArgs e)
		{
			Password = pwdCtrl.Text;
			UpdateConnStrBox();	
		}
	}
}
