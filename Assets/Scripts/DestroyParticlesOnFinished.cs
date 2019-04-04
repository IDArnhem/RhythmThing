using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticlesOnFinished : MonoBehaviour
{
	private ParticleSystem FX_System;
	
	public void Start() 
	{
		FX_System = GetComponent<ParticleSystem>();
	}

	public void Update() 
	{
		if(FX_System)
		{
			if(!FX_System.IsAlive())
			{
				Destroy(this.gameObject);
			}
		}
	}
}
