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

		protected virtual void Start()
		{
			//	var message = new OSCMessage(Address);
			//	message.AddValue(OSCValue.String("Hello, world!"));

			//	Transmitter.Send(message);
		}

		public void ButtonOnClick(){
			var message = new OSCMessage(Address);

			System.Random valuenumber = new System.Random();
			float num = valuenumber.Next(1, 4);
			//message.AddValue(OSCValue.Int(num));
			message.AddValue(OSCValue.Float(num));

			Transmitter.Send(message);

		}

		#endregion
	}
}