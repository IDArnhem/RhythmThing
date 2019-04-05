using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.IO.Ports;

public class Main : MonoBehaviour {

    public GameObject reference;
    public Transform target;

    public OSC oscref;

    public float radius;
    public int count;

    // public string serialPort;
    // public string serialDataRate;

    // SerialPort stream = new SerialPort(serialPort, serialDataRate);

    // Start is called before the first frame update
    void Start()
    {
        float ainc = ((2f * Mathf.PI) / count);  // angular increment to form circle

        for(int i = 0; i < count; i++) {
            // calculate position in a circular array of "count" objects
            var pos = new Vector3();
            pos.x = Mathf.Cos(i*ainc) * radius;
            pos.z = Mathf.Sin(i*ainc) * radius;

            // add that position to the empty in the center
            var placing = this.transform.position + pos; //new Vector3(0, 0, i * 2);
            var cannon = Object.Instantiate(reference, placing, Quaternion.identity );
            var cs = cannon.GetComponent<CannonShooterScript>();
            cs.oscref = this.oscref;
//            Debug.Log( "CannonShooter.." + cs.oscref );
            cs.keystroke = Config.controlKeys[i];
            cs.target = this.target;
            //cs.power = Random.Range(8, 20);
            //cs.angle = Random.Range(30, 60);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
