using UnityEngine;
using System.Collections;

public class LungManager : MonoBehaviour {

    public int maxLungHealth= 100;
    public static int lungHealth;
    public int level;
    public int weakPointsNum;
    public GameObject[] weakPoints;
    public GameObject lungArea;
    LevelManager level_Manager = new LevelManager();
   

    // Use this for initialization


    public void Start () {
        lungHealth = maxLungHealth;
        level = 1;
        //initialize weakPoints according to level
        //hard coded for now (5 weak points)
        lungArea = GameObject.Find("LungArena");
        weakPointsNum = level_Manager.getNumWeakPoints(level);
        generateWeakPoints(weakPointsNum,lungArea);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void generateWeakPoints(int numObjects, GameObject area)
    {
        //terrain object
        Terrain terrain = area.GetComponent<Terrain>();

        //size of area
       Vector3 terrainSize = terrain.terrainData.size;
 
        //createP p= delegat
        // Random rand = new Random();
      //left lung
            for (int i = 0; i < numObjects; i++)
            {
                float xPos = Random.Range(0.18f * terrainSize.x, 0.73f * terrainSize.x);
                float zPos = Random.Range(0.014f * terrainSize.z, 0.33f * terrainSize.z);
                float yPos = Random.Range(-14.5f, -14f); // terrain.terrainData.GetInterpolatedHeight(xPos, zPos);
                Vector3 pos = new Vector3();
                RaycastHit hit;
                Ray ray = new Ray(new Vector3(xPos, 0.1f * terrainSize.y, zPos), Vector3.down);
                if (terrain.GetComponent<TerrainCollider>().Raycast(ray, out hit, 2.0f * terrainSize.y))
                {
                    pos = hit.point;
                    Debug.Log("Hit point: " + hit.point);
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    cube.AddComponent<Collider>();
                    cube.transform.localScale = new Vector3(35, 35, 35);
                    cube.transform.localPosition = pos;//new Vector3(xPos,yPos-80f,zPos);
               // weakPoints[i] = cube;
            }
                else { i--; }
            }

            //right lung
        for (int i = 0; i < numObjects; i++)
        {
            float xPos = Random.Range(0.20f * terrainSize.x, 0.77f * terrainSize.x);
            float zPos = Random.Range(0.50f * terrainSize.z, 0.78f * terrainSize.z);
            float yPos = Random.Range(-14.5f, -14f); // terrain.terrainData.GetInterpolatedHeight(xPos, zPos);
            Vector3 pos = new Vector3();
            RaycastHit hit;
            Ray ray = new Ray(new Vector3(xPos, 0.1f * terrainSize.y, zPos), Vector3.down);
            if (terrain.GetComponent<TerrainCollider>().Raycast(ray, out hit, 2.0f * terrainSize.y))
            {
                pos = hit.point;
                Debug.Log("Hit point: " + hit.point);
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                cube.AddComponent<Collider>();
                cube.transform.localScale = new Vector3(35, 35, 35);
                cube.transform.localPosition = pos;
               // weakPoints[i]=cube;
                //new Vector3(xPos,yPos-80f,zPos);
            }
            else { i--; }
        }
    }
}
