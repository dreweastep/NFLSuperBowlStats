using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;

namespace Project_NFL_Stats
{
    class Program
    {
        //DECLARATIONS
        static List<SuperBowl> superBowlList = new List<SuperBowl>();
        static List<string> formattedWinners = new List<string>();
        static List<string> formattedMVPs = new List<string>();

        static void Main(string[] args)
        {
            ReadIn();
            SuperBowlWinners();
            SuperBowlMVP();

            foreach (var winner in formattedWinners)
            {
                Console.WriteLine(winner);
            }

            foreach (var mvp in formattedMVPs)
            {
                Console.WriteLine(mvp);
            }




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
                formattedWinners.Add($"Team Name: {superBowl.WinningTeam.PadRight(22)} " +
                    $"Super Bowl Year: {superBowl.Date.Substring(superBowl.Date.Length - 2).PadRight(4)} " +
                    $"Quarterback: {superBowl.WinningQB.PadRight(20)} " +
                    $"Coach: {superBowl.WinningCoach.PadRight(16)} " +
                    $"MVP: {superBowl.MVP.PadRight(18)} " +
                    $"Point Difference: {superBowl.WinningTeamPoints - superBowl.LosingTeamPoints} ");

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
                formattedMVPs.Add($"MVP: {group[0].MVP}");
 
                foreach (var superBowl in group)
                {
                    formattedMVPs.Add($"Super Bowl Year: {superBowl.Date.Substring(superBowl.Date.Length - 2).PadRight(4)} " +
                        $"Winning Team: {superBowl.WinningTeam.PadRight(22)} " +
                        $"Losing Team: {superBowl.LosingTeam}");
                }
            }
        }

        //private static 
    }
}
