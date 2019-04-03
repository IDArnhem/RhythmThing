using System.Collections;
using System.Collections.Generic;
using System.Timers; 
using UnityEngine;

public class CannonShooterScript : MonoBehaviour
{
    public Transform target;
    private GameObject bullet;
    
    public float power;

    [Range(0, 90)]
    public float angle;

    public float range;

    public string keystroke;

    private List<CandyBall> AllCannonBalls;

    private AudioSource soundfx;

    void Start()
    {
        soundfx = this.gameObject.GetComponent<AudioSource>();

        AllCannonBalls = new List<CandyBall>();
        this.transform.eulerAngles = new Vector3(0, 0, -angle);

        LockOnTarget();

        //StartCoroutine("ShootingEvent");
        // InvokeRepeating("ShootingEvent", 0, 0.5f); // start on ms = 0 and repeat every 0.5
    }

    private IEnumerator ShootingEvent() 
    { 
        while(true)
        {
            ShootBullet();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update()
    {
        float dist = LockOnTarget();

        if( Input.GetKeyUp(keystroke) ) {
            ShootBullet();
        }

        for(int i = AllCannonBalls.Count - 1; i >= 0; i--) {
            CheckCannonBall( AllCannonBalls[i] );
        }
    }

    float LockOnTarget() {
        Vector3 dir = target.position - this.transform.position;
        Quaternion heading = Quaternion.LookRotation(dir); //, Vector3.up);
        Vector3 rot = heading.eulerAngles;
        this.transform.rotation = Quaternion.Euler(this.angle, rot.y, 0f);

        float dist = Vector3.Distance(target.position, this.transform.position);
        
        //this.angle = (Mathf.Asin( dist * 9.81f / power*power ) / 2.0f); // α = asin(R * g / v^2) / 2
        //Debug.Log(this.angle);

        return dist;
    }

    void ShootBullet() {
        soundfx.Play();
        bullet = Object.Instantiate( Resources.Load("CandyBall"), this.transform.position, Quaternion.identity ) as GameObject;
        var targetdist = Vector3.Distance(this.transform.position, target.position);
        
        var ballbody = bullet.GetComponent<Rigidbody>();

        // R = (2 * V*cos(a) * V*sin(a)) / g
        // R * g = (2 * V*cos(a) * V*sin(a))
        // R * g = 2 * V^2 * cos(a) * sin(a)
        // R * g / 2 * V^2 = cos(a) * sin(a)
        // from : Sin A Cos A = ½ Sin 2A
        // R * g / 2 * V^2 = ½ Sin 2A
        // invsin(R * g / V^2) = 2A
        // asin(R * g / v^2) / 2 = A
        // α = asin(R * g / v^2) / 2
        /*
        Projectile motion equations
        Uff, that was a lot of calculations! Let's sum that up to form the 
        most important projectile motion equations:

        Launching the object from the ground (initial height h = 0)
        Horizontal velocity component: Vx = V * cos(α)
        Vertical velocity component: Vy = V * sin(α)
        Time of flight: t = 2 * Vy / g
        Range of the projectile: R = 2 * Vx * Vy / g
        Maximum height: hmax = Vy² / (2 * g)

        Launching the object from some elevation (initial height h > 0)
        Horizontal velocity component: Vx = V * cos(α)
        Vertical velocity component: Vy = V * sin(α)
        Time of flight: t = [Vy + √(Vy² + 2 * g * h)] / g
        Range of the projectile: R = Vx * [Vy + √(Vy² + 2 * g * h)] / g
        Maximum height: hmax = h + Vy² / (2 * g)
        Using our projectile motion calculator will surely save you a lot of time. It can also work 'in reverse' – simply type in any two values (for example, the time of flight and maximum height) and watch it do all calculations for you!
        */


        //ballbody.AddForce( Vector3.Normalize(this.transform.up) * Random.Range(power*0.6f, power), ForceMode.Impulse );
        ballbody.AddForce( Vector3.Normalize(this.transform.up) * power, ForceMode.Impulse );
        AllCannonBalls.Add( bullet.GetComponent<CandyBall>() );
    }

    void CheckCannonBall(CandyBall ball) {
        if(ball.DestroyCannonBall()) {
            Destroy(ball.gameObject, 0.1f);
            AllCannonBalls.Remove( ball );
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    } 
}
