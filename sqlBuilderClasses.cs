/*

 * Namespace represent classes for SQL query text parsing and generating,
 * object model of basic query and its partitions,
 * utility classes to manage files and database structure.
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
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using Microsoft.Win32; // work with registry
using System.IO;

namespace SqlBuilderClasses
{
	
	public class timer
	{
		public static bool showTime = true;
		private DateTime start;
		private DateTime finish;

		public timer()
		{
			start = DateTime.Now;
		}
		public void Start()
		{
			start = DateTime.Now;
		}
		public void final()
		{
			if (showTime)
			{
				finish = DateTime.Now;
				TimeSpan workTime;
				workTime = finish - start;
//				Console.WriteLine("       *** Time => {0}", workTime);
			}
		}
		public string final(string mess)
		{
			finish = DateTime.Now;
			TimeSpan workTime;
			workTime = finish - start;
			return ("    ***" + mess + "  time => " + workTime.ToString());
		}
	}
	
	public class PrevValGetter
	{
		int prev;
		int curr;
		int prevAlreadyOut;
		int cou;
		
		public PrevValGetter()
		{
			prev = 0;
			curr = 0;
		}
		
		public int Current
		{
			get
			{
				return curr;
			}
			set
			{
				if ((value != curr))
				{
					prev = curr;
					curr = value;
				}
				prevAlreadyOut = -1;
				cou = 0;
			}
		}
		
		public int Previous
		{
			get
			{
				prevAlreadyOut = prev;
				return prev;
			}
		}
		
		public bool isBothEqual
		{
			get
			{
				bool rez = (prev == prevAlreadyOut);
				if (rez) {cou ++;}
				return (cou > 0);
			}
		}
		
		void SetPrevious(int p)
		{
			prev = p;
			prevAlreadyOut = -1;
		}
		
		
	}

	public delegate void ObjectProc(ref object o);
	public delegate string InterCrtlDragProc();

	
	public struct segmCoord
	{
		public int start;
		public int fin;
		
		public bool isEmpty
		{
			get
			{
				return (start == fin);
			}
		}
	}
	
	//   [Flags]
	public enum FieldModifiers   // (int) FieldModifiers
	{
		undef = 0,
		caseM = 1,
		agrF = 2,
		countF = 4,
		maxF = 8,
		minF = 16,
		constF = 32,
		func = 64,
		simpleF = 128,
		urest = 256, // unrestricted
		avgF = 512
	}
	
	public enum ConditionType
	{
		undef = 0,
		equality = 1,  // a > b
		unary = 2, // IS [NOT] NULL
		list = 4,  // IN (<val> [, <val> ...] | <select_list>)
		// ALL | SOME | ANY} (<select_list>)
		err = 8,
		minF = 16,
		urest = 32
	}
	
	public struct condition
	{
		string textOr;     // original text
		
		public string text
		{
			get
			{
				return textOr;
			}
			set
			{
				Clear();
				textOr = value.ToLower().Trim();
				textOr = QueryParser.OpenBrackets(textOr);
				QueryParser.StructureCondition(ref this);
			}
		}
		public string textForm
		{
			get
			{
				string rez;
				if ((type != ConditionType.undef) && (type != ConditionType.err) && (type != ConditionType.urest))
				{
					string addOp = "";
					string[] op = new string[1];
					string expr = QueryParser.GetExpresionStruct(cOperator.TrimEnd(), out op);
					if (QueryParser.EnumCharOccurrence(expr, QueryParser.variousOpGet) == 0)
					{
						addOp = expr.Trim();
					}
					
					string openingNot = "";
					int i = QueryParser.GetOperandExprIdx(expr, 0);
					if(i > 0) {addOp = expr.Substring(0, i) + " ";}
//					if(((i > 0)||(string.IsNullOrEmpty(op[0]))) && negation)
					if((i > 0) && negation)
					{
						openingNot = "not ";
					}
					
					Array.Resize(ref op, 2);
					rez = openingNot+ operand1 + ( (negation && (openingNot == "")) ? " not " : " ") + addOp + op[0] +" "+ operand2 +" "+ op[1] +" "+ operand3;
					
				} else
				{
					
					rez = textOr+" ";
				}
				rez = rez.Trim();
				return rez;
			}
		}
		public string cOperator;// <>= BETWEEN, LIKE ...
		public ConditionType type;
		public bool agregate;   // must be placed to HAVING block
		public bool negation;   // NOT modifier presence
		public string operand1; // evaluated value mostly, if err - hold detailed message
		public string operand2;
		public string operand3; // second bound in BETWEEN statement, escape text in LIKE statement
		
		public void Clear()
		{
			cOperator = "";
			
			agregate = false;
			negation= false;
			operand1 = "";
			operand2 = "";
			operand3 = "";
			textOr = "";
		}
	}
	
	public	struct join
	{
		public string leftTable;
		public string rightTable;
		public string type;
		int condNum;
		condition[] conditionz;
		
		public join(byte stub)
		{
			condNum = 0;
			conditionz = new condition[Query.n];
			leftTable = "";
			rightTable = "";
			type = "";
		}
		
		public void setConditionNumber(int newLength)
		{
			condNum = newLength;
		}
		
		public void addCondition(string s)
		{
			condition tmp = new condition();
			tmp.text = s;
			conditionz[condNum] = tmp;
			condNum++;
		}
		
		public condition[] ConditionzA
		{
			get
			{
				condition[] rez = new condition[condNum];
				Array.Copy (conditionz, rez, condNum);
				return rez;
			}
			
			set
			{
				Array.Copy (value, conditionz, value.Length);
				condNum = value.Length;
			}
			
		}
		
		public string[] Strings
		{
			get
			{
				string[] rez = new string[condNum]; // plus header
				string and = " (";
				
				for(int i=0; i < condNum; i++)
				{
					rez[i] = and + conditionz[i].textForm +") ";
					and = " and (";
				}
				return rez;
			}
		}
		
		public string ToStringWH()
		{
			string rez = " ";
			string[] arr = Strings;
			foreach (string s in arr)
			{
				rez += s;
			}
			
			return rez;
		}
		
		public override string ToString()
		{
			string rez = " "+ leftTable +". "+ rightTable+ ". " ;
			rez += ToStringWH();
			
			return rez;
		}
		
	}
	
	public	struct field
	{
		#region info about virtual table fields
		string textOr;   // original text without explicit ('as') alias
		public void ForOptimizationOnly(string textOr_)
		{
			textOr = textOr_;
		}
		public string text
		{
			get
			{
				return textOr;
			}
			set
			{
				textOr = value.ToLower().Trim();
				StringBuilder b = new StringBuilder(textOr);
				b.Replace(" as ", " ");
				b.Replace("'as ", "' ");
				b.Replace(")as ", ") ");
				textOr = b.ToString();
				QueryParser.StructureField(ref this, "unknown_");
			}
		}
		
		public string tableAlias;
		public string tableField;  // real field name that available in table interface
		public FieldModifiers fType;
		public int mark;
		#endregion
		
		public string alias; // field name, if not virtual
		public string type;  // type verification not implemented yet
		
		public override string ToString()
		{
			return textOr;
		}
	}
	
	public	struct table
	{
		byte fcou;     // fields count
		bool isVirtual;// is it a db table or virtual table that corresponds to subquery
		public bool virtualTbl
		{
			get{return isVirtual;}
		}
		int qNum;      // index of query represented by this table
		public string Name; // db table name (empty when virtual)
		public string alias;// table alias
		public bool addImpAlias;
		public bool isEmpty
		{
			get
			{
				bool rez = false;
				
				if((Name.Trim() == "")&&(alias.Trim() == "") && (fcou == 0))
				{
					rez = true;
				}
				
				return rez;
			}
		}
		field[] flds;  // fields struct array
		
		public table(string tName, string tAlias, int id)
		{
			flds = new field[256];
			Name = tName;
			tAlias = string.IsNullOrEmpty(tAlias)? "": tAlias;
			if (tAlias.Trim() != "") {alias = tAlias;} else {alias = tName;}
			isVirtual =  (tName == "");
			fcou = 0;
			qNum = id;
			addImpAlias = true;
		}
		
		public int queryId
		{
			get
			{
				if (isVirtual)
				{
					return qNum;
				} else
				{
					return -1;
				}
			}
		}
		
		public field[] Fields
		{
			get
			{
				field[] rez= new field[0];
				if (fcou>0)
				{
					rez = new field[fcou];
					Array.Copy (flds, rez, fcou);
				}
				return rez;
			}
			set
			{
				fcou = 0;
				foreach ( field f in value)
				{
					flds[fcou] = f;
					fcou ++;
				}
				CheckUniqueAliases();
//				fcou --;
			}
		}
		
		public void AddFld(string fName)
		{
			if(fName =="") {return;}
			flds[fcou] = new field();
			flds[fcou].text = fName;
			fcou ++;
		}
		
		public string[] Strings
		{
			get
			{
				string[] rez= new string[0];
				if (fcou>0)
				{
					rez = new string[fcou];
					for (int i = 0; i < fcou; i++)
					{
						rez[i] = flds[i].text;
						if(addImpAlias)
						{
							if ( !string.IsNullOrEmpty(flds[i].alias) && (! rez[i].EndsWith(flds[i].alias)))
							{
								rez[i] += " as "+ flds[i].alias;
							}
						}
					}
				}
				
				return rez;
			}
		}
		
		public int FieldAliasNum(string s)
		{
			int rez = -1;
			for (int i=0; i < fcou; i++)
			{
				if (flds[i].alias.Trim() == s.Trim())
				{
					rez = i;
					break;
				}
			}
			return rez;
		}
		
		public bool HaveSuchField(string s)
		{
			return (FieldAliasNum(s) != -1);
		}
		
		public void CheckUniqueAliases()
		{
			int notUniqAliasCou = 0;
			string[] aar = new string[fcou];
			
			for (int i=0; i < fcou; i++)
			{
				if (flds[i].alias == null)
				{
					continue;
				}
				string aliasCheck = flds[i].alias.Trim();
				
				for (int j = i+1; j < fcou; j++)
				{
					if ((flds[j].alias!=null) && (flds[j].alias.Trim() == aliasCheck))
					{
						notUniqAliasCou++;
						if (string.IsNullOrEmpty(aar[j]))
						{
							flds[j].alias = aliasCheck + notUniqAliasCou.ToString();
							aar[j] = aliasCheck;
						} else
						{
							flds[j].alias = aar[j] + notUniqAliasCou.ToString();
						}
					}
				}
			}
		}
		
	}
	
	public class DBEngine
	{
		
		public static string[] ProvNames;
		public static string[] ProvAlias;
		public static int provCount;
		
		public static void GetOleDbProviersInfo()
		{
			const string subkey = "CLSID";
			//	const string keyName = userRoot + "\\" + subkey;
			provCount = 0;
			ProvAlias = new string[200];
			ProvNames = new string[200];
			RegistryKey rk = Registry.ClassesRoot.OpenSubKey(subkey);
			
			foreach(string subKeyName in rk.GetSubKeyNames()) // HKEY_CLASSES_ROOT\CLSID\
			{
				using(RegistryKey tempKey = rk.OpenSubKey(subKeyName)) // {02D887FB-4358-11D2-BE22-080009DC0A8D}
				{
					string[] findKeys = tempKey.GetSubKeyNames();
					int idx = Array.IndexOf(findKeys, "OLE DB Provider");
					
					if(idx >= 0)
					{
						ProvAlias[provCount] = tempKey.GetValue("", "").ToString(); // Microsoft.Project.OLEDB.11.0
						ProvNames[provCount] = tempKey.OpenSubKey("OLE DB Provider").GetValue("").ToString(); // Microsoft Project 11.0 OLE DB Provider
						provCount++;
					}
					
				}
			}
			
		}
		
		public static table[] GetDBStruct(string s)
		{
			table[] rez = new table[100];
			int tableCou = 0;
			
			DataRowView ColumnDrv = null;
			DataTable dtCols = null;
			DataView dvColumns = null;
			
			try
			{
				using (OleDbConnection connection = new OleDbConnection(s))
				{
					
					try
					{
						connection.Open();
						
						#region Get Tables Schema
						DataTable schemaTable = connection.GetOleDbSchemaTable(
							OleDbSchemaGuid.Tables,
							new object[] { null, null, null, "TABLE" }
						);
						#endregion
						
						foreach(DataRow row in schemaTable.Rows)
						{
							#region new TAble
							tableCou++;
							if (tableCou > rez.Length)
							{
								Array.Resize(ref rez, rez.Length * 2);
							}
							rez[tableCou-1]= new table(row[2].ToString(), "", 0);
							#endregion
							
							#region Get Columns Schema
							dtCols = connection.GetOleDbSchemaTable(
								OleDbSchemaGuid.Columns,
								new Object[]{null,null, row[2], null});
							#endregion
							
							dvColumns = new DataView(dtCols);
							dvColumns.Sort = "ORDINAL_POSITION";
							
							field[] flds = new field[dvColumns.Count];
							
							
							#region Get the column names and their types.
							for (int j=0; j< dvColumns.Count; j++)
							{
								ColumnDrv = dvColumns[j];
//								flds[j] = new field();
								string	sColName = ColumnDrv.Row.ItemArray[3].ToString();
								flds[j].alias = sColName;
								flds[j].type  = ColumnDrv.Row.ItemArray[11].ToString();
							}
							#endregion
							
							rez[tableCou-1].Fields = flds;
							
						}
						
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			
			Array.Resize(ref rez, tableCou);
			return rez;
			
		}
		
	}
	
	public class Query
	{
		public const int n = 310;
		static int ObjNum;
		public static bool extendDb;
		public string handle;
		int oid; // ObjIdx data
		public int ObjIdx
		{
			get
			{
				return oid;
			}
			set
			{
				oid = value;
			}
		}
		public bool inEdit;    //  gui functionality
		public string UnionParam;  // determine modifier presence in UNION
		public bool distinct;  // determine [ALL/DISTINCT] modifier presence in SELECT statement of current query
		public byte nonameFldIdx;     // used to fill the undefined field alias by mask 'nonamefield_'+nonameFldIdx
		public static Query[] QueryObjects;  // array ref to interact with subqueries and unions
		public Query[] QueryUnions;  // temporary array of unions copies
		string[] procStr;
		bool isHeader_; // first object that is self checked (open in new form or parsing) stay header, for all unioned objects  isHeader_ = false
		public bool isHeader
		{
			get
			{
				return isHeader_;
			}
		}
		public static table[] dbTables;
		public int maxUFldCou;
		int[] clearObjList;
		
		#region counters
		int unionCou;
		public int unionsCount
		{
			get
			{
				return unionCou;
			}
		}
		int fieldCou;
		public int fieldsCount
		{
			get
			{
				return fieldCou;
			}
		}
		int tableCou;
		public int joinCou;
		public int wheresCou;
		int havingsCou;
		int clearObjListCou;
		#endregion
		
		#region data
		int[] unions;
		field[] fields;
		table[] tables;
		public join[] joins;
		public condition[] wheres;
		public string[] groups; // as is
		public condition[] havings;
		public string[] orders; // as is
		object[] tmp;
		
		public join[] joinZ
		{
			get
			{
				join[] rez= new join[0];
				if (joinCou > 0)
				{
					int offset = ((joinCou == 1) && (joins[0].leftTable == ""))? 0 : 1;
					
					rez = new join[joinCou + offset];
					Array.Copy (joins, 0, rez, offset, joinCou);
					if(offset >= 1)
					{
						rez[0] = new join(0);
						rez[0].rightTable = rez[1].leftTable;
					}
//					Array.Copy (joins, rez, joinCou);
				}
				return rez;
			}
			
			set
			{
				joinCou = 0;
				foreach(join c in value)
				{
//					if(c.textForm.Trim() != "")
//					{
					joins[joinCou] = c;
					joinCou++;
//					}
				}
				
				SortTablesInJoinOrder();
			}
		}
		
		public condition[] whereZ
		{
			get
			{
				condition[] rez= new condition[0];
				if (wheresCou > 0)
				{
					rez = new condition[wheresCou];
					Array.Copy (wheres, rez, wheresCou);
				}
				return rez;
			}
			
			set
			{
				wheresCou = 0;
				foreach(condition c in value)
				{
					if((c.textForm.Trim() != "") && ((c.textForm.Trim() != "=")))
					{
						wheres[wheresCou].text = c.textForm;
						wheresCou++;
					}
				}
//				Array.Copy (value, wheres, value.Length);
//				wheresCou = value.Length;
			}
		}
		
		#endregion
		
		public bool IsUnioned
		{
			get
			{
				if (unionCou > 1)
				{
					return true;
				} else
				{
					return false;
				}
			}
		}
		
		public Query()
		{
			ObjNum++;
			if(ObjNum ==1)
			{
				QueryObjects = new Query[1500];
			}
			handle = ObjNum.ToString();
			
			ObjIdx = -1;
			distinct = false;
			UnionParam = "";			
			inEdit   = false;
			nonameFldIdx = 1;
			unionCou = 0;
			fieldCou = 0;
			tableCou = 0;
			joinCou  = 0;
			wheresCou = 0;
			havingsCou = 0;
			unions = new int[n];
			fields = new field[n];
			tables = new table[n];
			joins  = new join[n];
			wheres  = new condition[n];
			havings = new condition[n]; // not in use, all this go to wheres
			
			groups = new  string[0] ;
			orders = new  string[0] ;
			isHeader_ = true;
			clearObjListCou = 0;
			clearObjList = new int[n];
		}
		
		#region list of Query Objects manipulation
		
		public static int AddListObject()
		{
			Query obj =  new Query();
			int rez = Query.GetFreeItemId(0);
			QueryObjects[rez] = obj;
			QueryObjects[rez].ObjIdx = rez;
			return rez;
		}
		
		public static int GetFreeItemId(int start)
		{
			int rez = start;
			while ((QueryObjects[rez] != null) && (rez < n))
			{
				rez++;
			}
			return rez;
		}
		
		public int GetMaxQueryObjectsIndex()
		{
			int rez = 0;
			GetFreeItemId(rez);
			
			return rez;
		}
		
		#endregion
		
		public void SelfCheck() // after parsed all queries
		{
			#region populate tables
			for(int i = 0; i < tableCou; i++)
			{
				if (tables[i].queryId != -1 )
				{
					tables[i].Fields = QueryObjects[tables[i].queryId].GetVirtTableFields();
				} else
				{
					if (dbTables != null)
					{
						foreach (table t in dbTables)
						{
							if(t.alias == null) {continue;}
							if (t.alias.ToLower() == tables[i].Name.ToLower())
							{
								tables[i].Fields = t.Fields;
							}
						}
					}
				}
			}
			#endregion
			
			UnmarkFields();
			
			#region init union array
			if ((this.QueryUnions == null) && (isHeader_) && (unionCou == 0))
			{
				this.QueryUnions = new Query[n/6];
				this.QueryUnions[0] = this;
				AddUnionedQuery(this.ObjIdx);
			}
			
			if((isHeader_) && (this.QueryUnions != null))
			{
				this.QueryUnions[0] = this;
				
				for(int i = 1; i < unionCou; i++)
				{
					QueryUnions[i].ResizePartition(7, 0);
					QueryUnions[i].isHeader_ = false;
				}
			}
			#endregion
			
			
		}
		
		public void CopyTO(Query receiver)
		{
//			if (receiver == null) {receiver = new Query();}
			
//			Query[] OldUnionHolder = receiver.QueryObjects;
			
//			receiver.QueryObjects = this.QueryObjects;
			receiver.handle += "( Copy "+ this.handle+ " )";
			
			if ((!receiver.IsUnioned) && (this.IsUnioned))
			{
				Array.Copy(this.unions, receiver.unions, unionCou);
				receiver.unionCou = this.unionCou;
				
				receiver.QueryUnions = new Query[n/6];
				
				for (int i = 1; i < receiver.unionCou; i++)
				{
					receiver.QueryUnions[i] = new Query();
					QueryObjects[ this.unions[i] ].CopyTO(receiver.QueryUnions[i]);
				}
			}
//			else
//			{
//				if ((receiver.unionCou > 0) && (receiver.isHeader_))
//				{
//					int qCouNew = GetMaxQueryObjectsIndex();
//					for (int i = 0; i < receiver.unionCou; i++)
//					{
//						receiver.QueryObjects[qCouNew] = OldUnionHolder[ receiver.unions[i] ];
//						receiver.QueryObjects[qCouNew].ObjIdx = qCouNew;
//						receiver.unions[i] = qCouNew;
//						qCouNew++;
//					}
//				}
//
//				// to add another union to existing set use unions tab, union persed in text is not processed
//			}
			
			Array.Copy(this.fields, receiver.fields, fieldCou);
			Array.Copy(this.tables, receiver.tables, tableCou);
			Array.Copy(this.joins, receiver.joins, joinCou);
			Array.Copy(this.wheres, receiver.wheres, wheresCou);
			
			receiver.groups = new string[this.groups.Length];
			receiver.orders = new string[this.orders.Length];
			Array.Copy(this.groups, receiver.groups, this.groups.Length);
			Array.Copy(this.orders, receiver.orders, this.orders.Length);
			
			for(int i = 0; i< joinCou; i++)
			{
				receiver.joins[i].ConditionzA = this.joins[i].ConditionzA;
			}
			
			for(int i = 0; i< tableCou; i++)
			{
				receiver.tables[i].Fields = this.tables[i].Fields;
			}
			
			receiver.inEdit = this.inEdit;
			if(receiver.ObjIdx < 0)
			{
				receiver.ObjIdx = this.ObjIdx;
			}
			receiver.distinct = this.distinct;
			receiver.UnionParam = (string.IsNullOrEmpty(receiver.UnionParam))? this.UnionParam : receiver.UnionParam;
			receiver.nonameFldIdx = this.nonameFldIdx;
			receiver.fieldCou = this.fieldCou;
			receiver.tableCou = this.tableCou;
			receiver.joinCou = this.joinCou;
			receiver.wheresCou = this.wheresCou;
			
		}

		public void Save()
		{
			if(ObjIdx < 0)
			{
				this.ObjIdx = GetFreeItemId(0);
			}
			
			#region save unions
			QueryObjects[ObjIdx] = this;
			
			if((IsUnioned) && (QueryUnions != null))
			{
				for(int i = 1; i < unionCou; i++)
				{
					QueryUnions[i].Save();
					this.unions[i]  = QueryUnions[i].ObjIdx;
				}
			}
			#endregion
			
			#region	clean deleted list objects
			for(int i = 0; i < clearObjListCou; i++)
			{
				if((clearObjList[i] >= 0) && (QueryObjects[clearObjList[i]] != null))
				{
					QueryObjects[clearObjList[i]].NullListObjects();
				}
			}
			clearObjListCou = 0;
			#endregion
			
		}
		
		public void NullListObjects()
		{
			QueryObjects[this.ObjIdx] = null;
			
			for(int i =0; i < tableCou; i++)
			{
				if (tables[i].queryId >= 0)
				{
					QueryObjects[tables[i].queryId] = null;
				}
			}
			
			for(int i = 0; i < unionCou; i++)
			{
				QueryObjects[this.unions[i]] = null;
			}
			
		}
		
		public void TraceTree(ref int startIdx, int host, string name,  ref string[] nameTree, ref int[] idTree, ref int[] hostTree)
		{
			if(extendDb){ExtendDB();}
			
			startIdx++;
			int thisIdx = startIdx;
			nameTree[startIdx] = name;
			idTree[startIdx] = this.ObjIdx;
			hostTree[startIdx] = host;
			
			if((!IsUnioned) || ((host >= 0)&&(this.ObjIdx == idTree[host] )))
			{
				for(int i = 0; i < tableCou; i++)
				{
					if (tables[i].queryId != -1)
					{
						QueryObjects[tables[i].queryId].TraceTree(ref startIdx, thisIdx, tables[i].alias,  ref nameTree, ref idTree, ref hostTree);
					}
				}
			} else
				
			{
				for(int i = 0; i < unionCou; i++)
				{
					QueryObjects[unions[i]].TraceTree(ref startIdx, thisIdx, "Query "+ (i+1).ToString(),  ref nameTree, ref idTree, ref hostTree);
				}
			}
		}
		
		public string GetSQLText(bool isroot)
		{
			if(isroot)
			{
				this.Save();
			}
			return GetSQLText(0);
		}

        string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public string GetSQLText(int lvl)
		{
//			string[] rez = new string[1000];
			if(lvl > 0) {inEdit = false;}
			StringBuilder SB = new StringBuilder();
			string[] procArr;
			int lineCou = 0;
			string tab = "    ";
			string tabForLvl = "     ";
			string lvlTab = "";
			for (int i =0; i<lvl; i++ )
			{
				lvlTab+= tabForLvl;
			}
			
			#region select
			SB.Append(lvlTab+ ((lvl>0)? "(" : " ") +"select "+ (distinct ? "distinct " : ""));
			table t = new table("","",0);
			t.Fields = GetVirtTableFields();
			procArr = t.Strings;
			
			if (procArr.Length <= 0) return "";
			string sep = ",";
			for(int i =0; i< procArr.Length; i++)
			{
				lineCou++;
				SB.Append("\n"+lvlTab+ tab+ procArr[i] + (((i+1) == procArr.Length) ? "" : sep));
			}
			#endregion
			
			#region from
			SB.Append("\n "+ lvlTab+ "from ");
			for (int i = 0; i< tableCou; i++)
			{
				int joinIdx = IsTableJoined(tables[i].alias);
				#region separator
				bool isJoined = (joinIdx >= 0);
				if (!isJoined && (i > 0))
				{
					SB.Append(sep);
				}
				#endregion
				
				#region define join parameters
				string jointype = "";
				string[] joinConditionz = new string[0];
				
				if (isJoined)
				{
					jointype = joins[joinIdx].type.Trim() + (QueryParser.ContainsExpr(joins[joinIdx].type, "join") ? " " : " join ") ;
					joinConditionz = joins[joinIdx].Strings;
				}
                jointype = jointype.ToLower();
                #endregion

                #region table body
                if (tables[i].queryId ==-1)
				{
                    string tblname = UppercaseFirst(tables[i].Name.ToLower());

                    SB.Append("\n"+lvlTab+ tab+ jointype + tblname + ((tables[i].Name == tables[i].alias) ? "" : " as "+tables[i].alias) );
				}else
				{
					SB.Append("\n"+lvlTab+ tab+ jointype +"\n"+ QueryObjects[tables[i].queryId].GetSQLText(lvl +1) +" as " + tables[i].alias.ToLower() );
				}
				#endregion
				
				#region appending join conditions
				if (isJoined)
				{
					SB.Append(" on " + "\n");
					for(int j = 0; j < joinConditionz.Length; j++)
					{
						SB.Append(lvlTab+ tab+ "          "+ joinConditionz[j] + (((j+1) == joinConditionz.Length) ? "" : "\n"));
					}
				}
				#endregion
				
			}
			#endregion
			
			#region wheres
			string[] havingsStr = new string[wheresCou];
			int havCou =0;
			
			if (wheresCou > 0)
			{
				SB.Append("\n "+lvlTab + "where ");
				
				for(int i = 0; i < wheresCou; i++)
				{
					if (wheres[i].agregate)
					{
						havingsStr[havCou] = wheres[i].textForm;
						havCou++;
					} else
					{
						SB.Append("\n"+lvlTab+tab+ (((i > 0) && (havCou == 0)&& ((havCou+1 != i) || (havCou == 0))) ? "and " : "") + "("+ wheres[i].textForm +")");
					}
				}
			}
			#endregion

			#region group by
			if ((groups != null) && (groups.Length > 0))
			{
				SB.Append("\n "+lvlTab + "group by ");
				int i = 0;
				foreach(string s in groups)
				{
					SB.Append("\n"+lvlTab+tab+ s +(((i+1) == groups.Length) ? "" : sep));
					i++;
				}
			}
			#endregion
			
			#region havings
			if (havCou > 0)
			{
				SB.Append("\n "+lvlTab + "having ");
				for(int i = 0; i < havCou; i++)
				{
					SB.Append("\n"+lvlTab+tab+ ((i > 0) ? "and " : "") + "("+ havingsStr[i] +")" );
				}
			}
			#endregion
			
			#region order by
			if (orders.Length > 0)
			{
				SB.Append("\n "+lvlTab + "order by ");
				for(int i = 0; i < orders.Length; i++)
				{
					SB.Append("\n"+lvlTab+tab+ orders[i] + (((i+1) == orders.Length) ? "" : sep) );
				}
			}
			#endregion
			
			if (isHeader && (!inEdit))
			{
				for(int i =1; i < unionCou; i++)
				{
					SB.Append("\n \n"+lvlTab + "union "+ QueryObjects[unions[i]].UnionParam +"\n");
					SB.Append("\n"+ QueryObjects[unions[i]].GetSQLText(lvl));
				}
			}
			
			if(lvl>0)
			{
				return SB.ToString()+ "\n"+ lvlTab + ")";
			} else
			{
				return SB.ToString();
			}
		}
		
		#region used DB objects
		void ExtendTableFields(field[] far, ref table tbl, string talias)
		{
			talias = talias.Trim().ToLower();
			foreach(field f in far)
			{
				if((f.tableAlias != null)
				   &&(f.tableAlias.Trim().ToLower() == talias)
				   &&(!tbl.HaveSuchField(f.tableField)))
				{
					tbl.AddFld(f.tableField);
				}
			}
		}
		
		public void ExtendDB()
		{
			table tbl;
			string tName = "";
			string tAlias = "";
			
			for(int i = 0; i < tableCou; i++)
			{
				if(!tables[i].virtualTbl)
				{
					tName = tables[i].Name;
					tAlias =  tables[i].alias.Trim().ToLower();
					
					#region get DB Table idx
					int idx = GetDBTableByName(tName);
					
					if(idx == -1)
					{
						tbl = new table(tName, tName, -1);
						idx = dbTables.Length;
						Array.Resize(ref dbTables, idx +1);
						
					} else
					{
						tbl = dbTables[idx];
					}
					#endregion
					
				    // fields					
					ExtendTableFields(this.GetVirtTableFields(), ref tbl, tAlias);							
					
					field[] far = new field[joinCou * 60 + orders.Length + groups.Length + wheresCou * 2];
					int farCou = 0;
					
					#region joins
					string[] tmp = new string[1];
					
					// conditions
					joins[joinCou] = new join(0);
					joins[joinCou].ConditionzA = whereZ;
					
					for(int k = 0; k < joinCou+1; k++)
					{
						tmp[0] = joins[k].ToStringWH();
						if(Query.GetModifyIdxArr(tmp, tAlias + ".", 0)[0])
						{
							condition[] car = joins[k].ConditionzA;
							foreach(condition c in car)
							{
								if(c.type != ConditionType.urest)
								{
									far[farCou].text = c.operand1.Trim();
									farCou++;
									far[farCou].text = c.operand2.Trim();
									farCou++;
								}
							}
						}
					}
					#endregion
					
					foreach(string s in orders)
					{
						far[farCou].text = s.Trim();
						farCou++;
					}
					foreach(string s in groups)
					{
						far[farCou].text = s.Trim();
						farCou++;
					}
					
					Array.Resize(ref far, farCou);
					
					ExtendTableFields(far, ref tbl, tAlias);
					
					dbTables[idx] = tbl;
				}
			}
		}
		#endregion
		
		#region data fill
		
		public void AddField(string s)
		{
			field f = new field();
			f.text = s;
			AddField( f);
		}
		
		public void AddField(field f, string alias)
		{
			int idx = AddField(f);
			if(!string.IsNullOrEmpty(alias))
			{
				fields[idx].alias = alias;
			}
		}
		
		public int AddField(field f)
		{
			int rez = fieldCou;
			fields[fieldCou] = f;
			fieldCou++;
			table t = new table("","",0);
			t.Fields = GetVirtTableFields(); // this unclear definition used to hire <table>'s internal alias unique check
			Array.Copy(t.Fields, fields, fieldCou);
			return rez;
		}
		
		public void AddTable(table t)
		{
			tables[tableCou] = t;
			tableCou++;
		}
		
		public void AddJoin(join j)
		{
			joins[joinCou] = j;
			joinCou++;
		}
		
		public void AddUnionedQuery(int u)
		{
			unions[unionCou] = u;
			unionCou++;
		}
		
		public void AddUnionQuery()
		{
			QueryUnions[unionCou] = new Query();
//			QueryObjects[i].ObjIdx = i;
			AddUnionedQuery(QueryUnions[unionCou].ObjIdx);
		}
		
		public void AddWhere(condition c)
		{
			wheres[wheresCou] = c;
			wheresCou++;
		}
		
		public void AddHaving(condition c)
		{
			c.agregate = true;
			AddWhere(c);
//			havings[havingsCou] = c;
//			havingsCou++;
		}
		
		public void AddGrouping(string s)
		{
			Array.Resize(ref groups, groups.Length +1);
			groups[groups.Length -1] = s;
		}
		
		public void AddGrouping(string s, int idx)
		{
			AddGrouping(s);
			MoveElementWithinPartition( groups.Length -1, idx, 4);
		}
		
		public void AddOrder(string s)
		{
			Array.Resize(ref orders, orders.Length +1);
			orders[orders.Length -1] = s + (QueryParser.IsSortExplicit(s) ? "" :" asc");
		}
		
		public void AddOrder(string s, int idx)
		{
			AddOrder(s);
			MoveElementWithinPartition( orders.Length -1, idx, 6);
		}

		void AddObjectToBeNulledOnSave(int queryListID)
		{
			clearObjList[clearObjListCou] = queryListID;
			clearObjListCou++;
		}
		
		#endregion
		
		#region Partition operations
		/// <summary>
		/// change max index of arry.
		/// 0: fields; 1: tables;  2: joins;  3: wheres; 4: groups; 5: havings; 6: orders; 7: unions
		/// </summary>
		public void ResizePartition(int p, int val)
		{
			switch (p)
			{
					case 0: fieldCou = val;
					break;
					case 1: tableCou = val;
					break;
					case 2: joinCou = val;
					break;
					case 3: wheresCou = val;
					break;
				case 4:
					Array.Resize(ref groups, val);
					break;
					case 5: havingsCou = val;
					break;
				case 6:
					Array.Resize(ref orders, val);
					break;
					case 7: unionCou = val;
					break;
				case 101:
					Array.Resize(ref tmp, val);
					break;
					
			}
		}
		
		/// <summary>
		/// Filter Partition.
		/// </summary>
		/// <param name="filter"> bool array, true - delete/proc corresponds element, array length must be the same as partition or more </param>
		/// <param name="p"> 0: fields; 1: tables;  2: joins;  3: wheres; 4: groups; 5: havings; 6: orders; 7: unions</param>
		/// <param name="proc"> delegate to process filtered objects </param>
		void FilterPartition(bool[] filter, int p, ObjectProc proc)
		{
			object[] uniwersal = new object[0];
			#region select array
			switch (p)
			{
					
					case 0: uniwersal = new object[fieldCou];
					Array.Copy(fields, uniwersal, fieldCou);
					break;
					
					case 1:uniwersal = new object[tableCou];
					Array.Copy(tables, uniwersal, tableCou);
					break;
					
					case 2:uniwersal = new object[joinCou];
					Array.Copy(joins, uniwersal, joinCou);
					break;
					
					case 3: uniwersal = new object[wheresCou];
					Array.Copy(wheres, uniwersal, wheresCou);
					break;
					
				case 4:
					if (groups != null)
					{
						uniwersal = new object[groups.Length];
						Array.Copy(groups, uniwersal, groups.Length);
//						uniwersal = groups;
					} else return;
					break;
					
					case 5:uniwersal = new object[havingsCou];
					Array.Copy(havings, uniwersal, havingsCou);
					break;
					
				case 6:
					if (orders != null)
					{
						uniwersal = new object[orders.Length];
						Array.Copy(orders, uniwersal, orders.Length);
//						uniwersal = orders;
					}else return;
					break;
					
					case 7:	uniwersal = new object[unionCou];
					Array.Copy(unions, uniwersal, unionCou);
					break;
					
				case 101:
					if (tmp != null)
					{
						uniwersal = tmp;
					}else return;
					break;
			}
			#endregion
			
			int newCou = 0;
			
			if (proc != null)
			{
				newCou = uniwersal.Length;
				for(int i=0; i < uniwersal.Length; i++)
				{
					if (filter[i])
					{
						proc(ref uniwersal[i]);
					}
				}
			} else
			{
				for(int i=0; i < uniwersal.Length; i++)
				{
					if (!filter[i])
					{
						uniwersal[newCou] = uniwersal[i];
						newCou++;
					}
				}
				Array.Resize(ref uniwersal, newCou);
				ResizePartition(p, newCou);
			}
			
			#region updating array
			switch (p)
			{
				case 0:
					Array.Copy(uniwersal, fields,  newCou);
					break;
					
				case 1:
					Array.Copy(uniwersal, tables, newCou);
					break;
					
				case 2:
					Array.Copy(uniwersal, joins, newCou);
					break;
					
				case 3:
					Array.Copy(uniwersal, wheres, newCou);
					break;
					
				case 4:
					Array.Copy(uniwersal, groups, newCou);
					break;
					
					case 5:	Array.Copy(uniwersal, havings, newCou);
					break;
					
				case 6:
					Array.Copy(uniwersal, orders, newCou);
					break;
					
				case 7:
					Array.Copy(uniwersal, unions, newCou);
					break;
//				case 101:
//					Array.Copy(uniwersal, tmp, newCou);
//					break;
			}
			#endregion
		}
		
		/// <param name="p">0: fields; 1: tables;  2: joins;  3: wheres; 4: groups; 5: havings; 6: orders; 7: unions</param>
		void MoveElementWithinPartition(int Idx, int newIdx, int p)
		{
			if (Idx == newIdx)
			{
				return;
			}
			
			object[] uniwersal = new object[0];
			#region select array
			switch (p)
			{
					
				case 0:
					uniwersal = new object[fieldCou];
					Array.Copy(fields, uniwersal, fieldCou);
					break;
					
				case 1:
					uniwersal = new object[tableCou];
					Array.Copy(tables, uniwersal, tableCou);
					break;
					
				case 2:
					uniwersal = new object[joinCou];
					Array.Copy(joins, uniwersal, joinCou);
					break;
					
				case 3:
					uniwersal = new object[wheresCou];
					Array.Copy(wheres, uniwersal, wheresCou);
					break;
					
				case 4:
					if (groups != null)
					{
						uniwersal = new object[groups.Length];
						Array.Copy(groups, uniwersal, groups.Length);
					} else return;
					break;
					
				case 5:
					uniwersal = new object[havingsCou];
					Array.Copy(havings, uniwersal, havingsCou);
					break;
					
				case 6:
					if (orders != null)
					{
						uniwersal = new object[orders.Length];
						Array.Copy(orders, uniwersal, orders.Length);
					}else return;
					break;
					
				case 7:
					uniwersal = new object[unionCou];
					Array.Copy(unions, uniwersal, unionCou);
					break;
			}
			#endregion
			
			object o = uniwersal[Idx];
			
			if (Idx > newIdx)
			{
				for (int i =Idx ; i > newIdx ; i--)
				{
					uniwersal[i] = uniwersal[i -1];
				}
				
			} else
			{
				for (int i =Idx; i < newIdx; i++)
				{
					uniwersal[i] = uniwersal[i +1];
				}
			}
			
			uniwersal[newIdx] = o;
			
			int newCou = uniwersal.Length;
			
			#region updating array
			switch (p)
			{
				case 0:
					Array.Copy(uniwersal, fields,  newCou);
					break;
					
				case 1:
					Array.Copy(uniwersal, tables, newCou);
					break;
					
				case 2:
					Array.Copy(uniwersal, joins, newCou);
					break;
					
				case 3:
					Array.Copy(uniwersal, wheres, newCou);
					break;
					
				case 4:
					Array.Copy(uniwersal, groups, newCou);

					break;
					
				case 5:
					Array.Copy(uniwersal, havings, newCou);
					break;
					
				case 6:
					Array.Copy(uniwersal, orders, newCou);
					break;
					
				case 7:
					Array.Copy(uniwersal, unions, newCou);
					break;
			}
			#endregion
		}
		
		#endregion
		
		void ObjectDo(ref object o)
		{
			string otype = o.GetType().Name;

			StringBuilder b = new StringBuilder();
			
			switch (otype)
			{
					
				case "field":
					field f = (field) o;
					f.text =QueryParser.Rename(f.text, procStr[0],  procStr[1]);
					o = f;
					break;
					
				case "String":
					o = QueryParser.Rename((string)o, procStr[0],  procStr[1]);
					break;
					
				case "condition":
					condition c = (condition) o;
					c.text = QueryParser.Rename(c.text, procStr[0],  procStr[1]);
					o = c;
					break;
			}
		}
		
		void FieldDoMark(ref object o)
		{
			field f = (field) o;
			if (procStr[0] == "clear")
			{
				f.mark = 0;
			} else
			{
				f.mark = 1;
			}
			o = f;
		}
		
		#region array filter proc
		
		public static bool[] GetModifyIdxArr(string[] a,  string reference, int offset)
		{
			if (a == null) {return new bool[0];}
			bool[] rez = new bool[a.Length];
			
			GetModifyIdxArr(a, ref rez, reference, offset);
			return rez;
		}
		
		public static void GetModifyIdxArr(string[] a, ref bool[] filter, string reference, int offset)
		{
			int i = offset;
			foreach(string s in a)
			{
				if (i >=0)
				{
					filter[i] = filter[i] ? true : QueryParser.ContainsExpr(s, reference);
				}
				
				i++;
			}
		}
		
		bool[] GetModifyIdxArrFullMatch(string[] a, string reference, int offset)
		{
			if (a == null) {return new bool[0];}
			bool[] rez = new bool[a.Length];
			
			GetModifyIdxArrFullMatch(a, ref rez, reference, offset);
			return rez;
		}
		
		void GetModifyIdxArrFullMatch(string[] a, ref bool[] filter, string reference, int offset)
		{
			int i = offset;
			
			foreach(string s in a)
			{
				if (i >=0)
				{
					filter[i] = filter[i] ? true : (s.Trim() == reference.Trim());
				}
				
				i++;
			}
		}
		
		void GetModifyIdxArrAliasMatch(string[] a, ref bool[] filter, string reference, int offset)
		{
			int i = offset;
			field f = new field();
			
			foreach(string s in a)
			{
				f.alias ="";
				f.text = s;
				string al = f.alias;
				
				if (i >=0)
				{
					filter[i] = filter[i] ? true : (al.Trim() == reference.Trim());
				}
				
				i++;
			}
		}
		
		#endregion
		
		/// <summary>
		/// search in all objects of query for specified ref to table or field
		/// </summary>
		/// <param name="reference"> ref name</param>
		/// <param name="reftype"> true - table, false - field (in search determine where add a dot - reference+"." or "."+reference )</param>
		/// <param name="del"> true - delete all objects that have reference, false - change ref obj</param>
		void FindAndProcRefTo(string reference, string newRef, bool reftype, bool del)
		{
			string r, n;
			if (reftype)
			{
				reference = reference.Replace(' ', '_');
				newRef = newRef.Replace(' ', '_');
			}
			r = reference;
			n = newRef;
			#region change reference for parse in sql text blocks
			if (reftype)
			{
				reference = " "+ reference + ".";
				newRef = " "+ newRef + ".";
			} else
			{
				reference =" " + reference +" " ;
				newRef = " " + newRef +" ";
			}
			#endregion
			
			#region define global param for Do proc
			procStr = new string[4];
			procStr[0] = reference;
			procStr[1] = newRef;
			procStr[2] = r;
			procStr[3] = n;
			#endregion
			
			string[] procArr;
			bool[] filter;
			ObjectProc Do = null;
			
			#region define ObjectProc - Do
			if (del)
			{
				Do = null;
			} else
			{
				Do = ObjectDo;
			}
			#endregion
			
			#region fields
			if(reftype || del)
			{
				table t = new table("","",0);
				t.Fields = GetVirtTableFields();
				procArr = t.Strings;
				
				filter =GetModifyIdxArr(procArr, reference, 0);
				FilterPartition(filter, 0, Do);
			}
			#endregion
			
			if (!reftype)  // extract field alias for parsing in next blocks
			{
				field f = new field();
				f.text = reference;
				
				if (!string.IsNullOrEmpty(f.alias))
				{
					reference = " " + f.alias + " ";
				}
				
				#region filtering by alias of the field (in groups and orders fields can be presented both ways: by field alias or full text)
				//groups
				filter =GetModifyIdxArrFullMatch(groups, reference, 0);
				FilterPartition(filter, 4, Do);
				//orders
				filter =GetModifyIdxArrFullMatch(TruncOrderSorting(orders), reference, 0);
				FilterPartition(filter, 6, Do);
				#endregion
				
				if (r.Trim() == f.alias) // if reference is a single word, it is alias, and searching as full-pathed field not necessary
				{
					reference = "###";
				} else
				{
					reference = r;
				}
			}
			
			//groups
			filter =GetModifyIdxArr(groups, reference, 0);
			FilterPartition(filter, 4, Do);
			//orders
			filter =GetModifyIdxArr(TruncOrderSorting(orders), reference, 0);
			FilterPartition(filter, 6, Do);
			
			//HA CK: field alias change must be processed
			if ((reftype)) // when we delete field from selection list, it can stay in this sections
			{
				
				#region wheres
				join jw = new join(0); // join struct uses here to wrap conditions array, and get its Strings property
				jw.ConditionzA = wheres;
				jw.setConditionNumber(wheresCou);
				procArr = jw.Strings; // first element is join header
				
				filter =GetModifyIdxArr(procArr, reference, 0); // offset -1 because first element is header
				FilterPartition(filter, 3, Do);
				#endregion
				
				#region Joins
				procArr = new string[joinCou];
				StringBuilder b = new StringBuilder();
				
				for (int i = 0; i< joinCou; i++)
				{
					procArr[i] = joins[i].ToString();
				}
				
				filter =GetModifyIdxArr(procArr, reference, 0);
				
				if (del)
				{
					FilterPartition(filter, 2, Do);
					
				} else
				{
					#region  joins ref changing
					for (int i = 0; i< joinCou; i++)
					{
						if (filter[i])
						{
							condition[] ca = joins[i].ConditionzA;
							#region each condition loop
							for (int j=0; j < ca.Length; j++)
							{
								string s = ca[j].textForm; // ca[j].text delete all join conditions currently edited
//								b.Remove(0, b.Length);
//								b.Append(s);
//								b.Replace(reference, newRef);
								QueryParser.Rename(s, reference, newRef);
								ca[j].text = QueryParser.Rename(s, reference, newRef);
							}
							#endregion
							joins[i].ConditionzA = ca;
						}
						if (reftype)
						{
							if (joins[i].leftTable == r)  { joins[i].leftTable = n; }
							if (joins[i].rightTable == r) { joins[i].rightTable = n; }
						}
					}
					#endregion
				}
				
				#endregion
				
			}
		}
		
		#region fields manipulations
		
		public field[] GetVirtTableFields()
		{
			field[] rez = new field[fieldCou];
			Array.Copy(fields, rez, fieldCou);
			
			return rez;
		}
		
		public string[] FldStrings()
		{
			table t = new table("","",0);
			t.Fields = GetVirtTableFields();
			return t.Strings;
		}
		
		void UnmarkFields()
		{
			procStr = new string[1];
			procStr[0] = "clear";
			bool[] filter = new bool[fieldCou];
			for(int i =0; i < fieldCou; i++)
			{
				filter[i] = true;
			}
			FilterPartition(filter, 0, FieldDoMark);
		}
		
		public void MarkFldFromTable(string TableNAme)
		{
			table t = new table("","",0);
			t.Fields = GetVirtTableFields();
			string[] procArr = t.Strings;
			if (procArr.Length > 0)
			{
				TableNAme = TableNAme.Replace(' ', '_');
				procStr = new string[1];
				procStr[0] = "clear";
				bool[] filter = new bool[procArr.Length];
				for(int i =0; i < procArr.Length; i++)
				{
					filter[i] = true;
				}
				FilterPartition(filter, 0, FieldDoMark);
				
				procStr[0] = "fill";
				filter = GetModifyIdxArr(procArr, " "+ TableNAme.ToLower() + ".", 0);
				FilterPartition(filter, 0, FieldDoMark);
			}
		}
		
		public bool IsMArkedFld(int idx)
		{
			return (fields[idx].mark == 1);
		}
		
		public void FieldMoveTo(int fIdx, int newIdx)
		{
			if(newIdx >= fieldCou)
			{
				for(int i = fieldCou; i <= newIdx ; i++)
				{
					AddField("null");
				}
			}
			MoveElementWithinPartition( fIdx, newIdx, 0);
		}
		
		public void FieldReplace(int fIdx, int newIdx, string[] aliases)
		{
			if(newIdx >= fieldCou)
			{
				if (aliases.Length > newIdx)
				{
					for(int i = fieldCou; i <= newIdx ; i++)
					{
						AddField("null as "+aliases[i] );
					}
				} else
				{
					MessageBox.Show("there no alias with index " +newIdx.ToString());
				}
			} else
			{
				string defAli = "";
				if((aliases != null) && (aliases.Length > fieldCou))
				{
					defAli = aliases[fieldCou];
				}
				
				if(fields[newIdx].text.Trim() != "null")
				{
					AddField(fields[newIdx], defAli);
				}
			}

			string newa = fields[newIdx].alias;
			string olda = fields[fIdx].alias;
			
			fields[newIdx] = fields[fIdx];
			fields[newIdx].alias = newa;
			
			fields[fIdx].text = "null";
			fields[fIdx].alias = olda;
			
			for(int j = (fieldCou -1); j >= 0; j-- )
			{
				if(fields[j].text == "null")
				{
					fieldCou --;
				} else
					break;
			}

		}
		
		public void FieldChangeAlias(int fIdx, string alias)
		{
			if(fields[fIdx].alias != alias)
			{
				FindAndProcRefTo(fields[fIdx].alias, alias, false, false);
				fields[fIdx].alias = alias;
			}
		}
		
		public void DeleteField(string alias)
		{
			FindAndProcRefTo(alias, "", false, true);
		}
		
		public void UpdateField(int fIdx, string newText)
		{
			string saveAlias = fields[fIdx].alias;
			fields[fIdx].alias = "";
			fields[fIdx].text = newText;
			if(!string.IsNullOrEmpty(saveAlias))
			{
				fields[fIdx].alias = saveAlias;
			}
			if(fIdx >= fieldCou) {fieldCou = fIdx+1;}
		}
		
		#endregion
		
		#region tables man
		
		/// <summary>
		/// if index = true, returns index of first occurence such alias;
		/// if index = false, returns count of tables such alias
		/// </summary>
		public int HaveSuchTable(string s, bool index)
		{
			int rez;
			
			if (index)
			{
				rez = -1;
			} else
			{
				rez = 0;
			}
			s = s.ToLower();
			
			for(int i = 0; i < tableCou; i++)
			{
				if(tables[i].alias.ToLower() == s)
				{
					if (index)
					{
						return i;
					} else
					{
						rez++;
					}
				}
			}
			return rez;
		}
		
		public int EnumSuchDBTable(string n)
		{
			int rez = 0;
			n = n.ToLower();
			
			for(int i = 0; i < tableCou; i++)
			{
				if(tables[i].Name.ToLower() == n)
				{
					rez++;
				}
			}
			return rez;
		}
		
		public table[] GetTables()
		{
			table[] rez = new table[tableCou];
			Array.Copy(tables, rez, tableCou);
			return rez;
		}
		
		public static int GetDBTableByName(string n)
		{
			int rez = -1;
			n = n.ToLower();
			int c = 0;
			
			if (dbTables != null)
			{
				foreach (table t in dbTables)
				{
					if(t.alias == null) {continue;}
					if (t.alias.ToLower() == n)
					{
						rez = c;
					}
					c++;
				}
			}
			
			return rez;
		}
		
		public void DeleteTable(string alias)
		{
			alias = alias.Replace(' ', '_');
			int idx = HaveSuchTable(alias, true);
			if (idx != -1)
			{
				if(tables[idx].queryId >= 0)
				{
					AddObjectToBeNulledOnSave(tables[idx].queryId);
				}
				
				tableCou--;
				for (int i = idx; i < tableCou; i++)
				{
					tables[i] = tables[i + 1];
				}
				FindAndProcRefTo(alias.ToLower(), "", true, true);
			}
		}
		
		public string AddDBTable(string tableName)
		{
			int idx = EnumSuchDBTable(tableName);
			string alias = "";
			if (idx != 0)
			{
				idx ++;
				alias = tableName + "_" + idx.ToString();
			}
			
			while (HaveSuchTable(alias, true) >= 0)
			{
				alias += "_";
			}
			
			table t = new table(tableName, alias, 0);
			AddTable(t);
			
			return t.alias;
		}
		
		public string ChangeAlias(string alias, string newAlias)
		{
			string rez = "";
			alias = alias.Trim().Replace(' ','_');
			newAlias = newAlias.Trim().Replace(' ','_');
			
			int idx = HaveSuchTable(alias, true);
			if (idx == -1)
			{
				rez += " %(  No such alias: "+alias;
			}
			
			if (HaveSuchTable(newAlias, true) != -1)
			{
				rez += "  Alias already exists: "+newAlias;
			}
			
			if (string.IsNullOrEmpty(newAlias))
			{
				rez += "  Empty alias :(";
			}
			
			if (!QueryParser.IsValidOperandName(newAlias))
			{
				rez += "  Reserved word can NOT be an alias :(";
			}
			
			if (rez == "")
			{
				tables[idx].alias = newAlias;
				FindAndProcRefTo(alias.ToLower(), newAlias.ToLower(), true, false);
			}
			
			return rez;
		}
		
		public void AddVirtTable()
		{
			int i= 1;
			string alias = "VirtTable";
			while (HaveSuchTable(alias, false) > 0)
			{
				alias = "VirtTable_"+ i.ToString();
				i++;
			}
			
			i = AddListObject();
			
			table t = new table("", alias, i);
			AddTable(t);
		}
		
		public void AddExisingVirtTable(string alias, int queryIdx)
		{
			int i= 1;
			alias = alias.Replace(' ', '_');
			if(string.IsNullOrEmpty(alias)) {alias = "VirtTable";}
			string originalAlias = alias;
			while (HaveSuchTable(alias, false) > 0)
			{
				alias = originalAlias +"_"+ i.ToString();
				i++;
			}
			
			table t = new table("", alias, queryIdx);
			AddTable(t);
		}
		
		public table GetTableByName(string alias)
		{
			table rez;
			int tId = HaveSuchTable(alias, true);
			
			if(tId >= 0)
			{
				rez = tables[tId];
			} else
			{
				rez = new table("","", -1);
			}
			
			if (rez.queryId != -1 )
			{
				rez.Fields = QueryObjects[rez.queryId].GetVirtTableFields();
			}
			
			return rez;
		}
		
		public int IsTableJoined(string tableAlias)
		{
			int rez = -1;
			tableAlias = tableAlias.ToLower();
			int idx = HaveSuchTable(tableAlias, true);
			string tableName = (idx >=0)? tables[idx].Name : tableAlias;
			
			for(int i = 0; i < joinCou; i++)
			{
				if ((joins[i].rightTable == tableAlias) || (joins[i].rightTable == tableName))
				{
					rez = i;
				}
			}
			
			return rez;
		}
		
		public void SortTablesInJoinOrder()
		{
			if(joinCou <=0 ){return;}
			string[] names = new  string[joinCou +1];
			
			for(int i = 0; i < joinCou; i++)
			{
				names[i] = joins[i].leftTable.Trim().ToLower();
			}
			names[joinCou] = joins[joinCou -1].rightTable.Trim().ToLower();
			if(string.IsNullOrEmpty(names[0]))
			{
				names[0] = names[joinCou];
			}
			
			int[] positions = new int[tableCou];
			int notFound = 0;
			for(int i = 0; i < tableCou; i++)
			{
				positions[i] = Array.IndexOf(names, tables[i].alias.Trim().ToLower());
				if(positions[i] < 0) {notFound ++;}
			}
			
			table[] rez = new table[tableCou];
			int notJoinedIdx = 0;
			
			for(int i = 0; i < tableCou; i++)
			{
				rez[(positions[i] >= 0) ? (positions[i] + notFound) : notJoinedIdx++ ] =  tables[ i ];
			}
			
			Array.Copy(rez, tables, tableCou);
		}
		
		#endregion
		
		#region union utilities
		
		public int[] GetUnionedQId()
		{
			int[] rez = new int[unionCou];
			if (IsUnioned)
			{
				Array.Copy(unions, rez, unionCou);
			}
			return rez;
		}
		
		public int GetNUnion(int n)
		{
			int rez;
			rez = ((n >= unionCou) || (n < 0)) ? ObjIdx : unions[n];
			return rez;
		}
		
		public string[,] GetUnionAliasTable()
		{
			int columns = unionCou +1;
			string[,] rez = new string[100, columns];
			int aliCou = 0;
			string[] totalFlds = new string[100];
			int currCol = 0;
			
			for(int i = 0; i < unionCou  ; i++) 
			{
				field[] tmp;
				
				tmp = QueryUnions[i].GetVirtTableFields();
				currCol = i + 1;
				
				if (aliCou < tmp.Length)
				{
					for(int j= aliCou - ((aliCou >0) ? 1 : 0); j< tmp.Length; j++)
					{
						rez[j,0] = tmp[j].alias;
					}
					
					aliCou = tmp.Length; //+ ((unionCou > 1)? 1 :0 );
				}
				
				for(int j= 0; j< tmp.Length; j++)
				{
					rez[j, currCol] = tmp[j].text;
				}
			}
			maxUFldCou = aliCou;
			
			return rez;
		}
		
		public void DeleteUnionedQuery(int idx)
		{
			if(idx > 0)
			{
				if(QueryUnions[idx].ObjIdx >= 0)
				{
					AddObjectToBeNulledOnSave(QueryUnions[idx].ObjIdx);
				}
				
				for(int i = idx; i < (unionCou-1); i++)
				{
					QueryUnions[i] = QueryUnions[i + 1] ;
				}
				bool[] filter = new bool[unionCou];
				filter[idx] = true;
				FilterPartition(filter, 7, null);
			}
		}
		
		public string ChangeUnionModifier()
		{
			UnionParam = string.IsNullOrEmpty(UnionParam.Trim()) ? " all" : "";
			return ((UnionParam == "") ? " ___" : UnionParam);
		}
		
		#endregion
		
		/// <param name="p"> 0: grouping available; 1: ordering available</param>
		string[] GetAvailableItems(int p)
		{
			string[] rez = FldStrings(); // get all fields
			bool[] filter = new bool[fieldCou];
			#region get partition data
			string[] filtrArr;
			if(p == 1)
			{
				filtrArr = TruncOrderSorting(orders);
			} else
			{
				filtrArr = groups;
			}
			#endregion
			
			// filtering already grouped fields
			field f = new field();
			foreach (string s in filtrArr)
			{
				f.text = s;
				
				if ((!string.IsNullOrEmpty(f.alias)) && (f.alias == s.Trim()))
				{
					GetModifyIdxArrAliasMatch(rez, ref filter, s, 0);
				} else
				{
					GetModifyIdxArr(rez, ref filter, s, 0);
				}
			}
			
			if (p == 0) // group
			{
				#region filtering aggregate fields
				for (int i=0; i < fieldCou; i++)
				{
					rez[i] = string.IsNullOrEmpty(fields[i].alias) ? fields[i].text : fields[i].alias;
					filter[i] =  filter[i] ? true : QueryParser.GetExpresionStruct(fields[i].text).Contains("A(");
				}
				#endregion
			}
			
			if (p == 1) // order
			{
				for (int i=0; i < fieldCou; i++)
				{
					rez[i] = string.IsNullOrEmpty(fields[i].alias) ? fields[i].text : fields[i].alias;
				}
			}
			
			tmp = rez;
			FilterPartition(filter, 101, null);
			
			Array.Resize(ref rez, tmp.Length);
			Array.Copy(tmp, rez, tmp.Length);
			return rez;
		}
		
		#region Grouping list man
		
		public string[] GetGroupingAvailableItems()
		{
			return GetAvailableItems(0);
		}
		
		public string[] GetAgregateFields()
		{
			string[] rez = FldStrings(); // get all fields
			bool[] filter = new bool[fieldCou];
			
			// filtering aggregate fields
			for (int i=0; i < fieldCou; i++)
			{
				filter[i] = !QueryParser.GetExpresionStruct(fields[i].text).Contains("A(");
			}
			tmp = rez;
			FilterPartition(filter, 101, null);
			
			Array.Resize(ref rez, tmp.Length);
			Array.Copy(tmp, rez, tmp.Length);
			return rez;
		}
		
		public void GroupingMoveTo(int fIdx, int newIdx)
		{
			MoveElementWithinPartition( fIdx, newIdx, 4);
		}
		
		public void DeleteGrouping(string s)
		{
			FilterPartition(GetModifyIdxArr(groups, s, 0), 4 ,null);
		}
		
		#endregion
		
		#region Order list man
		
		public string[] GetOrderingAvailableItems()
		{
			return GetAvailableItems(1);
		}
		
		public void DeleteOrder(string s)
		{
			FilterPartition(GetModifyIdxArr(orders, s, 0), 6 ,null);
		}
		
		public void OrderMoveTo(int fIdx, int newIdx)
		{
			MoveElementWithinPartition( fIdx, newIdx, 6);
		}
		
		public string SwitchOrderSorting(int fIdx)
		{
			string mdf = " asc";
			if (QueryParser.IsSortExplicit(orders[fIdx]))
			{
				int cutl = orders[fIdx].Length;
				orders[fIdx] = orders[fIdx].TrimEnd();
				mdf = orders[fIdx].Substring(orders[fIdx].Length -4, 4);
				
				if(mdf.ToLower() == " asc")
				{
					mdf = " desc";
				} else
				{
					mdf = " asc";
				}
				
				orders[fIdx] = QueryParser.TruncOrderMdf(orders[fIdx]) + mdf;
				
			} else
			{
				orders[fIdx] += " asc";
			}
			
			return orders[fIdx];
		}
		
		string[] TruncOrderSorting(string[] orderList)
		{
			string[] rez = new string[orderList.Length];
			
			for(int i = 0; i < orderList.Length; i++)
			{
				string tmp = orderList[i].ToLower().TrimEnd();
				if ((tmp.EndsWith(" asc")) || (tmp.EndsWith(" desc")))
				{
					tmp = tmp.Substring(0, tmp.Length - 4);
				}
				rez[i]= tmp;
			}
			
			return rez;
		}
		
		#endregion
		
		
		public void DeleteJoing(int idx)
		{
			if(idx >= joinCou) {return;}
			bool[] f = new bool[joinCou];
			f[idx] = true;
			FilterPartition(f, 2, null);
		}
		
		public void MakeFieldAggregate(string alias, string func)
		{
			string[] op;
			string expr = QueryParser.GetExpresionStruct(alias, out op);
			alias = op[op.Length -1];
			
			
			for (int i=0; i < fieldCou; i++)
			{
				if (fields[i].alias == alias)
				{
					string text = fields[i].text;
					#region deleting AS block  A(O)OO => A(O)
					if ((fields[i].fType == FieldModifiers.agrF) || (expr.StartsWith("A(") && (expr.EndsWith(")RO"))))
					{
						expr = expr.Replace('R', 'O');
						while(expr[expr.Length-1] == 'O')
						{
							expr = expr.Substring(0, expr.Length-1);
						}
					}
					#endregion
					
					if (string.IsNullOrEmpty(func))
					{
						
						if (expr.IndexOf('A', 0) >= 0)
						{
							#region updating function operends
							int idx = expr.IndexOf('A', 0);
							while(idx >= 0)
							{
								op[QueryParser.GetOperandIdxByExprIdx(expr, idx)] = func;
								idx = expr.IndexOf('A', idx + 1);
							}
							#endregion
							
							fields[i].text = QueryParser.OpenBrackets(QueryParser.Merge(expr, op));
						} else
						{
							// err handling
						}
						
					} else
					{
						if (expr.IndexOf('A', 0) >= 0)
						{
							#region updating function operends
							int idx = expr.IndexOf('A', 0);
							while(idx >= 0)
							{
								op[QueryParser.GetOperandIdxByExprIdx(expr, idx)] = func;
								idx = expr.IndexOf('A', idx + 1);
							}
							#endregion
							
							fields[i].text = QueryParser.OpenBrackets( QueryParser.Merge(expr, op));
						} else
						{
							fields[i].text = func+ "(" + text + ") ";
						}
						
					}
					
					return;
				}
			}
		}
		
	}


	public class QueryParser
	{
		#region constants
		const byte sqCou = 210;
		string[] syntaxBlocksGlobal = {"### ", "SELECT ", " FROM ", " WHERE ", " GROUP BY ", " HAVING ", " ORDER BY ", " UNION "};
		string[] syntaxUNION = {" UNION "," UNION "};
		string[] syntaxSelect= {"### ", "DISTINCT ", "ALL ", " AS ", ",", " AS ", ","};
		string[] syntaxFrom  = {"### ", " AS ", " ON ", " AND ", " AND ", " LEFT ", " RIGHT ", " FULL ", " INNER ", " OUTER ", " JOIN "};
		
		//  between like in is not null all some any
		string[] AgregateFunctionsReservedW = {"count", "sum", "avg", "max", "min", "between", "like", "in", "is", "not", "null", "all", "some", "any"};
		static string[] ReservedWBlock = {"select", "from", "where", "group", "by", "having", "order", "union"};
		static string[] AgregateFunctions = {"count", "sum", "avg", "max", "min", "stddev", "variance"};
		static string[] ReservedW = {"between", "like", "escape", "in", "is", "not", "null", "all", "some", "any", "and", "or", "as", "asc", "desc"};
		static string[] ReservedJoinsW = {"distinct", "left", "right", "full", "inner", "outer", "join", "on"};
		//    CASE WHEN (resultTotal.hr24v=resultSumm.hr24v) THEN 1 ELSE 0 END
		static string[] caseSyntax = {"case", "when", "then", "else", "end"};
		
		static char[] specSymb = {' ','+', '-', ',', '.', '(', ')', '%', '/', '?', '|', '*', '=', '<', '>', ':', ';', '{', '}',  (char)92, (char)39 /* quote char ' */};
		static char[] variousOp= {'A', 'B', 'C', 'O', 'R', 'S', 'J'};// agregate function, query block (select, from ...), case lexeme, operand, reserved words, string const, join reserved word
		static char[] reservedOp= {'A', 'B', 'C', 'R', 'S', 'J'};
		static char[] equalityOperators = {'=', '<', '>'};
		
		public static char[] reservedOpGet
		{
			get
			{
				char[] rez = new char[reservedOp.Length];
				Array.Copy (reservedOp, rez, reservedOp.Length);
				return rez;
			}
		}
		
		public static char[] variousOpGet
		{
			get
			{
				char[] rez = new char[variousOp.Length];
				Array.Copy (variousOp, rez, variousOp.Length);
				return rez;
			}
		}
		string[] rowClosedUNION = {" UNION(", ")UNION(", ")UNION ",  " WHERE(", ")WHERE(", ")WHERE ",    " FROM(", ")FROM(", ")FROM ", ")AS ",    ")and ", ")and(", " and(",     ")or ", ")or(",  " or(",   "><", "> <", ")as ", " as ", " all("}; 
		string[] normClosedUNION= {" UNION (",") UNION (",") UNION "," WHERE (",") WHERE (",") WHERE ", " FROM (", ") FROM (",") FROM ", ") AS ", ") and ",") and (", " and (", ") or ",") or (", " or (",  "<>",  "<>", ") ",    " " ,  " all ("};
		#endregion
		
		#region Data
		
		public string errMessages; // куда ж без них :)
		string inlineText;     // GLOBAL CON_TEXT
		int procTextPos;       // processed part of inlineText (no more parsed for lexems)
		string previousBlock;  // previously processed block (in string form) from current syntaxBlocks array
		public byte Q;         // number of queries (and inlineTextArr count)
		public int[] QueryIdx;
		string[] inlineTextArr;// all [sub]Queries parsed from initiall text
		string[] syntaxBlocks; // содержит текущий набор лексем который будет обрабатываться функциями сегментации
		public int root
		{
			get
			{
				return QueryIdx[0];
			}
		}
		
		bool BlockComment;  // uses only for comments abjunction in Text
		#endregion
		
		#region arrays of blocks  // public only for debug time
		 string[] selects;
		 string[] fromz;
		 string[] wherez;
		 string[] groupingz;
		 string[] havingz;
		 string[] orderz;
		#endregion
		
		public string[] Text
		{
			set
			{
				StringBuilder SB = new StringBuilder();
				BlockComment = false;
				
				string tmp;
				foreach (string ln in value)
				{
					tmp = DelComment(ln);
					SB.Append(' '+ tmp.Trim());
				}
				inlineText = SB.ToString();
				inlineTextArr[0] = inlineText.ToLower();
				
			}
		}
		
		public QueryParser()
		{
			Q = 0;
			errMessages = "";
			procTextPos = 0;
			inlineTextArr = new string[sqCou];
			selects  = new string[sqCou];
			fromz    = new string[sqCou];
			wherez   = new string[sqCou];
			groupingz = new string[sqCou];
			havingz  = new string[sqCou];
			orderz   = new string[sqCou];
			QueryIdx = new int[sqCou];
		}
		
		//<<========================= FUNCTIONS declaration =================================>>		
		//  TODO: обрабатывать условия OR
