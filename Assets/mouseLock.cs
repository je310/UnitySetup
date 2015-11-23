using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;


public class mouseLock : MonoBehaviour {
	public GameObject FPSController;
    void OnMouseDown() {
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    }
	void Start(){
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

    void Update() {
        if (Input.GetKeyDown("escape")){
            Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			
		}
		if (Input.GetKeyDown("f")){
            Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
    }
}