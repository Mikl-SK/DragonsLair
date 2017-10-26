﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace DragonsLair_1
{
    public class Round
    {
        private List<Match> matches = new List<Match>();
        
        public void AddMatch(Match m)
        {
            matches.Add(m);
        }

        public Match GetMatch(string teamName1, string teamName2)
        {
            foreach (Match match in matches)
            {
                if ((match.FirstOpponent.Equals(teamName1) && match.SecondOpponent.Equals(teamName2)) ||
                    (match.FirstOpponent.Equals(teamName2) && match.SecondOpponent.Equals(teamName1)))
                {
                    return match;
                }
            }
            return null;
        }

        public bool IsMatchesFinished()
        {
            bool areMatchesFinished = true;
            foreach (Match match in matches)
            {
                if (match.Winner == null)
                {
                    areMatchesFinished = false;
                }
            }
            return areMatchesFinished;
        }

        public List<Team> GetWinningTeams()
        {
            List<Team> winningTeams = new List<Team>();
            foreach (Match match in matches)
            {
                winningTeams.Add(match.Winner);
            }
            return winningTeams;
        }

        public List<Team> GetLosingTeams()
        {
            List<Team> losingTeams = new List<Team>();
            foreach (Match match in matches)
            {
                if (match.FirstOpponent.Name == match.Winner.Name)
                {
                    losingTeams.Add(match.SecondOpponent);
                }
                else
                {
                    losingTeams.Add(match.FirstOpponent);
                }

            }
            return losingTeams;
        }
    }
}
