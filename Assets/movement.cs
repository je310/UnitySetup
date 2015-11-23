using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
	private Transform trans;
	public float speed=0.1f;
	void Start ()
    {
        trans = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical);
		movement = movement*speed;
		trans.position = new Vector3(trans.position.x, trans.position.y,trans.position.z + movement.z);
		trans.rotation = Quaternion.Euler(trans.rotation.x , trans.rotation.y + moveHorizontal*100, trans.rotation.z);
	}
}
