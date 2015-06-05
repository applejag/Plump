using UnityEngine;
using System.Collections;

public class CardController : MonoBehaviour {

	public Card card;
	public float hoverSpeed = 1f;

	// Updated in GameController
	[HideInInspector] public bool hover;

	void Update() {
		Vector3 size = Vector3.one * (hover ? 2.2f : 2f);
		transform.localScale = Vector3.Lerp (transform.localScale, size, Time.deltaTime * hoverSpeed);
	}

}
