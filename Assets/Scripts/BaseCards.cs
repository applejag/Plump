using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum CardColor {
	heart, spade, diamond, clover, unknown
}

#region Structs (Suit, Deck)
public struct Card {
	public Sprite sprite;
	public CardColor color;
	public int num;
	public GameObject assignedObject;

	#region Constructor
	public Card(Sprite sprite, CardColor color, int num) {
		this = new Card (sprite, color, num, null);
	}

	public Card(Sprite sprite, CardColor color, int num, GameObject gameobject) {
		this.sprite = sprite;
		this.color = color;
		this.num = num;
		this.assignedObject = gameobject;
	}
	#endregion

	#region Methods
	public bool SameColor(CardColor color) {
		return this.color == color;
	}
	public static bool SameColor(Card card, CardColor color) {
		return card.SameColor (color);
	}
	#endregion
}

public struct Suit {
	public List<Card> cards;
	public CardColor color;

	#region Constructor
	public Suit(List<Card> cards, CardColor color) {
		this.cards = cards;
		this.color = color;
	}
	#endregion

	#region Methods
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

public struct Deck {
	public List<Suit> suits;
	public Sprite cardback;

	#region Constructor
	public Deck(List<Suit> suits, Sprite cardback) {
		this.suits = suits;
		this.cardback = cardback;
	}

	// Group cards into suits
	public Deck(List<Card> cards, Sprite cardback) {
		List<Suit> suits = new List<Suit> (4);

		foreach (CardColor color in Enum.GetValues(typeof(CardColor))) {
			if (color == CardColor.unknown) continue;
			Suit suit = Suit.CreateFromColor (cards, color);
			suits.Add (suit);
		}

		this = new Deck (suits, cardback);
	}

	// Split up sprites
	public Deck(List<Sprite> cardSprites, Sprite cardback) {
		List<Card> cards = new List<Card> (52);

		for (int num = 0; num < 52; num++) {
			CardColor color = CardColor.clover; // 0-12
			if (num >= 13) color = CardColor.diamond; // 13-25
			if (num >= 26) color = CardColor.heart; // 26-38
			if (num >= 39) color = CardColor.spade; // 39-51
			
			Card card = new Card(cardSprites[num], color, (num % 13) + 1);
			cards.Add(card);
		}

		this = new Deck (cards, cardback);
	}
	#endregion
}
#endregion

public class BaseCards : MonoBehaviour {

	public List<Sprite> cardSprites = new List<Sprite> ();
	public Sprite cardback;
	[HideInInspector]
	public Deck deck;

	public GameObject cardPrefab;
	private Coroutine spawnerCoroutine;

	void Awake () {
		deck = new Deck (cardSprites, cardback);
	}

	/*
	void Start() {
		spawnerCoroutine = StartCoroutine (SpawnCards ());
	}

	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			spawnerCoroutine = StartCoroutine (SpawnCards ());
		}
	}

	IEnumerator SpawnCards() {
		if (spawnerCoroutine != null) {
			foreach(SpriteRenderer ren in FindObjectsOfType<SpriteRenderer>()) {
				Destroy(ren.gameObject);
			}
			StopCoroutine(spawnerCoroutine);
		}

		for (int suit = 0; suit < 4; suit++) {
			foreach (Card card in deck.suits[suit].cards) {
				yield return new WaitForFixedUpdate();

				Vector3 pos = new Vector3(card.num-1+suit*.5f,suit,card.num-1+suit*2);
				GameObject clone = Instantiate(cardPrefab, pos, Quaternion.identity) as GameObject;
				clone.GetComponent<SpriteRenderer>().sprite = card.sprite;
				clone.name = card.color.ToString() + " " + card.num;
			}
		}
	}
	*/

}
