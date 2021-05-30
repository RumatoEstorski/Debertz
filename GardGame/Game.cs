using Cards;
using GardGame;
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
        int countMove;
        public CardSet Table { get; }
        //public CardSet DiscardPile { get; }
        //public CardSet DiscardPileOfSecondPlayer { get; }
        //public CardSet DiscardPileOfThirdPlayer { get; }
        public List<Player> Players { get; }
        public CardSet Deck { get; }

        private CardSuite? Trump;
        private Player activePlayer;
        private IComparer<Card> comparer;

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
        public Action<Player> GetGameWinner;
        //public Func<string> Reqest;
        public Func<string, CardSuite?, bool> YesOrNo;
        public Func<string, bool, CardSuite?> TrumpRequest;
        public Player MainPlayer;

        public Game(Action<string> showMessage,
            Action<Player> markActivePlayer,
            Action<CardSet> showCards,
            Action<Player> getGameWinner,
            Func<string, CardSuite?, bool> yesOrNo,
            Func<string, bool, CardSuite?> trumpRequest,
            params Player[] players)
        {
            ShowMessage = showMessage;
            MarkActivePlayer = markActivePlayer;
            ShowCards = showCards;
            GetGameWinner = getGameWinner;
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
            countMove = 0;
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
                if (YesOrNo($"Do you play in {Trump}?", Trump))
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
                    else if (Trump != card.Suite && ActivePlayer.HandCards.Cards.FirstOrDefault(c => c.Suite == Trump) != null)
                    {
                        ShowMessage($"У тебя есть {Trump}, ты должен класть ее");
                        return;
                    }
                }
            }
            else
            {
                comparer = new DebertzRoundCompairer((CardSuite)Trump, card.Suite);
            }

            Table.Add(ActivePlayer.HandCards.Pull(card));
            player.PleyerCard = card;

            if (Table.Count == Players.Count)
            {
                GetInDiscardPile();
                GetGameWinner(CheckEnd());
            }
            else
            {
                ActivePlayer = NextPlayer(ActivePlayer);
            }
            Refresh();

        }

        private Player CheckEnd()
        {
            countMove++;
            if (countMove == 9)
            {
                return GetWinner();
            }
            else return null;
            
        }

        private void GetInDiscardPile()
        {
            Table.Cards.Sort(comparer);
            Card maxCard = Table.LastCard;
            Player winner = Players.First(p => p.PleyerCard == maxCard);
            winner.DiscardPile.Add(Table);
            Table.Clean();
            ActivePlayer = winner;
        }

        private Player GetWinnerOfRound(CardSet table)
        {
            int trumpCounter = 0;
            for (int i = 0; i < table.Count; i++)
            {
                if (table[i].Suite == Trump)
                {
                    trumpCounter++;
                }
                
            }
            if (trumpCounter == 1)
            {
                foreach (var player in Players)
                {
                    if (player.PleyerCard.Suite == Trump) return player;
                }
            }

            if (trumpCounter > 1)
            {
                int usualTrampCounter = 0;
                foreach (var player in Players)
                {
                    if (player.PleyerCard.Figure != CardFigure.nine &&
                        player.PleyerCard.Figure != CardFigure.Jack &&
                        player.PleyerCard.Figure != CardFigure.ten &&
                        player.PleyerCard.Figure != CardFigure.Ace &&
                        player.PleyerCard.Suite == Trump)
                    {
                        usualTrampCounter++;
                    }
                }
                if (trumpCounter == usualTrampCounter)
                {
                    
                    for (int x = 0; x < Players.Count; x++)
                    {
                        int count = 0;
                        for (int y = 0; y < Players.Count; y++)
                        {
                            if (Players[x].PleyerCard.Suite == Trump && Players[y].PleyerCard.Suite == Trump)
                            {
                                if (Players[x].PleyerCard.Figure < Players[y].PleyerCard.Figure) break;
                                else count++;
                            }
                        }
                        if (count == Players.Count) return Players[x];
                    }
                    
                    //if (Players[0].PleyerCard.Suite == Trump && Players[1].PleyerCard.Suite == Trump)
                    //{
                    //    return Players[0].PleyerCard.Figure > Players[1].PleyerCard.Figure ? Players[0] : Players[1];
                    //}
                    //else if (Players[1].PleyerCard.Suite == Trump && Players[2].PleyerCard.Suite == Trump)
                    //{
                    //    return Players[1].PleyerCard.Figure > Players[2].PleyerCard.Figure ? Players[1] : Players[2];
                    //}
                    //else if (Players[0].PleyerCard.Suite == Trump && Players[2].PleyerCard.Suite == Trump)
                    //{
                    //    return Players[0].PleyerCard.Figure > Players[2].PleyerCard.Figure ? Players[0] : Players[2];
                    //}
                }
                else if (trumpCounter != usualTrampCounter)
                {
                    int jackCounter = 0;
                    int trampTensCounter = 0;
                    int trampAseCounter = 0;
                    int ninesCounter = 0;
                    for (int i = 0; i < table.Count; i++)
                    {
                        if (table[i].Suite==Trump)
                        {
                            if (table[i].Figure == CardFigure.ten) trampTensCounter++;
                            if (table[i].Figure == CardFigure.Ace) trampAseCounter++;
                            if (table[i].Figure == CardFigure.nine) ninesCounter++;
                            if (table[i].Figure == CardFigure.Jack) jackCounter++;
                        }
                    }
                    foreach (var player in Players)
                    {
                        if (player.PleyerCard.Suite == Trump)
                        {

                            if (player.PleyerCard.Figure == CardFigure.Jack)
                            {
                                return player;
                            }
                            else if (player.PleyerCard.Figure == CardFigure.nine && jackCounter == 0)
                            {
                                return player;
                            }
                            else if(player.PleyerCard.Figure ==CardFigure.Ace&&
                                ninesCounter == 0 && jackCounter == 0)
                            {
                                return player;
                            }
                            else if (player.PleyerCard.Figure == CardFigure.ten &&
                                trampAseCounter == 0&&
                                ninesCounter==0&&
                                jackCounter==0)
                            {
                                return player;
                            }
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
            int tensCounter = 0;
            int aseCounter = 0;
            for (int i = 0; i < table.Count; i++)
            {
                if (table[i].Figure == CardFigure.ten) tensCounter++;
                if (table[i].Figure == CardFigure.Ace) aseCounter++;
            }
            foreach (var player in Players)
            {
                if (player.PleyerCard.Figure == CardFigure.Ace)
                {
                    return player;
                }
                else if (player.PleyerCard.Figure == CardFigure.ten &&
                    aseCounter == 0)
                {
                    return player;
                }
            }
            int countFailCards = 0;
            for (int i = 0; i < table.Count; i++)
            {
                if (table[0].Suite != table[i].Suite)
                    countFailCards++;

            }
            if (countFailCards == Players.Count - 1) return Players[0];
            else if (countFailCards == 1) 
            {
                for (int i = 0; i < table.Count; i++)
                {
                    if (table[0].Suite != table[i].Suite)
                        countFailCards++;
                    if (countFailCards == 1)
                        table[i].Figure = 0;
                }
            }
            if (tensCounter == 0 && aseCounter == 0)
            {
                for (int x = 0; x < Players.Count; x++)
                {
                    int count = 0;
                    for (int y = 0; y < Players.Count; y++)
                    {
                        if (Players[x].PleyerCard.Figure < Players[y].PleyerCard.Figure) break;
                        else count++;
                    }
                    if (count == Players.Count) return Players[x];
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
        public Player GetWinner()
        {
            Player winner = Players[0];


            foreach (var player in Players)
            {
                if (CardsPoints(player.DiscardPile) > CardsPoints(winner.DiscardPile))
                    winner = player;
            }
            
            return winner;
        }

        private int CardsPoints(CardSet cardSet)
        {
            int points = 0;
            foreach (var item in cardSet.Cards)
            {
                points += Value(item);
            }
            return points;
        }

        private int Value(Card card)
        {
            if(card.Suite == Trump && card.Figure == CardFigure.Jack)
            {
                return 20;
            }
            if (card.Suite == Trump && card.Figure == CardFigure.nine)
            {
                return 14;
            }
            if (card.Figure == CardFigure.Ace) return 11;
            if ((int)card.Figure >= 11) return (int)card.Figure - 9;
            else return 0;
        }

        public void HangUp()
        {
            Table.Cards.Clear();
            Refresh();
        }
    }
}
