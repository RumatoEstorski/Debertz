using Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    public class Game
    {
        readonly Random random = new Random();
        bool pass;
        public CardSet Table { get; }
        //public CardSet DiscardPile { get; }
        //public CardSet DiscardPileOfSecondPlayer { get; }
        //public CardSet DiscardPileOfThirdPlayer { get; }
        public List<Player> Players { get; }
        public CardSet Deck { get; }

        private CardSuite? Trump;
        private Player activePlayer;

        public Player ActivePlayer
        {
            get
            {
                return activePlayer;
            }
            set
            {
                activePlayer = value;
                foreach (Player player in Players)
                {
                    if (player == activePlayer)
                        player.HandCards.Show();
                    else
                        player.HandCards.Hide();
                }
                MarkActivePlayer(activePlayer);
            }
        }

        public Action<string> ShowMessage;
        public Action<Player> MarkActivePlayer;
        public Action<CardSet> ShowCards;
        //public Func<string> Reqest;
        public Func<string, bool> YesOrNo;
        public Func<string, bool, CardSuite?> TrumpRequest;
        public Player MainPlayer;

        public Game(Action<string> showMessage,
            Action<Player> markActivePlayer,
            Action<CardSet> showCards,
            Func<string, bool> yesOrNo,
            Func<string, bool, CardSuite?> trumpRequest,
            params Player[] players)
        {
            ShowMessage = showMessage;
            MarkActivePlayer = markActivePlayer;
            ShowCards = showCards;
            YesOrNo = yesOrNo;
            TrumpRequest = trumpRequest;
            Table = GetCardSet();
            //DiscardPile = GetCardSet();
            //DiscardPileOfSecondPlayer = GetCardSet();
            //DiscardPileOfThirdPlayer = GetCardSet();
            Players = new List<Player>(players);
            Deck = GetCardSet();
            Deck.Full(32);

        }


        public void Start()
        {
            Deck.Mix();
            Player lastPlayer = Players[0];
            Deal(6);
            Deck.LastCard.Show();
            Refresh();
            MainPlayer = TrumpDefinition(lastPlayer);
            Deal(3);
            ActivePlayer = MainPlayer;
            Refresh();
        }

        public Player TrumpDefinition(Player lastPlayer)
        {
            Trump = Deck.LastCard.Suite;//Reqest().Length;
            ActivePlayer = lastPlayer;
            do
            {
                ActivePlayer = NextPlayer(ActivePlayer);
                if (YesOrNo($"Do you play in {Trump}?"))
                {
                    return ActivePlayer;
                }
                
            } while (ActivePlayer != lastPlayer);


            do
            {
                ActivePlayer = NextPlayer(ActivePlayer);
                Trump = TrumpRequest("What card suit do you want?", ActivePlayer != lastPlayer);
                if (Trump != null) return ActivePlayer;
            } while (ActivePlayer != lastPlayer);
            return lastPlayer;
        }

        public virtual CardSet GetCardSet()
        {
            return new CardSet();
        }

        public void Move(Card card, Player player)
        {

            if (!ActivePlayer.HandCards.Cards.Contains(card)) return;
            //if()
            if (Table.Count != 0)
            {
                if (Table.Cards[0].Suite != card.Suite)
                {
                    if (ActivePlayer.HandCards.Cards.FirstOrDefault(c => c.Suite == Table.Cards[0].Suite) != null)
                    {
                        ShowMessage($"У тебя есть {Table.Cards[0].Suite}, ты должен класть ее");
                        //дописать другие причины
                        return;
                    }


                    if (Trump != card.Suite && ActivePlayer.HandCards.Cards.FirstOrDefault(c => c.Suite == Trump) != null) return;
                }
            }

            Table.Add(ActivePlayer.HandCards.Pull(card));
            player.PleyerCard = card;

            if (Table.Count == Players.Count)
            {
                GetInDiscardPile();
            }
            ActivePlayer = NextPlayer(ActivePlayer);
            Refresh();

        }

        private void GetInDiscardPile()
        {
            for (int i = 0; i < 3; i++)
            {
                if (GetWinnerOfRound(Table) == Players[i])
                {
                    CardSet Adder= Table; 
                    Players[i].DiscardPile.Add(Adder.Deal(Table.Count));
                    Table.GetCardSet();
                }
            }
        }

        private Player GetWinnerOfRound(CardSet table)
        {
            int trumpCounter = 0;
            int tensCounter = 0;
            for (int i = 0; i < table.Count; i++)
            {
                if (table[i].Suite == Trump)
                {
                    trumpCounter++;
                }
                if (table[i].Figure == CardFigure.ten)
                {
                    tensCounter++;
                }
            }
            
                

                if (trumpCounter == 1)
                {
                  for (int i = 0; i < Players.Count; i++) 
                  {
                    if (Players[i].PleyerCard.Suite == Trump) return Players[i];
                  }
                }

            if (trumpCounter > 1)
            {
                int usualTrampCounter = 0;
                for (int i = 0; i < Players.Count; i++)
                {
                    if (Players[i].PleyerCard.Figure != CardFigure.nine &&
                        Players[i].PleyerCard.Figure != CardFigure.Jack &&
                        Players[i].PleyerCard.Figure != CardFigure.ten &&
                        Players[i].PleyerCard.Suite == Trump)
                    {
                        usualTrampCounter++;
                    }
                }
                if (trumpCounter == usualTrampCounter)
                {

                    if (Players[0].PleyerCard.Suite == Trump && Players[1].PleyerCard.Suite == Trump)
                    {
                        return Players[0].PleyerCard.Figure > Players[1].PleyerCard.Figure ? Players[0] : Players[1];
                    }
                    else if (Players[1].PleyerCard.Suite == Trump && Players[2].PleyerCard.Suite == Trump)
                    {
                        return Players[1].PleyerCard.Figure > Players[2].PleyerCard.Figure ? Players[1] : Players[2];
                    }
                    else if (Players[0].PleyerCard.Suite == Trump && Players[2].PleyerCard.Suite == Trump)
                    {
                        return Players[0].PleyerCard.Figure > Players[2].PleyerCard.Figure ? Players[0] : Players[2];
                    }
                }
                else if (trumpCounter != usualTrampCounter)
                {
                    for (int i = 0; i < Players.Count; i++)
                    {
                        if (Players[i].PleyerCard.Figure == CardFigure.Jack &&
                        Players[i].PleyerCard.Suite == Trump)
                        {
                            return Players[i];
                        }
                        else if (Players[i].PleyerCard.Figure == CardFigure.nine &&
                        Players[i].PleyerCard.Suite == Trump)
                        {
                            return Players[i];
                        }
                        else if (Players[i].PleyerCard.Figure == CardFigure.ten &&
                        Players[i].PleyerCard.Suite == Trump)
                        {
                            return Players[i];
                        }
                    }
                }

                //if (Players[i].PleyerCard.Suite == Trump.Suite&& 
                //Players[i+1].PleyerCard.Suite == Trump.Suite|| 
                //Players[i - 1].PleyerCard.Suite == Trump.Suite|| 
                //Players[i + 2].PleyerCard.Suite == Trump.Suite|| 
                //Players[i - 2].PleyerCard.Suite == Trump.Suite)
                //{
                //}
            }
            for (int i = 0; i < table.Count; i++)
            {
                for(int j =0; j<Players.Count;j++)
                {
                    if (table[i].Figure == CardFigure.ten)
                    {
                        if (table[i].Figure == Players[j].PleyerCard.Figure)
                            return Players[j];
                    }
                }
                
            }
            return PlayerWithMaxCard(); 
        }

        private Player PlayerWithMaxCard()
        {
            if (Players[0].PleyerCard.Figure > Players[1].PleyerCard.Figure|| Players[0].PleyerCard.Figure < Players[1].PleyerCard.Figure)
            {
                return Players[0].PleyerCard.Figure > Players[1].PleyerCard.Figure ? Players[0] : Players[1];
            }
            if(Players[1].PleyerCard.Figure > Players[2].PleyerCard.Figure || Players[1].PleyerCard.Figure < Players[2].PleyerCard.Figure)
            {
                return Players[1].PleyerCard.Figure > Players[2].PleyerCard.Figure ? Players[1] : Players[2];
            }
            return Players[0].PleyerCard.Figure > Players[2].PleyerCard.Figure ? Players[0] : Players[2];
            //> Players[2].PleyerCard :Players[2].PleyerCard; ;
        }

        public void Refresh()
        {
            ShowCards(Deck);
            foreach (var player in Players)
            {
                ShowCards(player.HandCards);
            }
            ShowCards(Table);
            
        }

        private Player NextPlayer(Player player)
        {
            if (player == Players[Players.Count - 1]) return Players[0];

            return Players[Players.IndexOf(player) + 1];
        }

        private Player PreviousPlayer(Player player)
        {
            if (player == Players[0]) return Players[Players.Count - 1];

            return Players[Players.IndexOf(player) - 1];
        }

        public void Deal(int numerOfCards)
        {
            foreach (var player in Players)
            {
                player.HandCards.Add(Deck.Deal(numerOfCards));
            }
            Refresh();   
        }
        public void GetWinner()
        {
            foreach (var player in Players)
            {
                int playerScore = 0;
                foreach (var card in player.DiscardPile.Cards)
                { 
                //for(int i =0;i<Players.Count;i++)
                //{
                //    for(int j = 0; j < Players[i].DiscardPile.Count; j++)
                //    {
                        if (player.PleyerCard.Figure == CardFigure.Jack &&
                        player.PleyerCard.Suite == Trump)
                        {
                            playerScore += 20;
                        }
                        else if (player.PleyerCard.Figure == CardFigure.Jack)
                        {
                            playerScore += 2;
                        }
                        if (player.PleyerCard.Figure == CardFigure.nine &&
                        player.PleyerCard.Suite == Trump)
                        {
                            playerScore += 14;
                        }
                        if(player.PleyerCard.Figure == CardFigure.Ace)
                        {
                            playerScore += 11;
                        }
                        if (player.PleyerCard.Figure == CardFigure.ten)
                        {
                            playerScore += 10;
                        }
                        if(player.PleyerCard.Figure == CardFigure.King)
                        {
                            playerScore += 4;
                        }
                        if (player.PleyerCard.Figure == CardFigure.Queen)
                        {
                            playerScore += 3;
                        }
                       //}
                    //}

                }
                    
                if (playerScore>162-playerScore)
                    ShowMessage(player.Name + "Won with a score" + playerScore);
            }
        }

        public void HangUp()
        {
            Table.Cards.Clear();
            Refresh();
        }
    }
}
