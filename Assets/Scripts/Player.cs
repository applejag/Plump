using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	
	public int handSize = 5;
	public Hand hand;
	
	private GameController game;
	private BaseCards baseCards;

	void Awake() {
		game = FindObjectOfType<GameController> ();
		baseCards = FindObjectOfType<BaseCards> ();
	}
	
	void Start() {
		hand = new Hand (this, game.deck, handSize);
		hand.InstantiateCards (baseCards.cardPrefab);

	}

	void Update() {
		hand.cards.ForEach (delegate(Card obj) {
			obj.assignedObject.InHand();
		});
	}

}
