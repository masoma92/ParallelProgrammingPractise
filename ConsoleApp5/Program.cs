using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Oktato> oktatok = Enumerable.Range(0, 3).Select(x => new Oktato(x)).ToList();

            List<Hallgato> hallgatok = Enumerable.Range(0, 30).Select(x => new Hallgato(x, oktatok)).ToList();

            foreach (var item in oktatok)
            {
                item.hallgatok = hallgatok;
            }

            List<Task> oktatoTasks = new List<Task>();
            List<Task> hallgatoTasks = new List<Task>();

            foreach (Oktato item in oktatok)
                oktatoTasks.Add(new Task(() => item.Feleltet(), TaskCreationOptions.LongRunning));

            foreach (Hallgato item in hallgatok)
                hallgatoTasks.Add(new Task(() => item.HallgatoProcess(), TaskCreationOptions.LongRunning));

            new Task(() =>
            {
                while (true)
                {
                    foreach (var item in hallgatok)
                    {
                        Console.WriteLine(item);
                    }
                    foreach (var item in oktatok)
                    {
                        Console.WriteLine(item);
                    }
                    Thread.Sleep(500);
                    Console.Clear();
                }
            }, TaskCreationOptions.LongRunning).Start();

            oktatoTasks.ForEach(x => x.Start());

            hallgatoTasks.ForEach(x => x.Start());



            Console.ReadLine();
        }
    }
}
