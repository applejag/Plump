using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public Hand hand;

	private BaseCards baseCards;

	void Awake() {
		baseCards = FindObjectOfType<BaseCards> ();
	}

	void Start() {
		hand = Hand.RandomHand ();
		hand.Instantiate (baseCards.cardPrefab);
	}

	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			hand.Instantiate(baseCards.cardPrefab);
		}
	}

}
