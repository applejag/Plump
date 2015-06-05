using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Hand {
	public List<Card> cards;
	private bool visable; // Default: true
	public Player player;
	
	#region Constructor
	public Hand (List<Card> cards, Player player, bool visable = true) {
		this.cards = cards;
		this.visable = visable;
		this.player = player;
	}

	public Hand(Player player, Deck deck, int cards = 5, bool visable = true) {
		this = new Hand (player, visable);

		for (int count = 0; count < cards && deck.CardsLeft(); count++) {
			this.cards.Add(deck.TakeFirstCard());
		}
	}
	
	public Hand(Player player, bool visable = true) {
		this = new Hand (new List<Card> (), player, visable);
	}
	#endregion

	#region Methods
	public void ClearObjects() {
		foreach (Card card in cards) {
			if (card.assignedObject != null) {
				GameObject.Destroy(card.assignedObject);
				card.assignedObject = null;
			}
		}
	}

	public void InstantiateCards(GameObject prefab) {
		float minRot = Mathf.Min(cards.Count,6) * -15;
		float maxRot = Mathf.Min(cards.Count,6) * 15;
		
		for (int num = 0; num < cards.Count; num++) {
			Quaternion rotation = Quaternion.Euler(0,0,Mathf.Lerp(minRot,maxRot,(float)num/(cards.Count-1)));

			cards[num].InstantiateCard(prefab, Vector3.zero, rotation).transform.SetParent(player.transform,false);
		}

		SetVisable (visable);
	}

	public void Swap(Card a, Card b) {
		Swap (cards.IndexOf (a), cards.IndexOf (b));
	}
	public void Swap(int indexA, int indexB) {
		Card tmp = cards [indexA];
		cards [indexA] = cards [indexB];
		cards [indexB] = tmp;
	}

	public void SetVisable(bool visable) {
		this.visable = visable;

		cards.ForEach (delegate(Card obj) {
			obj.SetVisable(visable);
		});
	}
	#endregion
}