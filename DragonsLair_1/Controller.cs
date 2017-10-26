using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonsLair_1
{
    public class Controller
    {
        private TournamentRepo tournamentRepository = new TournamentRepo();
        public void ShowScore(string tournamentName)
        {
            Tournament tournament = tournamentRepository.GetTournament(tournamentName);

            // starter en Dictionary til at indeholde holdnavn og score
            // se ressource: https://www.dotnetperls.com/dictionary
            Dictionary<string, int> score = new Dictionary<string, int>();

            // Henter alle vindere i alle runder og giver holdene et point per vunden kamp
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

            // Her tilføjes de spillere som tabte i første runde - de har alle 0 point
            List<Team> losingTeams = tournament.GetRound(0).GetLosingTeams();
            foreach (Team losingTeam in losingTeams)
            {
                if (!score.TryGetValue(losingTeam.Name, out int number))
                {
                    score.Add(losingTeam.Name, 0);
                }

            }

            // sorterer dictionary "score" så holdene med flest point er i toppen
            // se ressourcen: https://www.dotnetperls.com/sort-dictionary
            var items = from pair in score
                        orderby pair.Value descending
                        select pair;

            // skriver rudimentært scoreboard til skærm
            Console.Clear();
            foreach (KeyValuePair<string, int> pair in items)
            {

                Console.WriteLine( pair.Key +": " + pair.Value);
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
