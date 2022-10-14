using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace template_csharp_virtual_pet
{
    public class Shelter
    {
        #region Properties
        public List<Pet> ShelterPets { get; set; } //Pet inventory
        #endregion

        #region Constructor
        public Shelter()
        {
            ShelterPets = new List<Pet>();
        }
        #endregion

        #region Methods
        //Adds Current ActivePet to Shelter
        public void AdmitPet(Pet activePet)
        {
            //Confirm user's decision to add most recently created pet to Shelter
            Console.WriteLine($"Confirm you would like to add {activePet.Name} to shelter? (Y/N)");
            var inShelter = false;
            //If choice to add is confirmed, check whether pet is already in shelter so as not to add pet twice
            if (Console.ReadLine().ToLower().Trim() == "y")
            {
                foreach (Pet pet in ShelterPets)
                {
                    if (pet.Equals(activePet))
                    {
                        inShelter = true;
                        Console.Clear();
                        Console.WriteLine($"It looks like {pet.Name} is already in the shelter!\n\n" +
                            $"Press any key to return to the Main Menu.");
                        Console.ReadKey();
                        Console.Clear();

                        break;
                    }
                }
            }
            //If player changes mind about adding pet to shelter, confirm their choie and return to main menu
            else if (Console.ReadLine() == "n")
            {
                Console.WriteLine($"{activePet.Name} was not added to the shelter.\n" +
                    $"Press any key to return to the Main Menu.");
                Console.ReadKey();
                Console.Clear();
            }
            //If decision to add is confirmed YES and pet is not already in shelter, add most recenlty created pet to shelter
            if (inShelter == false)
            {
                ShelterPets.Add(activePet);
                Console.WriteLine($"{activePet.Name} was admitted to the shelter.\n\nPress any key to return to the Main Menu.");
                Console.ReadKey();
                Console.Clear();
            }

        }

        //Removes (Adopts Out) One Pet of User's Choice from Shelter
        public void AdoptMe()
        {
            Console.WriteLine("Who Would You Like To Adopt?\n");
            //Create and display a string representation of all available pets in shelter
            var listOfPets = "";
            for (var i = 0; i < ShelterPets.Count; i++)
            {
                Pet pet = ShelterPets[i];
                listOfPets += "\n" + (i + 1) + "." + pet.ToStringRepresentation() + "\n";
            }
            Console.WriteLine(listOfPets);
            //Allow player to select which pet they wish to adopt out and confirm their choice
            var deleteindex = int.Parse(Console.ReadLine()) - 1;
            Console.WriteLine("Are You Sure You Want To Adopt " + ShelterPets[deleteindex].Name + "? Y/N");
            //If confirmed, remove pet from shelter
            if (Console.ReadLine().ToLower().Trim() == "y")
            {
                Console.Clear();
                Console.WriteLine($"{ShelterPets[deleteindex].Name} has found their forever home!");
                ShelterPets.RemoveAt(deleteindex);
            }
            //If player changes their mind, confirm their choice and do not remove pet from shelter
            else
            {
                Console.Clear();
                Console.WriteLine($"{ShelterPets[deleteindex].Name} will stay in a cage. Poor, poor {ShelterPets[deleteindex].Name}");
            }
        }

        //Returns Number of Pets in Shelter
        public static void TotalPets(Shelter count)
        {
            Console.WriteLine("Currently, there are " + count.ShelterPets.Count + " pets in the shelter" +
                "\nPress any key to return to the Main Menu.");
            Console.ReadKey();
            Console.Clear();
        }
       
        //Returns List of Pets and Their Statuses That Are Currently In Shelter
        public void ViewShelter()
        {
            Console.Clear();
            Console.WriteLine("Current Pets in Shelter:\n");
            foreach(Pet pet in ShelterPets)
            {
                Console.WriteLine(pet);
            }
            Console.WriteLine("\nPress any key to return to main menu.");
            Console.ReadKey();
            Console.Clear();
        }

        //Creates and returns a string containing status of all pets in Shelter
        public override string ToString()
        {
            var listOfPets = "";
            for(var i = 0; i < ShelterPets.Count; i++)
            {
                Pet pet = ShelterPets[i];
                listOfPets += i + 1 + "." + pet.Name + "\n" +
                    pet.ToStringRepresentation() + "\n\n";
            }

            return "Pets in Shelter:\n\n" + listOfPets;
        }

        #endregion
    }
}
