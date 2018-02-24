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
        public static List<SuperBowl> superBowlList = new List<SuperBowl>();
        public static List<string> formattedWinners = new List<string>();
        public static List<string> formattedMVPs = new List<string>();
        public static List<string> formattedTopAttended = new List<string>();
        public static List<string> formattedHostStates = new List<string>();
        public static List<string> formattedHostStatesInfo = new List<string>();
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
                formattedWinners.Add($"{superBowl.WinningTeam.PadRight(22)} " +
                    $"{superBowl.Date.Substring(superBowl.Date.Length - 2).PadRight(4)} " +
                    $"{superBowl.WinningQB.PadRight(20)} " +
                    $"{superBowl.WinningCoach.PadRight(16)} " +
                    $"{superBowl.MVP.PadRight(18)} " +
                    $"{superBowl.WinningTeamPoints - superBowl.LosingTeamPoints} ");

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
                formattedTopAttended.Add($"{superBowl.Attendance.ToString().PadRight(12)} " +
                    $"{superBowl.Date.Substring(superBowl.Date.Length - 2).PadRight(4)} " +
                    $"{superBowl.WinningTeam.PadRight(22)} " +
                    $"{superBowl.LosingTeam.PadRight(22)} " +
                    $"{superBowl.City.PadRight(13)} " +
                    $"{superBowl.State.PadRight(12)} " +
                    $"{superBowl.Stadium}");
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
                    formattedHostStatesInfo.Add($"{superBowl.SuperBowlNumber.PadRight(12)}" +
                        $"{superBowl.City.PadRight(15)} " +
                        $"{superBowl.Stadium}");
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
                    formattedMVPs.Add($"{superBowl.Date.Substring(superBowl.Date.Length - 2).PadRight(15)} " +
                        $"{superBowl.WinningTeam.PadRight(22)} " +
                        $"{superBowl.LosingTeam}");
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
                formattedLosingCoach.Add($"{superBowl.PadRight(12)}");
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
                formattedWinningCoach.Add($"{superBowl.PadRight(12)}");
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
                formattedLosingTeam.Add($"{superBowl.PadRight(12)}");
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
                formattedWinningTeam.Add($"{superBowl.PadRight(12)}");
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
                formattedPointDifference.Add($"Super Bowl: {superBowl.SuperBowlNumber.PadRight(7)}" +
                    $"Point Difference: {superBowl.WinningTeamPoints - superBowl.LosingTeamPoints}");
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
            formattedAverageAttendence.Add($"The average attendence for a superbowl is: {average}");
        }

        private static void CreateTextFile()
        {
            Console.WriteLine("Welcome to the NFL Statistics Program. \n" +
                    "Please enter the file path for text file to be created (without .txt extension)");

            bool exitFileLocation = false;
            while (!exitFileLocation)
            {
                string filePath = Console.ReadLine().Trim();
                string fileName = filePath + ".txt";

                try
                {
                    using (var writer = new StreamWriter(@fileName))
                    {

                        writer.WriteLine("NFL Statistics:");
                        writer.WriteLine();
                        writer.WriteLine();

                        writer.WriteLine("Below is a list of all NFL Super Bowl Winners: ");
                        writer.WriteLine();

                        writer.WriteLine("Team Name".PadRight(23) +
                            "Year".PadRight(5) +
                            "Quarterback".PadRight(21) +
                            "Coach".PadRight(17) +
                            "MVP".PadRight(19) +
                            "Winning Team Points");

                        writer.WriteLine();

                        foreach (var element in formattedWinners)
                        {
                            writer.WriteLine(element);
                        }

                        writer.WriteLine();
                        writer.WriteLine();

                        writer.WriteLine("Below is a list of all the top 5 most attended super bowls: ");
                        writer.WriteLine();

                        writer.WriteLine("Attendence".PadRight(13) +
                            "Year".PadRight(5) +
                            "Winning Team".PadRight(23) +
                            "Losing Team".PadRight(23) +
                            "City".PadRight(14) +
                            "State".PadRight(13) +
                            "Stadium");
                        writer.WriteLine();

                        foreach (var element in formattedTopAttended)
                        {
                            writer.WriteLine(element);
                        }

                        writer.WriteLine();
                        writer.WriteLine();

                        writer.WriteLine("Below is a list of all the superbowls hosted by the state that host hosted the most superbowls: ");
                        writer.WriteLine();

                        writer.WriteLine(formattedHostStates[0]);
                        writer.WriteLine();

                        writer.WriteLine("Superbowl".PadRight(12) +
                            "City".PadRight(16) +
                            "Stadium");
                        writer.WriteLine();

                        foreach (var element in formattedHostStatesInfo)
                        {
                            writer.WriteLine(element);
                        }
                        writer.WriteLine();
                        writer.WriteLine();

                        writer.WriteLine("Below is a list of players that have been mvp more than twice and the superbowls in which they were named mvp: ");
                        
                        foreach(var element in formattedMVPs)
                        {
                            if (element.Length < 30)
                            {
                                writer.WriteLine();
                                writer.WriteLine(element);
                                writer.WriteLine();

                                writer.WriteLine($"Super Bowl Year".PadRight(16) +
                                    "Winning Team".PadRight(23) +
                                    "Losing Team");
                                writer.WriteLine();
                            }
                            else
                            {
                                writer.WriteLine(element);
                            }
                        }
                        writer.WriteLine();
                        writer.WriteLine();

                        writer.WriteLine("Below is a list of the coaches that have lost the most superbowls: ");

                        foreach (var element in formattedLosingCoach)
                        {
                            writer.WriteLine(element);
                        }
                        writer.WriteLine();
                        writer.WriteLine();

                        writer.WriteLine("Below is a list of the coaches that have won the most superbowls: ");

                        foreach (var element in formattedWinningCoach)
                        {
                            writer.WriteLine(element);
                        }
                        writer.WriteLine();
                        writer.WriteLine();

                        writer.WriteLine("Below is a list of the teams that have lost the most superbowls: ");

                        foreach (var element in formattedLosingTeam)
                        {
                            writer.WriteLine(element);
                        }
                        writer.WriteLine();
                        writer.WriteLine();

                        writer.WriteLine("Below is a list of the teams that have won the most superbowls: ");

                        foreach (var element in formattedWinningTeam)
                        {
                            writer.WriteLine(element);
                        }
                        writer.WriteLine();
                        writer.WriteLine();

                        writer.WriteLine("Below is the superbowl with the greatest point difference between the winning and losing team: ");

                        foreach (var element in formattedPointDifference)
                        {
                            writer.WriteLine(element);
                        }
                        writer.WriteLine();
                        writer.WriteLine();

                        foreach (var element in formattedAverageAttendence)
                        {
                            writer.WriteLine(element);
                        }
                        writer.WriteLine();
                        writer.WriteLine();

                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();

                        Console.WriteLine("Thank You for using the NFL Statistics Program!");

                        var file = new FileInfo(fileName);
                        string absolutePath = file.FullName;

                        Console.WriteLine("The absolute path for your file is: " + absolutePath);

                    }//End of using

                    exitFileLocation = true;

                }//End of try
                catch
                {
                    Console.WriteLine("That file path cannot be used, please enter a valid file path.");
                }

            }//End of while loop
        }
    }
}
