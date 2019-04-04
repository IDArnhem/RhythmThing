﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.IO.Ports;

public class Main : MonoBehaviour {

    public GameObject CannonReference;
	public Transform AlignToPoint;
	public int AmountOfCannons;
	public float CircleRadius;
	[Range(5, 90)]
	public int CannonAngles = 45;
	public List<string> ShootCannonsKeys = new List<string>();
	public List<Transform> CannonTargets = new List<Transform>();

	private List<Transform> CannonLocations;
	private Vector3 TargetLocShootingCannon;
	private bool rotateTheCannons = false;
	private float RotateAmount;

	void Start () {

		CannonLocations = new List<Transform>();
		TargetLocShootingCannon = Vector3.Normalize(AlignToPoint.position - this.transform.position) * CircleRadius;

		for (int i = 0; i < AmountOfCannons; i++)
		{
			float angle = i * Mathf.PI*2f / AmountOfCannons;
			float xPos = Mathf.Cos(angle)*CircleRadius;
			float zPos = Mathf.Sin(angle)*CircleRadius;
			Vector3 CannonPos = this.transform.position + new Vector3(xPos, 0, zPos);
			GameObject Cannon = Instantiate(CannonReference, CannonPos, Quaternion.identity, this.transform);

			CannonLocations.Add(Cannon.transform);

			CannonShooterScript CannonParameters = Cannon.GetComponent<CannonShooterScript>();
			CannonParameters.OnCannonGoingToShootEvent += OnCannonGoingToShoot;
			CannonParameters.Angle = CannonAngles;
			CannonParameters.ShootButton = ShootCannonsKeys[i];
			if(CannonTargets.Count == 1) CannonParameters.Target = CannonTargets[0];
			else CannonParameters.Target = CannonTargets[i];
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(rotateTheCannons){
			if(RotateAmount >= 0){
				var rotateSpeed = 80 * Time.deltaTime;
				transform.RotateAround(this.transform.position, Vector3.up, rotateSpeed);
				RotateAmount -= rotateSpeed;
			}else{
				RotateAmount = 0;
				rotateTheCannons = false;
			}
		}
	}

	void OnCannonGoingToShoot(Vector3 pos){
		RotateAmount = Vector3.SignedAngle(pos, TargetLocShootingCannon, this.transform.position);
		if(RotateAmount >= 5){
			rotateTheCannons = true;
		}
	}
}
