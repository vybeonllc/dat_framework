using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Dat.V1.Utils.Extensions;

namespace Dat.V1.Dto.Bom
{
    [DataContract()] 
    public class Filtering
    {
        [DataMember(Name = "filters")]
        public List<FilteredColumn> Filters { get; set; }

        [DataMember(Name = "total_filters")]
        public int TotalFilters { get; set; }

        public override string ToString()
        {
            Dictionary<FilterTypes, string> types = new Dictionary<FilterTypes, string>()
            {
                { FilterTypes.Equal,"equal"},
                { FilterTypes.NotEqual,"not_equal"},
                { FilterTypes.LessThanOrEqual,"less_than_or_equal"},
                { FilterTypes.GreaterThanOrEqual, "greater_than_or_equal"},
                { FilterTypes.LessThan,"less_than"},
                { FilterTypes.GreaterThan,"greater_than"},
                { FilterTypes.Contains, "contains"},
                { FilterTypes.NotContains,"not_contains"},
                { FilterTypes.StartsWith,"starts_with"}
            };

            int column_count = -1;
            return string.Join(";", Filters
                                    .Select<FilteredColumn, string>(column => ("column_name" + ++column_count + "=" + column.Name.ToLower() + ";"
                                        + string.Join(";", column.Filters
                                                            .Select(filter => types[filter.Type] + column_count + "=" + filter.Value)).Trim(';')).Trim(';'))).Trim(';');
        }
        public static Filtering Parse(string str_filters)
        {
            Filtering filtering = new Filtering()
            {
                Filters = new List<FilteredColumn>()
            };

            if (string.IsNullOrWhiteSpace(str_filters)) return filtering;

            Func<string, FilterTypes> _throw = (filter) => { throw new ArgumentException("Filter " + filter + " is not exists."); };

            Dictionary<string, FilterTypes> types = new Dictionary<string, FilterTypes>()
            {
                { "equal", FilterTypes.Equal},
                { "not_equal", FilterTypes.NotEqual},
                { "less_than_or_equal", FilterTypes.LessThanOrEqual},
                { "greater_than_or_equal", FilterTypes.GreaterThanOrEqual},
                { "less_than", FilterTypes.LessThan},
                { "greater_than", FilterTypes.GreaterThan},
                { "contains", FilterTypes.Contains},
                { "not_contains", FilterTypes.NotContains},
                { "starts_with", FilterTypes.StartsWith}
            };
            filtering.Filters = str_filters
                .Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(w => w.LeftOf("=").EndsWithNumber())
                .GroupBy<string, string>(selector => selector.LeftOf("=").GetNumbers())
                .Select(s =>
                                new FilteredColumn()
                                {
                                    Name = ((s.FirstOrDefault(f => f.StartsWith("column_name"))) ?? "").RightOf("="),
                                    Filters = s.Where(w => !w.StartsWith("column_name"))
                                                .Select<string, Filter>(str =>
                                                    new Filter()
                                                    {
                                                        Type = types.ContainsKey(str.LeftOf("=").RemoveNumbers().ToLower().Trim()) ? types[str.LeftOf("=").RemoveNumbers().ToLower().Trim()] : _throw(str),
                                                        Value = str.RightOf("=").Trim()
                                                    }).ToList()
                                }).ToList();
            filtering.TotalFilters = filtering.Filters.Sum(f => f.Filters == null ? 0 : f.Filters.Count);
            return filtering;

        }
     
    }
}