using UnityEngine;
using System.Collections;

public class Card {
	public Sprite sprite;
	public Sprite cardback;
	public CardColor color;
	public int num;
	public GameObject assignedObject; // Card prefab
	
	#region Constructor
	public Card(Sprite sprite, Sprite cardback, CardColor color, int num, GameObject gameobject = null) {
		this.sprite = sprite;
		this.cardback = cardback;
		this.color = color;
		this.num = num;
		this.assignedObject = gameobject;
	}
	#endregion
	
	#region Static methods
	public static bool SameColor(Card card, CardColor color) {
		return card.SameColor (color);
	}
	#endregion
	
	#region Methods
	public bool SameColor(CardColor color) {
		return this.color == color;
	}
	
	public GameObject InstantiateCard(GameObject prefab, Vector3 position, Quaternion rotation) {
		GameObject clone = GameObject.Instantiate(prefab, position, rotation) as GameObject;
		clone.GetComponent<SpriteRenderer>().sprite = sprite;
		
		assignedObject = clone;
		clone.GetComponent<CardController> ().card = this;
		
		return clone;
	}

	public void SetVisable(bool visable) {
		if (assignedObject != null) {
			assignedObject.GetComponent<SpriteRenderer>().sprite = visable ? sprite : cardback;
		}
	}
	#endregion
}