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
            for (int team = 0; team < tournament.GetNumberOfRounds(); team++)
            {
                List<Team> winningTeams = tournament.GetRound(team).GetWinningTeams();
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
            TournamentRepo tr = new TournamentRepo();
            Tournament t = tr.GetTournament(tournamentName);
            int numberOfRounds = t.GetNumberOfRounds();
            if(numberOfRounds == 0)
            {
                List<Team> teams = new List<Team>();
                teams = t.GetTeams();
            }
            else
            { 
                Round lastRound;
                lastRound = t.GetRound(numberOfRounds - 1);
                bool isRoundFinished = lastRound.IsRoundFinished();
                if(isRoundFinished)
                {
                    List<Team> teams = lastRound.GetWinningTeams();

                    if (teams.Count > 1)
                    {
                        //fundet på stackoverflow skal undersøges nærmere.
                        Random rng = new Random();
                        int n = teams.Count;
                        while (n > 1)
                        {
                            n--;
                            int k = rng.Next(n + 1);
                            Team value = teams[k];
                            teams[k] = teams[n];
                            teams[n] = value;
                        }
                        Round newRound = new Round();
                        if (teams.Count % 2 == 1)
                        {
                            Team oldFreeRider = lastRound.GetFreeRider();
                            Team newFreeRider;
                            //freerider holdet springer runden over
                            int i = 0;
                            do
                            {
                                newFreeRider = teams[i];
                                i++;
                            } while (oldFreeRider.Equals(teams[i]));

                            teams.Remove(newFreeRider);
                            newRound.Add(newFreeRider);
                        }
                        int numberOfMatches = teams.Count / 2;
                        for (int i = 0; i < numberOfMatches; i++)
                        {
                            Match newMatch = new Match();
                            Team first = teams[0];
                            teams.Remove(teams[0]);
                            Team second = teams[0];
                            teams.Remove(teams[0]);
                            newMatch.FirstOpponent = first;
                            newMatch.SecondOpponent = second;
                            newRound.Add(newMatch);
                        }
                        t.Add(newRound);
                    }
                    else
                    {
                        t.SetStatusFinished();
                    }
                    
                }
                else
                {
                    Console.WriteLine("Fejl runden er ikke færdig");
                    Console.ReadLine();
                }
            }
        }

        public string SaveMatch(string tournamentName, int roundNumber, string winningTeam)
        {
            TournamentRepo tr = new TournamentRepo();
            Tournament t = tr.GetTournament(tournamentName);
            Round r = t.GetRound(roundNumber -1);
            Match m = r.GetMatch(winningTeam);

            if (m != null && m.Winner == null)
            {
                Team w = t.GetTeam(winningTeam);
                m.SetWinner(w);
                return "Du har opdateret vinder";
            }
            else
            {
                return "Der er sket en fejl";
            }
        }

        
    }
}
