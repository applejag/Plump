using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Hand {
	public List<Card> cards;
	public bool visable; // Default: true
	public Player player;
	
	public List<GameObject> cardObjects;
	
	#region Constructor
	public Hand (List<Card> cards, bool visable, Player player) {
		this.cards = cards;
		this.visable = visable;
		this.player = player;
		this.cardObjects = new List<GameObject>();
	}
	
	public Hand (List<Card> cards, Player player) {
		this = new Hand (cards, true, player);
	}
	#endregion
	
	#region Methods
	public static Hand RandomHand(Deck deck, Player player, uint size = 5) {
		Hand hand = new Hand(cards, player);
		hand.Randomize (deck, size);
	}
	public void Randomize(Deck deck) {
		this.Randomize (deck, cards.Count);
	}
	public void Randomize(Deck deck, int size) {
		List<Card> cards = new List<Card> (size);
		
		for (int count = 0; count < size; count++) {
			Suit suit = deck.suits [Random.Range (0, 4)];
			Card card = suit.cards [Random.Range (0, 13)];
			cards.Add (card);
		}
		
		this.cards = cards;
	}
	public void ClearObjects() {
		foreach (GameObject card in cardObjects) {
			GameObject.Destroy(card);
		}
	}
	public void Instantiate(GameObject prefab) {
		this.ClearObjects ();
		float minRot = -45;
		float maxRot = 45;
		
		for (int num = 0; num < cards.Count; num++) {
			
			Quaternion rotation = Quaternion.Euler(0,0,Mathf.LerpAngle(minRot,maxRot,(float)num/cards.Count));
			GameObject clone = GameObject.Instantiate(prefab, player.transform.position,rotation) as GameObject;
			
			Card card = cards[num];
			
			clone.GetComponent<SpriteRenderer>().sprite = card.sprite;
			clone.transform.parent = player.transform;
			this.cardObjects.Add(clone);
			
			card.assignedObject = clone;
			cards[num] = card;
		}
	}
	#endregion
}