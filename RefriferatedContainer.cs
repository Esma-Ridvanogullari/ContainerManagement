namespace ContainerManagement
{
    public class RefrigeratedContainer : Container
    {
        public double Temperature{get;}

        public RefrigeratedContainer(double maxPayload,double tareWeight,double height,double depth, double temperature)
            : base("C", maxPayload,tareWeight, height, depth)
        {
            Temperature =temperature;
        }
        public override string ToString()
        {
            return base.ToString() + $", Temperature: {Temperature}°C";
        }
    }
}