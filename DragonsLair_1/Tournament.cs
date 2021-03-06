﻿using System;
using System.Collections.Generic;

namespace DragonsLair_1
{
    public class Tournament
    {
        public string Name { get; set; }
        private List<Round> rounds = new List<Round>();
        public bool IsTournamentFinished { get; set; }

        public Tournament(string tournamentName)
        {
            Name = tournamentName;
            IsTournamentFinished = false;
        }

        public List<Team> GetTeams()
        {
            return new List<Team>(new Team[] {
                new Team("The Valyrians"),
                new Team("The Spartans"),
                new Team("The Cretans"),
                new Team("The Thereans"),
                new Team("The Coans"),
                new Team("The Cnideans"),
                new Team("The Megareans"),
                new Team("The Corinthians")
            });
        }

        public int GetNumberOfRounds()
        {
            return 1;
        }
        
        public Round GetRound(int idx)
        {
            Round r = new Round();
            if (idx == 0)
            {
                Match match1 = new Match();
                match1.FirstOpponent = new Team("The Valyrians");
                match1.SecondOpponent = new Team("The Spartans");
                match1.Winner = new Team("The Valyrians");
                r.Add(match1);

                Match match2 = new Match();
                match2.FirstOpponent = new Team("The Cretans");
                match2.SecondOpponent = new Team("The Thereans");
                match2.Winner = new Team("The Thereans");
                r.Add(match2);

                Match match3 = new Match();
                match3.FirstOpponent = new Team("The Coans");
                match3.SecondOpponent = new Team("The Cnideans");
                match3.Winner = new Team("The Coans");
                r.Add(match3);

                Match match4 = new Match();
                match4.FirstOpponent = new Team("The Megareans");
                match4.SecondOpponent = new Team("The Corinthians");
                match4.Winner = new Team("The Corinthians");
                r.Add(match4);

            }
            else if (idx == 1)
            {
                Match match1 = new Match();
                match1.FirstOpponent = new Team("The Valyrians");
                match1.SecondOpponent = new Team("The Thereans");
                match1.Winner = new Team("The Valyrians");
                r.Add(match1);

                Match match2 = new Match();
                match2.FirstOpponent = new Team("The Coans");
                match2.SecondOpponent = new Team("The Corinthians");
                match2.Winner = new Team("The Coans");
                r.Add(match2);
            }
            else if (idx == 2)
            {
                Match match1 = new Match();
                match1.FirstOpponent = new Team("The Valyrians");
                match1.SecondOpponent = new Team("The Coans");
                match1.Winner = new Team("The Coans");
                r.Add(match1);
            }
            return r;
        }
        public void Add(Round newRound)
        {
            rounds.Add(newRound);
        }

        public Team GetTeam(string teamName)
        {
            List<Team> teams = GetTeams();
            foreach (Team team in teams)
            {
                if (teamName == team.Name)
                {
                    return team;
                }
            }

            return null;
        }

        public void SetStatusFinished()
        {
            IsTournamentFinished = true;

        }
    }
}
