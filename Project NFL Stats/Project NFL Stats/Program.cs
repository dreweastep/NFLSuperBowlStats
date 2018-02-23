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
        static List<SuperBowl> superBowlList = new List<SuperBowl>();

        static void Main(string[] args)
        {
            ReadIn();
            SuperBowlWinners();
            SuperBowlMVP();
            
            Console.ReadLine();
        }//End of Main method

        private static void ReadIn()
        {
            const char DELIMITER = ',';
            const string FILEPATH = @"C:\Users\easandb\OneDrive - dunwoody.edu\Documents\Advanced Programming\Projects\NFLStatistics\Super_Bowl_Project.csv";

            using(var reader = new StreamReader(FILEPATH))
            {
                string header = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] elements = line.Split(DELIMITER);

                    var thisSuperBowl = new SuperBowl
                    {

                        Date = elements[0],
                        SuperBowlNumber = elements[1],
                        Attendance = Int32.Parse(elements[2]),
                        WinningQB = elements[3],
                        WinningCoach = elements[4],
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

                    };
                    superBowlList.Add(thisSuperBowl);

                }//End of while loop
            }//End of using block


        }//End of ReadIn method

        private static void SuperBowlWinners()
        {
            
            foreach(var superBowl in superBowlList)
            {
                Console.WriteLine($"Team Name: {superBowl.WinningTeam} \n" +
                    $"Super Bowl Won: {superBowl.SuperBowlNumber}\n" +
                    $"Quarterback: {superBowl.WinningQB}\n" +
                    $"Coach: {superBowl.WinningCoach}\n" +
                    $"MVP: {superBowl.MVP}\n" +
                    $"Point Difference: {superBowl.WinningTeamPoints - superBowl.LosingTeamPoints}\n");

            }
        }

        private static void SuperBowlMVP()
        {
            var myMVPList =
                from superBowl in superBowlList
                group superBowl by superBowl.MVP into mvpList
                where mvpList.Count() > 2
                select mvpList.ToList();
            //select new { superBowl.WinningTeam, superBowl.SuperBowlNumber, superBowl.WinningQB, superBowl.WinningCoach, superBowl.MVP, superBowl.WinningTeamPoints, superBowl.LosingTeamPoints };

            foreach (var group in myMVPList)
            {
                Console.WriteLine($"MVP: {group[0].MVP}");
                Console.WriteLine("Super Bowls won: \n");
                foreach (var superBowl in group)
                {
                    Console.WriteLine($"Super Bowl: {superBowl.SuperBowlNumber}\n" +
                        $"Winning Team: {superBowl.WinningTeam} \n" +
                        $"Losing Team: {superBowl.LosingTeam} \n");
                }
            }
        }
    }
}
