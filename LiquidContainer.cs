using System;

namespace ContainerManagement
{
    public class LiquidContainer : Container, IHazardNotifier
    {
        public bool IsHazardous{get;}

        public LiquidContainer(double maxPayload,double tareWeight,double height, double depth, bool isHazardous)
            : base("L", maxPayload, tareWeight, height, depth)
        {
            IsHazardous=isHazardous;
        }
        public new void LoadCargo(double weight)
        {
            double allowedCapacity=IsHazardous ? MaxPayload * 0.5 : MaxPayload * 0.9;

            if (CargoWeight + weight >allowedCapacity)
            {
                NotifyHazard($"WARNING! Hazardous liquid overfill attempt in {SerialNumber}.");
                throw new InvalidOperationException("OverfillException: Hazardous liquids can only be loaded up to 50%, others up to 90%.");
            }
            base.LoadCargo(weight);
        }
        public void NotifyHazard(string message)
        {
            Console.WriteLine("[HAZARD NOTIFICATION]: " +message);
        }
        public override string ToString()
        {
            return base.ToString() + $", Hazardous: {IsHazardous}";
        }
    }
}

