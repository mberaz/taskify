using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taskify
{
    public static class TaskExtentions
    {
        public static List<Task<TO>> ToTask<T,TO> (this IEnumerable<T> source, Func<T, TO> func)
        {
            if(!source.Any())
            {
                throw new InvalidOperationException("Cannot create list of an empty list.");
            }

            return source.Select(item=> Task.Run(() => func(item))).ToList();
        }

        public static TO[] WhenAll<TO>(this IEnumerable<Task<TO>> source)
        {
            return Task.WhenAll(source).Result;
        }

        public async static Task<TO[]> WhenAllAsync<TO>(this IEnumerable<Task<TO>> source)
        {
            return await Task.WhenAll(source);
        }
    }
}
