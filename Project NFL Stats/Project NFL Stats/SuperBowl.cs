using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project_NFL_Stats
{
    public class SuperBowl
    {
        public string Date { get; }

        public string SuperBowlNumber { get; }

        public int Attendance { get; }

        public string WinningQB { get; }

        public string WinningCoach { get; }

        public string WinningTeam { get; }

        public int WinningTeamPoints { get; }

        public string LosingQB { get; }

        public string LosingCoach { get; }

        public string LosingTeam { get; }

        public int LosingTeamPoints { get; }

        public string MVP { get; }

        public string Stadium { get; }

        public string City { get; }

        public string State { get; }


        public SuperBowl(string date, string superBowl, int attendance, string winningQB, string winningCoach, string winningTeam, 
                         int winningTeamPoints, string losingQB, string losingCoach, string losingTeam, int losingTeamPoints,
                         string mvp, string stadium, string city, string state)
        {
            Date = date;
            SuperBowlNumber = superBowl;
            Attendance = attendance;
            WinningQB = winningQB;
            WinningTeam = winningTeam;
            WinningTeamPoints = winningTeamPoints;
            LosingQB = losingQB;
            LosingCoach = losingCoach;
            LosingTeam = losingTeam;
            LosingTeamPoints = losingTeamPoints;
            MVP = mvp;
            Stadium = stadium;
            City = city;
            State = state;
        }
    }
}