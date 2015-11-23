using UnityEngine;
using WebSocketSharp;
using SimpleJSON;

using System.Threading;

public class Main : MonoBehaviour {
    private WebSocket ws;
	public  JSONNode N;
	public  JSONNode toSend;
	public  JSONNode data;
    void Start() {
		toSend = new JSONClass();
		N = JSONNode.Parse("{\"op\": \"advertise\",\"topic\": \"helloString\",\"type\": \"std_msgs/String\"}");
		toSend["op"] = "publish";
		toSend["topic"] = "helloString";
		data = new JSONClass();
		data["data"] = "Why Hello there ros, Fancy meeting you here.";
		toSend["msg"] = data;
        ws = new WebSocket("ws://localhost:9090");

        ws.OnOpen += OnOpenHandler;
        ws.OnMessage += OnMessageHandler;
        ws.OnClose += OnCloseHandler;

        ws.ConnectAsync();        
    }

    private void OnOpenHandler(object sender, System.EventArgs e) {
        Debug.Log("WebSocket connected!");
        Thread.Sleep(3000);
		ws.SendAsync(N.ToString(), OnSendComplete);
    }

    private void OnMessageHandler(object sender, MessageEventArgs e) {
        Debug.Log("WebSocket server said: " + e.Data);
        Thread.Sleep(3000);
        //ws.CloseAsync();
    }

    private void OnCloseHandler(object sender, CloseEventArgs e) {
        Debug.Log("WebSocket closed with reason: " + e.Reason);
    }

    private void OnSendComplete(bool success) {
        Debug.Log("Message sent successfully? " + success);
		Thread.Sleep(3000);
		ws.SendAsync(toSend.ToString(),OnSendComplete);
    }
}
