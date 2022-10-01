using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConvertListToDataSet
{
    public static class ExtensionMethods
    {
        public static DataSet ToDataSet<T>(this List<T> list)
        {
            var converted = new DataSet();
            converted.Tables.Add(NewTable(list));
            return converted;
        }
        private static DataTable NewTable<T>(List<T> list)
        {
            PropertyInfo[] propInfo = typeof(T).GetProperties();
            DataTable table = Table<T>(list, propInfo);
            IEnumerator<T> enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
                table.Rows.Add(CreateRow<T>(table.NewRow(), enumerator.Current, propInfo));
            return table;
        }
        private static DataRow CreateRow<T>(DataRow row, T listItem, PropertyInfo[] pi)
        {
            foreach (PropertyInfo p in pi)
                row[p.Name.ToString()] = p.GetValue(listItem, null);
            return row;
        }
        private static DataTable Table<T>(List<T> list, PropertyInfo[] pi)
        {
            DataTable table = new DataTable();
            foreach (PropertyInfo p in pi)
                table.Columns.Add(p.Name, p.PropertyType);
            return table;
        }

    }
}
