using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
    public Texture aTexture;
    public RenderTexture rTex;
	public Material mat;
    void Start() {
        if (!aTexture || !rTex)
            Debug.LogError("A texture or a render texture are missing, assign them.");
        
    }
    void Update() {
        Graphics.Blit(aTexture, rTex,mat);
    }
}