//		global problems
		
		
		#region string utils
		
		string DelComment(string s)
		{
			string rez = "";
			int path = 0;
			bool tmpBlockComment = BlockComment;
			
			if (s.Contains("/*"))
			{
				rez = GetEnclosedSegmText("### "+s, "### ", "/*", true);
				tmpBlockComment = true;
				path++;
			}
			
			if (s.Contains("*/"))
			{
				rez += GetEnclosedSegmText(s + "###", "*/", "###", true);
				tmpBlockComment = false;
				path++;
			}
			
			if ((rez == "") && (!tmpBlockComment) && (path != 2))
			{
				rez = s;
			}
			
			if (rez.Contains("--"))
			{
				rez = GetEnclosedSegmText("### "+rez, "### ", "--", true);
			}
			
			if (rez.Contains("//"))
			{
				rez = GetEnclosedSegmText("### "+rez, "### ", "//", true);
			}
			
			if (BlockComment)
			{
				rez = "";
			}
			BlockComment = tmpBlockComment;
			return rez;
		}
		
		public static bool IsValidOperandName(string s)
		{
			return !(EnumCharOccurrence(GetExpresionStruct(s), reservedOp) > 0);
		}
		
		public static bool CheckBracketsClosed(string s)
		{
			int tmp = 0;
			bool rez = true;
			
			for (int i = 0; i < s.Length; i++ )
			{
				if (s[i] == '(')
				{
					tmp++;
				}
				if (s[i] == ')')
				{
					tmp--;
					if(tmp < 0){rez = false;}
				}
			}
			return ((tmp == 0) && rez);
		}
		
		public static int EnumCharOccurrence(string s, char[] c)
		{
			int rez = 0;
			foreach(char item in c)
			{
				rez += EnumCharOccurrence(s, item);
			}
			return rez;
		}
		
		public static int EnumCharOccurrence(string s, char c)
		{
			int rez = 0;
			
			for (int i = 0; i < s.Length; i++ )
			{
				if (s[i] == c)
				{
					rez++;
				}
			}
			return rez;
		}
		
		public static bool IsNumber(string s)
		{
			if (s == null)
			{
				return false;
			}
			bool rez = true;
			
			for (int i = 0; i < s.Length; i++ )
			{
				if (Char.IsNumber(s[i]) == false)
				{
					rez = false;
					break;
				}
			}
			return rez;
		}
		
		public static string TrimBrackets(string s)
		{
			s = s.Trim();
			char[] br = {'(',')'};
			
			s = s.Trim(br);
			
			return s;
		}
		
		public static string TruncOrderMdf(string s)
		{
			string rez = s;
			if (IsSortExplicit(s))
			{
				rez = s.TrimEnd().Substring(0, s.Length - 4).TrimEnd();
			}
			
			return rez;
		}
		
		public static bool IsSortExplicit(string s)
		{
			bool rez = false;
			string tmp = s.ToLower().TrimEnd();
			if ((tmp.EndsWith(" asc")) || (tmp.EndsWith(" desc")) || (tmp.EndsWith(" all")) || (tmp.EndsWith(" ___")) )
			{
				rez = true;
			}
			
			return rez;
		}
		
		void NormalizeLexemSpacing()
		{
			StringBuilder b = new StringBuilder(inlineText);
			
			for(int i = 0; i < rowClosedUNION.Length; i++)
			{
				b.Replace(rowClosedUNION[i], normClosedUNION[i]);
			}
			inlineText = b.ToString();
		}
		
		public static string NormalizeSpaces(string s)
		{
			char[] arr;
			int newLen = 0;
			char prev = 'a';
			arr = new char[s.Length];
			bool constOpen = false;
			
			for (int i = 0; i < s.Length; i++ )
			{
				if (s[i] == (char)39) {constOpen = !constOpen;}
				if (s[i] == (char)0) {continue;}
				
				if ( (((prev == ' ') && (s[i] == ' ') ) != true) || (constOpen) )
				{
					arr[newLen] = s[i];
					prev = s[i];
					newLen++;
				}
			}
			Array.Resize(ref arr, newLen);
			
			string rez = new string(arr);
			return rez;
		}
		
		void NormalizeSpaces()
		{
			inlineText = NormalizeSpaces(inlineText);
		}
		
		public static string OpenBrackets(string s)
		{
			s = s.Trim();
			string orig = s;
			
			if(string.IsNullOrEmpty(s)) {return "";}
			
			#region path for 0 terminated string
			if ( s[s.Length -1] == (char)0)
			{
				s = s.Substring(0, s.Length - 1 );
			}
			#endregion
			
			if ( (s.StartsWith("(")) && (s.EndsWith(")")) )
			{
				s = s.Substring(1, s.Length - 1 );
				s = s.Trim();
				s = s.Substring(0, s.Length - 1 );
			}
			if(!CheckBracketsClosed(s))
			{
				s = orig;
			}
			
			return s;
		}
		
		public static string InserText(string s, string ins, int pos)
		{
			if(pos > s.Length) {return s;}
			string rez = s;
			string space = "";
			ins = ins.TrimEnd();
			
			segmCoord sc = QueryParser.GetSubstrSegmByCharNum(s, pos);
			if(!sc.isEmpty)
			{
				pos = sc.fin;
				space = " ";
			}
			
			rez = s.Substring(0, pos +((pos == s.Length) ? 0 : 1)) + space + ins + s.Substring(pos +((pos == s.Length) ? 0 : 1));
			
			return rez;
//			operand1.SelectionStart
		}
		
		public static segmCoord GetSubstrSegmByCharNum(string s, int pos)
		{
			segmCoord rez;
			bool spaceFound = false;
			int p = pos;
			rez.start = p;
			rez.fin = p;
			while ((p > 0) && (!spaceFound))
			{
				p -= 1;
				int index = Array.IndexOf( specSymb, s[p]);
				if ((index != -1) && (s[p] != '.'))
				{
					spaceFound = true;
				} else
				{
					rez.start = p;
				}
			}
			
			p = pos-1; // -1 works well
			spaceFound = false;
			while ((p < (s.Length -1)) && (!spaceFound))
			{
				p += 1;
				int index = Array.IndexOf( specSymb, s[p]);
				if ((index != -1) && (s[p] != '.'))
				{
					spaceFound = true;
				} else
				{
					rez.fin = p;
				}
			}
			
			return rez;
		}
		
		string[] SplitBy(string s, string separator)
		{
			string[] rez = new string[255];
			int rezCou = 0;
			byte lastBlock = 0;
			string[] com = {"###", separator, separator};
			
			initNewParse("###" + s, com);
			
			bool repeat = true;
			while (repeat)
			{
				string block = "";
				repeat = false;
				
				block = GetBlockTextByNumberInline(ref lastBlock).Trim();
				
				#region	separating loop flow
				if (syntaxBlocks[lastBlock] == separator )
				{
					repeat = true;
					lastBlock = 1;
				}
				#endregion
				
				#region	add new array item
				if ((block != "") && (block != separator))
				{
					rez[rezCou] = block;
					if (rezCou == 255)
					{
						//err handle
					} else
					{
						rezCou++;
					}
				}
				#endregion
			}
			
			Array.Resize(ref rez, rezCou);
			
			return rez;
		}
		
		public static string OpenUnionParamBrackets(string s)
		{			
			s = s.Trim();
			string rez = s;
			
			if((s.StartsWith("all (")) && (s.EndsWith(")")))
			{
				rez = "all " + OpenBrackets(s.Substring(3));
			} else
			{
				rez =  OpenBrackets(s);
			}
			
			return rez;
		}
		
		#endregion string utils
		
		#region parse utils
		
		void initNewParse(string s, string[] blocks)
		{
			inlineText = s;
			procTextPos = 0;
			Array.Resize(ref syntaxBlocks, blocks.Length);
			Array.Copy(blocks, syntaxBlocks, blocks.Length);
			previousBlock = "";
		}
		
		#region обложки для GetEnclosedSegm
		
		string GetEnclosedSegmText(string s, string open, string enclose)
		{
			return GetEnclosedSegmText(s, open, enclose, false);
		}
		
		string GetEnclosedSegmText(string s, string open, string enclose, bool ignoreBrackets)
		{
			string rez = "";
			string TMPinlineText = inlineText;
			inlineText = s;
			segmCoord Region = GetEnclosedSegm(0, open, enclose, true, ignoreBrackets);
			
			if ((Region.start <= Region.fin) && (Region.start > 0))
			{
				rez = inlineText.Substring(Region.start, Region.fin - Region.start +1);
			}
			
			inlineText = TMPinlineText;
			return rez;
		}
		
		segmCoord GetEnclosedSegm(int st, string open, string enclose, bool allowSequental)
		{
			return GetEnclosedSegm(st, open, enclose, allowSequental, false);
		}
		
		#endregion
		
		segmCoord GetEnclosedSegm(int st, string open, string enclose, bool allowSequental, bool ignoreBrackets)
		{
			segmCoord rez;
			byte brackets = 0;
			byte openedCou = 0;
			int num = st;
			int openLen = open.Length;
			int enLen = enclose.Length;
			string br = "(";
			rez.fin = 0;
			rez.start = 0;
			open = open.ToLower();
			enclose = enclose.ToLower();
			int  maxL = inlineText.Length;
			int serclen = openLen;
			
			while ((num >= 0)&&((num - 1 +  Math.Min(enLen,openLen) + (allowSequental ? 0 : 1)) < maxL) )
			{
				
				if ((brackets == 0) || (open.Contains(br)) || ignoreBrackets)  // not searching open and enclose in the internal statements enclosed ( ) , thus there can be local appearance of open and enclose substr that will be omitted
				{
					#region search open
					if (inlineText[num].ToString().ToLower() == open[0].ToString())
					{
						string sub = allowSequental ? inlineText.Substring(num , ((num + openLen) < maxL) ? openLen : 1 ) : inlineText.Substring(num -1, openLen +1);
						if (sub.ToLower() == open)
						{
							openedCou++;
							if (openedCou == 1)
							{
								rez.start = num + openLen;
								num = num + openLen;
								continue;
							} else
							{
								if (open == enclose)  // no comments :)
								{
									openedCou--;
									rez.fin = num - 1;
									num = -3;
									continue;
								}
							}
						}
					}
					#endregion
					
					#region search close
					if (inlineText[num].ToString().ToLower() == enclose[0].ToString())
					{
						string sub = allowSequental ? inlineText.Substring(num ,  ((num + enLen) < maxL) ? enLen : 1 ) : inlineText.Substring(num -1, enLen +1);
						if (sub.ToLower() == enclose)
						{
							if (openedCou > 0)
							{
								openedCou--;
								if (openedCou == 0)
								{
									rez.fin = num - 1;
									num = -3;
								}
							} else
							{
								// err handle
								errMessages = string.Concat( errMessages, "  enclosing err at:  ", inlineText.Substring(num - 2, 21 ) );
								num = -3;
							}
						}
					}
					#endregion
				}
				
				#region brackets
				if ((num >= 0) && (ignoreBrackets != true))
				{
					if (inlineText[num] == '(')
					{
						brackets++;
//						MessageBox.Show(brackets.ToString());
					}
					
					if (inlineText[num] == ')')
					{
						if (brackets==0)
						{
							//errhandling
							errMessages = string.Concat( errMessages, " closed brackets err at:  ", inlineText.Substring(num - 20, 21 ) );
							num = -3;
						} else	 {brackets--;}
//						MessageBox.Show(brackets.ToString());
					}
				}
				#endregion
				
				num++;
			}
			
			return rez;
		}
		
		#region обложки для GetBlockTextByNumber
		
		string GetBlockTextByNumberInline(ref byte n)
		{
			return GetBlockTextByNumber(ref n, (byte)syntaxBlocks.Length, true, 1);
		}
		
		string GetBlockTextByNumberInline(ref byte n, byte finReg)
		{
			return GetBlockTextByNumber(ref n, finReg, true, 1);
		}
		
		string GetBlockTextByNumber(ref byte n)
		{
			return GetBlockTextByNumber(ref n, (byte)syntaxBlocks.Length, false, 1);
		}
		
		#endregion
		
		string GetBlockTextByNumber(ref byte n, byte finReg, bool inline, byte offset)
		{
			segmCoord Region;
			byte next = 1;
			Region.start = 0;
			Region.fin = 0;
			string rez ="";
			
			
			while ((Region.fin == 0) && ((n+next) < finReg))
			{
				Region = GetEnclosedSegm(procTextPos, syntaxBlocks[n], syntaxBlocks[n+next], inline);
				next++;
				if (errMessages != "")
				{
//					errMessages = errMessages + " !!!at Block " +syntaxBlocks[n];
//					break;
				}
			}
			
			if (Region.fin != 0)
			{
				if (Region.start <= Region.fin)
				{
					rez = inlineText.Substring(Region.start, Region.fin - Region.start +1);
				} else
				{rez = "";}
				previousBlock = syntaxBlocks[n];
				n = (byte)(n+next -1);
				
				procTextPos = Region.fin + offset;  // wery important and crock
				
			} else
			{
				previousBlock = syntaxBlocks[n];
				rez = inlineText.Substring(Region.start, inlineText.Length - Region.start );
				n = 0; // coma (and other substr) repeating issue
			}
			
			return rez;
		}
		
		#region Обложки для GetExpresionStruct
		public static string GetExpresionStruct(string s)
		{
			string[] OpArr;
			return GetExpresionStruct(s, out OpArr, false, true);
		}
		
		public static string GetExpresionStruct(string s, out string[] OpArr)
		{
			return GetExpresionStruct(s, out OpArr, false, true);
		}
		
		public static string GetExpresionStruct(string s, out string[] OpArr, bool opSpacing)
		{
			return GetExpresionStruct(s, out OpArr, opSpacing, true);
		}
		
		public static string GetExpresionStructUntyped(string s, out string[] OpArr)
		{
			return GetExpresionStruct(s, out OpArr, false, false);
		}
		
		#endregion
		
		/// <summary>
		/// function not trimming input string;
		/// if string ends by space, last item in OpArr will be null !!!
		/// </summary>
		public static string GetExpresionStruct(string s, out string[] OpArr, bool opSpacing , bool useTypification)
		{
			if (s == null)
			{
				OpArr = new string[0];
				return "";
			}

            s = s;// s.ToLower();
			string rez = "";
			bool isPrevSpec = true;
			int OperCou = 0;
			
			string[] OperandArr = new string[(int)(s.Length / 2)+1]; // in incredible case there can be a half-length number of operands, because else part of characters will be a spec symbols to separate it
			bool strConstOpened = false;
			
			#region character-oriented processing for separation on the operands
			for (int i = 0; i < s.Length; i++ )
			{
				int index = Array.IndexOf( specSymb, s[i]);
				
				#region string constants
				if ((strConstOpened == true) && (s[i] != (char)39))
				{
					if (isPrevSpec)
					{
						rez += "S";
						isPrevSpec = false;
					}
					OperandArr[OperCou] += s[i];
					continue;
				}
				
				if (s[i] == (char)39)
				{
					strConstOpened = !strConstOpened;
				}
				#endregion
				
				if (index == -1)
				{
					#region	Operand proc
					if (isPrevSpec)   // current char is a start of new operand, so operand mask added to the struct
					{
						rez += "O";
						isPrevSpec = false;
					}
					OperandArr[OperCou] += s[i];
					#endregion
				} else
				{
					#region Spec symbol separating
					
					if ((s[i] != ' ') || (opSpacing)) // space not including to the expresion structure
					{
						rez += s[i];
					}
					
					if (isPrevSpec == false) // when couple spec symbols go inline ( >=, <>, ...) there no (new) operands between
					{
						OperCou++;
					}
					
					isPrevSpec = true;
					#endregion
				}
			}
			#endregion
			
			OpArr = OperandArr;
			Array.Resize(ref OpArr, OperCou+1); // OperCou+1
			
			int opNum = -1;
			string tempRez = ""; // string is read only ( immutable	)
			
			if(useTypification)
			{
				#region search SQL operators among operands
				for (int i = 0; i < rez.Length; i++ )
				{
					char curr = rez[i];
					
					if (curr == 'S') // string constant not separating on operands (substrings divided by spaces, etc) and appear as single operand. example: 'this is a string constant' its wouldn't be a sequence of string operands 'this', 'is', 'a'...
					{
						opNum++;
					}
					
					if (curr == 'O')
					{
						opNum++;
						
						#region Agregate Functions
						int index = Array.IndexOf( AgregateFunctions, OperandArr[opNum]);
						if (index != -1)
						{
							curr = 'A';
						}
						#endregion
						
						#region Reserved Words
						index = Array.IndexOf( ReservedW, OperandArr[opNum]);
						if (index != -1)
						{
							curr = 'R';
						}
						#endregion
						
						#region case Syntax
						index = Array.IndexOf( caseSyntax, OperandArr[opNum]);
						if (index != -1)
						{
							curr = 'C';
						}
						#endregion
						
						#region Blocks Syntax
						index = Array.IndexOf( ReservedWBlock, OperandArr[opNum]);
						if (index != -1)
						{
							curr = 'B';
						}
						#endregion
						
						#region Blocks Joins
						index = Array.IndexOf( ReservedJoinsW, OperandArr[opNum]);
						if (index != -1)
						{
							curr = 'J';
						}
						#endregion
						
					}
					tempRez += curr;
					
				}
				#endregion
				
			} else
			{
				tempRez = rez;
			}
			
			return tempRez;
		}
		
		public static bool ContainsExpr(string s, string sub)
		{
			bool rez = false;
			string[] sArr;
			string[] subArr;
			string sExpr = GetExpresionStruct(s, out sArr);
			string subExpr = GetExpresionStruct(sub, out subArr);
			
			if (sExpr.Contains(subExpr))
			{
				int idx = 0;
				while (idx != -1)
				{
					
					idx = Array.IndexOf(sArr, subArr[0], idx);
					bool opMatch = true;
					if (idx >= 0)
					{
						for (int i = 0; i< subArr.Length; i++)
						{
							if (subArr[i] != null)
							{
								if ((sArr[idx + i] != subArr[i]) )
								{
									opMatch = false;
								}
							}
						}
						
						if (opMatch) {idx = -1; rez = true;} else {idx++;}
					}
					
				}

			}
			return rez;
		}
		
		public static string Rename(string s, string sub, string newn)
		{
			string rez = s;
			string[] sArr;
			string[] subArr;
			string[] newArr;
			string sExpr = GetExpresionStruct(s, out sArr);
			string subExpr = GetExpresionStruct(sub, out subArr);
			GetExpresionStruct(newn, out newArr);
			
			if (sExpr.Contains(subExpr))
			{
				int idx = 0;
				while (idx != -1)
				{
					
					idx = Array.IndexOf(sArr, subArr[0], idx);
					bool opMatch = true;
					if (idx >= 0)
					{
						for (int i = 0; i< subArr.Length; i++)
						{
							if (subArr[i] != null)
							{
								if ((sArr[idx + i] != subArr[i]))
								{
									opMatch = false;
								}

							}
						}
						
						string sttm = sExpr.Substring(GetOperandExprIdx(sExpr, idx ), subExpr.Length);
						if(sttm != subExpr)
						{
							opMatch = false;
						}
						
//						subExpr += "@"; // commented 23-10-10 because cause onlu one renaming in current s; why did it appear here i dont know
						
						if (opMatch)
						{
							for (int i = 0; i< subArr.Length; i++)
							{
								if (newArr[i] != null)
								{
									sArr[idx + i] = newArr[i];
								}
							}
							rez = Merge(sExpr, sArr);
//							idx = -1;
						} else
						{
							idx++;
						}
						
//						idx++;
					}
					
				}
				
			}
			
			
			
			return rez;
		}
		
		public static string Merge(string expr, string[] operands)
		{
			string rez ="";
			string space =" ";
			int opCou = -1;
			bool prevSpec = false;
			for (int i = 0; i < expr.Length; i++)
			{
				int index = Array.IndexOf(variousOp, expr[i]);
				if (index != -1)
				{
					opCou++;
					
					rez += (prevSpec ? space : "") + operands[opCou] +(EnumCharOccurrence( ".'",expr[i+ ((i+ 1 < expr.Length)? 1:0) ]) > 0 ? "" : " ");
					space =" ";
					prevSpec = false;
				} else
				{
					prevSpec = true;
					rez += expr[i];
					space = ((expr[i] == '.') || (expr[i] == (char)39))? "" : " ";
//					rez += space + expr[i];
				}
			}
			
			return rez;
		}
				
		/// <summary>
		/// Use case: we have array of operands(lexemes) that appear in expr, and needed to find an lexeme index in expr, that associated with item index in array;  lexeme: 'O', 'R', 'B'...
		/// </summary>		
		/// <returns>lexeme index in expr</returns>
		public static int GetOperandExprIdx(string expr, int opnum)
		{
			int opCou = -1;
			for (int i = 0; i < expr.Length; i++)
			{
				int index = Array.IndexOf(variousOp, expr[i]);
				if (index != -1) {opCou++;}
				if (opCou == opnum) {return i;}
			}
			return -1;
		}
		
		public static int GetOperandIdxByExprIdx(string expr, int ExprIdx)
		{
			int opCou = -1;
			for (int i = 0; i < expr.Length; i++)
			{
				int index = Array.IndexOf(variousOp, expr[i]);
				if (index != -1) {opCou++;}
				if (i == ExprIdx) {return opCou;}
			}
			return -1;
		}
		
		public static string GetConditionStruct(string expr, string[] OpArr, out string[] EqOpArr, out string ConditionStr)
		{
			EqOpArr = new string[OpArr.Length + 2];
			segmCoord[] opSegmA = new segmCoord[EqOpArr.Length];
			bool openBr = false;
			bool prevSpec = false;
			int opCou = -1;
			int specOpCou = -1;
			int bracketCou = 0;
			string specExprMap = "";
			ConditionStr = "";
			
			for(int i=0; i < expr.Length; i++)
			{
				int index = Array.IndexOf(variousOp, expr[i]);
				if (index != -1) {opCou++;}
				
				#region brackets
				if(expr[i] == '(')
				{
					bracketCou++;
					openBr = true;
				}
				if(expr[i] == ')')
				{
					bracketCou--;
					openBr = (bracketCou > 0);
				}
				#endregion
				
				if (  ((Array.IndexOf(equalityOperators, expr[i]) == -1) && (expr[i] != 'R')) || (openBr)  )
				{
					#region add new operand if needed
					if((prevSpec) || (specExprMap == ""))
					{
						specExprMap += "O";
						specOpCou++;
						opSegmA[specOpCou].start = (index != -1) ? opCou : -1; // case of ' start begin from previous operand, not correct
						prevSpec = false;
					}
					#endregion
					if((opSegmA[specOpCou].start == -1) &&(index != -1))
					{
						opSegmA[specOpCou].start = opCou;
					}
					opSegmA[specOpCou].fin = opCou;
					EqOpArr[specOpCou] += expr[i];
				}
				
				if(!openBr)
				{
					#region check for Condition operands
					
					if ( (expr[i] == 'R') || (Array.IndexOf(equalityOperators, expr[i]) >= 0) )
					{
						if (! prevSpec)
						{
							specExprMap += "E";
						}
						prevSpec = true;
						#region assemble Condition substrings
						if (expr[i] == 'R')
						{
							ConditionStr += " " + OpArr[opCou]+" ";
						} else
						{
							ConditionStr += "" + expr[i].ToString();
						}
						#endregion
					}
					#endregion
				}
			}
			
			string tmp = ConditionStr +" \n"; // for debug only
			#region  Merge operand strings
			string[] mergeOp = new string[OpArr.Length + 2];
			for(int i = 0; i<=specOpCou; i++)
			{
				int len = (opSegmA[i].fin - opSegmA[i].start) + 1;
				Array.Copy(OpArr, opSegmA[i].start, mergeOp, 0, len);
				EqOpArr[i] = Merge(EqOpArr[i], mergeOp);
				tmp += EqOpArr[i]+" \n";
			}
			#endregion
			
			Array.Resize(ref EqOpArr, specOpCou+1);
			return specExprMap;
//			return tmp;
		}
		
		public static string LeftToRightPath(string TreeFullPath)
		{
			string rez = "";
			string[] oparr;
			string expr = QueryParser.GetExpresionStruct(TreeFullPath.Trim(), out oparr);
			int opCou = -1;
			int opCouAdded = opCou;
			bool prevQuery = false;
			string separator = "";
			int dropOpr = -1;
			
			oparr[0] = "Query constructor";
			
			for(int i=0; i < expr.Length; i++)
			{
				int index = Array.IndexOf(variousOp, expr[i]);
				if (index != -1) {opCou++;}
				
				if(opCou == dropOpr)
				{
					prevQuery = false;
				}
				
				if((opCouAdded != opCou) && (!prevQuery))
				{
					string queryNumber = "";
					
					if((oparr[opCou] == "query") && (IsNumber(oparr[opCou+1])) )
					{
						dropOpr = opCou+2;
						prevQuery = true;
						queryNumber = " "+oparr[opCou+1];
					}
					rez = ( !prevQuery ? (opCou ==0 ? oparr[opCou] : oparr[opCou].ToUpper()) : "Query") +queryNumber +separator+ rez;
					opCouAdded = opCou;
					
					separator = prevQuery  ? " /" : " |";
				}
				
				
			}
			
			return rez;
		}
		
		#endregion parse utils
		
		static FieldModifiers GetFuncMdfByText(string s)
		{
//			count sum avg max min
			FieldModifiers rez = FieldModifiers.undef;
			if (s == "count")
			{
				rez = FieldModifiers.agrF;
			}
			if (s == "sum")
			{
				rez = FieldModifiers.agrF;
			}
			if (s == "avg")
			{
				rez = FieldModifiers.agrF;
			}
			if (s == "max")
			{
				rez = FieldModifiers.agrF;
			}
			if (s == "min")
			{
				rez = FieldModifiers.agrF;
			}
			if ((s == "stddev")||(s == "variance"))
			{
				rez = FieldModifiers.agrF;
			}
			
			return rez;
		}
		
		string AddFromTableParseInlineView(string s, int sqlNum)
		{
			string[] nameAlias;
			string name = "";
			string alias = "";
			string expr = GetExpresionStruct(s.Trim(), out nameAlias);
			bool isVirtAdded = false;
			
			if ((expr == "OO") || (expr == "ORO") || (expr == "O"))
			{
				name = nameAlias[0];
				alias = nameAlias[nameAlias.Length -1];
			}
			
			if (expr == "O.O") // databasename.table (mysql)
			{
				name = nameAlias[nameAlias.Length -1];
				alias = name;
			}
			
			if (expr.StartsWith("(B") && expr.EndsWith(")O") && (nameAlias[0] == "select"))
			{
				alias = nameAlias[nameAlias.Length -1];
				expr = expr.Substring(1, expr.Length -3);
				Q++;
				inlineTextArr[Q] = Merge(expr, nameAlias);
				isVirtAdded = true;
			}
			
			table ft = new table("", "ErrorParsingThisTable", -1);
			
			if ((name.Trim() == "") && (string.IsNullOrEmpty(alias)))
			{
				if(Q > 0) {Q--;}
				errMessages+= s+" : can not parse such table construction  \n";
			} else
			{
				ft = new table(name, alias, (isVirtAdded)? AddQuery(Q) : -1);
				Query.QueryObjects[QueryIdx[sqlNum]].AddTable(ft);	}
			
			return ft.alias;
		}
		
		public static void StructureCondition(ref condition c)
		{
			string[] opr; // operands
			string workText = c.text.Trim();
			if (workText.StartsWith("not "))
			{
				c.negation = true;
				workText = workText.Substring(3);
			}
			string exprMap = GetExpresionStruct(workText , out opr);
			
			#region consistency check
			
			if(EnumCharOccurrence(exprMap, 'O') == 0)
			{
				c.type = ConditionType.err;
				c.operand1 = "No [dynamic] operands found";
				return;
			}
			
			if (!CheckBracketsClosed(exprMap))
			{
				c.type = ConditionType.err;
				c.operand1 = "Not all brackets closed";
				return;
			}
			#endregion
			
			
			string[] specifiedOpr;
			string specExprMap = "";
			string ConditionStr = "";
			
			if (EnumCharOccurrence(exprMap, 'A') > 0){c.agregate = true;}
			
			specExprMap = GetConditionStruct(exprMap, opr, out specifiedOpr, out ConditionStr);
			
			if (!c.negation){
				c.negation = ConditionStr.Contains(" not ");}
			if (c.negation){
				ConditionStr = ConditionStr.Replace(" not ", " ");}
			c.cOperator = ConditionStr.Trim();
			
			#region check equality operators available
			if (EnumCharOccurrence(specExprMap, 'E') == 0)
			{
//				string[] splOR = SplitBy(c.text, " or ");
				string[] splOR = new string[1];
				if (splOR.Length >= 2)
				{
					c.type = ConditionType.urest;
					c.operand1 = "'OR' condition not supported, so whole 'OR' expr go as one unrestricted condition";
				} else
				{
					c.type = ConditionType.err;
					c.operand1 = "No equality operators found";
				}
			}
			#endregion
			
		unary:
			#region unary  OE , EO
			if (specExprMap == "OE")
			{
				if ((ConditionStr.Contains("is") && ConditionStr.Contains("null") ) || (ConditionStr.Trim() == "not"))
				{
					c.type = ConditionType.unary;
					c.operand1 = specifiedOpr[0];
					
				} else
				{
					c.type = ConditionType.err;
					c.operand1 = "regexp like this can be only unary function (is [not] null)";
				}
			}
			
			if (specExprMap == "EO")
			{
				if (c.negation)
				{
					c.type = ConditionType.unary;
					c.operand1 = specifiedOpr[0];
					
				} else
				{
					c.type = ConditionType.err;
					c.operand1 = "regexp like this can be only unary function NOT";
				}
			}
			#endregion
			
			#region OEO - standart (operand equation operand)
			if (specExprMap == "OEO")
			{
				bool standart = true;
				if(ConditionStr.Contains("is"))
				{
					standart = false;
					specExprMap = "OE";
					goto unary;
				}
				
				if(ConditionStr.Contains("between"))
				{
					standart = false;
					c.type = ConditionType.err;
					c.operand1 = "BETWEEN statement is not complete";
				}
				
				if(standart)
				{
					c.type = ConditionType.equality;
					c.operand1 = specifiedOpr[0];
					c.operand2 = specifiedOpr[1];
				}
			}
			#endregion
			
			#region OEOEO
			if ((specExprMap == "OEOEO") )
			{
				if ( (ConditionStr.Contains("between") && ConditionStr.Contains("and")) || (ConditionStr.Contains("like") && ConditionStr.Contains("escape")) )
				{
					c.type = ConditionType.equality;
					c.operand1 = specifiedOpr[0];
					c.operand2 = specifiedOpr[1];
					c.operand3 = specifiedOpr[2];
				} else
				{
					c.type = ConditionType.urest;
					c.operand1 = "such regexp structure matches only to [val BETWEEN a AND b] or [val LIKE a ESCAPE b], \n but its not any of them, please, check this out";
				}
			}
			#endregion
			
			if(EnumCharOccurrence(specExprMap, 'E') > 2)
			{
				c.type = ConditionType.urest;
				c.operand1 = "You alone are responsible for this :)";
			}
			
			
		}
		
		public static void StructureField(ref field f, string defaultAlias)
		{
			string[] opr;
			string exprMap = GetExpresionStruct(f.text, out opr);
			f.fType = FieldModifiers.undef;
			
			int cOc = EnumCharOccurrence(exprMap, 'C');
			
			switch (exprMap)
			{
					#region	simple fields
					
				case "O":
					#region aliasing
					if (string.IsNullOrEmpty(f.alias))
					{
						f.alias = opr[opr.Length -1];
						if (exprMap == "O")
						{
							f.fType = FieldModifiers.simpleF;
						}
						if (IsNumber(f.alias))
						{
							f.fType = FieldModifiers.constF;
							f.tableField = f.alias;
						}
					} else
					{
						// err handle
					}
					#endregion
					break;
				case "O.O":
					f.fType = FieldModifiers.simpleF;
					f.tableAlias = opr[0];
					f.tableField = opr[1];
					goto case "O";
					
				case "O.ORO":
				case "O.OO":
					f.tableField = opr[1];
					goto case "O.O";
					
				case "ORO":
				case "OO":
					if (IsNumber(opr[0]))
					{
						f.fType = FieldModifiers.constF;
					} else
					{
						f.fType = FieldModifiers.simpleF;
					}
					f.tableField = opr[0];
					goto case "O";
					
					#endregion
					
					#region string constants
				case "'S'":
					f.fType = FieldModifiers.constF;
					f.tableField = opr[0];
					break;
					
				case "'S'RO":
				case "'S'O":
					#region aliasing
					if (string.IsNullOrEmpty(f.alias))
					{
						f.alias = opr[opr.Length -1];
					} else
					{
						// err handle
					}
					#endregion
					goto case "'S'";
					#endregion
					
					#region aggregate functions
					
				case "A(O)":
					f.fType = GetFuncMdfByText(opr[0]);
					if (string.IsNullOrEmpty(f.alias))
					{
						f.alias = opr[opr.Length -1];
					}
					break;
					
				case "A(O.O)":
					f.tableField = opr[2];
					f.tableAlias = opr[1];
					goto case  "A(O)";
					
				case "A(O.O)RO":
				case "A(O.O)O":
					goto case "A(O.O)";
					
				case "A(O)RO":
				case "A(O)O":
					f.tableField = opr[1];
					goto case "A(O)";
					
					#endregion
					
					#region undefined functions
					
				case "O(O)":
					f.fType = FieldModifiers.func;
					if (string.IsNullOrEmpty(f.alias))
					{
						f.alias = opr[opr.Length -1];
					}
					break;
					
				case "O(O.O)":
					f.tableField = opr[2];
					f.tableAlias = opr[1];
					goto case  "O(O)";
					
				case "O(O.O)RO":
				case "O(O.O)O":
					goto case "O(O.O)";
					
				case "O(O)RO":
				case "O(O)O":
					f.tableField = opr[1];
					goto case "O(O)";
					
					#endregion
					
				default:
					// stub
					break;
			}
			
			#region case field
			if (cOc > 0)
			{
				f.fType = FieldModifiers.caseM;
				if (exprMap.EndsWith("CO"))
				{
					if (f.alias == "")
					{
						f.alias = opr[opr.Length -1];
					}
				} else
				{
					// err handling
				}
			}
			#endregion
			
			#region  not,  null
			if (exprMap == "RO") // TODO: handle "RO.0" and "RO.00" not meaning
			{
				if (string.IsNullOrEmpty(f.alias))     // null pid
				{
					if (opr[0] == "null")
					{
						f.alias = opr[1];
						f.fType = FieldModifiers.constF;
					} else
					{
						// err handle
					}
				} else                         // not table.isred
				{
					if (opr[0] == "not")
					{
						f.tableField = opr[1];
						f.fType = FieldModifiers.urest;
					} else
					{
						// err handle
					}
				}
				
			}
			
			if (exprMap == "R")
			{
				if (opr[0] == "null")
				{
					f.fType = FieldModifiers.constF;
					f.tableField = "null";
					f.alias = "empty_field";
				}else
				{
					// err handle
				}
				
			}
			
			#endregion
			
			if (f.fType == FieldModifiers.undef)
			{
				if (string.IsNullOrEmpty(f.alias))
				{
					if (exprMap.EndsWith(")O") || exprMap.EndsWith("'O") || exprMap.EndsWith("OO"))
					{
						f.alias = opr[opr.Length -1];
					} else
					{
						f.alias = defaultAlias;
					}
				}
				
				f.fType = FieldModifiers.urest;
			}
			
			if ((f.fType == FieldModifiers.constF) && (string.IsNullOrEmpty(f.alias)))
			{
				f.alias = "constant_fld";
			}
			
			if (!string.IsNullOrEmpty(f.alias) && !string.IsNullOrEmpty(f.alias.Trim()))
				if ((f.text.EndsWith(" "+ f.alias)) || (f.text.EndsWith(")"+ f.alias))|| (f.text.EndsWith("'"+ f.alias)))
			{
				f.ForOptimizationOnly(f.text.Substring(0, f.text.Length - f.alias.Length));
			}
		}
		
		#region Detailed Section Parse
		
		void ParseSelect(int sqlNum)
		{
			if ((selects[sqlNum] == null) || (selects[sqlNum].Trim() == ""))
			{
				return;
			}
			
			procTextPos = 0;
			syntaxBlocks = syntaxSelect;
			inlineText = "### " + (string) selects[sqlNum].Clone(); // substring ### (and its syntax block) added because there can be no appearance of other meaningful blocks in inlineText; Example: '### books.icbn'  - in that case we put whole string to ### block, so string will be processed anyway
			previousBlock = "";
			byte lastBlock = 0;
			string results = "";
			
			// {"### ", "DISTINCT ", "ALL ", " AS ", ",", " AS ", ","};
			field f = new field();
			
			bool repeat = true;
			while (repeat)
			{
				repeat = false;
				
				#region coma loop
				if (syntaxBlocks[lastBlock] == ",")
				{
					results = GetBlockTextByNumberInline(ref lastBlock);
				}
				#endregion
				
				#region ###
				if (syntaxBlocks[lastBlock] == "### ")
				{
					if (previousBlock != ",")
					{
						results = GetBlockTextByNumberInline(ref lastBlock);
					}
				}
				#endregion
				#region DISTINCT
				if (syntaxBlocks[lastBlock] == "DISTINCT ")
				{
					results = GetBlockTextByNumberInline(ref lastBlock);
					Query.QueryObjects[QueryIdx[sqlNum]].distinct = true;
				}
				#endregion
				#region ALL
				if (syntaxBlocks[lastBlock] == "ALL ")
				{
					results = GetBlockTextByNumberInline(ref lastBlock);
				}
				#endregion
				#region AS
				if (syntaxBlocks[lastBlock] == " AS ")
				{
					f.text = results;
					results = GetBlockTextByNumberInline(ref lastBlock);
				}
				#endregion
				#region  COMA ,
				if (syntaxBlocks[lastBlock] == ",")
				{
					if (previousBlock == " AS ")
					{
						f.alias = results;
					} else
					{
						f.alias = "";
						f.text = results;
					}
					repeat = true;
					lastBlock = 4;
				} else
				{
					if (previousBlock == " AS ")
					{
						f.alias = results;
					} else
					{
						f.alias = "";
						f.text = results;
					}
				}
				#endregion
				
				Query.QueryObjects[QueryIdx[sqlNum]].AddField(f);
			}
			
//			FieldsTypification(sqlNum);
			
		}
		
		void ParseFrom(int sqlNum)
		{
			if ((fromz[sqlNum] == null) || (fromz[sqlNum].Trim() == ""))
			{
				return;
			}
			
			bool repeat = true;
			string  fromText = (string) fromz[sqlNum].Clone();
			
			// есть ли разница при соединении join'ами последовательно table1 join table2 join table3
			//        и параллельно table1 join table2, table2 join table3, table join table3 (и возможно ли второе)
			
			//	syntaxFrom  = {"### ", " AS ", " ON ", " AND ", " AND ", " LEFT ", " RIGHT ", " FULL ", " INNER ", " OUTER ", " JOIN "};
			
			string[] spl;
			spl = SplitBy(fromText, ",");
			foreach ( string comaBlock in spl)
			{
				string[] splJo = SplitBy(comaBlock, " join ");
				if (splJo.Length > 1)
				{
					int tableNum = 0;
					join currJoin = new join(0);
					
					foreach ( string tbl in splJo)
					{
						#region init
						tableNum++;
						byte lastBlock = 0;
						
						initNewParse("### " + tbl+"  ", syntaxFrom);
						
						string tableAlias = "";
						string joinSpec = "";
						repeat = true;
						#endregion
						
						while (repeat)
						{
							repeat = false;
							string block = "";
							// because join regexp is sepatared by " join " lexeme, sequence of blocks extraction is changed
							#region AND loop
							if (syntaxBlocks[lastBlock] == " AND ")
							{
								block = GetBlockTextByNumberInline(ref lastBlock);
//								if (lastBlock == 0)
//								{
//									syntaxBlocks[lastBlock] = "XXX"; // path for situation when 'and' loop (conditions separate by "and") processed all text (procedure returned lastBlock == 0), but (syntaxBlocks[lastBlock == 0] == "### "), here error, so its easiest way to fix it, is to change value of syntaxBlocks[0]
//								}
							}
							#endregion
							
							#region ###
							if (syntaxBlocks[lastBlock] == "### ")
							{
								if (previousBlock != " AND ")
								{
									block = GetBlockTextByNumberInline(ref lastBlock);
								}
							}
							#endregion
							
							#region AliaS
							if ((syntaxBlocks[lastBlock] == " AS ") || ((syntaxBlocks[lastBlock] == "### ") && (tableNum == 1)) )
							{
								tableAlias = block;
								block = GetBlockTextByNumberInline(ref lastBlock);
							}
							#endregion
							
							#region ON
							if (syntaxBlocks[lastBlock] == " ON ")
							{
								tableAlias = tableAlias +" "+ block;
								block = GetBlockTextByNumberInline(ref lastBlock);
							}
							#endregion
							
							#region AND, Condition
							if (syntaxBlocks[lastBlock] == " AND ")
							{
								if (previousBlock == " AND ")
								{
									lastBlock--;
								}
								repeat = true;
								currJoin.addCondition(OpenBrackets(block));
								continue;
								
							} else
							{
								if ((previousBlock == " ON ") || (previousBlock == " AND "))
								{
									currJoin.addCondition(OpenBrackets(block));
								}
							}
							
							#endregion
							
							#region join Specifications
							if ( (syntaxBlocks[lastBlock] == " LEFT ") || (syntaxBlocks[lastBlock] == " RIGHT ") || (syntaxBlocks[lastBlock] == " FULL ") )
							{
								if (previousBlock == "### ")
								{
									tableAlias = tableAlias +" "+ block;
								}
								joinSpec = joinSpec +" "+ syntaxBlocks[lastBlock];
								block = GetBlockTextByNumberInline(ref lastBlock);
							}
							
							if ( (syntaxBlocks[lastBlock] ==  " INNER ") || (syntaxBlocks[lastBlock] == " OUTER ") )
							{
								if (previousBlock == "### ")
								{
									tableAlias = tableAlias +" "+ block;
								}
								joinSpec = joinSpec +" "+ syntaxBlocks[lastBlock];
								block = GetBlockTextByNumberInline(ref lastBlock);
							}
							#endregion
							
						}
						
						#region process current join struct
						if (tableNum > 1)
						{
							currJoin.rightTable = AddFromTableParseInlineView(tableAlias, sqlNum);
							Query.QueryObjects[QueryIdx[sqlNum]].AddJoin(currJoin);
							
							
							join temp = new join(0);
							
							temp.type = joinSpec;
							temp.leftTable = currJoin.rightTable;
							
							currJoin = temp; // if it's the last substring, new initialized currJoin doesnt added to QueryObj
							// else at the next parse we have left init join to add parsed table to the right
						} else
						{
							currJoin.leftTable = AddFromTableParseInlineView(tableAlias, sqlNum);
							currJoin.type = joinSpec;
						}
						#endregion
						
					}
					
				} else  // its just a table (or Inline view), not a join
				{
					AddFromTableParseInlineView(splJo[0], sqlNum);
				}
				
			} // foreach  comaBlock loop end
			
			
		}
		
		void ParseWhere(int sqlNum)
		{
			if ((wherez[sqlNum] == null) || (wherez[sqlNum].Trim() == ""))
			{
				return;
			}
			
			string whereText = (string) wherez[sqlNum].Clone();
			
			string[] spl;
			spl = SplitBy(whereText, " and ");
			foreach ( string s in spl)
			{
				condition c = new condition();
				c.text = OpenBrackets(s);
				Query.QueryObjects[QueryIdx[sqlNum]].AddWhere(c);
			}
			
		}
		
		void ParseGroupBy(int sqlNum)
		{
			if ((groupingz[sqlNum] == null) || (groupingz[sqlNum].Trim() == ""))
			{
				return;
			}
			
			string GroupText = (string) groupingz[sqlNum].Clone();
			
			string[] spl;
			spl = SplitBy(GroupText, ",");
			int i = 0;
			foreach ( string s in spl)
			{
				spl[i] = OpenBrackets(s);
				i++;
			}
			
			Query.QueryObjects[QueryIdx[sqlNum]].groups = spl;
		}
		
		void ParseHaving(int sqlNum)
		{
			if ((havingz[sqlNum] == null) || (havingz[sqlNum].Trim() == ""))
			{
				return;
			}
			
			string haweText = (string) havingz[sqlNum].Clone();
			string[] spl;
			spl = SplitBy(haweText, " and ");
			
			foreach ( string s in spl)
			{
				condition c = new condition();
				c.text = OpenBrackets(s);
				Query.QueryObjects[QueryIdx[sqlNum]].AddHaving(c);
			}
			
		}
		
		void ParseOrderBy(int sqlNum)
		{
			
//			ConditionsTypification(sqlNum);  // all blocks witH conditions parsed, typifiction cAn be stArted
			
			if ((orderz[sqlNum] == null) || (orderz[sqlNum].Trim() == ""))
			{
				return;
			}
			
			string OrderText = (string) orderz[sqlNum].Clone();
			
			string[] spl;
			spl = SplitBy(OrderText, ",");
			int i = 0;
			foreach ( string s in spl)
			{
				spl[i] = OpenBrackets(s);
				spl[i] += IsSortExplicit(spl[i]) ? "" :" asc";
//				if (!( (spl[i].TrimEnd().EndsWith(" asc")) || (spl[i].TrimEnd().EndsWith(" desc"))))
//				{
//					spl[i] += " asc";
//				}
				i++;
			}
			
			Query.QueryObjects[QueryIdx[sqlNum]].orders = spl;
		}
		
		#endregion Detailed Section Parse
		
		int AddQuery(int sqlNum)
		{
			QueryIdx[sqlNum] = Query.AddListObject();
			return QueryIdx[sqlNum];
		}
		
		public void ParseRootSql(int sqlNum)
		{
			#region init
			byte lastBlock = 0;
			procTextPos = 0;
			inlineText = "### "+ (string)inlineTextArr[sqlNum].Clone();
			syntaxBlocks = syntaxBlocksGlobal;

			#endregion
			
			#region Normalizez
			if (sqlNum == 0)
			{
				AddQuery(sqlNum);
				NormalizeLexemSpacing();
				NormalizeSpaces();
			}
			#endregion
			
			bool repeat = true;
			while (repeat)
			{
				repeat = false;
				#region UNION separating
				if (syntaxBlocks[lastBlock] == " UNION ")
				{
					byte uniBlock = 1;
					syntaxBlocks = syntaxUNION;
					string ParallelQuery = "";
					Query.QueryObjects[QueryIdx[sqlNum]].AddUnionedQuery(QueryIdx[sqlNum]);
					
					while (uniBlock != 0)
					{
						uniBlock = 0;
						ParallelQuery = GetBlockTextByNumberInline(ref uniBlock);
						if (ParallelQuery.Trim() != "")
						{
							Q++;
							inlineTextArr[Q] = OpenUnionParamBrackets(ParallelQuery);
							Query.QueryObjects[QueryIdx[sqlNum]].AddUnionedQuery(AddQuery(Q));
						}
					}
					continue;
				}
				#endregion
				
				#region Union Param
				string unionParamVal = GetBlockTextByNumberInline(ref lastBlock);
				#endregion
				
				#region SELECT
				if (syntaxBlocks[lastBlock] == "SELECT ") // if select block omitted, further parsing will be cancelled, due to returned lastBlock = 0,  => syntaxBlocks[0] == "SELECT "
				{
					Query.QueryObjects[QueryIdx[sqlNum]].UnionParam = unionParamVal;
					selects[sqlNum]  = GetBlockTextByNumberInline(ref lastBlock);
				}
				#endregion

				#region FROM
				if (syntaxBlocks[lastBlock] == " FROM ")  // читали блок select и он закончился лексемой from , значит читаем блок from (номер последней лексемы возвращается в lastBlock, если блок дочитали до конца текста lastBlock = 0). соответственно любой блок может быть пропущен, в установленном синтаксисом порядке читаться будут только присутствующие в обрабатываемом тексте блоки
				{
					fromz[sqlNum]    = GetBlockTextByNumberInline(ref lastBlock);
				}
				#endregion

				#region WHERE
				if (syntaxBlocks[lastBlock] == " WHERE ")
				{
					wherez[sqlNum]   = GetBlockTextByNumberInline(ref lastBlock);
				}
				#endregion

				#region GROUP BY
				if (syntaxBlocks[lastBlock] == " GROUP BY ")
				{
					groupingz[sqlNum] = GetBlockTextByNumberInline(ref lastBlock);
				}
				#endregion
				
				#region HAVING
				if (syntaxBlocks[lastBlock] == " HAVING ")
				{
					havingz[sqlNum]   = GetBlockTextByNumberInline(ref lastBlock);
				}
				#endregion
				
				#region ORDER BY
				if (syntaxBlocks[lastBlock] == " ORDER BY ")
				{
					orderz[sqlNum]    = GetBlockTextByNumberInline(ref lastBlock);
				}
				#endregion
				
				#region UNION
				if (syntaxBlocks[lastBlock] == " UNION ") // unionz
				{
					repeat = true;
					continue;
				}
				#endregion
				
			} // parse header query and move to queue all related unions
			
			ParseSelect(sqlNum);
			ParseFrom(sqlNum);
			ParseWhere(sqlNum);
			ParseGroupBy(sqlNum);
			ParseHaving(sqlNum);
			ParseOrderBy(sqlNum);
			
			if (Q > sqlNum)  // queue processing
			{
				ParseRootSql((sqlNum +1));
			}			
			
		}
		
	}
	
	public class QueryTemplatesManager
	{
		int templCou;
		string[][] TemplatesTexts;
		string[] names;
		public static string lastOpenedDB;
		public string[] dbNames;
		public string[] TemplNames
		{
			get
			{
				string[] rez= new string[0];
				if (templCou > 0)
				{
					rez = new string[templCou];
					Array.Copy(names, rez, templCou);
				}
				return rez;
			}
		}
		
		public QueryTemplatesManager()
		{
			templCou = 0;
			TemplatesTexts = new string[300][];
			names = new string[300];
		}
		
		public void  SaveTemplate(string name, string[] text, string description)
		{
			CheckFreeName(Environment.CurrentDirectory + @"\Templates\"+ name+ ".txt" , true);
			using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + @"\Templates\"+ name+ ".txt"))
			{
				if(description.Length > 0)
				{
					sw.WriteLine("-- Description: " + description);
				}
				foreach(string s in text)
				{
					sw.WriteLine(s);
				}
			}
		}
		
		public void  SaveTemplate(string name, string text, string description)
		{
			CheckFreeName(Environment.CurrentDirectory + @"\Templates\"+ name+ ".txt" , true);
			using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + @"\Templates\"+ name+ ".txt"))
			{
				if(description.Length > 0)
				{
					sw.WriteLine("-- Description: " + description);
				}
				sw.WriteLine(" " + text);
			}
		}
		
		public void DeleteTemplate(int templIdx)
		{
			string fileName = Environment.CurrentDirectory + @"\Templates\"+ TemplNames[templIdx]+ ".txt";
			if (!CheckFreeName(fileName, true))
			{
				FileInfo info = new FileInfo(fileName);
				info.Delete();
			}
			ScanAndLoadTemplates();
		}
		
		/// <param name="fileName">file name to chech for existence</param>
		/// <param name="doSmth">if true - make .bak file of checked file with timestamp in name; else just return result of verification</param>
		/// <returns>true - there no such file; false - this file already exist </returns>
		public bool CheckFreeName(string fileName, bool doSmth)
		{
			bool rez = true;
			if(string.IsNullOrEmpty(fileName)) {return false;}
			
			FileInfo info = new FileInfo(fileName);
			
			if(info.Exists)
			{
				rez = false;
				if (doSmth)
				{
					info.CopyTo(info.Directory.FullName + @"\" + TruncFileNameExt(info) +" "+ DateTime.Now.ToString().Replace(':', '_') + ".bac" );
				}
			}
			
			return rez;
		}
		
		public void ClearInfo(int templIdx)
		{
			int i = 0;
			while(TemplatesTexts[templIdx][i].TrimStart().StartsWith("--"))
			{
				TemplatesTexts[templIdx][i] = "";
				i++;
			}
		}
		
		public string GetInfo(int templIdx)
		{
			string rez = "";
			if(TemplatesTexts[templIdx][0].StartsWith("-- Description:"))
			{
				rez = TemplatesTexts[templIdx][0].Substring(15);
				int i = 1;
				while(TemplatesTexts[templIdx][i].TrimStart().StartsWith("--"))
				{
					rez += "\n " + TemplatesTexts[templIdx][i].TrimStart().Substring(2);
					i++;
				}
			}
			return rez;
		}
		
		public int LoadTemplate(int templIdx)
		{
			int rez = -1;
			QueryParser qp = new QueryParser();
			qp.Text = TemplatesTexts[templIdx];
			qp.ParseRootSql(0);
			if (qp.errMessages != "")
			{
				MessageBox.Show("Template [" + names[templIdx] + "] contain some err. \n Operation cancelled");
			}else
			{
				rez = qp.root;
			}
			
			return rez;
		}
		
		public static string TruncFileNameExt(FileInfo file)
		{
			string fileName = file.Name;
			string rez = fileName.Remove(fileName.Length - file.Extension.Length, file.Extension.Length);
			return rez;
		}
		
		public void ScanAndLoadTemplates()
		{
			templCou = 0;
//			Environment.CurrentDirectory
			DirectoryInfo dir = new DirectoryInfo( Environment.CurrentDirectory + @"\Templates\");
			if (!dir.Exists)
			{
				Directory.CreateDirectory(Environment.CurrentDirectory + @"\Templates\");
				return;
			}
			FileInfo[] files = dir.GetFiles("*.txt");
			
			
			foreach (FileInfo file in files)
			{
				string fullname = file.FullName;
				string[] lines = new string[1000];
				int linescou = 0;
				try
				{
					using (StreamReader sr = new StreamReader(fullname))
					{
						string line;
						while ((line = sr.ReadLine()) != null)
						{
							if(linescou >= lines.Length)
							{
								Array.Resize(ref lines, linescou + 1000);
							}
							lines[linescou] = line;
							linescou++;
							
						} // read line loop
					}
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message);
				}
				
				Array.Resize(ref lines, linescou);
				string fileName = file.Name;
				names[templCou] = TruncFileNameExt(file); //fileName.Remove(fileName.Length - file.Extension.Length, file.Extension.Length);
				TemplatesTexts[templCou] = lines;
				templCou++;
			}
			
		}
		
		public void ScanAvailableDBStruct()
		{
			DirectoryInfo dir = new DirectoryInfo( Environment.CurrentDirectory + @"\DB Structure\");
			if (!dir.Exists)
			{
				Directory.CreateDirectory(Environment.CurrentDirectory + @"\\DB Structure");
				dbNames = new string[0];
				return;
			}
			FileInfo[] files = dir.GetFiles("*.txt");
			dbNames = new string[files.Length];
			int i  = 0;
			foreach (FileInfo file in files)
			{
				dbNames[i] = file.Name;
				i++;
			}
		}
		
		public static string Extractfilename(string fileName)
		{
			string rez = "";
			FileInfo info = new FileInfo(fileName);
			rez = TruncFileNameExt(info);
			
			return rez;
		}
		
		#region registry settings
		
		public static object LoadAppSettings(string name)
		{
//			HKEY_LOCAL_MACHINE\SOFTWARE             "SOFTWARE\kibitz\setting", true
			
			object rez = null;
			RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Kibitz\SQL");
			if (rk==null)
			{
				Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Kibitz\SQL");
			} else
			{
				rez = rk.GetValue(name);
			}
			return rez;
		}
		
		public static void SaveAppSettings(string name, object v)
		{
			if(v == null) {return;}
			RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Kibitz\SQL", true);
			rk.SetValue(name, v);
			rk.Close();
		}
		
		#endregion
		
		#region DB structure IO
		
		public static void saveDBStruct(string FileName)
		{
			using (StreamWriter sw = new StreamWriter(FileName))
			{
				foreach (table t in Query.dbTables)
				{
					sw.WriteLine(" " + t.Name);
					field[] flds = t.Fields;
					
					foreach(field f in flds)
					{
						sw.WriteLine("     " + f.alias +"   "+ f.type);
					}
				}
			}
		}
		
		public static table[] LoadDBStruct(string FileName)
		{
			table[] rez = new table[100];
			field[] flds = new field[100];
			int fcou = 0;
			int tcou = 0;
            string errLine = "";
			
			try
			{
				using (StreamReader sr = new StreamReader(FileName))
				{
					string line;
					string[] t;
					string expr;
					string spacing;
					while ((line = sr.ReadLine()) != null)
					{
                        errLine = line;
                        expr = QueryParser.GetExpresionStruct(line, out t, true);
						
						if (t.Length > 0)
						{
							#region expr.Length checking
							if (expr.Length >= 4)
							{
								spacing = expr.Substring(0, 4);
							} else
							{
								spacing = "";
							}
							#endregion
							
							if (spacing != "    ") // no tabulation, it means a Table info line
							{
								rez[tcou]= new table(t[0], "", 0);
								if (tcou != 0)
								{
									Array.Resize(ref flds, fcou);
									rez[tcou - 1].Fields = flds;
									fcou = 0;
									flds = new field[100];
								}
								tcou++;
								#region rez Array size
								if (tcou > rez.Length)
								{
									Array.Resize(ref rez, rez.Length * 2);
								}
								#endregion
								
								
							} else // field information
							{
								flds[fcou] = new field();
								
								flds[fcou].alias = t[0];
								if (t.Length >= 2)
								{
									flds[fcou].type = t[1];
								}
								
								fcou++;
								#region flds Array size
								if (fcou > flds.Length)
								{
									Array.Resize(ref flds, flds.Length * 2);
								}
								#endregion
							}
						}  // if some words are present
					} // read line loop
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message + " "+ errLine);
				return new table[0];
			}
			lastOpenedDB = FileName;
			
			Array.Resize(ref flds, fcou);
			rez[tcou - 1].Fields = flds;
			Array.Resize(ref rez, tcou);
			return rez;
		}
		
		#endregion
		
	}
}
