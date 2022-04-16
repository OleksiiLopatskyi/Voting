using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.BAL.Extensions
{
    public static class DictionaryExtensions
    {
        public static T ToObject<T>(this IDictionary<string, object> source)
        where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                var key = item.Key.ToString();
                var editedKey = Char.IsUpper(key.First()) ? key : key.FirstCharToUpper();
                var propertyType = someObjectType.GetProperty(editedKey).PropertyType;
                object value = null;
                switch (propertyType.Name)
                {
                    case "String":
                        value = item.Value.ToString();
                        break;
                    case "Int32":
                        value = Int32.Parse(item.Value.ToString());
                        break;
                    case "Boolean":
                        value = Boolean.Parse(item.Value.ToString());
                        break;
                    case "Double":
                        value = Double.Parse(item.Value.ToString());
                        break;
                }
                someObjectType
                         .GetProperty(editedKey)
                         .SetValue(someObject, value, null);
            }
            return someObject;
        }
    }
}
