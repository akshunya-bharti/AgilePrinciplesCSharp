using System;

namespace AgilePrinciplesCSharp.Chapter12.Listing12_1
{
    public class Listing12_1
    {
        static void Main(string[] args)
        {
            // While creating the client object we pass one particular implementation of Door
            var client = new Client(new Door());
            client.OpenDoor();
        }
    }

    // Door is coded as an interface so that clients can use objects 
    // that conform to the Door interface without having to depend on particular implementations of Door
    public interface IDoor
    {
        void Lock();
        void Unlock();
        bool IsDoorOpen();
    }

    // Client uses Door Interface without depending upon any particular implementation of Door
    public class Client
    {
        IDoor door;

        public Client(IDoor door)
        {
            this.door = door;
        }

        public void OpenDoor()
        {
            door.Unlock();
        }
    }

    // One Such Implementation of Door Interface
    public class Door : IDoor
    {
        public bool IsDoorOpen()
        {
            return true;
        }

        public void Lock()
        {
            Console.WriteLine("Door Locked");
        }

        public void Unlock()
        {
            Console.WriteLine("Door Unlocked");
        }
    }
}
