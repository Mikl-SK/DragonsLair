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
            //for (int team = 0; team < tournament.GetNumberOfRounds(); team++)
            // Demo sætning
            for (int team = 0; team < 3; team++)
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
            Console.WriteLine("  Slutstilling for " + tournament.Name + ":");
            Console.WriteLine("--------------------------------------------\n");
            foreach (KeyValuePair<string, int> pair in items)
            {

                Console.WriteLine( "  " +pair.Key +": " + pair.Value);
            }
            Console.ReadKey();
            Console.Clear();
        }

        public void ScheduleNewRound(string tournamentName, bool printNewMatches = true)
        {
            TournamentRepo tr = new TournamentRepo();
            Tournament t = tr.GetTournament(tournamentName);
            int numberOfRounds = t.GetNumberOfRounds()-1;
            List<Team> teams = new List<Team>();
            Team oldFreeRider;
            Round lastRound;
            lastRound = t.GetRound(numberOfRounds);
            if (numberOfRounds == 0)
            {
                teams = t.GetTeams();
            }
            else
            { 

                bool isRoundFinished = lastRound.IsRoundFinished();
                if(isRoundFinished)
                {
                    teams = lastRound.GetWinningTeams();

                }
                else
                {

                    Console.WriteLine("Runden er endnu ikke afsluttet");
                }
            }
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
                    oldFreeRider = lastRound.GetFreeRider();
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

                // Jesper har tilføjet herfra
                t.Add(newRound);
                if (printNewMatches)
                {
                    Console.Clear();
                    Console.WriteLine("Kommende Runde:");
                    Console.WriteLine("---------------");
                    List<Match> matches = newRound.GetMatches();
                    int i = 1;
                    foreach (Match match in matches)
                    {
                        Console.WriteLine(i + ". " + match.FirstOpponent.Name + " vs. " + match.SecondOpponent.Name);
                        i++;
                    }
                    Console.ReadKey();
                }
            }
            else
            {
                // SetStatusFinished() er tilføjet til tournament-klassen
                t.SetStatusFinished();
                Console.WriteLine("Turneringen " + t.Name + " er afsluttet");
                Console.ReadKey();
                Console.Clear();
            }

            // Jesper out.
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
