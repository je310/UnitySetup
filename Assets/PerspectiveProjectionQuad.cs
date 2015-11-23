using UnityEngine;
using System.Collections;

public class PerspectiveProjectionQuad : MonoBehaviour {
	public GameObject ManMesh;
	public GameObject flatMeshObject;
	private Mesh inputMesh;
	private Camera camera;
	private bool ran;
	private Vector3 viewPos;
	private Vector2[] UV;
	private Mesh flatMesh;
	private Vector2 testVector;
	private Vector3 manPosition;
	private Vector3[] vertexList;

	// Use this for initialization
	void Start () {
		inputMesh = new Mesh();
		inputMesh = ManMesh.GetComponent<MeshFilter>().mesh;
		camera = GetComponent<Camera>();
		ran = false;
		viewPos  = new Vector3(0f,0f,0f);
		UV= new Vector2[inputMesh.vertexCount];
		flatMesh = flatMeshObject.GetComponent<MeshFilter>().mesh;
		testVector = new Vector2(1f,1f);
		manPosition = ManMesh.transform.position;
		vertexList = inputMesh.vertices;// accessing this is the slow part it seems. 
		
		

	}
	
	// Update is called once per frame
 	void Update () {
		
		if(true){
			for(int i = 0; i < inputMesh.vertexCount; i++){
				testVector = manPosition+vertexList[i];
				viewPos = camera.WorldToViewportPoint(testVector);
				//viewPos = i*testVector;
				//viewPos = camera.WorldToViewportPoint(i*testVector);
				//UV[i] = new Vector2(viewPos.x,viewPos.y);
				UV[i].x = viewPos.x;
				UV[i].y = viewPos.y;
				// flatMesh.uv[i].x = 0;//viewPos.x;
				// flatMesh.uv[i].y = 0;//viewPos.y;
				
			}
			flatMesh.uv = UV;
		}

		ran = true;
	 } 
	
}
