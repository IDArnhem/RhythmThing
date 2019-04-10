using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System; 
using UnityEngine;

public class CannonShooterScript : MonoBehaviour
{
   [Range(0, 90)]
	public int Angle = 45;
	public Transform Target; 
	public string ShootButton;

	public event Action<Vector3> OnCannonGoingToShootEvent;
	private AudioSource CannonSounds; 

	void Start () {
		CannonSounds = this.gameObject.GetComponent<AudioSource>();
		this.transform.eulerAngles = new Vector3(0, 0, Angle);
	}
	
	void Update () { 
		//prediction code here -> exrersize for the reader
		//get the current speed and direction to where the target is moving
		//calculate the time it takes the projectile to travel to its location. formula: t = [V * sin(α) + √(V * sin(α))² + 2 * g * h)] / g
		//set the angle for the cannon in the future
		//the decide to shoot. 
		acquireTarget(Target.position);
		HVDist distances = PredictTrajectoryDistances(this.transform.position, Target.position);

		if(Input.GetKeyUp(ShootButton)){
			OnCannonGoingToShootEvent(this.transform.position);
			shootCannonBall(distances);
		}
	}

	void acquireTarget(Vector3 target){
		Vector3 dir = target - this.transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = lookRotation.eulerAngles;
		this.transform.rotation = Quaternion.Euler(Angle, rotation.y, 0);
	}

	HVDist PredictTrajectoryDistances(Vector3 cannonLoc, Vector3 targetLoc){
		return new HVDist(cannonLoc, targetLoc);
	}



	void shootCannonBall(HVDist distances){
		CannonSounds.Play();
		var CannonBall = Instantiate(Resources.Load("Objects/CandyBall"), this.transform.position, Quaternion.identity) as GameObject;
		var PhysicsProperties = CannonBall.GetComponent<Rigidbody>();

		float RadiansAngle = Angle * Mathf.PI/180;
		float a = distances.HorizontalDist * 9.8f;
		float b = Mathf.Pow(a, 2);
		float c = Mathf.Cos(RadiansAngle) * Mathf.Sin(RadiansAngle);
		float d = 2 * a * c;
		float e = Mathf.Pow(Mathf.Cos(RadiansAngle), 2);
		float f = e * 2 * 9.8f * distances.VerticalDist;

		float ShootPower = Mathf.Sqrt(b / (d + f));
		PhysicsProperties.AddForce(Vector3.Normalize(this.transform.up) * ShootPower, ForceMode.VelocityChange);
	}

	private struct HVDist
	{
		public float HorizontalDist, VerticalDist;

		public HVDist(Vector3 cannonPos, Vector3 targetPos)
		{
			VerticalDist = cannonPos.y - targetPos.y;
			cannonPos.y = 0;
			targetPos.y = 0;
			HorizontalDist = Vector3.Distance(cannonPos, targetPos); 
		}
	}
}
