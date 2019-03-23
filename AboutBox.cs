
using System;
using System.Drawing;
using System.Windows.Forms;

namespace sql_constructor
{

//	http://www.sharpdevelop.net/OpenSource/SD/
	public partial class AboutBox : Form
	{
		Game gm;
		
		public AboutBox()
		{
			
			InitializeComponent();
			
			gm = new Game(skoba);
			
			gameBox.Image = Zagogulina.Image();
		}
		
		
		void TextBox1MouseEnter(object sender, EventArgs e)
		{
			textBox1.SelectionStart = 0;
			textBox1.SelectionLength = textBox1.Text.Length;
		}
		
		void TextBox1MouseLeave(object sender, EventArgs e)
		{
			textBox1.SelectionLength = 0;
		}
		
		void TextBox1MouseDown(object sender, MouseEventArgs e)
		{
			textBox1.Copy();
		}
		
		
		void TextBox1KeyDown(object sender, KeyEventArgs e)
		{
			switch(e.KeyData)
			{
				case Keys.Up:
					gm.MoveDot(1);
					break;
				case Keys.Down:
					gm.MoveDot(2);
					break;
				case Keys.Left:
					gm.MoveDot(3);
					break;
				case Keys.Right:
					gm.MoveDot(4);
					break;
			}
			
			if(gm.lvl>5)
			{
				gameBox.Image = winImg.Images[0];
			} else
			{
				gameBox.Image = Zagogulina.Image();
			}
			
		}
		
		void ReloadClick(object sender, EventArgs e)
		{
			gm.ReLoadLvl();
			gameBox.Image = Zagogulina.Image();
		}
	}
	
	
	public class Zagogulina
	{
		public static Rectangle Battlefield;
		public static ImageList skoba;
		public static Point[] Participant;
		public static Zagogulina[] Milieu;
		public static Rectangle tgt;
		public static int idx;
		public static int LockId;
		
		int itsIdx;
		int hostIdx;
		int childIdx;
		
		int x;
		int y;
		bool posInited;
		public bool isRed;
		
		public Point pos
		{
			
			get{return new Point(x, y);}
			set
			{
				if (posInited)
				{
					ExecMove(value);
				} else
				{
					x = value.X;
					y = value.Y;
					
					Participant[itsIdx] = this.pos;
					posInited = true;
				}
			}
		}
		string t;
		public string type
		{
			get{return t;}
			set
			{
				t = value;
				
				switch(value)
				{
					case "o":
						top  = true;
						left = true;
						right= true;
						bottom= true;
						sizeType = 0;
						break;
						
					case "c":
						top  = true;
						left = true;
						right= false;
						bottom= true;
						break;
					case "u":
						top  = false;
						left = true;
						right= true;
						bottom= true;
						break;
					case "n":
						top  = true;
						left = true;
						right= true;
						bottom= false;
						break;
					case "bc":
						top  = true;
						left = false;
						right= true;
						bottom= true;
						break;
				}
			}
		}
		
		public int sizeType;
		
		bool top;
		bool left;
		bool right;
		bool bottom;
		
		void InitMilieu()
		{
			isRed = false;
			posInited = false;
			itsIdx = idx;
			Milieu[itsIdx] = this;
			idx++;
			hostIdx = -1;
			childIdx= -1;
			sizeType = 2;
		}
		
		public Zagogulina()
		{
			InitMilieu();
		}
		
		public Zagogulina( Point p, string t,  bool red)
		{
			InitMilieu();
			pos = p;
			type = t;
			isRed = red;
			
		}
		
		public Point GetMovedPos(int to)
		{
			int ex = x;
			int ey = y;
			
			switch(to)
			{
				case 1: // up
					ey = (top)? y-1 : y;
					break;
				case 2: // down
					ey = (bottom)? y+1 : y;
					break;
				case 3: // left
					ex = (left)? x-1 : x;
					break;
				case 4: // right
					ex = (right)? x+1 : x;
					break;
			}
			
			return new Point(ex, ey);
		}
		
		void ExecMove(Point newPos)
		{
			if((Battlefield.Contains(newPos)) && (CanMoveTo(newPos)))
			{
				x = newPos.X;
				y = newPos.Y;
				
				if(hostIdx != -1)
				{
					Milieu[hostIdx].pos = newPos;
				}
				
				if(childIdx != -1)
				{
					Milieu[childIdx].x =  newPos.X;
					Milieu[childIdx].y = newPos.Y;
					Participant[childIdx] = newPos;
				}
				
				int[] findArr = GetAllAtPos(newPos); ;
				
				if((findArr.Length == 2) && (sizeType == 0))
				{
					int maxZ = (Milieu[findArr[0]].sizeType > Milieu[findArr[1]].sizeType) ? findArr[0] :findArr[1];
					
					if ((Milieu[findArr[0]].isRed) && (Milieu[findArr[1]].isRed))
					{
						Milieu[maxZ].Lock();
					}
				}
				
				Participant[itsIdx] = newPos;
			}
		}
		
