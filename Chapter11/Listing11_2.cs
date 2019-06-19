using System;
using System.Threading;

namespace AgilePrinciplesCSharp.Chapter11
{
    enum TemperatureDirection { Rising, Falling };

    class Listing11_2
    {
        static void Main(string[] args)
        {
            // Set the current temperature
            IoChannelThermometer.CurrentTemperature = 11.5;

            // Initially the Heater is Off
            Regulate.TemperatureDirection = TemperatureDirection.Falling;

            // Creating an instance of Regulate to start the Regulator
            var regulate = new Regulate(new IoChannelThermometer(), new IoChannelHeater(), 10.0, 15.5);
        }
    }

    class Regulate : IRegulate
    {
        public static TemperatureDirection TemperatureDirection;

        public IThermometer Thermometer { get; set; }

        public IHeater Heater { get; set; }

        public double MinTemp { get; set; }

        public double MaxTemp { get; set; }

        public Regulate(IThermometer thermometer, IHeater heater, double minTemp, double maxTemp)
        {
            Thermometer = thermometer;
            Heater = heater;
            MinTemp = minTemp;
            MaxTemp = maxTemp;

            for (; ; )
            {
                while (Thermometer.Read() > MinTemp)
                {
                    Wait();
                }
                Heater.Engage();
                TemperatureDirection = TemperatureDirection.Rising;

                while (Thermometer.Read() < MaxTemp)
                {
                    Wait();
                }
                Heater.Disengage();
                TemperatureDirection = TemperatureDirection.Falling;
            }
        }

        public void Wait()
        {
            Thread.Sleep(1000);
        }
    }

    interface IRegulate
    {
        IThermometer Thermometer { get; set; }
        IHeater Heater { get; set; }
    }

    interface IThermometer
    {
        double Read();
    }

    interface IHeater
    {
        void Engage();
        void Disengage();
    }

    class IoChannelThermometer : IThermometer
    {
        public static double CurrentTemperature;

        public double Read()
        {
            if (Regulate.TemperatureDirection == TemperatureDirection.Rising)
            {
                CurrentTemperature += 0.5;
            }
            else if (Regulate.TemperatureDirection == TemperatureDirection.Falling)
            {
                CurrentTemperature -= 0.5;
            }

            Console.WriteLine($"Current Temperature: { CurrentTemperature}");
            return CurrentTemperature;
        }
    }

    class IoChannelHeater : IHeater
    {
        public void Disengage()
        {
            Console.WriteLine("Heater is Off");
        }

        public void Engage()
        {
            Console.WriteLine("Heater is On");
        }
    }
}
