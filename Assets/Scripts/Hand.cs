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
				GameObject.Destroy(card.assignedObject.gameObject);
				card.assignedObject = null;
			}
		}
	}

	public void InstantiateCards(GameObject prefab) {
		
		for (int num = 0; num < cards.Count; num++) {
			Quaternion rotation = GetRotation(num);
			Vector3 position = Vector3.forward * GetDepth(num);

			cards[num].InstantiateCard(prefab, position, rotation).transform.SetParent(player.transform, false);
			cards[num].assignedObject.hand = this;
		}

		SetVisable (visable);
	}

	public Quaternion GetRotation(CardController card) {
		return GetRotation (cards.Find (delegate(Card obj) {
			return obj.assignedObject == card;
		}));
	}
	public Quaternion GetRotation(Card card) {
		if (!cards.Contains (card))
			return Quaternion.identity;

		return GetRotation (cards.IndexOf (card));
	}
	public Quaternion GetRotation(int index) {
		float minRot = Mathf.Min(cards.Count,6) * -15;
		float maxRot = Mathf.Min(cards.Count,6) * 15;

		return Quaternion.Euler (0, 0, Mathf.Lerp (minRot, maxRot, (float)index / (cards.Count - 1)));
	}

	public float GetDepth(CardController card) {
		return GetDepth (cards.Find (delegate(Card obj) {
			return obj.assignedObject == card;
		}));
	}
	public float GetDepth(Card card) {
		if (!cards.Contains (card))
			return 0f;
		
		return GetDepth (cards.IndexOf (card));
	}
	public float GetDepth(int index) {
		return (float)index;
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