		/// <param name="to"> 1:  up  2: down 3: left  4: right</param>
		public void Move(int to)
		{
			ExecMove(GetMovedPos(to));
			Participant[itsIdx] = this.pos;
		}
		
		int[] GetAllAtPos(Point p)
		{
			int[] findArr = new int[3];
			int findCou = 0;
			
			int find = Array.IndexOf(Participant, p);
			while(find != -1)
			{
				if(find != itsIdx)
				{
					findArr[findCou] = find;
					findCou++;
				}
				
				find = Array.IndexOf(Participant, p, find+1);
			}
			
			Array.Resize(ref findArr, findCou);
			return findArr;
		}
		
		bool CanMoveTo(Point newPos)
		{
			bool rez = true;
			int[] findArr ;
			
			// can move inside Zagogulina placed at new pos
			findArr = GetAllAtPos(newPos); // new probable hosts array
			foreach(int i in findArr)
			{
				if((Milieu[i].sizeType > this.sizeType))
				{
					rez = rez ? Milieu[i].OpenSide(this.pos) : false;
				} else
					rez = false;
			}
			
			hostIdx  = -1;
			childIdx = -1;
			if(rez) // check host ability to move
			{
				findArr = GetAllAtPos(this.pos); // current host/child array
				foreach(int i in findArr)
				{
					if ((Milieu[i].sizeType > this.sizeType) && (!Milieu[i].OpenSide(newPos))) //can not leave current place without move host Zagogulina
					{
						rez = rez ? Milieu[i].CanMoveTo(newPos) : false;
						hostIdx = rez ? i : -1;
						break; // Milieu[i] chech other bigger items
					}
					
					if ((Milieu[i].sizeType >= 1) && (Milieu[i].sizeType < this.sizeType) && (!Milieu[i].OpenOppositeSide(newPos))) //can not leave current place without move host Zagogulina
					{
						childIdx = i;
					}
					
					
				}
			}
			
			
			return rez;
			
		}
		
		public bool OpenSide(Point outPos)
		{
			bool rez = false;
			
			if(outPos.X == x)
			{
				rez = !((outPos.Y > y) ? bottom : top);
			}
			
			if(outPos.Y == y)
			{
				rez = !((outPos.X > x) ? right : left);
			}
			
			return rez;
		}
		
		public bool OpenOppositeSide(Point outPos)
		{
			bool rez = false;
			
			if(outPos.X == x)
			{
				rez = !((outPos.Y > y) ? top : bottom);
			}
			
			if(outPos.Y == y)
			{
				rez = !((outPos.X > x) ? left : right);
			}
			
			return rez;
		}
		
		void Lock()
		{
			top  = true;
			left = true;
			right= true;
			bottom= true;
			LockId= itsIdx;
		}
		
		public void Draw(Graphics g)
		{
			
			int imgId = 0;
			RotateFlipType rot;
			rot = RotateFlipType.RotateNoneFlipNone;
			
			switch(t)
			{

				case "u":
					rot = RotateFlipType.Rotate270FlipNone;
					imgId = 2 *sizeType;
					
					break;
				case "n":
					rot = RotateFlipType.Rotate90FlipNone;
					imgId = 3 *sizeType;
					
					break;
				case "bc":
					rot = RotateFlipType.Rotate180FlipNone;
					
					break;
			}
			
			Image img = skoba.Images[sizeType + (isRed ? 2 : 0)];
			img.RotateFlip(rot);
			
			Point dpt = new Point(x * 32 +3, y *32 +3);
			g.DrawImage(img, dpt);
		}
		
		public static Bitmap Image()
		{
			Bitmap b = new Bitmap(Battlefield.Width *32 +6,  Battlefield.Height *32 +6);
			Graphics g = Graphics.FromImage(b);
			
			foreach(Zagogulina z in Milieu)
			{
				z.Draw(g);
			}
			
			g.DrawRectangle(Pens.DarkGreen, new Rectangle(0, 0, b.Width -2, b.Height -2));
			g.DrawRectangle(Pens.Aqua, tgt);
			
			return b;
		}
		
	}
	
	public class Game
	{
		public Zagogulina Dot;
		Point Target;
		Point FinPos;
		public int lvl;
		

		public Game(ImageList imgList)
		{
			Zagogulina.skoba = imgList;
			Level1();
			lvl = 1;
		}
		
		public void ReLoadLvl()
		{
			LoadLvl(lvl);
		}
		
		void LoadLvl(int l)
		{
			switch(l)
			{
				case 1:
					Level1();
					break;
				case 2:
					Level2();
					break;
				case 3:
					Level3();
					break;
				case 4:
					Level4();
					break;
				case 5:
					Level5();
					break;
			}
		}
		
