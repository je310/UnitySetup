using UnityEngine;
using System.Collections;


public class PerspectiveProjection : MonoBehaviour {
	public GameObject ManMesh;
	public GameObject flatMeshObject;
	public Texture textureToSave;
	private Mesh inputMesh;
	private Camera camera;
	private bool ran;
	private Vector3 viewPos;
	private Vector2[] UV;
	private Mesh flatMesh;
	private Vector3 testVector;
	private Vector3 manPosition;
	private Vector3[] vertexList;
	private Vector3 vectorToCamera;
	private float dot;
	private Vector3[] normals;
	private Vector3 forward;
	private Color[] colours;
	// Use this for initialization
	void Start () {
		inputMesh = new Mesh();
		SkinnedMeshRenderer inputSkinnedMesh = ManMesh.GetComponent<SkinnedMeshRenderer>();
		inputSkinnedMesh.BakeMesh(inputMesh);
		camera = GetComponent<Camera>();
		ran = false;
		viewPos  = new Vector3(0f,0f,0f);
		UV= new Vector2[inputMesh.vertexCount];
		flatMesh = flatMeshObject.GetComponent<MeshFilter>().mesh;
		testVector = new Vector3(1f,1f,1f);
		manPosition = ManMesh.transform.position;
		vertexList = inputMesh.vertices;// accessing this is the slow part it seems. 
		normals = inputMesh.normals;
		colours = new Color[inputMesh.vertexCount];
		

	}
	
	// Update is called once per frame
 	void Update () {
		//colours = inputMesh.colors;
		if(true){
			for(int i = 0; i < inputMesh.vertexCount; i++){
				forward = camera.transform.TransformDirection(Vector3.forward);
				testVector = manPosition+vertexList[i];
				vectorToCamera = camera.transform.position - testVector;
				dot = Vector3.Dot(forward.normalized,normals[i].normalized);
				float PosDot = Vector3.Dot(vectorToCamera.normalized,forward.normalized);
				//viewPos = i*testVector;
				//viewPos = camera.WorldToViewportPoint(i*testVector);
				//UV[i] = new Vector2(viewPos.x,viewPos.y);
				viewPos = camera.WorldToViewportPoint(testVector);
				if(viewPos.x > 10 || viewPos.y >10){
					viewPos.x = 10;
					viewPos.y = 10;
				}
				if(viewPos.x < -10 || viewPos.y <-10){
					viewPos.x = -10;
					viewPos.y = -10;
				}
				UV[i].x = viewPos.x;
				UV[i].y = viewPos.y;
				colours[i] = Color.white;
				if(dot > -0.1f || PosDot >-0.5f || viewPos.x >1f || viewPos.x < 0f || viewPos.y >1f || viewPos.y < 0f ){
					colours[i] = Color.black;
				}
				
				
				// flatMesh.uv[i].x = 0;//viewPos.x;
				// flatMesh.uv[i].y = 0;//viewPos.y;
				
			}
			flatMesh.colors = colours;
			flatMesh.uv = UV;
		}
		
		ran = true;
	 } 
	
}
