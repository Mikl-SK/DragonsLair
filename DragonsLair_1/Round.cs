﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace DragonsLair_1
{
    public class Round
    {
        private Team freeRider;
        private List<Match> matches = new List<Match>();
        
        public void Add(Match m)
        {
            matches.Add(m);
        }

        public Match GetMatch(string teamName)
        {
            foreach (Match match in matches)
            {
                if ((match.FirstOpponent.Name == teamName || match.SecondOpponent.Name == teamName))
                {
                    return match;
                }
            }
            return null;
        }

        public List<Match> GetMatches()
        {
            return matches;
        }

        public bool IsRoundFinished()
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
            if (freeRider != null)
            {
                winningTeams.Add(freeRider);
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
        public Team GetFreeRider()
        {
            return freeRider;
        }
        public void Add(Team newFreeRider)
        {
            freeRider = newFreeRider;
        }

    }
}
