using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LungManager : MonoBehaviour
{

    public int maxLungHealth = 100;
    public static int lungHealth;
    public int current_level;
    public int weakPointsNum;
    public List<GameObject> weakPoints;
    public GameObject lungArea;
<<<<<<< HEAD
    public GameObject spawnPoint;
    Object weakPointPrefab;

=======
    LevelManager LM;
    //load enemy prefabs
    Object weakPointPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefab/weakPoint.prefab", typeof(GameObject));
    Object polPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefabs/Pollution.prefab", typeof(GameObject));
    Object pollenPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefabs/Pollen.prefab", typeof(GameObject));
    Object smokePrefab = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefabs/SmokeCloud.prefab", typeof(GameObject));

    //waves of enemies
    public static int wavesLeft = 0;
    public static int enemiesperwave = 0;
    //enemies per wave
    public static int enemiesLeft = 0;
>>>>>>> refs/remotes/origin/master
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
    void Awake()
    {
         weakPointPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefab/weakPoint.prefab", typeof(GameObject));
    }

    public void init(LevelManager.Level level)
    {
        LM = gameObject.GetComponent<LevelManager>();
        lungHealth = maxLungHealth;
        current_level = level.level;
        //initialize weakPoints according to level
        lungArea = GameObject.Find("LungArena");
        //  weakPointsNum = level_Manager.getNumWeakPoints(level);
        generateWeakPoints(level.weakPoints,lungArea);
        wavesLeft = level.waves;
        enemiesLeft = level.enemiesPerWave;
        enemiesperwave = enemiesLeft;
        //initialize the first wave of enemies
        initWave();
    }


<<<<<<< HEAD
=======
    // Update is called once per frame
    void Update()
    {
         //keep track of number of enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesLeft = enemies.Length;
        if (enemiesLeft == 0)
        {   //if there are no more waves and enemies left, start next level or end game
            if (wavesLeft == 0)
            {
                //end game and/or load next level
                //get level object
                //response from levelmanager, if next level is started or game ends
                string response = LM.initNextLevel(current_level);
                Debug.Log(response);

}
            //if there are waves left then init next wave
            else if (wavesLeft > 1)
            {
                //load next wave
                initWave();

            }
        }

    }
    //initiliaze the wave of enemies
    public void initWave()
    {
        
        //array of spawn points
        GameObject[] pos = GameObject.FindGameObjectsWithTag("spawnpoint");

        //spawn enemies and decrement waves num
        List<Enemy> enemy=new List<Enemy>();

        for (int i = 0; i < enemiesperwave; i++)
        {
            //random enemy type
            int enemyType =Random.Range(0,2);

            //three random types of enemies
            //pollution
            if (enemyType == 0)
            {
                GameObject pol = (GameObject)Instantiate(polPrefab, pos[Random.Range(0, pos.Length - 1)].transform.position, Quaternion.identity);
                pol.AddComponent<Pollution>();
            }
            //pollen
            else if (enemyType == 1)
            {
                GameObject pol = (GameObject)Instantiate(pollenPrefab, pos[Random.Range(0, pos.Length - 1)].transform.position, Quaternion.identity);
                pol.AddComponent<PollenParticle>();
            }
            //smoke
            else
            {
                GameObject smoke = (GameObject)Instantiate(smokePrefab, pos[Random.Range(0, pos.Length - 1)].transform.position, Quaternion.identity);
                smoke.AddComponent<SmokeCloud>();
            }
           // enemy[i]=gameObject.GetComponent<Pollution>();
        }
        //decrement waves
        wavesLeft--;
>>>>>>> refs/remotes/origin/master

    }
    //generate weak points in lung based on level
    public void generateWeakPoints(int weakPointsNum, GameObject area)
    {
        //terrain object
        Terrain terrain = area.GetComponent<Terrain>();

        //size of area
        Vector3 terrainSize = terrain.terrainData.size;

        //left lung
      //  Vector3 scale = new Vector3(35, 35, 35);
        for (int i = 0; i < weakPointsNum; i++)
        {
            float xPos = Random.Range(0.18f * terrainSize.x, 0.73f * terrainSize.x);
            float zPos = Random.Range(0.014f * terrainSize.z, 0.33f * terrainSize.z);
            //float yPos = Random.Range(-14.5f, -14f); // terrain.terrainData.GetInterpolatedHeight(xPos, zPos);
            Vector3 pos = new Vector3();
            RaycastHit hit;
            Ray ray = new Ray(new Vector3(xPos, 0.1f * terrainSize.y, zPos), Vector3.down);
            if (terrain.GetComponent<TerrainCollider>().Raycast(ray, out hit, 2.0f * terrainSize.y))
            {
                pos = hit.point;
<<<<<<< HEAD
                
                Instantiate(weakPointPrefab, pos, Quaternion.identity); }// weakPoints.Add(weakPoint);
=======
                // Debug.Log("Hit point: " + hit.point);

                // GameObject weakPoint = new GameObject("weakpoint"+1,typeof(sphere.type));
                //  weakPoint.transform.localScale

                // weakPoint.transform.localPosition = pos;//new Vector3(xPos,yPos-80f,zPos);
                // weakPoints[i] = weakPoint;
                GameObject weakPoint = (GameObject)Instantiate(weakPointPrefab, pos, Quaternion.identity);
                weakPoint.AddComponent<WeakPoint>();

            }// weakPoints.Add(weakPoint);
>>>>>>> refs/remotes/origin/master

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
                //new Vector3(xPos,yPos-80f,zPos);
            }
            else { i--; }
        }
    }

<<<<<<< HEAD

    public void DamageLung(int amount)
    {
        Debug.Log("damaged");
        lungHealth -= amount;
=======
    //weakpoint collider to take damage from enemies
    public class WeakPoint : MonoBehaviour
    {

        //takes damage

        public void takeDamage(int dmg)
        {
            LungManager.lungHealth -= dmg;
            Debug.Log("health: " + lungHealth);
        }
        //void OnTriggerEnter(Collider other)
        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Enemy e = other.gameObject.GetComponent<Enemy>();

                {
                    takeDamage(e.AttackDamage);

                }
            }
        }

>>>>>>> refs/remotes/origin/master
    }

}
