using System.Runtime.InteropServices.JavaScript;
using ConnectionFactory;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Sqlite;

namespace SqliteExtensions.UnitTests;

public class TestObject
{
    public string StringProperty { get; set; }
    public long LongProperty { get; set; }
    public DateTime DateTimeProperty { get; set; }
    public bool BoolProperty { get; set; }
}

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

    [TestMethod]
    public void TestDataTableCreation()
    {
        var factory = new QueryFactory();
        var testObjectList = new List<TestObject>()
        {
            new TestObject()
            {
                BoolProperty = true,
                DateTimeProperty = DateTime.Now,
                LongProperty = 1,
                StringProperty = "test"
            },
            new TestObject()
            {
                BoolProperty = true,
                DateTimeProperty = DateTime.Now,
                LongProperty = 2,
                StringProperty = "test2"
            }
        };

        var tableName = "testObjectTable";
        var testObjectTable = factory.ToDataTable<TestObject>(testObjectList,tableName);
        
        Assert.IsTrue(testObjectTable.Rows.Count == 2, $"Expected two rows but found {testObjectTable.Rows.Count} instead");
        Assert.IsTrue(testObjectTable.TableName == tableName, $"Expected table name of {tableName} but found {testObjectTable.TableName} instead");
    }
}