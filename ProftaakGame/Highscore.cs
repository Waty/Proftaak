using System;
using System.Collections.Generic;

namespace ProftaakGame
{
    public class Highscore
    {
        private static readonly List<Highscore> Highscores = new List<Highscore>();

        public Highscore(Player p)
        {
            Player = p;
            Time = DateTime.Now;
        }

        public Player Player { get; set; }
        public DateTime Time { get; set; }

        public int Points
        {
            get { return Player.Coins*Player.Lives; }
        }

        public static void Add(Highscore highscore)
        {
            Highscores.Add(highscore);
        }
    }
}