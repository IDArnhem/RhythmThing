using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchAll : MonoBehaviour
{

	public OSC oscref;

	public Main main;

	void Start () {
		oscref = this.gameObject.GetComponent<OSC>();
		oscref.SetAddressHandler("/cannon/shoot", OnCannonShoot);
		oscref.SetAllMessageHandler(OnUnknown);

		main = GameObject.Find("Main").GetComponent<Main>();
	}

	void OnCannonShoot(OscMessage msg) {
        Debug.Log( "Cannon #" + msg.GetInt(0) + " fired" );
		if(null != main) {
			Debug.Log( "Total cannons #" + main.AllCannons.Count);
		}
	}

	void OnUnknown(OscMessage msg){
        Debug.Log( "Received unknown: " + msg.address );
	}


	// Update is called once per frame
	void Update () {
	}
}
