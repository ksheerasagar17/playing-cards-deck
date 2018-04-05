using playing_cards_deck.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Runtime.Caching;
using System.Linq;

namespace playing_cards_deck.Controllers
{
    public class CardsController : ApiController
    {

        public CardsController()
        {

        }

        [HttpGet]
        [Route("Cards/Create")]
        public Deck CreateDeck()
        {
            var cards = new List<Card>();
            var cntr = 0;
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Number number in Enum.GetValues(typeof(Number)))
                {
                    cards.Add(new Card()
                    {
                        Id = ++cntr,
                        Color = (suit == Suit.Diamond || suit == Suit.Heart) ? Color.Red : Color.Black,
                        Suit = suit,
                        Number = number,
                    });

                }
            }
            var deck = new Deck()
            {
                Id = Guid.NewGuid(),
                Cards = cards,
                CreatedAt = DateTime.UtcNow
            };
            ObjectCache cache = MemoryCache.Default;
            cache.Add(new CacheItem(deck.Id.ToString(), deck), new CacheItemPolicy() { AbsoluteExpiration = DateTimeOffset.MaxValue });
            return deck;
        }

        [HttpGet]
        [Route("Cards/Shuffle/{deckId:Guid}")]
        public Deck ShuffleDeck(Guid deckId)
        {
            ObjectCache cache = MemoryCache.Default;
            var cacheItem = cache.GetCacheItem(deckId.ToString());
            if (cacheItem == null)
            {
                return null;
            }
            var deck = (Deck)cacheItem.Value;
            var shuffledCards = new List<Card>();
            var random = new Random();
            while (shuffledCards.Count < deck.Cards.Count)
            {
                var randomIndex = random.Next(52);
                var shuffledCard = deck.Cards[randomIndex];
                if (!shuffledCards.Contains(shuffledCard))
                {
                    shuffledCards.Add(shuffledCard);
                }
            }
            deck.Cards = shuffledCards;
            deck.UpdatedAt = DateTime.UtcNow;
            cache.Add(new CacheItem(deck.Id.ToString(), deck), new CacheItemPolicy() { AbsoluteExpiration = DateTimeOffset.MaxValue });
            return deck;
        }

        [HttpGet]
        [Route("Cards/Pop/{deckId:Guid}")]
        public Card PopDeck(Guid deckId)
        {
            ObjectCache cache = MemoryCache.Default;
            var cacheItem = cache.GetCacheItem(deckId.ToString());
            if (cacheItem == null || cacheItem.Value == null)
            {
                return null;
            }
            var deck = (Deck)cacheItem.Value;
            if (deck.Cards.Count == 0)
            {
                return null;
            }
            deck.Cards[0].Poped = true;
            deck.UpdatedAt = DateTime.UtcNow;
            cache.Add(new CacheItem(deck.Id.ToString(), deck), new CacheItemPolicy() { AbsoluteExpiration = DateTimeOffset.MaxValue });
            return deck.Cards[0];
        }
    }
}
