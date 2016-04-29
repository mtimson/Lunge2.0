using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PollenCollection : MonoBehaviour {

	public List<GameObject> pollenList;

	void Awake ()
	{
		pollenList = new List<GameObject> ();
	}

	public void AddPollen(GameObject item) 
	{
		pollenList.Add (item);
	}

	public void DeletePollen(GameObject item)
	{
		pollenList.Remove (item);
	}

	public List<GameObject> GetPollenList()
	{
		//Debug.Log (pollenList.Count);
		return pollenList;
	}
}
