/* Copyright (c) 2020 ExT (V.Sigalkin) */

using UnityEngine;
using UnityEngine.UI;

namespace extOSC.Examples
{
	public class SimpleMessageTransmitter : MonoBehaviour
	{
		#region Public Vars

		public string Address = "/test2";

		[Header("OSC Settings")]
		public OSCTransmitter Transmitter;

		#endregion

		#region Unity Methods


		public void ButtonOnClick(){
			var message = new OSCMessage(Address);

			System.Random valuenumber = new System.Random();
			float num = valuenumber.Next(1, 4);
			message.AddValue(OSCValue.Float(num));

			Transmitter.Send(message);

		}

		#endregion
	}
}