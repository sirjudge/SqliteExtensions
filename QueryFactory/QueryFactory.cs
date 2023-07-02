using System.Data;
using System.Reflection;
using System.Text;
using Microsoft.Data.Sqlite;


namespace Sqlite;

public class QueryFactory
{
    public DataTable ToDataTable<T>(List<T> items, string tableName)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);

        //Get all the properties
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            //Defining type of data column gives proper data table 
            var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name, type);
        }
        foreach (var item in items)
        {
            var values = new object[Props.Length];
            for (var i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = Props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }

        dataTable.TableName = tableName;
        //put a breakpoint here and check datatable
        return dataTable;
    }


    public string ObjectToCreateTableString<T>(string tableNameOverride = null)
    {
        var tableName = string.IsNullOrEmpty(tableNameOverride) ? typeof(T).Name : tableNameOverride;
        var queryStringBuilder = new StringBuilder($"CREATE TABLE IF NOT EXISTS\"{tableName}\" (");
        
        //TODO: Do we want to exclude certain flags? these bindings only parse public and instance properties
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            //Defining type of data column gives proper data table 
            var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
            //Setting column names as Property names
            // dataTable.Columns.Add(prop.Name, type);
            queryStringBuilder.Append($" {prop.Name} ");
        }
        
        return queryStringBuilder.Append(")").ToString();
    }
    
    
    
    public void InsertData(DataTable table)
    {
        using (var connection = new SqliteConnection(""))
        {
            //build insert command
            using (var transaction = connection.BeginTransaction())
            {
                var command = connection.CreateCommand();
                command.CommandText = @"INSERT INTO data VALUES ($value)";

                var parameter = command.CreateParameter();
                parameter.ParameterName = "$value";
                command.Parameters.Add(parameter);

                // Insert a lot of data
                var random = new Random();
                for (var i = 0; i < 150_000; i++)
                {
                    parameter.Value = random.Next();
                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
        }
        
    }
}