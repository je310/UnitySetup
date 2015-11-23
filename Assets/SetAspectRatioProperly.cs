using UnityEngine;
using System.Collections;

public class SetAspectRatioProperly : MonoBehaviour {
	private Projector proj;
	private float initialAspect;
	public GameObject FirstPersonCharcter;
	private Camera camera;
	// Use this for initialization
	void Start () {
		proj = GetComponent<Projector>();
		//initialAspect = Screen.width/Screen.height;
		//proj.aspectRatio = initialAspect;
		camera = FirstPersonCharcter.GetComponent<Camera>();
		initialAspect = camera.aspect;
		proj.aspectRatio = initialAspect;
	}
	
	// Update is called once per frame
	void Update () {
		if(initialAspect != camera.aspect){
			initialAspect = camera.aspect;
			proj.aspectRatio = initialAspect;
		}
	}
}
