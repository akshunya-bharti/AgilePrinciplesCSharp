using System;
using System.Diagnostics;

namespace AgilePrinciplesCSharp.Chapter12
{
    class Figure12_1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Provide Timeout for how long can the door be kept open (in secs)");
            var timeoutInSec = Console.ReadLine();

            var timedDoor = new TimedDoor();
            var timer = new Timer();

            Console.WriteLine("Press O for opening the door and C for closing the door (and any other key to exit the app)");

            // Infinite Loop (For Testing purpose only)
            for (; ; )
            {
                timer.Register(Convert.ToInt32(timeoutInSec), timedDoor);

                var input = Console.ReadLine();

                if (input == "O")
                {
                    if (!timedDoor.DoorOpen)
                    {
                        timedDoor.Unlock();
                        timer.stopwatch.Start();
                        timedDoor.DoorOpen = true;
                    }
                    else
                    {
                        Console.WriteLine("Door Already Open");
                    }
                }
                else if (input == "C")
                {
                    timer.Register(Convert.ToInt32(timeoutInSec), timedDoor);

                    if (timedDoor.DoorOpen)
                    {
                        timedDoor.Lock();
                        timer.stopwatch.Reset();
                        timedDoor.DoorOpen = false;
                    }
                    else
                    {
                        Console.WriteLine("Door Already Closed");
                    }
                }
                else
                {
                    break;
                }
            }
        }

        // Interface that consists the TimeOut method for a door 
        // which needs to sound an alarm when the door has been left open for too long
        public interface ITimerClient
        {
            void TimeOut();
        }

        // IDoor Interface which needs to implement ITimerClient interface
        // to ensure that the TimeOut method is implemented by every door
        public interface IDoor : ITimerClient
        {
            void Lock();
            void Unlock();
            bool IsDoorOpen();
        }

        public class TimedDoor : IDoor
        {
            public bool DoorOpen { get; set; } = false;

            public bool IsDoorOpen()
            {
                if (DoorOpen)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void Lock()
            {
                Console.WriteLine("Door Locked");
                DoorOpen = false;
            }

            public void Unlock()
            {
                Console.WriteLine("Door Unlocked");
                DoorOpen = true;
            }

            public void TimeOut()
            {
                Console.WriteLine("WARNING: Door opened for too long! (Timeout Expired)!");
            }
        }

        public class Timer
        {
            public Stopwatch stopwatch = new Stopwatch();

            public void Register(int timeout, ITimerClient client)
            {
                if (stopwatch.Elapsed > TimeSpan.FromSeconds(timeout))
                {
                    client.TimeOut();
                }
            }
        }
    }
}
