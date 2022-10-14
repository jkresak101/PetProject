using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template_csharp_virtual_pet
{
    public abstract class Pet
    {
        #region Properties

        public string Name;
        public string Species { get; set; }

        #endregion

        #region Abstract Methods
        public abstract void Feed();
        public abstract void Play();
        public abstract void SeeDoctor();
        public abstract void Tick();
        public abstract String ToStringRepresentation();

        #endregion

        #region Methods
        //Allow User to Set Pet's Name
        public static String SetPetName()
        {
            Console.Clear();
            Console.WriteLine("\nWhat do you want to name your pet?\n");
            return Console.ReadLine();
        }

        //Allow User to Set Pet's Species
        public static String SetPetSpecies()
        {
            Console.WriteLine("\nWhat species is your pet?\n1. Cat\n2. Dog\n3. Catdog\n");
            switch (Console.ReadLine().ToLower().Trim())
            {
                case "2":
                case "dog":
                    return "Dog";
                    break;
                case "3":
                case "catdog":
                    return "Catdog";
                    break;
                default:
                    return "Cat";
                    break;
            }
        }

        #endregion
    }
}