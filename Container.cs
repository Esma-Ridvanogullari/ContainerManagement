using System;

namespace ContainerManagement
{
    public abstract class Container
    {
        public string SerialNumber { get; }
        public double MaxPayload{ get; }
        public double CargoWeight{ get; private set;}
        public double TareWeight { get; }
        public double Height{ get; }
        public double Depth { get; }

        private static int _idCounter = 1;
        protected Container(string type, double maxPayload, double tareWeight,double height,double depth)
        {
            SerialNumber = $"KON-{type}-{_idCounter++}";
            MaxPayload=maxPayload;
            TareWeight= tareWeight;
            Height =height;
            Depth=depth;
            CargoWeight = 0;
        }

        public void LoadCargo(double weight)
        {
            if (CargoWeight + weight > MaxPayload)
                throw new InvalidOperationException("OverfillException: Cargo exceeds max payload!");
            CargoWeight += weight;
        }
        public void UnloadCargo()
        {
            CargoWeight=0;
        }
        protected void SetCargoWeight(double weight) 
        {
            CargoWeight=weight;
        }
        public override string ToString()
        {
            return $"{SerialNumber} - MaxPayload: {MaxPayload}kg, CargoWeight: {CargoWeight}kg,TareWeight: {TareWeight}kg";
        }
    }
}

