using System;
using System.Collections.Generic;

namespace ContainerManagement
{
    public class ContainerShip
    {
        public string Name{ get; }
        public double MaxSpeed{get;}
        public int MaxContainerCapacity { get;}
        public double MaxWeightCapacity { get; } 
        private List<Container> Containers { get; }
        public ContainerShip(string name,double maxSpeed,int maxContainerCapacity, double maxWeightCapacity)
        {
            Name=name;
            MaxSpeed = maxSpeed;
            MaxContainerCapacity = maxContainerCapacity;
            MaxWeightCapacity=maxWeightCapacity * 1000; 
            Containers = new List<Container>();
        }
        public void LoadContainer(Container container)
        {
            if (Containers.Count >=MaxContainerCapacity)
                throw new InvalidOperationException("Error: Ship has reached max container capacity!");

            if (GetTotalWeight()+container.TareWeight +container.CargoWeight > MaxWeightCapacity)
                throw new InvalidOperationException("Error: Loading this container will exceed the ship's weight capacity!");
            Containers.Add(container);
            Console.WriteLine($"Container {container.SerialNumber} loaded onto {Name}.");
        }
        public void UnloadContainer(string serialNumber)
        {
            Container container=Containers.Find(c => c.SerialNumber ==serialNumber);

            if (container == null)
                throw new InvalidOperationException("Error: No container with this serial number found on the ship!");
            Containers.Remove(container);
            Console.WriteLine($"Container {serialNumber} has been unloaded from {Name}.");
        }
        public void TransferContainer(ContainerShip targetShip,string containerSerial)
        {
            Container container = Containers.Find(c => c.SerialNumber == containerSerial);

            if (container == null)
                throw new InvalidOperationException($"Error: Container {containerSerial} not found on {Name}.");

            targetShip.LoadContainer(container); 
            Containers.Remove(container); 
            Console.WriteLine($"Container {containerSerial} transferred from {Name} to {targetShip.Name}.");
        }
        public List<Container> GetContainers()
        {
            return new List<Container>(Containers); 
        }
        public double GetTotalWeight()
        {
            double totalWeight=0;
            foreach(var container in Containers)
            {
                totalWeight += container.TareWeight + container.CargoWeight;
            }
            return totalWeight;
        }
        public void PrintShipInfo()
        {
            Console.WriteLine($"Ship: {Name} | Max Speed: {MaxSpeed} knots | Capacity: {Containers.Count}/{MaxContainerCapacity}");
            Console.WriteLine($"Total Weight: {GetTotalWeight() / 1000} tons / {MaxWeightCapacity / 1000} tons");
            Console.WriteLine("Containers on board:");
            foreach (var container in Containers)
            {
                Console.WriteLine("  - " + container);
            }
        }
    }
}

