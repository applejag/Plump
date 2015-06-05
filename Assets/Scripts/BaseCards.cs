using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum CardColor {
	heart, spade, diamond, clover, unknown
}

#region Structs (Suit, Deck)
public struct Suit {
	public List<Card> cards;
	public CardColor color;

	#region Constructor
	public Suit(List<Card> cards, CardColor color) {
		this.cards = cards;
		this.color = color;
	}
	#endregion

	#region Static methods
	public static Suit CreateFromColor(List<Card> cards, CardColor color) {
		List<Card> suitedCards = new List<Card> (13);

		cards.ForEach ((Card card) => {
			if (card.SameColor (color))
				suitedCards.Add (card);
		});

		suitedCards.Sort ((a, b) => a.num.CompareTo(b.num));
		return new Suit(suitedCards, color);
	}
	#endregion
}

public struct Collection {
	public List<Suit> suits;
	public Sprite cardback;

	#region Constructor
	public Collection(List<Suit> suits, Sprite cardback) {
		this.suits = suits;
		this.cardback = cardback;
	}

	// Group cards into suits
	public Collection(List<Card> cards, Sprite cardback) {
		List<Suit> suits = new List<Suit> (4);

		foreach (CardColor color in Enum.GetValues(typeof(CardColor))) {
			if (color == CardColor.unknown) continue;
			Suit suit = Suit.CreateFromColor (cards, color);
			suits.Add (suit);
		}

		this = new Collection (suits, cardback);
	}

	// Split up sprites
	public Collection(List<Sprite> cardSprites, Sprite cardback) {
		List<Card> cards = new List<Card> (52);

		for (int num = 0; num < 52; num++) {
			CardColor color = CardColor.clover; // 0-12
			if (num >= 13) color = CardColor.diamond; // 13-25
			if (num >= 26) color = CardColor.heart; // 26-38
			if (num >= 39) color = CardColor.spade; // 39-51
			
			Card card = new Card(cardSprites[num], cardback, color, (num % 13) + 1);
			cards.Add(card);
		}

		this = new Collection (cards, cardback);
	}
	#endregion

	#region Methods
	public List<Card> GetCards() {
		List<Card> cards = new List<Card> ();

		suits.ForEach (delegate(Suit suit) {
			suit.cards.ForEach(delegate(Card card) {
				cards.Add(card);
			});
		});

		return cards;
	}
	#endregion
}

public struct Deck {
	public List<Card> cards;
	public Collection collection;

	#region Contructor
	public Deck(Collection collection) {
		this.cards = collection.GetCards ();
		this.collection = collection;
	}
	#endregion

	#region Methods
	public Card TakeCard(CardColor color, int num) {
		Card card = cards.Find (delegate(Card obj) {
			return obj.color == color && obj.num == num;
		});

		cards.Remove (card);

		return card;
	}
	public Card TakeCard(Card card) {
		if (cards.Contains(card))
			cards.Remove (card);
		
		return card;
	}

	public Card TakeRandomCard() {
		return cards.Count > 0 ? TakeCard (cards [UnityEngine.Random.Range (0, cards.Count)]) : null;
	}
	public Card TakeFirstCard() {
		return cards.Count > 0 ? TakeCard (cards [0]) : null;
	}

	public bool CardsLeft() {
		return cards.Count > 0;
	}

	public List<Card> GetCards(Suit suit) {
		return GetCards (suit.color);
	}
	public List<Card> GetCards(CardColor color) {
		return cards.FindAll (delegate(Card obj) {
			return obj.color == color;
		});
	}
	public List<Card> GetCards(int num) {
		return cards.FindAll (delegate(Card obj) {
			return obj.num == num;
		});
	}

	public void Shuffle() {
		// Fisher Yates Shuffle, source: http://answers.unity3d.com/questions/16531/randomizing-arrays.html
		for (int i = cards.Count-1; i > 0; i--) {
			Swap(UnityEngine.Random.Range(0,i), i);
		}
	}

	public void Swap(Card a, Card b) {
		Swap (cards.IndexOf (a), cards.IndexOf (b));
	}
	public void Swap(int indexA, int indexB) {
		Card tmp = cards [indexA];
		cards [indexA] = cards [indexB];
		cards [indexB] = tmp;
	}
	#endregion
}
#endregion

public class BaseCards : MonoBehaviour {

	public List<Sprite> cardSprites = new List<Sprite> ();
	public Sprite cardback;
	public GameObject cardPrefab;

	[HideInInspector] public Collection collection;

	void Awake () {
		collection = new Collection (cardSprites, cardback);
	}

}