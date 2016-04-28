using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This is modelled after the SteeringBehavior class from the Buckland text
//methods are overloaded to allow steering to depend on a neighbourhood of limited radius or not
//Flocking behaviours and Seek are implemented. 

public class Steering
{
	private GameObject agent;
	private Rigidbody agentRB;
	private float speed;

    private Vector3 wanderTarget;
    public float wanderRadius = 10.0f;
    public float wanderDist = 15.0f;
    public float wanderJitter = 2.0f;

    //Constructor
	public Steering(GameObject particle, float maxSpeed)
	{
		agent = particle;
		agentRB = particle.GetComponent<Rigidbody> ();
		speed = maxSpeed;
        wanderTarget = new Vector3(1.0f, 0.0f, 0.0f);
	}

    //Move toward a target
	public Vector3 Seek(Vector3 target)
	{
		Vector3 DesiredVelocity = Vector3.Normalize (target - agent.transform.position) * speed;

		return (DesiredVelocity - agentRB.velocity);
	}
    public Vector3 Wander()
    {
        wanderTarget += new Vector3(UnityEngine.Random.value * wanderJitter - 1, 0.0f, UnityEngine.Random.value * wanderJitter - 1);
        wanderTarget = wanderTarget.normalized * wanderRadius;

        Vector3 target = new Vector3();
        target =  agent.transform.forward * wanderDist + wanderTarget;
        return target;
    }

    /* METHODS WITHOUT NEIGHBOURHOOD RADIUS*/
    //Ensure separation from flock mates
    public Vector3 Separation(List<GameObject> flock)
    {
        Vector3 SteeringForce = new Vector3();
        Vector3 ToAgent;
        foreach (GameObject particle in flock)  //iterate over flock mates
        {
            //vector to agent being considered
            ToAgent = agent.transform.position - particle.transform.position;
            if (particle != agent)  //no separation from self
            {
                //increment steering force by amount inveresly proportional to distance
                if (ToAgent.magnitude == 0)
                    SteeringForce += new Vector3(UnityEngine.Random.value * 5, UnityEngine.Random.value * 5, UnityEngine.Random.value * 5);
                else
                    SteeringForce += ToAgent.normalized / ToAgent.magnitude;
            }
        }
        return SteeringForce;
    }

    //Ensure flock stays together
    public Vector3 Cohesion(List<GameObject> flock)
    {
        Vector3 SteeringForce = new Vector3();
        Vector3 CenterMass = new Vector3();
        int NeighbourCount = 0;

        //find center of mass
        foreach (GameObject particle in flock)  //iterate over flock mates
        {
            if (particle != agent)
            {
                CenterMass += particle.transform.position;
                NeighbourCount++;
            }
        }
        //Seek towards CM
        if (NeighbourCount > 0)
        {
            CenterMass /= NeighbourCount;   //CM is the average position
            SteeringForce = Seek(CenterMass);
        }
        return SteeringForce;
    }

    //Ensure flock moves together
    public Vector3 Alignment(List<GameObject> flock)
    {
        Vector3 AvgHeading = new Vector3();
        int NeighbourCount = 0;

        //find average heading
        foreach (GameObject particle in flock)  //iterate over flock mates
        {
            if (particle != agent)
            {
                AvgHeading += particle.transform.forward;
                NeighbourCount++;
            }
        }
        
        if (NeighbourCount > 0)
        {
            AvgHeading /= NeighbourCount;
            AvgHeading -= agent.transform.forward;
        }
        return AvgHeading;
    }

    /*METHODS THAT CONSIDER ONLY FLOCKMATES WITHIN A NEIGHBOURHOOD OF GIVEN RADIUS*/

    //Ensure separation from flock mates
    public Vector3 Separation(List<GameObject> flock, float neighbourhoodRadius)
	{
		Vector3 SteeringForce = new Vector3 ();
		Vector3 ToAgent;
		foreach (GameObject particle in flock)  //iterate over flock mates
        {
			ToAgent = agent.transform.position - particle.transform.position;
			if((particle != agent) && (ToAgent.magnitude < neighbourhoodRadius))
			{
				SteeringForce += ToAgent.normalized / ToAgent.magnitude;
			}
		}
		return SteeringForce;
	}

    //Ensure flock moves together
    public Vector3 Alignment(List<GameObject> flock, float neighbourhoodRadius)
	{
		Vector3 AvgHeading = new Vector3 ();
		int NeighbourCount = 0;

		foreach(GameObject particle in flock)  //iterate over flock mates
        {
			if((particle != agent) && Vector3.Magnitude(agent.transform.position - particle.transform.position) < neighbourhoodRadius)
			{
				AvgHeading += particle.transform.forward;
				NeighbourCount++;
			}
		}
        if (NeighbourCount > 0)
        {
            AvgHeading /= NeighbourCount;
            AvgHeading -= agent.transform.forward;
        }
        return AvgHeading;
	}

    //Ensure flock stays together
    public Vector3 Cohesion(List<GameObject> flock, float neighbourhoodRadius)
	{
		Vector3 SteeringForce = new Vector3 ();
		Vector3 CenterMass = new Vector3 ();
		int NeighbourCount = 0;

		foreach(GameObject particle in flock)  //iterate over flock mates
        {
			if((particle != agent) && Vector3.Magnitude(agent.transform.position - particle.transform.position) < neighbourhoodRadius)
			{
				CenterMass += particle.transform.position;
				NeighbourCount++;
			}
		}
        if (NeighbourCount > 0)
        {
            CenterMass /= NeighbourCount;
            SteeringForce = Seek(CenterMass);
        }
        return SteeringForce;
	}
}
