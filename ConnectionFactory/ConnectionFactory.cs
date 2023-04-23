
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
            Pooling = true,
            DataSource = connectionOptions.DataSource,
            DefaultTimeout = 30,
            // Password = connectionOptions.Password,
            // DateTimeKind = DateTimeKind.Utc,
            // SyncMode = SynchronizationModes.Normal,
            // JournalMode = SQLiteJournalModeEnum.Delete,  //turn this off for increased performanc
            BaseSchemaName = null,
            // Flags = SQLiteConnectionFlags.None,
        };

        return new SqliteConnection(connectionStringBuilder.ConnectionString);
    }

    public static void DeleteSqliteInstance(string filePath)
    {
        if (!File.Exists(filePath)) return;
        File.Delete(filePath);
    }
}