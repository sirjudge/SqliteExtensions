
using System.Data;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;

namespace ConnectionFactory;

public class ConnectionOptions
{
    public string DBConnectionPath { get; set; }
    public SqliteOpenMode SqliteOpenMode { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string DataSource { get; set; }
}

public static class ConnectionFactory
{
    public static void CreateSqliteInstance(string filePath)
    {
        if (File.Exists(filePath)) return;
        SQLiteConnection.CreateFile(filePath);
    }

    public static SqliteConnection CreateConnection(ConnectionOptions connectionOptions)
    {
        var connectionStringBuilder = new SQLiteConnectionStringBuilder
        {
            SyncMode = SynchronizationModes.Normal,
            Pooling = true,
            DataSource = connectionOptions.DataSource,
            DefaultTimeout = 30,
            ReadOnly = false,
            Password = connectionOptions.Password,
            DateTimeFormat = SQLiteDateFormats.ISO8601,
            DateTimeKind = DateTimeKind.Utc,
            BaseSchemaName = null,
            JournalMode = SQLiteJournalModeEnum.Delete,  //turn this off for increased performance
            DefaultIsolationLevel = (IsolationLevel)0,
            DefaultDbType = DbType.AnsiString,
            Flags = SQLiteConnectionFlags.None,
        };

        return new SqliteConnection(connectionStringBuilder.ConnectionString);
    }

    public static void DeleteSqliteInstance(string filePath)
    {
        if (!File.Exists(filePath)) return;
        File.Delete(filePath);
    }
}