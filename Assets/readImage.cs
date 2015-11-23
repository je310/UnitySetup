using UnityEngine;
using System.Collections;

public class readImage : MonoBehaviour {
	public Renderer render;
	public Material mat;
	private static Texture2D tex;
	private	static byte[] fileData;
	// Use this for initialization
	void Start () {
		render = GetComponent<Renderer>();
		tex = new Texture2D(2, 2);
	}
	
	// Update is called once per frame
	void Update () {
		
		string filepath = "inputFile.png";
		//if (System.IO.File.Exists(filepath))     {
			fileData = System.IO.File.ReadAllBytes(filepath);
			System.IO.File.Delete(filepath);
			tex.LoadImage(fileData);
			render.material.mainTexture = tex;
			mat.mainTexture = tex;
		//}
		
	}
}
