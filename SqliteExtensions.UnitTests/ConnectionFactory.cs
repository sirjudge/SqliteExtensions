using ConnectionFactory;
using Microsoft.Data.Sqlite;

namespace SqliteExtensions.UnitTests;

[TestClass]
public class ConnectionFactoryTests
{
    [TestMethod,Priority(1)]
    public void CreateSqliteDatabase()
    {
        ConnectionFactory.ConnectionFactory.CreateSqliteInstance("factoryDBCreationAndTearDownTests.sqlite");
        Assert.IsTrue(File.Exists("factoryDBCreationAndTearDownTests.sqlite"));
    }
    
    [TestMethod,Priority(2)]
    public void DeleteSqliteDatabase()
    {
        ConnectionFactory.ConnectionFactory.DeleteSqliteInstance("factoryDBCreationAndTearDownTests.sqlite");
        Assert.IsTrue(!File.Exists("factoryDBCreationAndTearDownTests.sqlite"));
    }
    
    [TestMethod]
    public void CreateDbAndCreateConnection()
    {
        
        ConnectionFactory.ConnectionFactory.CreateSqliteInstance("CreateConnectionTest.sqlite");

        var connectionOptions = new ConnectionOptions()
        {
            DataSource = "CreateConnectionTest.sqlite",
            SqliteOpenMode = SqliteOpenMode.ReadWriteCreate
        };
        var connection = ConnectionFactory.ConnectionFactory.CreateConnection(connectionOptions);
        Assert.IsNotNull(connection);

        try
        {
            connection.Open();
            connection.Close();
        }
        catch (Exception e)
        {
            Assert.Fail($"Could not open database:{e.Message}");
        }
        ConnectionFactory.ConnectionFactory.DeleteSqliteInstance("CreateConnectionTest.sqlite");

    }
}