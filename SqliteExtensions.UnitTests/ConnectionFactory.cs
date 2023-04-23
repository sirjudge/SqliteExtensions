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
        
    }
}