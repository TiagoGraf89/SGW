using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGW.DataAccess.Configuration
{
	public static class DatabaseHelper
	{
		public static List<string> GetProcedures(string nameFilter, bool onlyWithOutputParam)
		{
			List<string> result = new List<string>();

			string outparamFilter = "";
			if (onlyWithOutputParam)
				outparamFilter = "AND EXISTS (SELECT 1 FROM SYSCOLUMNS C WHERE C.ID = O.ID AND C.isoutparam = 1)";

			using (SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["DataAccess.Properties.Settings.SGWConnectionString"].ConnectionString))
			{
				sql.Open();
				using (var command = sql.CreateCommand())
				{
					command.CommandText = string.Format("SELECT O.Name FROM SYSOBJECTS O WHERE O.Name LIKE '{0}%' AND XTYPE = 'P' {1}", nameFilter, outparamFilter);
					DataTable tb = new DataTable("Procedures");
					tb.Load(command.ExecuteReader());
					foreach (DataRow row in tb.Rows)
						result.Add(row[0].ToString());
				}
			}
			return result;
		}

		public static List<string> GetColumnValues(string fieldName, string tableName)
		{
			List<string> result = new List<string>();

			using (SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["DataAccess.Properties.Settings.SGWConnectionString"].ConnectionString))
			{
				sql.Open();
				using (var command = sql.CreateCommand())
				{
					command.CommandText = string.Format("SELECT DISTINCT {0} FROM {1}", fieldName, tableName);
					DataTable tb = new DataTable("Values");
					tb.Load(command.ExecuteReader());
					foreach (DataRow row in tb.Rows)
						result.Add(row[0].ToString());
				}
			}
			return result;
		}

		public static List<string> GetTables(string nameFilter)
		{
			List<string> result = new List<string>();

			using (SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["DataAccess.Properties.Settings.SGWConnectionString"].ConnectionString))
			{
				sql.Open();
				using (var command = sql.CreateCommand())
				{
					command.CommandText = string.Format("SELECT '[' + s.name + '].[' + o.name +']' from sys.objects o join sys.schemas s on s.schema_id = o.schema_id WHERE O.Name NOT LIKE '{0}%' AND O.Name NOT LIKE 'SYS%' AND o.type = 'U' order by s.name, o.name ", nameFilter);
					DataTable tb = new DataTable("Tables");
					tb.Load(command.ExecuteReader());
					foreach (DataRow row in tb.Rows)
						result.Add(row[0].ToString());
				}
			}
			return result;
		}

		public static List<KeyValuePair<string, string>> GetTableColumns(string tableName)
		{
			List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

			using (SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["DataAccess.Properties.Settings.SGWConnectionString"].ConnectionString))
			{
				sql.Open();
				using (var command = sql.CreateCommand())
				{
					command.CommandText = string.Format("SELECT c.name, c.system_type_id Xtype from sys.objects o join sys.schemas s on s.schema_id = o.schema_id join sys.columns c on c.object_id = o.object_id where '[' + s.name + '].[' + o.name +']' = '{0}'", tableName); 
					DataTable tb = new DataTable("Columns");
					tb.Load(command.ExecuteReader());
					foreach (DataRow row in tb.Rows)
						result.Add(new KeyValuePair<string,string>(row[0].ToString(), row[1].ToString()));
				}
			}
			return result;
		}

		public static bool CreateTrigger(string tableName, Guid entityId, bool systemDefinedCodeField, string userCodeField, bool systemDefinedStatusField, string statusField, List<string> fields)
		{

			using (SqlConnection sql = new SqlConnection(ConfigurationManager.ConnectionStrings["DataAccess.Properties.Settings.SGWConnectionString"].ConnectionString))
			{
				sql.Open();

				StringBuilder sb = new StringBuilder();
				sb.AppendLine("DECLARE @SQL NVARCHAR(MAX) = '';");
				sb.Append("SELECT @SQL = @SQL + 'DROP TRIGGER ' + S.Name + '.' + O.Name + ';' ");
				sb.Append("FROM SYSOBJECTS O ");
				sb.Append("JOIN sys.tables t ON T.object_id = o.parent_obj ");
				sb.Append("join sys.schemas s on t.schema_id = s.schema_id ");
				sb.Append("WHERE O.NAME LIKE 'SGW_%' AND O.XTYPE = 'TR' AND t.object_id = object_id('");
				sb.Append(tableName);
				sb.AppendLine("');");
				sb.AppendLine("if (@SQL != '')");
				sb.AppendLine("exec sp_executesql @SQL;");
				
				using (var command = sql.CreateCommand())
				{
					command.CommandText = sb.ToString();
					command.ExecuteNonQuery();
				}
				
				sb.Clear();
				sb.Append("CREATE TRIGGER SGW_");
				sb.Append(statusField);
				sb.Append(" ON ");
				sb.AppendLine(tableName);
				sb.AppendLine("AFTER INSERT");
				sb.AppendLine("AS");
				sb.AppendLine("BEGIN");
				sb.Append("	SELECT NEWID() AS SGW_EntityInstanceId, ");
				if (systemDefinedCodeField)
					sb.Append("	[" + userCodeField + "] AS SGW_UserDefinedCode, ");
				else
					sb.Append(string.Format("	ISNULL((Select COUNT(1) FROM {0}),0) + 1 AS SGW_UserDefinedCode, ", tableName));

				foreach (var item in fields)
					sb.Append(string.Format("[{0}],",item));

				sb.Remove(sb.Length - 1, 1);
				sb.Append(" INTO #TMP FROM inserted;");
				sb.AppendLine("");

				sb.AppendLine("	INSERT INTO SGW_EntityInstance (EntityId, EntityInstanceId, CurrentStatus, UserDefinedCode, CreatedOn, CreatedBy, UpdatedOn, UpdatedBy)");
				sb.Append("SELECT '");
				sb.Append(entityId.ToString());
				sb.Append("', SGW_EntityInstanceId, ");
				if (systemDefinedStatusField)
					sb.Append(string.Format("[{0}]",statusField));
				else
					sb.Append(string.Format("''", statusField));
				sb.Append(", SGW_UserDefinedCode, GETDATE(), NULL, NULL, NULL");
				sb.AppendLine(" FROM #TMP; ");

				sb.AppendLine("DECLARE @SQL NVARCHAR(MAX) = '';");
				sb.AppendLine("SELECT @SQL = @SQL +");
				sb.AppendLine("'");
				sb.AppendLine("INSERT INTO SGW_EntityInstanceValues (ValueId,EntityId,EntityInstanceId,FieldName,DataValue,NumberValue,TextValue,IdValue)");
				sb.Append("SELECT NEWID(), ''");
				sb.Append(entityId.ToString());
				sb.Append("'', SGW_EntityInstanceId,''' + SGW_EntityField.Name +  ''','");
				sb.AppendLine(" + CASE WHEN SGW_EntityField.FieldType = 'D' THEN '[' + SGW_EntityField.Name + ']' ELSE 'NULL' END + ','");
				sb.AppendLine(" + CASE WHEN SGW_EntityField.FieldType = 'N' THEN '[' + SGW_EntityField.Name + ']' ELSE 'NULL' END + ','");
				sb.AppendLine(" + CASE WHEN SGW_EntityField.FieldType = 'T' THEN '[' + SGW_EntityField.Name + ']' ELSE 'NULL' END + ','");
				sb.AppendLine(" + CASE WHEN SGW_EntityField.FieldType = 'C' THEN '[' + SGW_EntityField.Name + ']' ELSE 'NULL' END + '");
				sb.AppendLine("FROM #TMP;");
				sb.AppendLine("'");
				sb.AppendLine(string.Format("FROM SGW_EntityField WHERE SGW_EntityField.UserDefined = 0 AND SGW_EntityField.EntityId = '{0}'", entityId.ToString()));

				sb.AppendLine("if (@SQL != '')");
				sb.AppendLine("exec sp_executesql @SQL;");
	
				sb.AppendLine("SET @SQL = '';");
				sb.Append("SELECT @SQL = @SQL + 'EXEC SGW_StartWorkflows ''' + CAST(SGW_EntityInstanceId AS VARCHAR(36)) + ''',''");
				sb.Append(entityId.ToString());
				sb.Append("'',''' + CAST(SGW_UserDefinedCode AS NVARCHAR(MAX)) + ''';'");
				sb.Append(" FROM #TMP;");

				sb.AppendLine("if (@SQL != '')");
				sb.AppendLine("exec sp_executesql @SQL;");
				
				sb.AppendLine("END;");

				using (var command = sql.CreateCommand())
				{
					command.CommandText = sb.ToString();
					command.ExecuteNonQuery();
				}
			}
			return true;
		}
	}
}
