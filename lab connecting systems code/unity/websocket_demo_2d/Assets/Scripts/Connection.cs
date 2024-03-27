using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NativeWebSocket;

// Take in account that you have to do more processing when you get json arrays.
// This thread has some examples to play with
// https://forum.unity.com/threads/how-to-load-an-array-with-jsonutility.375735/page-2
// https://docs.unity3d.com/Manual/JSONSerialization.html
// https://www.xspdf.com/help/50181968.html

[System.Serializable]
public class JSONObject
{
    public int xPos;
    public int yPos;

    public static JSONObject CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<JSONObject>(jsonString);
    }
}

public class Connection : MonoBehaviour
{
  WebSocket websocket;

  // Start is called before the first frame update
  async void Start()
  {
        
        // websocket = new WebSocket("ws://echo.websocket.org");
        websocket = new WebSocket("ws://localhost:8080/processing");

    websocket.OnOpen += () =>
    {
      Debug.Log("Connection open!");
      
    };

    websocket.OnError += (e) =>
    {
      Debug.Log("Error! " + e);
    };

    websocket.OnClose += (e) =>
    {
        Debug.Log("Connection closed!");
    };

    websocket.OnMessage += (bytes) =>
    {
      // Reading a plain text message
      String message = System.Text.Encoding.UTF8.GetString(bytes);
      Debug.Log(message);
      JSONObject json = JSONObject.CreateFromJSON(message);

      Debug.Log(json);

      float xPosUnityCoordinates = map(json.xPos, 0, 400, -9, 9);
      float yPosUnityCoordinates = map(json.yPos, 0, 400, 4, -4);

        //Debug.Log(json.xPos);
        //Debug.Log(json.yPos);

      transform.position = new Vector2(xPosUnityCoordinates, yPosUnityCoordinates);

        //JsonUtility.FromJson<JSONObject>(jsonString)

    };

    // Keep sending messages at every 0.3s
    // InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);

    await websocket.Connect();
  }

 
  void Update()
  {
    #if !UNITY_WEBGL || UNITY_EDITOR
      websocket.DispatchMessageQueue();
    #endif

    //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //transform.position = mousePosition;

  }

    // like Arduino, Processing map one value to another
    // 	map(value, start1, stop1, start2, stop2)
  float map(float s, float a1, float a2, float b1, float b2)
  {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
  }

  async void SendWebSocketMessage()
  {
    if (websocket.State == WebSocketState.Open)
    {
      // Sending bytes
      await websocket.Send(new byte[] { 10, 20, 30 });

      // Sending plain text
      await websocket.SendText("plain text message");
    }
  }

  private async void OnApplicationQuit()
  {
    await websocket.Close();
  }
}
