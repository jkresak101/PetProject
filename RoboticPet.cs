using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace template_csharp_virtual_pet
{
    public class RoboticPet : Pet
    {
        #region Properties
        public int Power { get; set; }
        public int Boredom { get; set; }
        public int Oil { get; set; }

        #endregion

        #region Constructors
        public RoboticPet()

        {
            Name = "Robot Name";
            Species = "Robot Species";
            Power = 80;
            Boredom = 80;
            Oil = 60;
        }
        public RoboticPet(string name, string species)

        {

            Name = name;
            Species = species;
            Power = 80;
            Boredom = 80;
            Oil = 60;
        }

        #endregion

        #region Methods
        //Creates New RoboticPet Object
        public static RoboticPet NewRoboticPet()
        {
            return new RoboticPet(SetPetName(), SetPetSpecies());
        }

        //Prints Pet's Name, Species and Current Status (Levels of Power, Boredom, and Oil)
        public void PetStatus()
        {
            Console.WriteLine("Current Pet's Status:");
            Console.WriteLine($"Name: {Name}\nSpecies: {Species}\nPower: {Power}\nBoredom: {Boredom}\nOil Level: {Oil}");
            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey();
            Console.Clear();
        }

        //Increases Pet's Power by 10
        public override void Feed()
        {
            Power += 10;
            Console.WriteLine($"\nYou have charged {Name}!");
        }

        //Decreases Pet's Power by 10, Increases Oil by 10, and Decreases Boredom by 20
        public override void Play()
        {
            Power -= 10;
            Boredom -= 20;
            Oil -= 10;
            Console.WriteLine($"{Name} has enjoyed playtime!\n");
            
        }

        //Increases Pet's Oil by 30
        public override void SeeDoctor()
        {
            Oil += 30;
            Console.WriteLine($"You have taken {Name} to the mechanic!");
            
        }

        //Decreases Pet's Power by 10, Oil by 5 and Increases Pet's Boredom by 5
        public override void Tick()
        {
            Power -= 10;
            Boredom += 5;
            Oil -= 5;

        }

        public override string ToStringRepresentation()
        {
            return $"{Name}'s Current Status:"
                + "\nType of Pet: Robotic"
                + $"\nPower: {Power}"
                + $"\nBoredom: {Boredom}"
                + $"\nOil Level: {Oil}";
        }

        public override bool Equals(object obj)
        {
            var other = obj as RoboticPet;

            if (other == null)
                return false;
            if (Name != other.Name
                && Species != other.Species
                && Power != other.Power
                && Boredom != other.Boredom
                && Oil != other.Oil)
                return false;

            return true;
        }

        #endregion
    }
}
