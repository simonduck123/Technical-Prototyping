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
			Receiver.Bind(Address, ReceivedMessage);
		}

		#endregion

		#region Private Methods

		//Receive message from Processing
		private void ReceivedMessage(OSCMessage message)
		{
			messageReceived = message.Values[0].FloatValue;

			Image img = GameObject.Find("ImageUI").GetComponent<Image>();

			if(messageReceived > 0.5f){
				img.color = new Color(125, 0, 0);
			}else if(messageReceived <= 0.5f){
				img.color = new Color(0, 0, 125);
			}

        }

		#endregion
	}
}