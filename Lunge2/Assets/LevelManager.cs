using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public int currentlevel;

 
	public int getNumWeakPoints (int level) {

        int weakPoints = level * 2;
        return weakPoints;
	}
	
    
	
}
