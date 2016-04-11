using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Steering
{
	private GameObject agent;
	private Rigidbody agentRB;
	private float speed;

	public Steering(GameObject particle, float maxSpeed)
	{
		agent = particle;
		agentRB = particle.GetComponent<Rigidbody> ();
		speed = maxSpeed;
	}

	public Vector3 Seek(Vector3 target)
	{
		Vector3 DesiredVelocity = Vector3.Normalize (target - agent.transform.position) * speed;

		return (DesiredVelocity - agentRB.velocity);
	}

	public Vector3 Separation(List<GameObject> neighbours, float radius)
	{
		Vector3 SteeringForce = new Vector3 ();
		Vector3 ToAgent;
		foreach (GameObject particle in neighbours)
		{
			ToAgent = agent.transform.position - particle.transform.position;
			if((particle != agent) && (ToAgent.magnitude < radius))
			{
				SteeringForce += ToAgent.normalized / ToAgent.magnitude;
			}
		}
		return SteeringForce;
	}

	public Vector3 Alignment(List<GameObject> neighbours, float radius)
	{
		Vector3 AvgHeading = new Vector3 ();
		int NeighbourCount = 0;

		foreach(GameObject particle in neighbours)
		{
			if((particle != agent) && Vector3.Magnitude(agent.transform.position - particle.transform.position) < radius)
			{
				AvgHeading += particle.transform.forward;
				NeighbourCount++;
			}
			if(NeighbourCount > 0)
			{
				AvgHeading /= NeighbourCount;
				AvgHeading -= agent.transform.forward;
			}
		}
		return AvgHeading;
	}

	public Vector3 Cohesion(List<GameObject> neighbours, float radius)
	{
		Vector3 SteeringForce = new Vector3 ();
		Vector3 CenterMass = new Vector3 ();
		int NeighbourCount = 0;

		foreach(GameObject particle in neighbours)
		{
			if((particle != agent) && Vector3.Magnitude(agent.transform.position - particle.transform.position) < radius)
			{
				CenterMass += particle.transform.position;
				NeighbourCount++;
			}
			if(NeighbourCount > 0)
			{
				CenterMass /= NeighbourCount;
				SteeringForce = Seek(CenterMass);
			}
		}
		return SteeringForce;
	}
}
