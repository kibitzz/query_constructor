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
using SqlBuilderClasses;
using System.Text;


namespace sql_constructor
{
	/// <summary>
	/// Description of QueryTextBox.
	/// </summary>
	public partial class QueryTextBox : Form
	{
		int ROW ; // caret row position
		int COL;  // caret column position
		int prevTextLen; // unchanged text length, to check the block insertion (deletion)
		bool notTextChanged;  // flag that text in edit box changed by program (colored text assigned to), and no reaction needed
		bool updateText;
		string Qtext;
		string[] QtextA;
		PrevValGetter pevOpenBr;
		PrevValGetter prevClosBr;
		bool selChangedProgr;
		bool brUnmarked;
		bool changesPresent;
		
		QueryForm hostForm;
		
		public string text
		{
			get{ return Qtext;}
			set{ Qtext = value;
				this.richTextBox2.Text = Qtext;
			}
		}
		
		public QueryTextBox()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
		}
		
		public QueryTextBox(QueryForm f)
		{
			
			InitializeComponent();
			hostForm = f;
			updateText = false;
			changesPresent = false;
			ROW = 0;
			COL = 0;
			pevOpenBr  = new PrevValGetter();
			prevClosBr = new PrevValGetter();
			prevTextLen = -300;
		}
		
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (updateText)
			{
				hostForm.QtextA = QtextA;
			}
			
