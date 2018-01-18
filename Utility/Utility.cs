using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class Utility
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> elements, int seed = 0)
        {
            if (seed == 0)
            {
                seed = DateTime.Now.Second * DateTime.Now.Millisecond;
            }
            Random random = new Random(seed);
            List<T> list = new List<T>();
            List<T> elementsList = elements as List<T>;
            for (int i = elements.Count() - 1; i >= 0; i--)
            {
                var ind = random.Next(i);
                list.Add(elementsList[ind]);
                elementsList.RemoveAt(ind);
            }
            return list;
        }

        public static string EncryptPassword(this string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }

        public static int Years(this DateTime date)
        {
            return DateTime.Now.Year - date.Year;
        }

        public static string SelectFileName(this string file)
        {
            return string.Join("", file.Reverse().TakeWhile(x => x != '\\').Reverse());
        }
    }
}
