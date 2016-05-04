using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace taskify
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<User> {
                    new User { Id=1, Name="michael"},
                    new User { Id=2, Name="edi"},
                    new User { Id=3, Name="alex"},
                    new User { Id=4, Name="avi"},
                    new User { Id=5, Name="yoni"}
                };
            Task.Run(async () =>
            {
                Stopwatch start = new Stopwatch();
                start.Start();

                List<Task<int>> taskRegular = list.Select(u =>
                Task.Run(() =>
                {
                    Thread.Sleep(3000);
                    return u.Id - 5;
                })).ToList();

                await Task.WhenAll(taskRegular);
                var firstRegular = taskRegular[0].Result;

                start.Stop();

                Console.WriteLine("regular time: " + start.Elapsed.TotalSeconds + " answer " + firstRegular);

                start.Reset();
                start.Start();
                var taskExt = await list.ToTask(u =>
                {
                    Thread.Sleep(3000);
                    return u.Id - 5;
                }).WhenAllAsync();

                var firstExt = taskExt[0];
                start.Stop();
                Console.WriteLine("regular time: " + start.Elapsed.TotalSeconds + " answer " + firstExt);
                Console.Read();
            }).Wait();
        }

        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
