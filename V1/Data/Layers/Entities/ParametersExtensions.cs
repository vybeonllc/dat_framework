using System;
using System.Collections.Generic;
using System.Linq;

namespace Dat.V1.Data.Layers.Entities {

  public static class ParametersExtensions {

    #region >>-- STATIC METHODS                                               -->>--

      public static Dictionary<string, object> ToStringIndexedDictionary(this object[] array) {
        int ParameterIndex = 0;
        return array
            .Select(Func => new { Index = ParameterIndex++, Value = Func })
            .Where(Func => Func.Index % 2 == 0)
            .ToDictionary(Func => Func.Value as string, Func => array[Func.Index + 1]);
      }

      public static object[] MergeToParametersArray(this Dictionary<string, object> dictionary, object[] parameters) {
        
        if (parameters != null && parameters.Length % 2 != 0)
          throw new ArgumentException("Not even parameters count");

        Dictionary<string, object> Parameters = parameters != null ? parameters.ToStringIndexedDictionary() : null;

        var Result = new List<object>();

        dictionary.Keys.ToList().ForEach(Func => {
          Result.Add("_" + Func);
          Result.Add((parameters != null && Parameters.ContainsKey(Func) ? Parameters : dictionary)[Func]);
        });

        return Result.ToArray();

      }

      public static T? SafeParse<T>(this string value) where T : struct {

        if (String.IsNullOrEmpty(value)) return null;

        T Result = default(T);

        try {
          switch (typeof(T).Name.ToLower()) {
            case "decimal"  : Result = (T)(object)decimal.Parse(value);     break;
            case "int64"    : Result = (T)(object)long.Parse(value);        break;
            case "int32"    : Result = (T)(object)int.Parse(value);         break;
            case "datetime" : Result = (T)(object)DateTime.Parse(value);    break;
            case "guid"     : Result = (T)(object)Guid.Parse(value);        break;
            case "boolean"  : Result = (T)(object)(int.Parse(value) == 1);  break;
          }
        }
        catch { }

        return Result;

      }

    #endregion

  }

}
