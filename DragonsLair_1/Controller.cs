using System;
using System.Collections.Generic;

namespace DragonsLair_1
{
    public class Controller
    {
        private TournamentRepo tournamentRepository = new TournamentRepo();
        private Tournament tournament;
        public void ShowScore(string tournamentName)
        {
            tournament = tournamentRepository.GetTournament("Vinter Turnering");
            Dictionary<string, int> score = new Dictionary<string, int>();
            int[] teamScore = new int[tournament.GetTeams().Count];
            for (int i = 0; i < teamScore.Length; i++)
            {
                teamScore[i] = 0;
            }
            
            for (int i = 0; i < tournament.GetTeams().Count; i++)
            {
                for (int j = 0; j < tournament.GetNumberOfRounds(); j++)
                {
                    List<Team> winningTeams = tournament.GetRound(j).GetWinningTeams();
                    foreach (Team winningTeam in winningTeams)
                    {
                        int updateNumber = 1;
                        if (score.TryGetValue(winningTeam.Name, out int number))
                        {
                            updateNumber = number + 1;
                            score.Remove(winningTeam.Name);
                        }
                            score.Add(winningTeam.Name, updateNumber);
                    }

                }

            }
            Console.Clear();
            foreach (KeyValuePair<string, int> entry in score)
            {

                Console.WriteLine( entry.Key +": " + entry.Value);
            }
            Console.ReadKey();
            Console.Clear();
            /*
             * TODO: Calculate for each team how many times they have won
             * Sort based on number of matches won (descending)
             */
        }

        public void ScheduleNewRound(string tournamentName, bool printNewMatches = true)
        {
            // Do not implement this method
        }

        public void SaveMatch(string tournamentName, int roundNumber, string team1, string team2, string winningTeam)
        {
            // Do not implement this method
        }
    }
}
