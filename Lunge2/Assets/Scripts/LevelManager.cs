using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.IO;

public class LevelManager : MonoBehaviour {

    
    public GameObject spawnPoint;
    LungManager lung;
    LevelContainer levelcontainer;
    int totalLevel;
    Level current_Level;
    string file = "Data/LevelData.xml";

    //initialize
    public void Start()
    {
        lung = gameObject.AddComponent<LungManager>();
        spawnPoint = GameObject.Find("EnemySpawn");
        levelcontainer = LevelContainer.Load(Path.Combine(Application.dataPath,file));
        //get current level using level number, first level is 0
         current_Level = levelcontainer.getLevel(0);
        totalLevel = levelcontainer.getTotalLevels();
        lung.init(current_Level);  
    }

	public String initNextLevel (int levelNum)
    {
        //if there is a next level then load it
        if (levelNum < totalLevel)
        {
            levelNum += 1;
            current_Level = levelcontainer.getLevel(levelNum);
            lung.init(current_Level);
            return "Next Level";
        }

        //else the game is done
        else { return "Game End"; }

    }

    //class level will in instantiated using serializable file with attributes
    //   [Serializable]
    [Serializable()]
    public class Level
    {
        [System.Xml.Serialization.XmlElement("level")]
        public int level { get; set; } //level number
        [System.Xml.Serialization.XmlElement("weakPoints")]
        public int weakPoints { get; set; } //number of weakpoints
        [System.Xml.Serialization.XmlElement("waves")]
        public int waves { get; set; } //number of waves
        [System.Xml.Serialization.XmlElement("enemiesPerWave")]
        public int enemiesPerWave { get; set; } //number of enemies per wave
        [System.Xml.Serialization.XmlElement("enemyStrength")]
        public int enemyStrength { get; set; } //enemy strength


    }


    [Serializable(),XmlRoot("LevelContainer")]
    public class LevelContainer

    {    

        [XmlArray("Levels"), XmlArrayItem("Level", typeof(Level))]
        public Level[] Levels;

        public void save() { }

        public static LevelContainer Load(string path)
        {
            var serializer = new XmlSerializer(typeof(LevelContainer));

            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as LevelContainer;
            }
        }

        //get level object
        public Level getLevel(int level_num)
        {
                            //level index
            return Levels[level_num];
        }

        public int getTotalLevels()
        {
            return Levels.Length;
        }
        
    }
	
}
