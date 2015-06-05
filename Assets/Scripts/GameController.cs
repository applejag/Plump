using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	[HideInInspector] public Deck deck;

	private BaseCards baseCards;

	void Start () {
		baseCards = GetComponent<BaseCards> ();
		deck = new Deck (baseCards.collection);

		deck.Shuffle ();
	}

	void Update() {
		UpdateCards ();
	}
		
	void UpdateCards() {
		// Raycast
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

		// Reset cards
		List<CardController> cardObjects = new List<CardController> (FindObjectsOfType<CardController> ());
		cardObjects.ForEach (delegate(CardController obj) {
			obj.hover = false;
		});

		// Collided with something
		if (hit.collider != null) {
			CardController card = hit.collider.GetComponent<CardController> ();
			// Got a CardController
			if (card != null) {
				card.hover = true;
			}
		}
	}

}
