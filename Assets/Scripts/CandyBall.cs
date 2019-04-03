using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyBall : MonoBehaviour
{

    public Color CannonballColor;
    public int LightIntensity, LightRange;
    private Light BallLight;
    private Material Looks;
    private Renderer LooksApply;

    private GameObject goCollided;

    // Start is called before the first frame update
    void Start()
    {
        var rc = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255 );
        Looks = new Material( Shader.Find("Standard") );
        Looks.color = rc;
        LooksApply = this.GetComponent<Renderer>();
        LooksApply.material = Looks;
        
        BallLight = this.transform.GetChild(0).GetComponent<Light>();
        BallLight.color = rc;

        BallLight.range = LightRange;
        BallLight.intensity = LightIntensity;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool DestroyCannonBall() {
        if(this.transform.position.y <= -20) {
            return true;
        } else {
            return false;
        }
    } // destroy

    void OnCollisionEnter(Collision other) {
        this.goCollided = other.gameObject;
        if(this.goCollided) {
            DoExplode( this.goCollided, other.contacts[0].point );
        } else {
            Debug.Log("Other object not found in collision: " + other);
        }
    }

    void OnCollisionStay(Collision other) {
    }

    void OnCollisionExit(Collision other) {
    }

    private void DoExplode(GameObject collided, Vector3 impact) {
        var explosion = Instantiate( Resources.Load("Effects/FX_Explosion"), impact, Quaternion.identity );
    }

} // clas
