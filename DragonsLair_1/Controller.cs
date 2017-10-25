using System;

namespace DragonsLair_1
{
    public class Controller
    {
        private TournamentRepo tournamentRepository = new TournamentRepo();
        private Tournament tournament;
        public void ShowScore(string tournamentName)
        {
            tournament = tournamentRepository.GetTournament("Vinter Turnering");
            int[] teamScore = new int[tournament.GetTeams().Count];
            for (int i = 0; i < teamScore.Length; i++)
            {
                teamScore[i] = 0;
            }
            for (int i = 0; i < tournament.GetTeams().Count; i++)
            {
                for (int j = 0; j < tournament.GetNumberOfRounds(); j++)
                {
                    foreach (Match match in tournament.GetRound(j))
                    {

                    }
                }
            }
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
