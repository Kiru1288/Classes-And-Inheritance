using System;
using System.Collections.Generic;
using System.IO;

namespace ClassesAndInheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            // welcome message and options
            Console.WriteLine("Welcome to the Modern Appliances store !\nHow can we assist you?");
            bool exit = false;
            List<Appliance> appliances = ParseAppliancesFromFile("appliances.txt");

            while (!exit)
            {
                // options menu
                Console.WriteLine("1 – Check out appliaces");
                Console.WriteLine("2 – Find appliances by brands");
                Console.WriteLine("3 – Display appliances by type");
                Console.WriteLine("4 – Produce random appliance lists");
                Console.WriteLine("5 – Save and exit");
                Console.WriteLine("Enter a option:");

                string option = Console.ReadLine();

                switch (option)
                {
                    // check out appliances
                    case "1":
                        Console.WriteLine("Enter the item number of an appliance:");
                        string itemNumber = Console.ReadLine();
                        PurchaseAppliance(appliances, itemNumber);
                        break;
                    // find appliances by brand
                    case "2":
                        Console.WriteLine("Enter brand to search for:");
                        string brand = Console.ReadLine();
                        SearchByBrand(appliances, brand);
                        break;
                    // display appliances by type
                    case "3":
                        DisplayAppliancesByType(appliances);
                        break;
                    // produce random appliance lists
                    case "4":
                        Console.WriteLine("Enter number of appliances:");
                        int count = int.Parse(Console.ReadLine());
                        DisplayRandomAppliances(appliances, count);
                        break;
                    // save and exit
                    case "5":
                        SaveAndExit(appliances);
                        exit = true;
                        break;
                    // invalid option
                    default:
                        Console.WriteLine("Invalid option. Please enter a number from 1 to 5.");
                        break;
                }
            }
        }

        public class Appliance
        {
            // properties
            public string ItemNumber { get; }
            public string Brand { get; }
            public int Quantity { get; }
            public int Wattage { get; }
            public string Color { get; }
            public double Price { get; }

            // constructor
            public Appliance(string itemNumber, string brand, int quantity, int wattage, string color, double price)
            {
                ItemNumber = itemNumber;
                Brand = brand;
                Quantity = quantity;
                Wattage = wattage;
                Color = color;
                Price = price;
            }

            // virtual method to get appliance details
            public virtual string GetApplianceDetails()
            {
                return $"Item Number: {ItemNumber}\nBrand: {Brand}\nQuantity: {Quantity}\nWattage: {Wattage}\nColor: {Color}\nPrice: {Price:C}";
            }
        }

        // Refrigerator class
        public class Refrigerator : Appliance
        {
            // additional properties
            public int NumberOfDoors { get; }
            public int Height { get; }
            public int Width { get; }

            // constructor
            public Refrigerator(string itemNumber, string brand, int quantity, int wattage, string color, double price, int numberOfDoors, int height, int width)
                : base(itemNumber, brand, quantity, wattage, color, price)
            {
                NumberOfDoors = numberOfDoors;
                Height = height;
                Width = width;
            }

            // override method to get appliance details
            public override string GetApplianceDetails()
            {
                return base.GetApplianceDetails() + $"\nNumber of Doors: {NumberOfDoors}\nHeight: {Height} inches\nWidth: {Width} inches";
            }
        }

        // Vacuum class
        public class Vacuum : Appliance
        {
            // additional properties
            public string Grade { get; }
            public int BatteryVoltage { get; }

            // constructor
            public Vacuum(string itemNumber, string brand, int quantity, int wattage, string color, double price, string grade, int batteryVoltage)
                : base(itemNumber, brand, quantity, wattage, color, price)
            {
                Grade = grade;
                BatteryVoltage = batteryVoltage;
            }

            // override method to get appliance details
            public override string GetApplianceDetails()
            {
                return base.GetApplianceDetails() + $"\nGrade: {Grade}\nBattery Voltage: {BatteryVoltage} V";
            }
        }

        // Microwave class
        public class Microwave : Appliance
        {
            // additional properties
            public double Capacity { get; }
            public char RoomType { get; }

            // constructor
            public Microwave(string itemNumber, string brand, int quantity, int wattage, string color, double price, double capacity, char roomType)
                : base(itemNumber, brand, quantity, wattage, color, price)
            {
                Capacity = capacity;
                RoomType = roomType;
            }

            // override method to get appliance details
            public override string GetApplianceDetails()
            {
                return base.GetApplianceDetails() + $"\nCapacity: {Capacity} cu.ft\nRoom Type: {RoomType}";
            }
        }

        // Dishwasher class
        public class Dishwasher : Appliance
        {
            // additional properties
            public string SoundRating { get; }
            public string Feature { get; }

            // constructor
            public Dishwasher(string itemNumber, string brand, int quantity, int wattage, string color, double price, string feature, string soundRating)
                : base(itemNumber, brand, quantity, wattage, color, price)
            {
                Feature = feature;
                SoundRating = soundRating;
            }

            // override method to get appliance details
            public override string GetApplianceDetails()
            {
                return base.GetApplianceDetails() + $"\nFeature: {Feature}\nSound Rating: {SoundRating}";
            }
        }

        // method to parse appliances from file
        public static List<Appliance> ParseAppliancesFromFile(string filePath)
        {
            List<Appliance> appliances = new List<Appliance>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    if (parts.Length < 7)
                        continue;

                    string itemNumber = parts[0];
                    string brand = parts[1];
                    int quantity = int.Parse(parts[2]);
                    int wattage = int.Parse(parts[3]);
                    string color = parts[4];
                    double price = double.Parse(parts[5]);

                    char applianceType = itemNumber[0];
                    switch (applianceType)
                    {
                        case '1':
                            if (parts.Length < 9)
                                continue;
                            int numberOfDoors = int.Parse(parts[6]);
                            int height = int.Parse(parts[7]);
                            int width = int.Parse(parts[8]);
                            appliances.Add(new Refrigerator(itemNumber, brand, quantity, wattage, color, price, numberOfDoors, height, width));
                            break;
                        case '2':
                            if (parts.Length < 8)
                                continue;
                            string grade = parts[6];
                            int batteryVoltage = int.Parse(parts[7]);
                            appliances.Add(new Vacuum(itemNumber, brand, quantity, wattage, color, price, grade, batteryVoltage));
                            break;
                        case '3':
                            if (parts.Length < 8)
                                continue;
                            double capacity = double.Parse(parts[6]);
                            char roomType = char.Parse(parts[7]);
                            appliances.Add(new Microwave(itemNumber, brand, quantity, wattage, color, price, capacity, roomType));
                            break;
                        case '4':
                        case '5':
                            if (parts.Length < 8)
                                continue;
                            string feature = parts[6];
                            string soundRating = parts[7];
                            appliances.Add(new Dishwasher(itemNumber, brand, quantity, wattage, color, price, feature, soundRating));
                            break;
                        default:
                            break;
                    }
                }
            }

            return appliances;
        }

        // method to purchase an appliance
        public static void PurchaseAppliance(List<Appliance> appliances, string itemNumber)
        {
            if (string.IsNullOrEmpty(itemNumber))
            {
                Console.WriteLine("Invalid item number. Please provide a valid item number.");
                return;
            }

            if (appliances == null)
            {
                Console.WriteLine("Appliance list is wrong. Cannot proceed with the purchase.");
                return;
            }

            Appliance appliance = appliances.Find(a => a.ItemNumber == itemNumber);
            if (appliance == null)
            {
                Console.WriteLine("Applince not found!");
                return;
            }

            if (appliance.Quantity > 0)
            {
                Console.WriteLine($"Appliance purchased:\n{appliance.GetApplianceDetails()}");

            }
            else
            {
                Console.WriteLine("Appliance not available.");
            }
        }

        // method to search appliances by brand
        public static void SearchByBrand(List<Appliance> appliances, string brand)
        {
            if (appliances == null)
            {
                Console.WriteLine("Appliance list is null. Cannot search by brand.");
                return;
            }

            List<Appliance> matchingAppliances = appliances.FindAll(a => string.Equals(a.Brand, brand, StringComparison.OrdinalIgnoreCase));
            if (matchingAppliances.Count == 0)
            {
                Console.WriteLine("No appliances found for the given brand.");
                return;
            }

            Console.WriteLine($"Matching appliances for brand '{brand}':");
            foreach (var appliance in matchingAppliances)
            {
                Console.WriteLine(appliance.GetApplianceDetails());
            }
        }

        // method to display appliances by type
        public static void DisplayAppliancesByType(List<Appliance> appliances)
        {
            Console.WriteLine("Appliance Types");
            Console.WriteLine("1 – Refrigerators");
            Console.WriteLine("2 – Vacuums");
            Console.WriteLine("3 – Microwaves");
            Console.WriteLine("4 – Dishwashers");
            Console.WriteLine("Enter type of appliance:");

            string type = Console.ReadLine();

            switch (type)
            {
                case "1":
                    Console.WriteLine("Enter number of doors: 2 (double door), 3 (three doors) or 4 (four doors):");
                    int numberOfDoors = int.Parse(Console.ReadLine());
                    DisplayRefrigerators(appliances, numberOfDoors);
                    break;
                case "2":
                    Console.WriteLine("Enter battery voltage value. 18 V (low) or 24 V (high):");
                    int batteryVoltage = int.Parse(Console.ReadLine());
                    DisplayVacuums(appliances, batteryVoltage);
                    break;
                case "3":
                    Console.WriteLine("Room where the microwave will be installed: K (kitchen) or W (work site):");
                    char roomType = char.Parse(Console.ReadLine());
                    DisplayMicrowaves(appliances, roomType);
                    break;
                case "4":
                    Console.WriteLine("Enter the sound rating of the dishwasher: Qt (Quietest), Qr (Quieter), Qu(Quiet) or M (Moderate):");
                    string soundRating = Console.ReadLine();
                    DisplayDishwashers(appliances, soundRating);
                    break;
                default:
                    Console.WriteLine("Invalid appliance type.");
                    break;
            }
        }

        // method to display refrigerators
        public static void DisplayRefrigerators(List<Appliance> appliances, int numberOfDoors)
        {
            Console.WriteLine($"Matching refrigerators with {numberOfDoors} doors:");
            foreach (var appliance in appliances)
            {
                if (appliance is Refrigerator refrigerator && refrigerator.NumberOfDoors == numberOfDoors)
                {
                    Console.WriteLine(refrigerator.GetApplianceDetails());
                }
            }
        }

        // method to display vacuums
        public static void DisplayVacuums(List<Appliance> appliances, int batteryVoltage)
        {
            Console.WriteLine($"Matching vacuums with {batteryVoltage} V battery voltage:");
            foreach (var appliance in appliances)
            {
                if (appliance is Vacuum vacuum && vacuum.BatteryVoltage == batteryVoltage)
                {
                    Console.WriteLine(vacuum.GetApplianceDetails());
                }
            }
        }

        // method to display microwaves
        public static void DisplayMicrowaves(List<Appliance> appliances, char roomType)
        {
            Console.WriteLine($"Matching microwaves for {roomType} installation:");
            foreach (var appliance in appliances)
            {
                if (appliance is Microwave microwave && microwave.RoomType == roomType)
                {
                    Console.WriteLine(microwave.GetApplianceDetails());
                }
            }
        }

        // method to display dishwashers
        public static void DisplayDishwashers(List<Appliance> appliances, string soundRating)
        {
            Console.WriteLine($"Matching dishwashers with {soundRating} sound rating:");
            foreach (var appliance in appliances)
            {
                if (appliance is Dishwasher dishwasher && dishwasher.SoundRating.Equals(soundRating, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(dishwasher.GetApplianceDetails());
                }
            }
        }

        // method to display random appliances
        public static void DisplayRandomAppliances(List<Appliance> appliances, int count)
        {
            if (appliances == null)
            {
                Console.WriteLine("Appliance list is null. Cannot display random appliances.");
                return;
            }

            Console.WriteLine($"Random appliances ({count}):");
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                int index = random.Next(appliances.Count);
                Console.WriteLine(appliances[index].GetApplianceDetails());
            }
        }

        // method to save and exit
        public static void SaveAndExit(List<Appliance> appliances)
        {
            PersistAppliancesToFile(appliances, "res/appliances.txt");
            Console.WriteLine("Appliances list saved to file. Exiting program.");
        }

        // method to persist appliances to file
        public static void PersistAppliancesToFile(List<Appliance> appliances, string filePath)
        {
            if (appliances == null)
            {
                Console.WriteLine("Appliance list is null. Cannot persist to file.");
                return;
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var appliance in appliances)
                {
                    writer.WriteLine(appliance.GetApplianceDetails());
                }
            }
        }
    }
}
