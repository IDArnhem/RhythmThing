using UnityEngine;
using System.Collections;

public class ReceiveAll : MonoBehaviour {

	public OSC OscRef;


	void Start () {
		OscRef.SetAllMessageHandler(OnReceive);
	}

	void OnReceive(OscMessage message){
        Debug.Log( "Received: " + message.address );
		// if ( message.address == "/noisiness" ) {

		// 	float data = message.GetFloat(0);
		// 	float scaled = Mathf.Lerp(0.1f,4f,data); // Scale to new range

		// 	noisinessGameObject.transform.localScale = new Vector3(scaled,scaled,scaled);

		// } else if ( message.address == "/brightness" ) {

		// 	float data = message.GetFloat(0);
		// 	Color c = new Color( data , data , data);
			
		// 	brightnessGameObject.GetComponent<Renderer>().material.color = c;

		// }
	}


	// Update is called once per frame
	void Update () {
	
	}
}