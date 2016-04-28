using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LungManager : MonoBehaviour
{

    public int maxLungHealth = 100;
    public static int lungHealth;
    public int current_level;
    public int weakPointsNum;
    public List<GameObject> weakPoints;
    public GameObject lungArea;
    public GameObject[] spawnPoint;
    public Enemy[] enemyTypes;

    Object weakPointPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefab/weakPoint.prefab", typeof(GameObject));
    //waves of enemies
    private int wavesLeft = 0;
    //enemies per wave
    private int enemiesLeft = 0;

    //WeakPoints wp = new WeakPoints();


    // Use this for initialization
    /* 
    Level Object attributes
    level //level number
    weakPoints  //number of weakpoints
     waves  //number of waves
     enemiesPerWave //number of enemies per wave
    */
    //init to level

    public void init(LevelManager.Level level)
    {
        Debug.Log(level.enemiesPerWave);
       


        //enemyTypes[0] = new PollenParticle();
        //enemyTypes[1] = new Pollution();
        lungHealth = maxLungHealth;
        current_level = level.level;
        //initialize weakPoints according to level
        lungArea = GameObject.Find("LungArena");
        //  weakPointsNum = level_Manager.getNumWeakPoints(level);
        generateWeakPoints(level.weakPoints, level.waves, level.enemiesPerWave, level.enemyStrength, lungArea);
        wavesLeft = level.waves;
        enemiesLeft = level.enemiesPerWave;
        //initialize the first wave of enemies
        initWave(level.enemiesPerWave);

    }


    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesLeft = enemies.Length;
        if (enemiesLeft == 0)
        {
            if(wavesLeft ==0)
        {
            //end game and/or load next level
        }
            //next wave
            else if (wavesLeft > 1)
            {
                //load next wave

            }
        }
    }


    public void initWave(int enemiesPerWave)
    {

        //spawn enemies and decrement waves num

        wavesLeft--;

}


    public void generateWeakPoints(int weakPointsNum, int waves, int enemiesPerWave, int enemyStrength, GameObject area)
    {
        //terrain object
        Terrain terrain = area.GetComponent<Terrain>();

        //size of area
        Vector3 terrainSize = terrain.terrainData.size;

        //left lung
        Vector3 scale = new Vector3(35, 35, 35);
        for (int i = 0; i < weakPointsNum; i++)
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
                // Debug.Log("Hit point: " + hit.point);

                // GameObject weakPoint = new GameObject("weakpoint"+1,typeof(sphere.type));
                //  weakPoint.transform.localScale

                // weakPoint.transform.localPosition = pos;//new Vector3(xPos,yPos-80f,zPos);
                // weakPoints[i] = weakPoint;
                GameObject weakPoint = (GameObject)Instantiate(weakPointPrefab, pos, Quaternion.identity);                 weakPoint.AddComponent<WeakPoint>();
   }// weakPoints.Add(weakPoint);

            else { i--; }
        }


        //right lung
        for (int i = 0; i < weakPointsNum; i++)
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
                // Debug.Log("Hit point: " + hit.point);
                /* GameObject weakPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);


                 weakPoint.AddComponent<Collider>();
                 weakPoint.transform.localScale = new Vector3(35, 35, 35);
                 weakPoint.transform.localPosition = pos;
                 */
                GameObject weakPoint = (GameObject)Instantiate(weakPointPrefab, pos, Quaternion.identity);
                weakPoint.AddComponent<WeakPoint>();
                //weakPoints.Add(weakPoint);
                //new Vector3(xPos,yPos-80f,zPos);
            }
            else { i--; }
        }


    }
public class WeakPoint :MonoBehaviour
{
       
    //void OnTriggerEnter(Collider other)
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("health: " + lungHealth);
        if (other.gameObject.tag == "Enemy")
        {
            Enemy e = other.gameObject.GetComponent<Enemy>();


            {
                lungHealth -= e.AttackDamage;
                Debug.Log("health: " + lungHealth);

            }
        }
    }

}
}

