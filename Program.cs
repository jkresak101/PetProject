// Your Program Code Here
using System.Runtime.InteropServices;
using System.Xml.Linq;
using template_csharp_virtual_pet;


internal class Program
{
    private static void Main(string[] args)
    {

        #region Introduction and Creation of First Pet
        Pet ActivePet;
        Pet DeadPet;
        Console.Clear();
        Console.WriteLine("Welcome to Virtual Pet!\n" +
            "In this game, you create pets for your Pet Shelter and Adopt them out to new homes, but be careful!\n" +
            "You must properly care for your pets by feeding them, playing with them,\n" +
            "and providing appropriate healthcare.\n" +
            "If you don't take proper care of your pets, they will die and you will no longer be allowed to care for pets.\n\n");

        //Create Initial Pet and Send User to Game's Main Menu
        Console.WriteLine("Do you want start with an Organic Pet or a Robotic Pet?\n"
                        + "1. Organic Pet\n2. Robotic Pet");
        if (Console.ReadLine() == "1")
        {
            ActivePet = OrganicPet.NewOrganicPet();
        }
        else
        {
            ActivePet = RoboticPet.NewRoboticPet();
        }
        Console.Clear();
        Console.WriteLine("You have created a new pet!\n");
        Console.WriteLine(ActivePet.ToStringRepresentation());
        Console.WriteLine("\nPress any key to go to the main menu.");
        Console.ReadKey();
        Console.Clear();
        #endregion

        #region Menu
        //Switch/Case Menu of Game Options Encapsulated in a While Loop
        var isRunning = true;
        Shelter MyShelter = new Shelter();
        while (isRunning)
        {
            //List Game Options in a Console Menu
            Console.WriteLine("Main Menu");
            Console.WriteLine("1. Create New Pet\n" +
                "2. Get Pet Status\n" +
                "3. Feed Pet\n" +
                "4. Play With Pet\n" +
                "5. Take Pet to Veterinarian");
            Console.WriteLine("\nShelter Commmands");
            Console.WriteLine("6. Admit your pet to the Shelter\n" +
                "7. Adopt Pet from Shelter\n" +
                "8. View Shelter Pets And Their Statuses\n" +
                "9. View Number of Pets in Shelter\n" +
                "10. Exit Game\n");
            switch (Console.ReadLine())
            {
                case "1":
                    ActivePet = CreatePet(); //Create New Pet
                    break;
                case "2":
                    ActivePetStatus(ActivePet); //Displays Status of most recently created Pet
                    break;
                case "3":
                    Console.Clear();
                    Feed(ActivePet, MyShelter); //Feed pet(s)
                    break;
                case "4":
                    Console.Clear();
                    Play(ActivePet, MyShelter); //Play with pet(s)
                    break;
                case "5":
                    Console.Clear();
                    Doctor(MyShelter); //Take pet(s) to doctor/mechanic
                    break;
                case "6":
                    Console.Clear();
                    MyShelter.AdmitPet(ActivePet); //Admit most recently created pet to Shelter
                    break;
                case "7":
                    Console.Clear();
                    MyShelter.AdoptMe(); //Adopt out player's choice of pets
                    Console.WriteLine("\nPress any key to return to the main menu.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "8":
                    Console.Clear();
                    Console.WriteLine(MyShelter); //Displays status of all pets in Shelter
                    Console.WriteLine("\nPress any key to return to the main menu.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "9":
                    Console.Clear();
                    Shelter.TotalPets(MyShelter); //Displays number of pets in shelter
                    break;
                case "10":
                    Console.Clear();
                    isRunning = false;
                    break;
                default:
                    continue;
            }
            //After Each Player Action, Degrade All Pet's Properties with Tick() and End Game with DeadPets() if Any Dead Pets Are Found
            //Check to See if Active Pet is in Shelter
            bool isIn = AllPetsInShelter(MyShelter);
            //Run Tick() on All Pets
            TickTock(isIn, ActivePet, MyShelter);
            //Check for Dead Pets and End Game if Any Are Found
            DeadPets(isIn, ActivePet, MyShelter);
        }
        #endregion

        #region Methods
        //Creates User's Choice of New Organic or Robotic Pet
        static Pet CreatePet()
        {
            Pet newPet;
            Console.Clear();
            //Ask for player's choice of pet type and create new pet
            Console.WriteLine("Do you want to make an Organic Pet or a Robotic Pet?\n"
                        + "1. Organic Pet\n2. Robotic Pet");
            if (Console.ReadLine() == "1")
            {
                newPet = OrganicPet.NewOrganicPet();
            }
            else
            {
                newPet = RoboticPet.NewRoboticPet();
            }

            Console.Clear();
            //Confirm pet creation and display new pets status
            Console.WriteLine("You have created a new pet!\n");
            Console.WriteLine(newPet.ToStringRepresentation());
            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey();
            Console.Clear();

            return newPet;
        }

        //Returns Current Status of Most Recentlty Created Pet
        static Pet ActivePetStatus(Pet activePet)
        {
            Console.Clear();
            Console.WriteLine(activePet.ToStringRepresentation());
            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey();
            Console.Clear();

            return activePet;
        }

        //Feeds User's choice of Most Recenlty Created Pet or All Pets in Shelter
        void Feed(Pet activePet, Shelter myShelter)
        {
            Console.Clear();
            Console.WriteLine("Do you want to feed the current pet, or all of the pets in the Shelter?");
            Console.WriteLine("1. Current Pet\n2. All Pets");

            var allPetsIn = false;

            //Only Feed Active Pet
            if (Console.ReadLine() == "1")
            {
                Console.Clear();
                ActivePet.Feed();
            }

            //Feed All Pets
            else
            {
                //Check to See if Active Pet is In Shelter
                allPetsIn = AllPetsInShelter(myShelter);
                
                //Give Option to Add Active Pet to Shelter Before Feeding so that Active Pet is Not Left Unfed
                if (allPetsIn == false)
                {
                    Console.WriteLine($"{ActivePet.Name} is not in the Shelter and will not be fed. Would you like to\n" +
                        $"add {ActivePet.Name} to the shelter before feeding? (Y/N)");
                    if (Console.ReadLine().ToLower().Trim() == "y")
                    {
                        MyShelter.ShelterPets.Add(ActivePet);
                        Console.WriteLine($"{ActivePet.Name} was admitted to the shelter!");
                    }
                    else
                    {
                        Console.WriteLine($"\n{ActivePet.Name} was not admitted to the shelter and will not be fed.");
                    }
                }
                //Feed All Pets in Shelter
                foreach (Pet pet in myShelter.ShelterPets)
                {
                    pet.Feed();
                }
            }
            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey();
            Console.Clear();
        }

        //Plays With User's choice of Most Recenlty Created Pet or All Pets in Shelter
        void Play(Pet activePet, Shelter myShelter)
        {
            Console.Clear();
            Console.WriteLine("Do you want to only play with the current pet, or all of the pets in the Shelter?");
            Console.WriteLine("1. Current Pet\n2. All Pets");

            var allPetsIn = false;

            //Only Play With Active Pet
            if (Console.ReadLine() == "1")
            {
                Console.Clear();
                ActivePet.Play();
            }

            //Play With All Pets
            else
            {
                //Check to See if Active Pet is In Shelter
                allPetsIn = AllPetsInShelter(myShelter);

                //Give Option to Add Active Pet to Shelter Before Play so that Active Pet is Not Left Out of Play Time
                if (allPetsIn == false)
                {
                    Console.WriteLine($"{ActivePet.Name} is not in the Shelter and will not be played with. Would you like to\n" +
                        $"add {ActivePet.Name} to the shelter before play time? (Y/N)");
                    if (Console.ReadLine().ToLower().Trim() == "y")
                    {
                        MyShelter.ShelterPets.Add(ActivePet);
                        Console.WriteLine($"{ActivePet.Name} was admitted to the shelter!");
                    }
                    else
                    {
                        Console.WriteLine($"\n{ActivePet.Name} was not admitted to the shelter and will not be played with. Poor {ActivePet.Name}.");
                    }
                }
                //Play With All Pets in Shelter
                foreach (Pet pet in myShelter.ShelterPets)
                {
                    pet.Play();
                }
            }
            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey();
            Console.Clear();
        }

        //Take User's choice of Most Recenlty Created Pet or All Pets to Doctor
        void Doctor(Shelter myShelter)
        {
            Console.Clear();
            Console.WriteLine("Do you want to only take the current pet, or all of the pets in the Shelter to the Doctor/Mechanic?");
            Console.WriteLine("1. Current Pet\n2. All Pets");

            var allPetsIn = false;

            //Only Take Active Pet to Doctor
            if (Console.ReadLine() == "1")
            {
                Console.Clear();
                ActivePet.SeeDoctor();
            }
            //Take All Pets to Doctor/Mechanic
            else
            {
                //Check to See if Active Pet is In Shelter
                allPetsIn = AllPetsInShelter(myShelter);

                //Give Option to Add Active Pet to Shelter Before Doctor/Mechanic visit so that Active Pet is Not Left Out of Visit
                if (allPetsIn == false)
                {
                    Console.WriteLine($"{ActivePet.Name} is not in the Shelter and will not be taken to the Doctor. Would you like to\n" +
                        $"add {ActivePet.Name} to the shelter before visting the Doctor? (Y/N)");
                    if (Console.ReadLine().ToLower().Trim() == "y")
                    {
                        MyShelter.ShelterPets.Add(ActivePet);
                        Console.WriteLine($"{ActivePet.Name} was admitted to the shelter!");
                    }
                    else
                    {
                        Console.WriteLine($"\n{ActivePet.Name} was not admitted to the shelter and will not recieve care. Poor {ActivePet.Name}.");
                    }
                }
                //Take All Pets in Shelter to Doctor/Mechanic
                foreach (Pet pet in myShelter.ShelterPets)
                {
                    pet.SeeDoctor();
                }
            }
            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey();
            Console.Clear();
        }

        //Check to See if Active Pet is In Shelter
        bool AllPetsInShelter(Shelter myShelter)
        {
            var allPetsInShelter = false;
            OrganicPet oTestPet = new OrganicPet();
            OrganicPet aOTestPet = new OrganicPet();
            RoboticPet rTestPet = new RoboticPet();
            RoboticPet aRTestPet = new RoboticPet();

            foreach (Pet pet in myShelter.ShelterPets)
            {
                //If ActivePet is an OrganicPet, test equality against all OrganicPets in Shelter
                if (pet is OrganicPet && ActivePet is OrganicPet)
                {
                    //Convert ActivePet and current Pet in foreach loop from type Pet to type OrganicPet for equality test
                    oTestPet = (OrganicPet)pet;
                    aOTestPet = (OrganicPet)ActivePet;
                    //Test for equality
                    if (oTestPet.Name == aOTestPet.Name &&
                            oTestPet.Species == aOTestPet.Species &&
                            oTestPet.Hunger == aOTestPet.Hunger &&
                            oTestPet.Boredom == aOTestPet.Boredom &&
                            oTestPet.Health == aOTestPet.Health)
                    {
                        allPetsInShelter = true;
                    }
                }
                //If ActivePet is a RoboticPet, test equality against all RoboticPets in Shelter
                else if (pet is RoboticPet && ActivePet is RoboticPet)
                {
                    //Convert ActivePet and current Pet in foreach loop from type Pet to type RoboticPet for equality test
                    rTestPet = (RoboticPet)pet; 
                    aRTestPet = (RoboticPet)ActivePet;
                    //Test for equality
                    if (rTestPet.Name == aRTestPet.Name &&
                            rTestPet.Species == aRTestPet.Species &&
                            rTestPet.Power == aRTestPet.Power &&
                            rTestPet.Boredom == aRTestPet.Boredom &&
                            rTestPet.Oil == aRTestPet.Oil)
                    {
                        allPetsInShelter = true;
                    }
                }
            }
            return allPetsInShelter;
        }

        //Runs Tick Method on All Pets
        static void TickTock(bool isIn, Pet activePet, Shelter myShelter)
        {
            //ActivePet is In Shelter so No Need to Run Tick() on ActivePet
            if (isIn)
            {
                foreach (Pet pet in myShelter.ShelterPets)
                {
                    pet.Tick();
                }
            }
            //ActivePet is NOT in Shelter so must run activePet.Tick() seperately
            else if (!isIn)
            {
                activePet.Tick();
                foreach(Pet pet in myShelter.ShelterPets)
                {
                    pet.Tick();
                }
            }
        }

        //Checks to See of Any Pets Have Died, IE Any of Pet Properties Are At or Above the Following Levels:
            //Organic Pet - Health 0, Boredom 150, Hunger 150
            //Robotic Pet = Power 0, Boredom 150, Oil 0
        static void DeadPets(bool isIn, Pet activePet, Shelter myShelter)
        {
            bool deadPets = false;
            OrganicPet oTestPet = new OrganicPet();
            RoboticPet rTestPet = new RoboticPet();

            //ActivePet is in Shelter, so Only Checks Dead Status on Shelter Pets
            if (isIn)
            {
                foreach(Pet pet in myShelter.ShelterPets)
                {
                    //If ActivePet is an OrganicPet, test equality against all OrganicPets in Shelter
                    if (pet is OrganicPet)
                    {
                        //Convert current Pet in foreach loop from type Pet to type OrganicPet for death test
                        oTestPet = (OrganicPet)pet;
                        if(oTestPet.Hunger == 150 || oTestPet.Boredom == 150 || oTestPet.Health == 0)
                        {
                            deadPets = true;
                            //DeadPet = pet;
                        }
                    }
                    //If ActivePet is a RoboticPet, test equality against all RoboticPets in Shelter
                    else
                    {
                        //Convert current Pet in foreach loop from type Pet to type RoboticPet for death test
                        rTestPet = (RoboticPet)pet;
                        if (rTestPet.Power == 0 || rTestPet.Boredom == 150 || rTestPet.Oil == 0)
                        {
                            deadPets = true;
                            //DeadPet = pet;
                        }
                    }
                }
            }
            //Checks Dead Status on ActivePet and Shelter Pets Seperately
            else if (!isIn)
            {
                //If ActivePet is an OrganicPet, convert ActivePet from type Pet to type OrganicPet and run death test
                if (activePet is OrganicPet)
                {
                    oTestPet = (OrganicPet)activePet;
                    if (oTestPet.Hunger == 150 || oTestPet.Boredom == 150 || oTestPet.Health == 0)
                    {
                        deadPets = true;
                        //DeadPet = activePet;
                    }
                }
                //If ActivePet is a RoboticPet, convert ActivePet from type Pet to type RoboticPet and run death test
                else
                {
                    rTestPet = (RoboticPet)activePet;
                    if (rTestPet.Power == 0 || rTestPet.Boredom == 150 || rTestPet.Oil == 0)
                    {
                        deadPets = true;
                        //DeadPet = activePet;
                    }
                }
                //Convert each pet in Shelter from type Pet to type OrganicPet or RoboticPet as appropriate and run death test
                foreach (Pet pet in myShelter.ShelterPets)
                {
                    if (pet is OrganicPet)
                    {
                        oTestPet = (OrganicPet)pet;
                        if (oTestPet.Hunger == 150 || oTestPet.Boredom == 150 || oTestPet.Health == 0)
                        {
                            deadPets = true;
                            //DeadPet = pet;
                        }
                    }
                    else
                    {
                        rTestPet = (RoboticPet)pet;
                        if (rTestPet.Power == 0 || rTestPet.Boredom == 150 || rTestPet.Oil == 0)
                        {
                            deadPets = true;
                            //DeadPet = pet;
                        }
                    }
                }
            }

            //If any dead pets are found, notify player and terminate game
            if (deadPets)
            {
                Console.WriteLine($"You let a pet die!\n\nGAME OVER!!!");
                Environment.Exit(0);
            }
            
        }
        #endregion
    
    }
}