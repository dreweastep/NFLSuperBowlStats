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
        public static List<SuperBowl> superBowlList = new List<SuperBowl>();
        public static List<string> formattedWinners = new List<string>();
        public static List<string> formattedMVPs = new List<string>();
        public static List<string> formattedTopAttended = new List<string>();
        public static List<string> formattedHostStates = new List<string>();
        public static List<string> formattedLosingCoach = new List<string>();
        public static List<string> formattedWinningCoach = new List<string>();
        public static List<string> formattedLosingTeam = new List<string>();
        public static List<string> formattedWinningTeam = new List<string>();
        public static List<string> formattedPointDifference = new List<string>();
        public static List<string> formattedAverageAttendence = new List<string>();



        static void Main(string[] args)
        {
            ReadIn();
            SuperBowlWinners();
            SuperBowlAttendence();
            SuperBowlState();
            SuperBowlMVP();
            SuperBowlLosingCoach();
            SuperBowlWinningCoach();
            SuperBowlLosingTeam();
            SuperBowlWinningTeam();
            SuperBowlPointDifference();
            SuperBowlAverageAttendence();

            CreateTextFile();

            foreach (var winner in formattedWinners)
            {
                Console.WriteLine(winner);
            }

            foreach (var mvp in formattedMVPs)
            {
                Console.WriteLine(mvp);
            }

            foreach (var attendence in formattedTopAttended)
            {
                Console.WriteLine(attendence);
            }

            foreach (var state in formattedHostStates)
            {
                Console.WriteLine(state);
            }

            foreach (var item in formattedLosingCoach)
            {
                Console.WriteLine(item);
            }

            foreach (var item in formattedWinningCoach)
            {
                Console.WriteLine(item);
            }

            foreach (var item in formattedLosingTeam)
            {
                Console.WriteLine(item);
            }

            foreach (var item in formattedWinningTeam)
            {
                Console.WriteLine(item);
            }

            foreach (var item in formattedPointDifference)
            {
                Console.WriteLine(item);
            }

            foreach (var item in formattedAverageAttendence)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }//End of Main method
        
        private static void ReadIn()
        {
            //Module reads in csv file and instantiates superBowl objects
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

                }
            }


        }

        private static void SuperBowlWinners()
        {
            //Module displays information about all super bowl winners and ads to formattedWinners list
            foreach(var superBowl in superBowlList)
            {
                formattedWinners.Add($"Team Name: {superBowl.WinningTeam.PadRight(22)} " +
                    $"Super Bowl Year: {superBowl.Date.Substring(superBowl.Date.Length - 2).PadRight(4)} " +
                    $"Quarterback: {superBowl.WinningQB.PadRight(20)} " +
                    $"Coach: {superBowl.WinningCoach.PadRight(16)} " +
                    $"MVP: {superBowl.MVP.PadRight(17)} " +
                    $"Point Difference: {superBowl.WinningTeamPoints - superBowl.LosingTeamPoints} ");

            }
        }

        private static void SuperBowlAttendence()
        {
            //Module finds top 5 attended super bowls and adds to formattedTopAttended
            var myAttendenceList =
                (from superBowl in superBowlList
                orderby superBowl.Attendance descending
                select superBowl).Take(5);

            foreach (var superBowl in myAttendenceList)
            {
                formattedTopAttended.Add($"Attendence: {superBowl.Attendance.ToString().PadRight(9)}" +
                    $"Super Bowl Year: {superBowl.Date.Substring(superBowl.Date.Length - 2).PadRight(4)} " +
                    $"Winning Team: {superBowl.WinningTeam.PadRight(22)} " +
                    $"Losing Team: {superBowl.LosingTeam.PadRight(22)}" +
                    $"City: {superBowl.City.PadRight(13)} " +
                    $"State: {superBowl.State.PadRight(12)} " +
                    $"Stadium: {superBowl.Stadium}");
            }
        }

        private static void SuperBowlState()
        {
            var myStateList =
                (from superBowl in superBowlList
                 group superBowl by superBowl.State into stateList
                 orderby stateList.Count() descending
                 select stateList).Take(1).ToList();


            foreach (var group in myStateList)
            {
                formattedHostStates.Add($"State: {group.Key.PadRight(12)}");

                foreach (var superBowl in group)
                {
                    formattedHostStates.Add($"Super Bowl: {superBowl.SuperBowlNumber.PadRight(7)}" +
                        $"City: {superBowl.City.PadRight(13)} " +
                        $"Stadium: {superBowl.Stadium}");
                }
            }
        }

        private static void SuperBowlMVP()
        {
            //Module finds MVPs that appeared more than twice and adds to formattedMVPs list
            var myMVPList =
                from superBowl in superBowlList
                group superBowl by superBowl.MVP into mvpList
                where mvpList.Count() > 2
                select mvpList.ToList();

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

        private static void SuperBowlLosingCoach()
        {
            var group = superBowlList.ToLookup(x => x.LosingCoach);
            var mostLosing = group.Max(x => x.Count());
            var mostLosingCoach = group.Where(x => x.Count() == mostLosing)
                                          .Select(x => x.Key).ToList();


            foreach (var superBowl in mostLosingCoach)
            {
                formattedLosingCoach.Add($"Coach: {superBowl.PadRight(12)}");
            }
        }

        private static void SuperBowlWinningCoach()
        {
            var group = superBowlList.ToLookup(x => x.WinningCoach);
            var mostWinning = group.Max(x => x.Count());
            var mostWinningCoach = group.Where(x => x.Count() == mostWinning)
                                          .Select(x => x.Key).ToList();


            foreach (var superBowl in mostWinningCoach)
            {
                formattedWinningCoach.Add($"Coach: {superBowl.PadRight(12)}");
            }
        }

        private static void SuperBowlLosingTeam()
        {
            var group = superBowlList.ToLookup(x => x.LosingTeam);
            var mostLosing = group.Max(x => x.Count());
            var mostLosingTeam = group.Where(x => x.Count() == mostLosing)
                                          .Select(x => x.Key).ToList();


            foreach (var superBowl in mostLosingTeam)
            {
                formattedLosingTeam.Add($"Team: {superBowl.PadRight(12)}");
            }
        }

        private static void SuperBowlWinningTeam()
        {
            var group = superBowlList.ToLookup(x => x.WinningTeam);
            var mostWinning = group.Max(x => x.Count());
            var mostWinningTeam = group.Where(x => x.Count() == mostWinning)
                                          .Select(x => x.Key).ToList();


            foreach (var superBowl in mostWinningTeam)
            {
                formattedWinningTeam.Add($"Team: {superBowl.PadRight(12)}");
            }
        }

        private static void SuperBowlPointDifference()
        {
            var myPointList =
                (from superBowl in superBowlList
                 orderby superBowl.WinningTeamPoints - superBowl.LosingTeamPoints descending
                 select superBowl).Take(1);

            foreach (var superBowl in myPointList)
            {
                formattedTopAttended.Add($"Super Bowl: {superBowl.SuperBowlNumber.PadRight(7)}" +
                    $"Point difference: {superBowl.WinningTeamPoints - superBowl.LosingTeamPoints}");
            }
        }

        private static void SuperBowlAverageAttendence()
        {
            double aggergate = 0;

            foreach(var superBowl in superBowlList)
            {
                aggergate = aggergate + superBowl.Attendance;
            }

            double average = Math.Round(aggergate / superBowlList.Count());
            formattedAverageAttendence.Add($"Average Attendence: {average}");
        }

        private static void CreateTextFile()
        {
            
        }
    }
}
