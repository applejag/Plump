using UnityEngine;
using System.Collections;

public class CardController : MonoBehaviour {

	public Card card; // set in Card.InstantiateCard
	public Hand hand; // set in Hand.InstantiateCards

	public float moveSpeed;
	public float rotateSpeed;
	public float hoverSpeed;

	// Updated in GameController
	[HideInInspector] public MouseState mouse;

	private Quaternion startRotation;
	private Vector3 startPosition;

	void OnDrawGizmosSelected() {
		if (card != null) {
			// pivot to pivot
			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, Camera.main.ScreenToWorldPoint (Input.mousePosition)-transform.TransformVector (mouse.offset));

			// target to target
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position+transform.TransformVector(mouse.offset)+Vector3.forward, Camera.main.ScreenToWorldPoint (Input.mousePosition)+Vector3.forward);

			// offset start
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine(transform.position,transform.position+transform.TransformVector(mouse.offset)+Vector3.forward);
			// offset end
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine(Camera.main.ScreenToWorldPoint (Input.mousePosition)-transform.TransformVector (mouse.offset),Camera.main.ScreenToWorldPoint (Input.mousePosition)+Vector3.forward);
		}
	}

	public void InHand() {
		Vector3 size = Vector3.one * (mouse.hover ? 2.2f : 2f);
		transform.localScale = Vector3.Lerp (transform.localScale, size, Time.deltaTime * hoverSpeed);
		
		if (mouse.down)
			MoveTowards (Camera.main.ScreenToWorldPoint (Input.mousePosition), Quaternion.identity);
		else
			MoveTowards (transform.parent.position, hand.GetRotation(this), false);
		
	}

	public void MoveTowards(Vector3 position, Quaternion rotation, bool useMouseOffset = true) {
		// Rotate
		transform.localRotation = Quaternion.Lerp (transform.localRotation, rotation, Time.deltaTime * rotateSpeed);
		
		// Move
		Vector3 offset = transform.TransformVector (useMouseOffset ? mouse.offset : Vector3.zero); // from object space to world space
		transform.position = Vector3.Lerp (transform.position, position - offset, Time.deltaTime * moveSpeed);
		
		// Puts it in front of the other cards
		transform.position = new Vector3 (transform.position.x, transform.position.y, mouse.down ? -1 : hand.GetDepth(this)); 
	}

	// EVENT
	public void MouseDown() {
		mouse.down = true;

		Vector3 offset = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		mouse.offset = transform.InverseTransformVector (offset); // from world space to object space
	}

	// EVENT
	public void MouseUp() {
		mouse.down = false;
	}

	[System.Serializable]
	public struct MouseState {
		public bool hover;
		public bool down;
		public Vector3 offset;
	}
}
