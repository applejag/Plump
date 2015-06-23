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
		Raycast ();
	}
		
	void Raycast() {
		// Raycast
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

		UpdateCards (hit.collider != null ? hit.collider.GetComponent<CardController> () : null);
	}

	void UpdateCards(CardController card) {
		if (card != null) {
			card.mouse.hover = true;
			
			if (Input.GetMouseButtonDown(0)) card.MouseDown();
			if (Input.GetMouseButtonUp(0)) card.MouseUp();
		}
		
		// Reset all other cards
		foreach (CardController obj in FindObjectsOfType<CardController> ()) {
			if (obj != card) {
				if (obj.mouse.down && !Input.GetMouseButton(0)) obj.MouseUp();
				
				obj.mouse.hover = obj.mouse.down;
			}
		}
	}

}
