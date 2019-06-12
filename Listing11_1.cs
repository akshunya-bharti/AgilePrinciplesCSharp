using System;

namespace AgilePrinciplesCSharp
{
    class Listing11_1
    {
        static void Main(string[] args)
        {
            var button = new Button(new Lamp());
            button.Poll();
        }
    }

    // Implementation that follows the Dependency inversion principle
    public class Button
    {
        public IButtonServer device;

        public Button(IButtonServer device)
        {
            this.device = device;
        }

        public void Poll()
        {
            if (SomeCondition())
                device.TurnOn();
        }

        private bool SomeCondition()
        {
            return true;
        }
    }

    public interface IButtonServer
    {
        void TurnOff();
        void TurnOn();
    }

    public class Lamp : IButtonServer
    {
        public void TurnOn()
        {
            Console.WriteLine("Lamp Turned On");
        }

        public void TurnOff()
        {
            Console.WriteLine("Lamp Turned Off");
        }
    }

    // Implementation that violates the Dependency inversion principle
    /*
    public class Button
    {
        private Lamp lamp = new Lamp();
        public void Poll()
        {
            if (SomeCondition())
                lamp.TurnOn();
        }

        private bool SomeCondition()
        {
            return true;
        }
    }    public class Lamp
    {
        public void TurnOn()
        {
            Console.WriteLine("Lamp Turned On");
        }

        public void TurnOff()
        {
            Console.WriteLine("Lamp Turned Off");
        }
    }    */
}
