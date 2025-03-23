using System;

namespace ContainerManagement
{
    public class GasContainer : Container, IHazardNotifier
    {
        public double Pressure{get; }

        public GasContainer(double maxPayload,double tareWeight,double height, double depth, double pressure)
            : base("G",maxPayload,tareWeight, height,depth)
        {
            Pressure=pressure;
        }
        public new void UnloadCargo()
        {
            SetCargoWeight(CargoWeight * 0.05); 
            NotifyHazard($"Gas unloading safety check in {SerialNumber}. 5% cargo remains.");
        }
        public void NotifyHazard(string message)
        {
            Console.WriteLine("[HAZARD NOTIFICATION]: " + message);
        }
        public override string ToString()
        {
            return base.ToString() + $", Pressure: {Pressure} atm";
        }
    }
}


