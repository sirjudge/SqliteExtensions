using System.Data;
using ConnectionFactory;
using Microsoft.Data.Sqlite;

namespace DataComparison;

public class ComparisonData
{
    public IEnumerable<object> ChangedData { get; set; }
    public IEnumerable<object> NewData { get; set; }
    public IEnumerable<object> RemovedData { get; set; }
}

public class DataComparer
{
    public DataComparer(string sqliteFilePath)
    {
        if (!File.Exists(sqliteFilePath))
        {
            var connectionOptions = new ConnectionOptions
            {
                DBConnectionPath = sqliteFilePath,
                SqliteOpenMode = SqliteOpenMode.ReadWriteCreate,
                DataSource = sqliteFilePath
            };
            var connection = ConnectionFactory.ConnectionFactory.CreateConnection(connectionOptions);
            connection.Open();           
        }
        //else do validation
    }

   
    public  ComparisonData CompareTables(DataSource source1, DataSource source2)
    {
        return new ComparisonData();
    }
}