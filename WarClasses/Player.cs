using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarClasses
{
    public class Player
    {
        //public Deck Deck { get; set; }
        public Deck Deck = new Deck();
        public string Name { get; set; }
        public int Score { get; set; }

        public Player()
        {

        }

        public Player(string name)
        {
            Name = name;
        }

        public Player(string name, Deck deck)
        {
            Name = name;
            Deck = deck;
        }
    }
}
