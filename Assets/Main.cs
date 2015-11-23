using UnityEngine;
using WebSocketSharp;
using SimpleJSON;
using System;
using System.Threading;
using System.Text;
using System.Collections;

public class Main : MonoBehaviour {
    private WebSocket ws;
	public  JSONNode N;
	public  JSONNode Unwrapped;
	public  JSONNode triggerUnwrapped;
	public  JSONNode chatter;
	public  JSONNode toSend;
	public  JSONNode data;
	public Material ThermalImg;
	public Material ThermalImg2;
	public Material ReturnImg;
	private string m_InGameLog = "";
	private Vector2 m_Position = Vector2.zero;
	private Texture2D tex;
	private byte[] decodedBytes;
	public bool publishUnwrapped;
	public Camera InCamera;
	[SerializeField]
	RenderTexture securityCameraTexture;  // drag the render texture onto this field in the Inspector
	[SerializeField]
	void P(string aText)
	{
		m_InGameLog += aText + "\n";
	}
    void Start() {
		publishUnwrapped = false;
		Texture2D tex = new Texture2D(2, 2);
		string filepath = "ThermalPic.jpg";
		//if (System.IO.File.Exists(filepath))     {
		decodedBytes = System.IO.File.ReadAllBytes(filepath);
		tex.LoadImage(decodedBytes);
		ThermalImg.mainTexture = tex;
		ThermalImg2.mainTexture = tex;
		//definitions of subscriptions and advertisments to be sent to ros.
		N = JSONNode.Parse("{\"op\": \"subscribe\",\"topic\": \"thermalText\",\"type\": \"std_msgs/String\"}");
		triggerUnwrapped = JSONNode.Parse("{\"op\": \"subscribe\",\"topic\": \"triggerUnwrapped\",\"type\": \"std_msgs/String\"}");
		Unwrapped = JSONNode.Parse("{\"op\": \"advertise\",\"topic\": \"unwrappedImg\",\"type\": \"std_msgs/String\"}");
		chatter = JSONNode.Parse("{\"op\": \"advertise\",\"topic\": \"helloString\",\"type\": \"std_msgs/String\"}");


		toSend = new JSONClass();
		toSend["op"] = "publish";
		toSend["topic"] = "helloString";
		data = new JSONClass();
		data["data"] = "Why Hello there ros, Fancy meeting you here.";
		toSend["msg"] = data;
        ws = new WebSocket("ws://localhost:9090");

		Thread.Sleep(300);
        ws.OnOpen += OnOpenHandler;
        ws.OnMessage += OnMessageHandler;
        ws.OnClose += OnCloseHandler;

        ws.ConnectAsync();        
    }

	void Update(){
		Texture2D tex= new Texture2D(2, 2);
		tex.LoadImage(decodedBytes);
		ThermalImg.mainTexture = tex;
		ThermalImg2.mainTexture = tex;
		ThermalImg2.mainTexture.wrapMode = TextureWrapMode.Clamp;
		ThermalImg.mainTexture.wrapMode = TextureWrapMode.Clamp;
		if(publishUnwrapped){
			//Texture2D tex2= ReturnImg.mainTexture as Texture2D;
			//Texture2D cameraImage= new Texture2D(InCamera.targetTexture.width, InCamera.targetTexture.height, TextureFormat.RGB24, false);
			//cameraImage.ReadPixels(new Rect(0, 0, InCamera.targetTexture.width, InCamera.targetTexture.height), 0, 0);
			//byte[] bytes = cameraImage.EncodeToPNG();

			//System.IO.File.WriteAllBytes("EasyToFind.png", bytes);
			//Debug.Log("EasyToFind.png saved");
			//string imgBase64 = Convert.ToBase64String(bytes);
			//toSend["topic"] = "unwrappedImg";
			//toSend["msg"]["data"] = "I saved a msg as an image";
			//ws.SendAsync(toSend.ToString(), OnSendComplete);

		}
		Resources.UnloadUnusedAssets();
		System.GC.Collect();
	}

	void LateUpdate()
	{
		
		if (publishUnwrapped)
		{
			StartCoroutine(SaveCameraView());
			publishUnwrapped = false;
		}
		
	}

	public IEnumerator SaveCameraView()
	{
		yield return new WaitForEndOfFrame();
		
		// get the camera's render texture
		RenderTexture rendText= RenderTexture.active;
		RenderTexture.active = InCamera.targetTexture;
		
		// render the texture
		InCamera.Render();
		
		// create a new Texture2D with the camera's texture, using its height and width
		Texture2D cameraImage= new Texture2D(InCamera.targetTexture.width, InCamera.targetTexture.height, TextureFormat.RGB24, false);
		cameraImage.ReadPixels(new Rect(0, 0, InCamera.targetTexture.width, InCamera.targetTexture.height), 0, 0);
		//cameraImage.Apply();
		//RenderTexture.active = rendText;
		
		// store the texture into a .PNG file
		byte[] bytes = cameraImage.EncodeToPNG();
		
		// save the encoded image to a file
		System.IO.File.WriteAllBytes("EasyToFind.png", bytes);
		Debug.Log("EasyToFind.png saved");
	}

	void OnApplicationQuit() {
			ws.Close ();
	}

    private void OnOpenHandler(object sender, System.EventArgs e) {
        Debug.Log("WebSocket connected!");
		ws.SendAsync(chatter.ToString(), OnSendComplete);
		ws.SendAsync(N.ToString(), OnSendComplete);
		ws.SendAsync(triggerUnwrapped.ToString(), OnSendComplete);
		ws.SendAsync(Unwrapped.ToString(), OnSendComplete);
		//ws.SendAsync(toSend.ToString(), OnSendComplete);
		Thread.Sleep(300);
    }

    private void OnMessageHandler(object sender, MessageEventArgs e) {
        //Debug.Log("WebSocket server said: " + e.Data);
		JSONNode recieved = JSONNode.Parse(e.Data);
		//Debug.Log(recieved["topic"]);
		string Topic = recieved["topic"];
		if(string.Equals(Topic,N["topic"])){
			decodedBytes = Convert.FromBase64String (recieved["msg"]["data"]);
			System.IO.File.WriteAllBytes("ThermalPic.jpg", decodedBytes);
		}
		else{
			//Debug.Log(Topic);
		}
		if(string.Equals (recieved["topic"],triggerUnwrapped["topic"])){
			Debug.Log("I have been asked to generate an unwrapped img");
			publishUnwrapped = true;
		}
		//tex.LoadImage(decodedBytes);
		//Debug.Log("loaded image");
		//ThermalImg.mainTexture = tex;
		//Debug.Log("Frame Recieved");
        //ws.CloseAsync();
    }

    private void OnCloseHandler(object sender, CloseEventArgs e) {
        Debug.Log("WebSocket closed with reason: " + e.Reason);
    }

    private void OnSendComplete(bool success) {
        //Debug.Log("Message sent successfully? " + success);
		//Thread.Sleep(3000);
		//ws.SendAsync(toSend.ToString(), OnSendComplete);
    }
}