		public void MoveDot(int to)
		{
			Dot.Move(to);
			
			if(Zagogulina.LockId != -1)
			{
				if(Dot.pos == FinPos)
				{
					NextLevel();
				}
			}
		}
		
		void Reset(int objCou)
		{
			Zagogulina.idx = 0;
			Zagogulina.LockId= -1;
			
			Zagogulina.Milieu = new Zagogulina[objCou];
			Zagogulina.Participant = new Point[objCou];
			
			Dot = new Zagogulina();
			Dot.type = "o";
			
		}
		
		Rectangle GetTargetDrawRec()
		{
			Rectangle rez = new Rectangle( 0, 0, 0, 0);
			
			if(Zagogulina.Battlefield.Top == Target.Y)
			{
//				x * 32 +3, y *32 +3
				rez = new Rectangle( Target.X * 32 +3, 0, 32, 3); // wide and low
			}
			
			if(Zagogulina.Battlefield.Left == Target.X)
			{
				rez = new Rectangle( 0, Target.Y * 32 +3, 3, 32); // thin and tall
			}
			
			if(Zagogulina.Battlefield.Bottom == Target.Y)
			{
				rez = new Rectangle( Target.X * 32 +3, Target.Y *32 , 32, 3); // wide and low
			}
			
			if(Zagogulina.Battlefield.Right == Target.X)
			{
				rez = new Rectangle( Target.X *32 , Target.Y * 32 +3, 3, 32); // thin and tall
			}
			
			return rez;
		}
		
		void NextLevel()
		{
			lvl++;
			if(lvl <6)
			{
				LoadLvl(lvl);
			}
		}
		
		void Level1()
		{
			Reset(3);
			
			Target =  new Point(3, 0);
			FinPos =  new Point(2, 0);
			Dot.pos = new Point(0, 0);
			Zagogulina.Battlefield = new Rectangle(0,0, 3, 2);
			Zagogulina.tgt = GetTargetDrawRec();
			
			Zagogulina nov;
			
			nov = new Zagogulina(new Point(1,1), "bc", true);
			nov.sizeType =1;
			nov = new Zagogulina(new Point(2,0), "bc", true);
			
			
		}
		
		
		void Level2()
		{
			Reset(4);
			
			Target =  new Point(3, 1);
			FinPos =  new Point(2, 1);
			Dot.pos = new Point(0, 0);
			Zagogulina.Battlefield = new Rectangle(0,0, 3, 3);
			Zagogulina.tgt = GetTargetDrawRec();
			
			Zagogulina nov;
			
			nov = new Zagogulina(new Point(0,2), "u", true);
			nov.sizeType =1;
			nov = new Zagogulina(new Point(1,1), "u", true);
			nov = new Zagogulina(new Point(2,0), "bc", false);

		}
		
		void Level3()
		{
			Reset(5);
			
			Target =  new Point(3, 1);
			FinPos =  new Point(2, 1);
			Dot.pos = new Point(1, 1);
			Zagogulina.Battlefield = new Rectangle(0,0, 3, 3);
			Zagogulina.tgt = GetTargetDrawRec();
			
			Zagogulina nov;
			
			nov = new Zagogulina(new Point(0,1), "c", true);
			nov.sizeType =1;
			nov = new Zagogulina(new Point(2,1), "n", true);
			nov = new Zagogulina(new Point(1,0), "n", false);
			nov = new Zagogulina(new Point(1,2), "u", false);
			
//			nov.sizeType =1; // small red
//			nov.isRed = true; // big red
		}
		
		void Level4()
		{
			Reset(4);
			
			Target =  new Point(4, 0);
			FinPos =  new Point(3, 0);
			Dot.pos = new Point(3, 1);
			Zagogulina.Battlefield = new Rectangle(0,0, 4, 2);
			Zagogulina.tgt = GetTargetDrawRec();
			
			Zagogulina nov;
			
			nov = new Zagogulina(new Point(2,1), "u", true);
			nov.sizeType =1;
			nov = new Zagogulina(new Point(1,1), "u", false);
			nov.sizeType =1;
			nov = new Zagogulina(new Point(2,0), "c", true);

		}
		
		
		void Level5()
		{
			Reset(5);
			
			Target =  new Point(5, 1);
			FinPos =  new Point(4, 1);
			Dot.pos = new Point(3, 1);
			Zagogulina.Battlefield = new Rectangle(0,0, 5, 2);
			Zagogulina.tgt = GetTargetDrawRec();
			
			Zagogulina nov;
			
			nov = new Zagogulina(new Point(4,1), "u", true);
			nov.sizeType =1;
			nov = new Zagogulina(new Point(3,0), "n", false);
			nov = new Zagogulina(new Point(2,1), "bc", false);
			nov = new Zagogulina(new Point(1,1), "u", true);
			

		}
		
	}
	
}
