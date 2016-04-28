
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//pollution collection
public class PollutionCollection : MonoBehaviour
{

    public List<GameObject> pollutionList;

    void Awake()
    {
        pollutionList = new List<GameObject>();
    }

    public void AddPollution(GameObject item)
    {
        item.transform.position = this.transform.position;
        pollutionList.Add(item);
        Debug.Log("added");

    }

    public void DeletePollution(GameObject item)
    {
        Debug.Log("removed?");
        pollutionList.Remove(item);
    }

    public List<GameObject> GetpollutionList()
    {
        //Debug.Log (pollenList.Count);
        return pollutionList;
    }
}
