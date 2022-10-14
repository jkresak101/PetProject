using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace template_csharp_virtual_pet
{
    public class OrganicPet : Pet
    {
        #region Properties
        public int Hunger { get; set; }
        public int Boredom { get; set; }
        public int Health { get; set; }

        #endregion

        #region Constructors
        public OrganicPet()
        {
            Name = "Pet Name";
            Species = "Pet Species";
            Hunger = 60;
            Boredom = 60;
            Health = 60;
        }

        public OrganicPet(string name, string species)
        {
            Name = name;
            Species = species;
            Hunger = 60;
            Boredom = 60;
            Health = 60;
        }

        #endregion

        #region Methods
        //Creates New OrganicPet Object
        public static OrganicPet NewOrganicPet()
        {
            return new OrganicPet(SetPetName(), SetPetSpecies());
        }

        //Prints Pet's Name, Species and Current Status (Levels of Hunger, Boredom, and Health)
        public void PetStatus()
        {
            Console.WriteLine("Current Pet's Status:");
            Console.WriteLine($"Name: {Name}\nSpecies: {Species}\nHunger: {Hunger}\nBoredom: {Boredom}\nHealth: {Health}");
            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey();
            Console.Clear();
        }

        //Decreases Pet's Hunger by 10
        public override void Feed()
        {
            Hunger -= 10;
            Console.WriteLine($"\nYou have fed {Name}!");
        }

        //Increases Pet's Hunger by 10, Increases Health by 10, and Decreases Boredom by 20
        public override void Play()
        {
            Hunger += 10;
            Boredom -= 20;
            Health += 10;
            Console.WriteLine($"{Name} has enjoyed playtime!\n");
        }

        //Increases Pet's Health by 30
        public override void SeeDoctor()
        {
            Health += 30;
            Console.WriteLine($"You have taken {Name} to the doctor!");
        }

        //Increases Pet's Health by 5, Boredom by 5 and Decreases Pet's Health by 5
        public override void Tick()
        {
            Hunger += 5;
            Boredom += 5;
            Health -= 5;

        }
        
        public override String ToStringRepresentation()
        {
            return $"{Name}'s Current Status:"
                + "\nType of Pet: Organic"
                + $"\nHunger: {Hunger}"
                + $"\nBoredom: {Boredom}"
                + $"\nHealth: {Health}";
        }

        public override bool Equals(object obj)
        {
            var other = obj as OrganicPet;

            if (other == null)
                return false;
            if (Name != other.Name
                && Species != other.Species
                && Hunger != other.Hunger
                && Boredom != other.Boredom
                && Health != other.Health)
                return false;

            return true;
        }
        
        #endregion
    }
}
