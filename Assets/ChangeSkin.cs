// Change renderer's texture each changeInterval/
// seconds from the texture array defined in the inspector.

using UnityEngine;
using System.Collections;

public class ChangeSkin : MonoBehaviour {
    public Texture[] textures;
    public float changeInterval = 0.33F;
    public Renderer rend;

    void Start() {
        rend = GetComponent<Renderer>();
    }
    
     void Update() {
		//WWW www = new WWW("file://C:/Users/Public/Documents/Unity Projects/Human/Assets/textures/scribble.png");
		WWW www = new WWW("file:///home/josh/Windows/scribble.png");
		Texture2D texTmp = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
		www.LoadImageIntoTexture(texTmp);
        if (textures.Length == 0)
            return;
        
        int index = Mathf.FloorToInt(Time.time / changeInterval);
        index = index % textures.Length;
        rend.material.mainTexture = texTmp;//textures[index];
    }

}