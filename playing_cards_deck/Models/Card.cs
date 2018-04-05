using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace playing_cards_deck.Models
{
    public class Card
    {
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Color Color { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Suit Suit { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Number Number { get; set; }

        public bool Poped { get; set; }
    }

    public enum Suit
    {
        Club,
        Diamond,
        Heart,
        Spade
    }

    public enum Number
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13
    }

    public enum Color
    {
        Red,
        Black
    }
}