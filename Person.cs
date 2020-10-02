using System;
using System.Collections.Generic;

namespace Tjuv_och_polis
{
    public class Person
    {

        public int XPosistion { get; set; }
        public int YPosition { get; set; }
        public int XDirection { get; set; }
        public int YDirection { get; set; }
        public bool Imprisoned { get; set; }
        public int prisonTime { get; set; }
        

        public List<string> Inventory = new List<string>();

        public Person()
        {
            StartPosition();
            Direction();
            Imprisoned = false; //All starts as not arrested. 
            prisonTime = 15; //Value set for if an person is arrested, then he will be arrested for 15 "klicks", = 30 sek.            
        }

        public void Move()
        {
            XPosistion = XPosistion + XDirection;
            YPosition = YPosition + YDirection;
            if (XPosistion > 99)  
            {
                XPosistion = 0;
            }
            if (XPosistion < 0)
            {
                XPosistion = 99; //If an position of instance is greater or smaller than the smallest or biggest span of X/Y it "respawns" at the oposite side. 
            }
            if (YPosition < 0)
            {
                YPosition = 24;
            }
            if (YPosition > 24)
            {
                YPosition = 0;
            }
        }


        private void StartPosition()
        {
            Random rnd = new Random();
            int Rnd = rnd.Next(0, 25);
            YPosition = Rnd;
            int Rnd2 = rnd.Next(0, 100);
            XPosistion = Rnd2;
        } 
                                                 //Every instance created of baseclass person gets a handed Position and direction value given to use in Move method. 
        private void Direction()
        {
            Random rnd = new Random();
            int Rnd = rnd.Next(-1, 2); 
            YDirection = Rnd;
            int Rnd2 = rnd.Next(-1, 2); 
            XDirection = Rnd2;
        }


    }

    public class Theif : Person
    {


    }

    public class Citizen : Person
    {



        public Citizen()
        {

            Inventory.Add("Nycklar");
            Inventory.Add("Pengar");
            Inventory.Add("Mobil");
            Inventory.Add("Klocka");

        }
    }

    public class Police : Person
    {

    }
}
