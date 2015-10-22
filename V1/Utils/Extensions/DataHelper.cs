using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dat.V1.Utils.Extensions;
namespace Dat.V1.Utils.Extensions
{
    public static class DataHelper
    {
        public static IEnumerable<T> Select<T>(this System.Data.DataRowCollection source, params object[] constructor_parameters) where T : new()
        {
            return source.Cast<System.Data.DataRow>().Select<System.Data.DataRow, T>(
                (row, name, defaultvalue) =>
                    row.Table.Columns.Contains(name) ? row[name] : defaultvalue
            , constructor_parameters);
        }
        public static bool TryParse<T>(this System.Data.DataRow source, T target, params object[] constructor_parameters) where T : new()
        {
            return Reflection.MemberInfoClass.TryParse<System.Data.DataRow, T>(source, ref target, (row, name, defaultvalue) =>
                    row.Table.Columns.Contains(name) ? row[name] : defaultvalue, constructor_parameters);
        }
    }
}
