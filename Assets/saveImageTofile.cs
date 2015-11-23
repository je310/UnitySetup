using UnityEngine;
using System.Collections;

public class saveImageTofile : MonoBehaviour {
	public bool grab;
    public Renderer display;
    void OnPostRender() {
        if (true) {
            Texture2D tex = new Texture2D(4096, 4096);
            tex.ReadPixels(new Rect(0, 0, 4096, 4096), 0, 0);
            //tex.Apply();
            display.material.mainTexture = tex;
			System.IO.File.WriteAllBytes("EasyToFind.png",tex.EncodeToPNG());
            grab = false;
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
