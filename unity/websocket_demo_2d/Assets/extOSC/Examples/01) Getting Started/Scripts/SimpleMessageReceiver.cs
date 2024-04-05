/* Copyright (c) 2020 ExT (V.Sigalkin) */

using UnityEngine;
using UnityEngine.UI;

namespace extOSC.Examples
{
	public class SimpleMessageReceiver : MonoBehaviour
	{
		#region Public Vars

		public string Address = "/example/1";
		public float messageReceived;

		[Header("OSC Settings")]
		public OSCReceiver Receiver;

		#endregion

		#region Unity Methods

		protected virtual void Start()
		{
			// Debug.Log("message2");
			Receiver.Bind(Address, ReceivedMessage);
		}

		#endregion

		#region Private Methods

		//Receive message from Processing
		private void ReceivedMessage(OSCMessage message)
		{
			//Debug.LogFormat("Received: {0}", message);
			//Debug.Log("message");
			messageReceived = message.Values[0].FloatValue;
			//float MessageReceived = message.ToFloat();
			//float x = message.GetFloat(0);
			//float y = message.GetFloat(1);
			//float z = message.GetFloat(2);
			//Debug.Log(messageReceived);
			Image img = GameObject.Find("ImageUI").GetComponent<Image>();

			if(messageReceived > 0.5f){
				img.color = new Color(255, 0, 0);
			}else if(messageReceived <= 0.5f){
				img.color = new Color(0, 0, 255);
			}

        }

		#endregion
	}
}