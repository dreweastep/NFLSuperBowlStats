using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project_NFL_Stats
{
    class Program
    {
        //DECLARATIONS
        List<SuperBowl> superBowlList = new List<SuperBowl>();

        static void Main(string[] args)
        {

        }//End of Main method

        private void ReadIn()
        {
            const char DELIMITER = ',';
            const string FILEPATH = @"C:\Users\easandb\OneDrive - dunwoody.edu\Documents\Advanced Programming\Projects\NFLStatistics\Super_Bowl_Projects.csv";

            using(var reader = new StreamReader(FILEPATH))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var elements = line.Split(DELIMITER);

                    SuperBowl thisSuperBowl = new SuperBowl();

                    thisSuperBowl.Date = elements[0];
                    thisSuperBowl.SuperBowlNumber = elements[1];
                    thisSuperBowl.Attendance = Int32.Parse(elements[2]);
                    thisSuperBowl.WinningQB = elements[4];
                    thisSuperBowl.WinningTeam = elements[5];
                    thisSuperBowl.WinningTeamPoints = Int32.Parse(elements[6]);
                    thisSuperBowl.LosingQB = elements[7];
                    thisSuperBowl.LosingCoach = elements[8];
                    thisSuperBowl.LosingTeam = elements[9];
                    thisSuperBowl.LosingTeamPoints = Int32.Parse(elements[10]);
                    thisSuperBowl.MVP = elements[11];
                    thisSuperBowl.Stadium = elements[12];
                    thisSuperBowl.City = elements[13];
                    thisSuperBowl.State = elements[14];

                    superBowlList.Add(thisSuperBowl);

                }//End of while loop
            }//End of using block


        }//End of ReadIn method
    }
}
