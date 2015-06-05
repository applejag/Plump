using UnityEngine;
using System.Collections;

public class CardController : MonoBehaviour {

	public Card card;
	public float moveSpeed;
	public float rotateSpeed;
	public float hoverSpeed;

	// Updated in GameController
	public MouseState mouse;

	[HideInInspector] public Quaternion startRotation;
	[HideInInspector] public Vector3 startPosition;

	void Start() {
		startRotation = transform.localRotation;
		startPosition = transform.position;
	}

	public void InHand() {
		Vector3 size = Vector3.one * (mouse.hover ? 2.2f : 2f);
		transform.localScale = Vector3.Lerp (transform.localScale, size, Time.deltaTime * hoverSpeed);
		
		if (mouse.down) {
			MoveTowards(Camera.main.ScreenToWorldPoint (Input.mousePosition), Quaternion.identity, mouse.offset);
		} else {
			// Rotate
			transform.localRotation = Quaternion.Lerp (transform.localRotation, startRotation, Time.deltaTime * rotateSpeed);
			
			// Move
			transform.localPosition = Vector3.Lerp (transform.localPosition, Vector3.forward * startPosition.z, Time.deltaTime * moveSpeed);
		}
	}

	public void MoveTowards(Vector3 position, Quaternion rotation, Vector3 localOffset) {
		// Rotate
		transform.localRotation = Quaternion.Lerp (transform.localRotation, Quaternion.identity, Time.deltaTime * rotateSpeed);
		
		// Move
		Vector3 offset = transform.TransformVector (localOffset);
		
		transform.position = Vector3.Lerp (transform.position, position - offset, Time.deltaTime * moveSpeed);
		
		// Puts it in front of the other cards
		transform.position = new Vector3 (transform.position.x, transform.position.y, -1f); 
	}

	public void MouseDown() {
		mouse.down = true;
		// mouse offset in object space, compared to world space
		mouse.offset = transform.InverseTransformVector (Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position);
	}

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
