using System;
using System.Collections.Generic;
using System.Threading;

namespace Tjuv_och_polis
{
    class Program
    {

        static string[,] board = new string[100, 25]; // (0)X;(1)Y



        static List<Person> theif = new List<Person>();
        static List<Person> citizen = new List<Person>(); //Societybuilding
        static List<Person> police = new List<Person>();

        static List<Person> prison = new List<Person>(); //Prison after arrested

        static int peopleRobbed = 0;
        static int thiefsArrested = 0; //Parameters
        static int isInPrison = 0;







        static void Main(string[] args)
        {

            SocietyBuild();

            while (true)
            {
                DrawGameBoard(theif, citizen, police);
                Moves(theif, citizen, police);
                ConsoleWrite();
                Sentence();
                Thread.Sleep(2000);
                Console.Clear();
            }

        }


        private static void Sentence()
        {
            foreach (Person person in theif)
            {
                if (person.Imprisoned == true)
                {
                    person.prisonTime = person.prisonTime - 1; //Each inmate have 15x in jail in propertys. 15 x 2sek = 30 sek.                     
                }

                if (person.prisonTime == 0) //When an incaserated inmate have done his in jail time counter is done to 0.
                {
                    person.Imprisoned = false; //Prisoner no longer imprisoned. 
                    Console.WriteLine("En fånge släpps nu ut ur fängelset!");
                    prison.Remove(person); // Realese of prisoner.
                    isInPrison--; //Parameter
                    person.prisonTime = 15; //Set back to start value. 
                }

            }

        }

        private static void ConsoleWrite()

        {

            Random rnd = new Random();
            int Rnd = rnd.Next(0, 4);

            foreach (Person person1 in theif)
            {

                foreach (Person person2 in citizen)
                {
                    if (person1.XPosistion == person2.XPosistion && person1.YPosition == person2.YPosition)
                    {
                        peopleRobbed++; //Parameter
                        Console.WriteLine();
                        Console.WriteLine("Tjuv rånar medborgare!");

                        if (person2.Inventory.Count > -1) //As long as the list is not empty, thief tries to steal. 
                        {
                            string Theft = person2.Inventory[Rnd];
                            person1.Inventory.Add(Theft); //Ads the stolen goods to theifs list.  
                            person2.Inventory.RemoveAt(Rnd); //Removes the stolen item from the citizens list.
                        }


                    }
                }

            }


            foreach (Person person1 in theif)
            {
                foreach (Person person2 in police)
                {
                    if (person1.XPosistion == person2.XPosistion && person1.YPosition == person2.YPosition)
                    {
                        thiefsArrested++; //Parameter
                        Console.WriteLine();
                        Console.WriteLine("Polis tar tjuv.");
                        person2.Inventory.AddRange(person1.Inventory); //Police consficate the whole list of the thief if he has stolen anything. 
                        person1.Inventory.Clear(); //Clears the list of the arrested theif. 
                        person1.Imprisoned = true; //The thief is now imprisonated.
                        prison.Add(person1); //Adds prisoner to prison. 
                        isInPrison++; //Parameter

                    }

                }
            }



            Console.WriteLine("\n");
            Console.WriteLine($"Antal medborgare rånade: {peopleRobbed}");
            Console.WriteLine($"Antal tjuvar gripna: {thiefsArrested}\n");
            Console.WriteLine($"Antal tjuvar i fängelset just nu: {isInPrison}");

        }



        private static void SocietyBuild()
        {



            for (int i = 0; i < 20; i++)
            {
                Theif Theif = new Theif();

                theif.Add(Theif);
            }

            for (int i = 0; i < 30; i++)
            {
                Citizen Citz = new Citizen(); // Citz items in List
                citizen.Add(Citz);
            }

            for (int i = 0; i < 20; i++)
            {
                Police Police = new Police();
                police.Add(Police);
            }

        }

        private static void Moves(List<Person> theif, List<Person> citizen, List<Person> police)
        {
            foreach (Person person in theif)
            {
                person.Move();
            }

            foreach (Person person in citizen) //Moves each instance to a new randomized value of X and Y. 
            {
                person.Move();
            }

            foreach (Person person in police)
            {
                person.Move();
            }
        }

        private static void DrawGameBoard(List<Person> theif, List<Person> citizen, List<Person> police)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                for (int x = 0; x < board.GetLength(0); x++)
                {
                    board[x, y] = " "; //Wright space as default

                    foreach (Person person in theif)
                    {
                        if (person.Imprisoned == false) //As long as the thief is not imprisoned he is written out on the board.
                        {
                            if (person.XPosistion == x && person.YPosition == y) //If X and Y in array Board is same as the instanse the instace is written out on the board.
                            {
                                board[x, y] = "T";
                            }
                        }

                    }

                    foreach (Person person in citizen)
                    {
                        if (person.XPosistion == x && person.YPosition == y)
                        {
                            board[x, y] = "M";
                        }
                    }

                    foreach (Person person in police)
                    {
                        if (person.XPosistion == x && person.YPosition == y)
                        {
                            board[x, y] = "P";
                        }
                    }


                }

            }



            for (int y = 0; y < board.GetLength(1); y++)
            {

                for (int x = 0; x < board.GetLength(0); x++)
                {
                    Console.Write(board[x, y]); //Wrights out the full board of X and Y. 
                }

            }

        }


    }
}
