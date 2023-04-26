using System.ComponentModel;
using System.Data;
using System.Text;

namespace Sqlite;

public class QueryFactory
{
    private DataTable ObjectToDataTable(object obj, string tableName)
    {
        // PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(Employee_Master_Model));
        // DataTable dt = new DataTable();
        // foreach (PropertyDescriptor p in props)
        //     dt.Columns.Add(p.Name, p.PropertyType);

        return new DataTable();
    }
    
    
    public void InsertData(DataTable table)
    {
        //build insert command
        var insertQueryStringBuilder = new StringBuilder();
    }
}