using System;

namespace AgilePrinciplesCSharp.Chapter12.Listing12_2
{
    class Listing12_2
    {
        static void Main(string[] args)
        {
            var door = new TimedDoor();
            var client = new Client(door);
            client.TimeOut();
        }
    }

    public interface ITimerClient
    {
        void TimeOut();
    }

    // We force Door, and therefore (indirectly) TimedDoor, to inherit from TimerClient.
    public interface IDoor : ITimerClient
    {
        void Lock();
        void Unlock();
        bool IsDoorOpen();
    }

    // Implementation of Door Interface with Timer functionality
    public class TimedDoor : IDoor
    {
        public bool IsDoorOpen()
        {
            return true;
        }

        public void Lock()
        {
            Console.WriteLine("Door Locked");
        }

        public void TimeOut()
        {
            var timer = new Timer();
            timer.Register(5, this);
            Console.WriteLine("Timeout! Door left open for so long");
        }

        public void Unlock()
        {
            Console.WriteLine("Door Unlocked");
        }
    }

    // Timer class can use an object of TimerClient
    public class Timer
    {
        public void Register(int timeout, ITimerClient client)
        {
            /* CODE */
        }
    }

    // Client uses Door Interface without depending upon any particular implementation of Door
    public class Client
    {
        IDoor door;

        public Client(IDoor door)
        {
            this.door = door;
        }

        public void TimeOut()
        {
            door.TimeOut();
        }
    }
}
