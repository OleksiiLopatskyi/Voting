using Microsoft.AspNetCore.Http;

namespace Voting.BAL.Extensions
{
    public static class FormExtensions
    {
        public static IDictionary<string, object> FormToDictionary(this IFormCollection source)
        {
            var dict = new Dictionary<string, object>();

            foreach (var key in source.Keys)
            {
                var value = source[key].ToString();
                dict.Add(key, value);
            }
            return dict;
        }
    }
}
