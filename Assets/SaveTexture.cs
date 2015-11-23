using UnityEngine;
using System.Collections;

public class SaveTexture : MonoBehaviour {
	public Material thisMat;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
				Texture2D savedTexture = thisMat.mainTexture as Texture2D;
		Texture2D newTexture = new Texture2D(savedTexture.width, savedTexture.height, TextureFormat.ARGB32, false);
         
		//newTexture.SetPixels( 0,0, savedTexture.width, savedTexture.height, savedTexture.ReadPixels( UnityEngine.Rect(0, 0, 4096, 4096),0,0,false) );
		//newTexture = FillInClear(newTexture);
		//newTexture.Apply();
		System.IO.File.WriteAllBytes("EasyToFind2.png", newTexture.EncodeToPNG());
	}
}
