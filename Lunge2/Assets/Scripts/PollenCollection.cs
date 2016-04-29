using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PollenCollection : MonoBehaviour {

	public List<GameObject> pollenList;

	void Awake ()
	{
		pollenList = new List<GameObject> ();
		Debug.Log ("init list");
	}

	public void AddPollen(GameObject item) 
	{
		pollenList.Add (item);
	}

	public void DeletePollen(GameObject item)
	{
		Debug.Log ("removed?");
		pollenList.Remove (item);
	}

	public List<GameObject> GetPollenList()
	{
		//Debug.Log (pollenList.Count);
		return pollenList;
	}
}
