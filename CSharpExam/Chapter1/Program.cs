using System;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Chapter1 {
    static class Program {
        private static int ThreadCount { get; set; }
        private static int _counter;

        static void Main(string[] args) {
            
            /*var items = Enumerable.Range(0, 500).ToArray();*/
            
            //Parallel.ForEach(items, item => { WorkOnItem(item); });
            
            //Parallel.For(0, items.Length, item => { WorkOnItem(item); });
            
            /*var result = Parallel.For(0, items.Length, (int i, ParallelLoopState loopState) => {
                if (i == 200) loopState.Break();
                WorkOnItem(items[i]);
            });
            Console.WriteLine("Completed:" + result.IsCompleted);
            Console.WriteLine("Items: " + result.LowestBreakIteration);*/

            var people = new[] {
                new Person {Name = "Josh", City = "London"},
                new Person {Name = "Dave", City = "Blackpool"},
                new Person {Name = "Alex", City = "Blackpool"},
                new Person {Name = "Nick", City = "Blackpool"},
                new Person {Name = "Katerina", City = "Boston"},
                new Person {Name = "Lenka", City = "Moscow"},
                new Person {Name = "Bex", City = "Hanoi"},
                new Person {Name = "Mer", City = "Madrid"},
                new Person {Name = "Mitch", City = "Tokyo"},
                new Person {Name = "Orville", City = "Paris"},
            };

            /*var result = 
                from person in people.AsParallel()
                where person.City == "Blackpool"
                select person;*/

            /*var result =
                from person in people.AsParallel()
                where CheckCity(person.City)
                select person;

            result.ForAll(person => DateStampMessage(person.Name));*/
            
            /*var newTask = new Task(Task1);
            newTask.Start();
            newTask.Wait();*/

            /*var task = Task.Run(CalculateResult);
            Console.WriteLine(task.Result);*/

            /*var tasks = new Task[10];
            for (var i = 0; i < 10; i++) {
                var taskNum = i;
                tasks[i] = Task.Run(() => DoWork(taskNum));
            }

            Task.WaitAll(tasks);
            DateStampMessage("Finished processing all tasks.");*/

            for (Initialise();  Test(); Update()) {
                
            }
        }

        private static void Update() {
            _counter++;
        }

        private static bool Test() {
            return _counter < 5;
        }

        private static void  Initialise() {
            _counter = 0;
        }
        
        

        static void DoWork(int i) {
            DateStampMessage($"Task {i} starting");
            Thread.Sleep(2000);
            DateStampMessage($"Thread {i} finished");
        }

        static bool CheckCity(string cityName) {
            if (cityName == "") throw new ArgumentException(cityName);
            return cityName == "Blackpool";
        }

        public static int CalculateResult() {
            DateStampMessage("Task starting");
            Thread.Sleep(2000);
            DateStampMessage("Task Ending");
            return 99;
        }

        static void Task1() {
            DateStampMessage("Task 1 starting");
            Thread.Sleep(2000);
            DateStampMessage("Task 1 Ending");
        }
        
        static void Task2() {
            DateStampMessage("Task 2 starting");
            Thread.Sleep(1000);
            DateStampMessage("Task 2 Ending");
        }

        static void WorkOnItem(object item) {
            ThreadCount++;
            DateStampMessage("Started working on " + item + "(" + ThreadCount + ")");
            Thread.Sleep(100);
            ThreadCount--;
            DateStampMessage("Finished working on " + item + "(" + ThreadCount + ")");
        }

        class Person {
            public string Name { get; set; }
            public string City { get; set; }
        }

        static void DateStampMessage(string message) => Console.WriteLine($"{DateTime.Now:hh:mm:ss.fff}: {message}");
    }
}