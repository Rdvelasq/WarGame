using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarClasses;
using WarEnums;

namespace WarRepoNameSpace
{
    public class WarRepo
    {
        private Queue<Card> _cards = new Queue<Card>();
        public void  MakeDeck()
        {
            //For loops thorugh the 4 suits
            for(int suit = 1; suit <= 4; suit++)
            {
                //Second For Loop to loop through the 13 cards and giving it the suit from the previous loop 
                for(int rank = 1; rank <= 13; rank++)
                {
                    //Only Spades and Clubs can be black. They are in positions 3 and 4 in our warWnums
                    if ((suit == 3) || (suit == 4))
                    {
                        Rank rankEnum = (Rank)rank;
                        Suit suitEnum = (Suit)suit;
                        Color colorEnum = (Color)1;
                        Card card = new Card(rankEnum, suitEnum, colorEnum);
                    }
                    else
                    {
                        Rank rankEnum = (Rank)rank;
                        Suit suitEnum = (Suit)suit;
                        Color colorEnum = (Color)2;
                        Card card = new Card(rankEnum, suitEnum, colorEnum);
                    }
                }
            }
        }
    }
}