
using System;
using System.Collections.Generic;

namespace ContainerManagement
{
    class Program
    {
        static List<ContainerShip> ships=new List<ContainerShip>();
        static List<Container> containers=new List<Container>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n=== Container Management System ===");
                Console.WriteLine("1. Add a Ship");
                Console.WriteLine("2. Add a Container");
                Console.WriteLine("3. Load Container onto Ship");
                Console.WriteLine("4. Transfer Container between Ships");
                Console.WriteLine("5. Print Ship Information");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");

                string choice =Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddShip();
                        break;
                    case "2":
                        AddContainer();
                        break;
                    case "3":
                        LoadContainerOntoShip();
                        break;
                    case "4":
                        TransferContainerBetweenShips();
                        break;
                    case "5":
                        PrintShipInfo();
                        break;
                    case "6":
                        Console.WriteLine("Exiting program...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void AddShip()
        {
            Console.Write("Enter Ship Name: ");
            string name=Console.ReadLine();
            Console.Write("Enter Max Speed (knots): ");
            double speed =double.Parse(Console.ReadLine());
            Console.Write("Enter Max Container Capacity: ");
            int capacity =int.Parse(Console.ReadLine());
            Console.Write("Enter Max Weight Capacity (tons): ");
            double weight = double.Parse(Console.ReadLine());

            ships.Add(new ContainerShip(name,speed,capacity, weight));
            Console.WriteLine($"Ship '{name}' added successfully.");
        }
        static void AddContainer()
        {
            Console.WriteLine("Select Container Type: (1) Refrigerated, (2) Liquid, (3) Gas");
            string type = Console.ReadLine();

            Console.Write("Enter Max Payload (kg): ");
            double maxPayload=double.Parse(Console.ReadLine());
            Console.Write("Enter Tare Weight (kg): ");
            double tareWeight= double.Parse(Console.ReadLine());
            Console.Write("Enter Height (cm): ");
            double height =double.Parse(Console.ReadLine());
            Console.Write("Enter Depth (cm): ");
            double depth = double.Parse(Console.ReadLine());
            Container container = null;

            switch (type)
            {
                case "1":
                    Console.Write("Enter Temperature (°C): ");
                    double temp= double.Parse(Console.ReadLine());
                    container =new RefrigeratedContainer(maxPayload, tareWeight,height,depth, temp);
                    break;
                case "2":
                    Console.Write("Is it Hazardous? (yes/no): ");
                    bool isHazardous=Console.ReadLine().ToLower() =="yes";
                    container=new LiquidContainer(maxPayload, tareWeight, height, depth, isHazardous);
                    break;
                case "3":
                    Console.Write("Enter Pressure (atm): ");
                    double pressure =double.Parse(Console.ReadLine());
                    container=new GasContainer(maxPayload, tareWeight, height, depth, pressure);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Returning to main menu.");
                    return;
            }
            containers.Add(container);
            Console.WriteLine($"Container '{container.SerialNumber}' added successfully.");
        }
        static void LoadContainerOntoShip()
        {
            if (ships.Count==0 || containers.Count == 0)
            {
                Console.WriteLine("No ships or containers available.");
                return;
            }
            Console.WriteLine("Available Ships:");
            for (int i = 0; i < ships.Count; i++)
                Console.WriteLine($"{i + 1}.{ships[i].Name}");

            Console.Write("Select a Ship: ");
            int shipIndex=int.Parse(Console.ReadLine()) - 1;

            Console.WriteLine("Available Containers:");
            for (int i = 0; i < containers.Count; i++)
                Console.WriteLine($"{i + 1}. {containers[i].SerialNumber}");

            Console.Write("Select a Container: ");
            int containerIndex =int.Parse(Console.ReadLine()) - 1;

            try
            {
                ships[shipIndex].LoadContainer(containers[containerIndex]);
                containers.RemoveAt(containerIndex);
                Console.WriteLine("Container loaded successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
        static void TransferContainerBetweenShips()
        {
            if (ships.Count< 2)
            {
                Console.WriteLine("At least two ships are required for transfer.");
                return;
            }
            Console.WriteLine("Available Ships:");
            for (int i = 0; i < ships.Count; i++)
                Console.WriteLine($"{i + 1}. {ships[i].Name}");

            Console.Write("Select Source Ship: ");
            int sourceIndex =int.Parse(Console.ReadLine()) - 1;
            Console.Write("Select Target Ship: ");
            int targetIndex= int.Parse(Console.ReadLine()) - 1;

            Console.WriteLine("Containers on Source Ship:");
            List<Container> sourceContainers = ships[sourceIndex].GetContainers();
            if (sourceContainers.Count == 0)
            {
                Console.WriteLine("No containers available on this ship.");
                return;
            }
            for (int i = 0; i < sourceContainers.Count; i++)
                Console.WriteLine($"{i + 1}. {sourceContainers[i].SerialNumber}");

            Console.Write("Select a Container to Transfer: ");
            int containerIndex =int.Parse(Console.ReadLine()) - 1;

            try
            {
                ships[sourceIndex].TransferContainer(ships[targetIndex], sourceContainers[containerIndex].SerialNumber);
                Console.WriteLine("Container transferred successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
        static void PrintShipInfo()
        {
            if (ships.Count ==0)
            {
                Console.WriteLine("No ships available.");
                return;
            }
            Console.WriteLine("Available Ships:");
            for (int i = 0; i < ships.Count; i++)
                Console.WriteLine($"{i + 1}. {ships[i].Name}");

            Console.Write("Select a Ship: ");
            int shipIndex = int.Parse(Console.ReadLine()) - 1;

            ships[shipIndex].PrintShipInfo();
        }
    }
}


