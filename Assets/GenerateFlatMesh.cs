using UnityEngine;
using System.Collections;

public class GenerateFlatMesh : MonoBehaviour {

	// Use this for initialization
	public GameObject flatMeshObject;
	private Mesh inputMesh;
	private Mesh outputMesh;
	private Vector3[] flatMesh;
	private int[] newTriangles;
	void Start () {
		inputMesh = new Mesh();
		SkinnedMeshRenderer inputSkinnedMesh = GetComponent<SkinnedMeshRenderer>();
		inputSkinnedMesh.BakeMesh(inputMesh);
		flatMesh = new Vector3[inputMesh.vertexCount];
		Vector2[] UV;
		UV= new Vector2[inputMesh.vertexCount];
		for(int i = 0; i < inputMesh.vertexCount; i++){
			Vector2 UVinput = inputMesh.uv[i];
			flatMesh[i].x = UVinput.x;
			flatMesh[i].y = UVinput.y;
			flatMesh[i].z = 0;
			
		}
		outputMesh = flatMeshObject.GetComponent<MeshFilter>().mesh;
		outputMesh.vertices = flatMesh;
		outputMesh.triangles =inputMesh.triangles;
		outputMesh.uv = inputMesh.uv;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