			base.OnFormClosing(e);
		}
		
		// color assignment functions
		Color GetColorByExpr(string s)
		{
			Color rez;
			switch (s)
			{
				case "O":
					rez = Color.MediumBlue;
					break;
				case "O.O":
					goto case "O";
				case "A":
					rez = Color.BlueViolet;
					break;
				case "B":
					rez = Color.DarkRed;
					break;
				case "C":
					rez = Color.DarkCyan;
					break;
				case "R":
					rez = Color.DarkCyan;
					break;
				case "S":
					rez = Color.Salmon;
					break;
				case "J":
					rez = Color.DarkCyan;
					break;
					
				default:
					rez = Color.Black;
					break;
			}
			
			return rez;
		}
		
		string GetStrColorByExpr(string s)
		{
			string rez;
			switch (s)
			{
				case "O":
					rez = @"\cf4 ";
					break;
				case "O.O":
					goto case "O";
				case "A":
					rez = @"\cf1 ";
					break;
				case "B":
					rez = @"\cf2 ";
					break;
				case "C":
					rez = @"\cf3 ";
					break;
				case "R":
					rez = @"\cf3 ";
					break;
				case "S":
					rez = @"\cf6 ";
					break;
				case "J":
					rez = @"\cf3 ";
					break;
				case "G":
					rez = @"\cf7 ";
					break;
					
				default:
					rez = @"\cf5 ";
					break;
			}
			
			return rez;
		}
		
		int GetBrPairPos(System.Windows.Forms.RichTextBox rtb, int pos, string br)
		{
			segmCoord rez;
			int row = ROW;
			int col = COL;
			rez.fin = 0; rez.start =0;
			
			string[] Lines = new string[rtb.Lines.Length+1];
			rtb.Lines.CopyTo(Lines, 0);
			
			int offset = 0;
			int open = 0;
			int close = 0;
			
			if   (br.EndsWith(")")) {offset = -1 * br.Length; close = 1;}
			if   (br.Contains("(")) {offset = br.Length - br.IndexOf("(") + 1; open = 1;}
			if (br.StartsWith("(")) {offset =  br.Length; open = 1;}
			if (br.StartsWith(")")) {offset = -1; close = 1;}

			int searchPos = pos + offset;
			string rowString;
			
			if(close == 1)
			{
				while ((open != close) && (row >= 0))
				{
					rowString= Lines[row];
					if(rowString == null){ searchPos--;	row--; continue;}
					
					int rowPos = (row == ROW) ? col + offset : rowString.Length -1;
					
					for(int i = rowPos; i >=0 ; i --)
					{
						if (rowString[i] == ')') {close ++;}
						if (rowString[i] == '(') {open ++;}
						if (open == close) {searchPos = searchPos +1; break;};
						searchPos = searchPos -1;
					}
					
					searchPos--;
					row--;
				}
			} else
				
				if(open == 1)
			{
				while ((open != close) && (row < Lines.Length))
				{
					rowString= Lines[row];
					
					int rowPos = (row == ROW) ? col + offset : 0;
					if(rowString == null){ searchPos++;	row++; continue;}
					
					for(int i = rowPos; i < rowString.Length; i ++)
					{
						if (rowString[i] == ')') {close ++;}
						if (rowString[i] == '(') {open ++;}
						if (open == close) {searchPos = searchPos -1; break;};
						searchPos++;
					}
					
					searchPos++;
					row++;
				}
			}

//			rez.start = searchPos;
//			rez.fin = 0;
			
			return searchPos;
		}
		
		// get caret coordinates by SelectionStart number
		segmCoord GetSelectionRowCol(System.Windows.Forms.RichTextBox rtb, int pos)
		{
			segmCoord rez;
			int row = 0;
			int col = 0;
			int templen = 0;
			rez.fin = 0; rez.start =0;
			
			string[] Lines = new string[rtb.Lines.Length+1];
			rtb.Lines.CopyTo(Lines, 0);
			
			while ((templen < (pos - 1)) && (row < Lines.Length))
			{
				templen+= Lines[row].Length;
				pos = pos -1;
				
				row++;
			}
			
			if ((templen != pos)&&(templen != (pos - 1)))
			{
				row -= 1;
			}
			
			if (Lines[row] == null)
			{
				return rez;
			}
			
			if ((templen == pos) || (templen == (pos - 1)))
			{
				col = 0;
				templen +=  Lines[row].Length;
			} else
			{
				col = templen - Lines[row].Length;
				col = pos - col;
			}

			ROW = row; // ROW ,COL is the coordinates inside lines array, returned values holds position in whole text
			COL = col;

			rez.start = templen - Lines[row].Length + row;
			if (rez.start < 0)
			{
				rez.start = pos+ row;
			}
			rez.fin = templen + row;
			
			return rez;
		}
		
		// whole text coloring
		void RichTextBox2TextChanged(object sender, EventArgs e)
		{
			char[] variousOp = QueryParser.variousOpGet;
			if ((((prevTextLen+100) < this.richTextBox2.Text.Length)
			     ||((prevTextLen-500) > this.richTextBox2.Text.Length))
			    && (!notTextChanged))
			{
				string rftHeader =@"{\rtf1\ansi\ansicpg1251\deff0{\fonttbl{\f0\fnil\fcharset204 Microsoft Sans Serif;}}";                                         //128  // 114   // 41  143 44    128
				rftHeader += @"{\colortbl ;\red138\green43\blue226;\red139\green0\blue0;\red0\green139\blue139;\red0\green0\blue205;\red0\green0\blue0;\red250\green90\blue160;\red250\green10\blue27;}";
				rftHeader += @"\viewkind4\uc1\pard\cf1\lang1049\f0\fs21";
				
				StringBuilder SB = new StringBuilder();
				
				notTextChanged = true;
				string RTF = "";
				
				#region init environment
				string[] uncoloredLines = new string[this.richTextBox2.Lines.Length+1];
				this.richTextBox2.Lines.CopyTo(uncoloredLines, 0);
				int SelStart = this.richTextBox2.SelectionStart;
				
//				string inserted = "";
//				if ((prevTextLen < this.richTextBox2.Text.Length) && !firstshow)
//				{
//					prevTextLen = this.richTextBox2.Text.Length - prevTextLen;
//					this.richTextBox2.SelectionStart = SelStart - prevTextLen;
//					this.richTextBox2.SelectionLength = prevTextLen;
//					inserted = this.richTextBox2.SelectedText;
//					uncoloredLines = new string[1];
//					uncoloredLines[0] = inserted;
//					this.richTextBox2.SelectedText ="zzxx";
//				}
//				firstshow = false;
				#endregion
				
				#region var
				bool comment= false;
				bool NextLinecomment= false;
				char prevSpec = ' ';
				string[] t;
				int wordCount = 0;
				#endregion
				
				foreach (string uncoloredText in uncoloredLines)
				{
					int opCou = -1;
					string exprMap = QueryParser.GetExpresionStruct(uncoloredText, out t, true); // third parameter save all space formatting of original text (if omitted spaces not including to exp map)
					string spaceBuffer = "";
//					wordCount += t.Length;
					
					for(int i=0; i < exprMap.Length; i++)
					{
						
						int index = Array.IndexOf(variousOp, exprMap[i]);
						
						#region output expr character or operand from array
						if (index != -1)
						{
							opCou++;
							#region coloring
							if (!comment)
							{
								SB.Append(GetStrColorByExpr(exprMap[i].ToString()));
							} else
							{
								SB.Append(GetStrColorByExpr("G"));
							}
							#endregion
							
							SB.Append( spaceBuffer + t[opCou]);
							spaceBuffer = "";
						} else
						{
							if (exprMap[i] == ' ')  // optimization: not drawing each space as separate colored substring
							{
								spaceBuffer += exprMap[i];
							} else
							{
								#region coloring
								if (!comment)
								{
									SB.Append(GetStrColorByExpr(""));
								}else
								{
									SB.Append(GetStrColorByExpr("G"));
								}
								#endregion
								SB.Append(spaceBuffer + exprMap[i].ToString());
								spaceBuffer = "";
								
								#region comment parsing
								if (!comment)
								{
									if ((prevSpec.ToString() + exprMap[i].ToString()) == "/*")
									{
										NextLinecomment = true;
										comment = true;
									}
									
									if ((prevSpec.ToString() + exprMap[i].ToString()) == "--")
									{
										NextLinecomment = false;
										comment = true;
									}
								}
								
								if (((prevSpec.ToString() + exprMap[i].ToString()) == "*/") && (NextLinecomment != false))
								{
									NextLinecomment = false;
									comment = false;
								}
								prevSpec = exprMap[i];
								
								#endregion
							}
						}
						#endregion
						
					}
					comment = NextLinecomment;
					SB.Append( @"\par ");
					wordCount += opCou +1;
				}
				RTF = SB.ToString();
				RTF = RTF.Substring(0, RTF.Length - 5); // delete last "\par "
				
//				if  (inserted != "")
//				{
//					int idx =this.richTextBox2.Rtf.IndexOf("zzxx");
//					this.richTextBox2.SelectedText ="";
//					this.richTextBox2.Rtf = this.richTextBox2.Rtf.Insert(idx, RTF);
//				} else
//				{
				this.richTextBox2.Rtf = rftHeader + RTF + "}";
//				}
				prevTextLen = this.richTextBox2.Text.Length;
				selChangedProgr = true;
				this.richTextBox2.SelectionStart = SelStart;
				notTextChanged = false;
				this.label1.Text = "Word count: "+wordCount.ToString();
				

			}
			
			prevTextLen = this.richTextBox2.Text.Length;
		}
		
		// one word (in edit) coloring
		void RichTextBox2KeyUp(object sender, KeyEventArgs e)
		{
			char c = (char)e.KeyValue; //32 , 8, 46
			if (((e.KeyValue == 32) || (e.KeyValue == 8) || (e.KeyValue == 46)) && (this.richTextBox2.Lines.Length >0))
			{
				c = 'a';
			}
			
			#region go to encloset position
			if( Math.Abs(pevOpenBr.Current - prevClosBr.Current) > 150)
			{
				if (pevOpenBr.Current == this.richTextBox2.SelectionStart )
				{
					selChangedProgr = true;
					this.richTextBox2.SelectionStart = prevClosBr.Current +1;
					return;
				}
				if (prevClosBr.Current == this.richTextBox2.SelectionStart )
				{
					selChangedProgr = true;
					this.richTextBox2.SelectionStart = pevOpenBr.Current +1;
					return;
				}
			}
			#endregion
			
			if ((e.KeyCode == Keys.Z)&&(e.Control))
			{
				this.richTextBox2.Undo();
			}
			
			if ((Char.IsLetterOrDigit(c) == false) || (e.Control))
			{
				return;
			}
			
			if ((this.richTextBox2.ReadOnly) || (this.richTextBox2.Text.Length < 3))
			{
				return;
			}
			
			if(!brUnmarked)
			{
				UnmarkBr();
			}
			
			
			this.richTextBox2.SelectionColor = Color.Black;
			segmCoord rez;
			rez = GetSelectionRowCol(this.richTextBox2, this.richTextBox2.SelectionStart);
			int oldSelStart = this.richTextBox2.SelectionStart;
			
			segmCoord substrToUpdate;
			string procRow = this.richTextBox2.Lines[ROW];
			substrToUpdate = QueryParser.GetSubstrSegmByCharNum(procRow, COL);
			
			
			if ((substrToUpdate.fin ==0) &&(substrToUpdate.start ==0))
			{
				return;
			}
			selChangedProgr = true;
			this.richTextBox2.SelectionStart = rez.start + substrToUpdate.start;
			selChangedProgr = true;
			int len = (substrToUpdate.fin - substrToUpdate.start +1);
			len = (procRow.Length >len) ? len : len-1;
			this.richTextBox2.SelectionLength = len;
			
			string parseSubstr = procRow.Substring(substrToUpdate.start, len);
			string[] t;
			string exprType = QueryParser.GetExpresionStruct(QueryParser.TrimBrackets(parseSubstr), out t);
			
			
			this.richTextBox2.SelectionColor = GetColorByExpr(exprType);
			this.richTextBox2.SelectionBackColor = Color.White;
			selChangedProgr = true;
			this.richTextBox2.SelectedText = parseSubstr;
			selChangedProgr = true;
			this.richTextBox2.SelectionStart = oldSelStart;
		}
		
		//     Edit / Save
		void Button1Click(object sender, EventArgs e)
		{
			if (this.richTextBox2.ReadOnly)
			{
				changesPresent = true;
				this.button1.Text = "S a v e";
				this.richTextBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
			} else
			{
				this.button1.Text = "E d i t";
				updateText = true;
				QtextA = this.richTextBox2.Lines;
				this.richTextBox2.BackColor = System.Drawing.SystemColors.ControlLight;
			}
			this.richTextBox2.ReadOnly = ! this.richTextBox2.ReadOnly;
			this.richTextBox2.Focus();
		}
		//     Save & Close
		void Button2Click(object sender, EventArgs e)
		{
			updateText = changesPresent;
			QtextA = this.richTextBox2.Lines;
			this.Close();
		}
		//     initial coloring
		void QueryTextBoxShown(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(text.Trim()))
			{
				Button1Click(null, null);
			} else
			{
				RichTextBox2TextChanged(null, null);
			}
		}
		//     Copy all to Clipboard
		void Button3Click(object sender, EventArgs e)
		{
			this.richTextBox2.SelectAll();
			this.richTextBox2.Copy();
			this.richTextBox2.DeselectAll();
		}
		//     Escape and undo
		void Button4Click(object sender, EventArgs e)
		{
			updateText = false;
			this.Close();
		}
		
		void RichTextBox2RegionChanged(object sender, EventArgs e)
		{
		}
		
		void UnmarkBr()
		{
//			this.richTextBox2.SuspendLayout();
			
			if ((pevOpenBr.Current != pevOpenBr.Previous) && (pevOpenBr.isBothEqual))
			{
				int oldSelStart = this.richTextBox2.SelectionStart;
				
//				selChangedProgr = true;
//				this.richTextBox2.SelectionStart = pevOpenBr.Previous;
//				selChangedProgr = true;
//				this.richTextBox2.SelectionLength = 1;
//				this.richTextBox2.SelectionBackColor = this.richTextBox2.BackColor;
//				selChangedProgr = true;
//				this.richTextBox2.SelectionLength = 0;
				selChangedProgr = true;
				this.richTextBox2.SelectionStart = pevOpenBr.Current;
				selChangedProgr = true;
				this.richTextBox2.SelectionLength = 1;
				this.richTextBox2.SelectionBackColor = this.richTextBox2.BackColor;
				selChangedProgr = true;
				this.richTextBox2.SelectionLength = 0;
				selChangedProgr = true;
				this.richTextBox2.SelectionStart = oldSelStart;
				
//				if(prevClosBr.Previous >= 0)
//				{
//					selChangedProgr = true;
//					this.richTextBox2.SelectionStart = prevClosBr.Previous;
//					selChangedProgr = true;
//					this.richTextBox2.SelectionLength = 1;
//					this.richTextBox2.SelectionBackColor = this.richTextBox2.BackColor;
//					selChangedProgr = true;
//					this.richTextBox2.SelectionLength = 0;
//				}
				if(prevClosBr.Current >= 0)
				{
					selChangedProgr = true;
					this.richTextBox2.SelectionStart = prevClosBr.Current;
					selChangedProgr = true;
					this.richTextBox2.SelectionLength = 1;
					this.richTextBox2.SelectionBackColor = this.richTextBox2.BackColor;
					selChangedProgr = true;
					this.richTextBox2.SelectionLength = 0;
					selChangedProgr = true;
					this.richTextBox2.SelectionStart = oldSelStart;
				}
			}
//			this.richTextBox2.ResumeLayout();
			brUnmarked = true;
		}
		
		void RichTextBox2SelectionChanged(object sender, EventArgs e)
		{
			GetSelectionRowCol(this.richTextBox2, this.richTextBox2.SelectionStart);
			rowcolCtrl.Text = "Ln "+ (ROW +1).ToString() +" col "+  (COL +1).ToString();
			
			if ((!selChangedProgr) && (this.richTextBox2.SelectionLength ==0 ))
			{
				segmCoord rez;
				rez = GetSelectionRowCol(this.richTextBox2, this.richTextBox2.SelectionStart);
				int oldSelStart = this.richTextBox2.SelectionStart;
				
				if (ROW >= this.richTextBox2.Lines.Length) {return;};
				segmCoord substrToUpdate;
				substrToUpdate = QueryParser.GetSubstrSegmByCharNum(this.richTextBox2.Lines[ROW], COL);
				
				string parseSubstr ="";
				if ((substrToUpdate.fin ==0) &&(substrToUpdate.start ==0))
				{
					parseSubstr = this.richTextBox2.Lines[ROW];
					parseSubstr = (string.IsNullOrEmpty(parseSubstr.Trim())) ? "###" : parseSubstr;
				}
				char[] abr = {'(', ')'};
				parseSubstr = (string.IsNullOrEmpty(parseSubstr.Trim())) ? this.richTextBox2.Lines[ROW].Substring(substrToUpdate.start, (substrToUpdate.fin - substrToUpdate.start +1)) : parseSubstr.Trim();
				parseSubstr = (QueryParser.EnumCharOccurrence(parseSubstr,  abr) > 1) ? "" : parseSubstr;
				if ((parseSubstr.EndsWith(")")) || (parseSubstr.StartsWith("(")) || (parseSubstr.StartsWith(")")) ||(parseSubstr.Contains("(")))
				{
					if (!brUnmarked)
					{
						UnmarkBr();
					}
					
					brUnmarked = false; // draving new, so mark is present
					pevOpenBr.Current = oldSelStart - 1;
					
					#region coloring
					
					selChangedProgr = true;
					this.richTextBox2.SelectionStart = oldSelStart - 1;
					selChangedProgr = true;
					this.richTextBox2.SelectionLength = 1;
					this.richTextBox2.SelectionBackColor = Color.Orange;
					selChangedProgr = true;
					this.richTextBox2.SelectionLength = 0;
					selChangedProgr = true;
					this.richTextBox2.SelectionStart = oldSelStart;
					
//					selChangedProgr = true;
//					this.richTextBox2.SelectionLength = 0;
					
					int enclose = GetBrPairPos(this.richTextBox2, oldSelStart, parseSubstr);
					prevClosBr.Current = enclose-1;
					if(enclose >= 0)
					{
						selChangedProgr = true;
						this.richTextBox2.SelectionStart = enclose -1;
						selChangedProgr = true;
						this.richTextBox2.SelectionLength = 1;
						this.richTextBox2.SelectionBackColor = Color.Orange;
						selChangedProgr = true;
						this.richTextBox2.SelectionLength = 0;
						selChangedProgr = true;
						this.richTextBox2.SelectionStart = oldSelStart;
					} else
					{MessageBox.Show(" Enclosing bracket not found ! \n Error raise when parse such text");}
					
					#endregion
				}
				
			}
			selChangedProgr = false;
		}
	}
}
