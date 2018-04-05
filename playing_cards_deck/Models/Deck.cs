using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace playing_cards_deck.Models
{
    public class Deck
    {
        public Guid Id { get; set; }

        public List<Card> Cards { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }
}