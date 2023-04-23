using ConnectionFactory;

namespace SqliteExtensions.UnitTests;

[TestClass]
public class QueryFactoryTests
{
    [TestInitialize]
    public void SetupTests()
    {
        // Create new testing Instance
        ConnectionFactory.ConnectionFactory.CreateSqliteInstance("sqliteQueryTests.sqlite");
    }

    [TestCleanup]
    public void TearDownTests()
    {
        // delete testing instance
        ConnectionFactory.ConnectionFactory.DeleteSqliteInstance("sqliteQueryTests.sqlite");
    }
    
    [TestMethod]
    public void ConnectToDb()
    {
        var connectionOptions = new ConnectionOptions()
        {
            DataSource = "sqliteQueryTests.sqlite"
        };
    }
}