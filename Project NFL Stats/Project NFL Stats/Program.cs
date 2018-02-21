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

                    SuperBowl thisSuperBowl = new SuperBowl
                    {

                        Date = elements[0],
                        SuperBowlNumber = elements[1],
                        Attendance = Int32.Parse(elements[2]),
                        WinningQB = elements[4],
                        WinningTeam = elements[5],
                        WinningTeamPoints = Int32.Parse(elements[6]),
                        LosingQB = elements[7],
                        LosingCoach = elements[8],
                        LosingTeam = elements[9],
                        LosingTeamPoints = Int32.Parse(elements[10]),
                        MVP = elements[11],
                        Stadium = elements[12],
                        City = elements[13],
                        State = elements[14]

                    }
                    superBowlList.Add(thisSuperBowl);

                }//End of while loop
            }//End of using block


        }//End of ReadIn method
    }
}
