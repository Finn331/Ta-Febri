using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{

    public static SaveManager instance { get; private set; }

    //what we want to save
    //public int coin;
    //public int attack;
    //public int level;
    //public int health;
    public bool level_1_RewardClaimed;

    public int levelSelected;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
        // Load();
    }

    public void Load()
    {
        try
        {
            if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
                PlayerData_Storage data = (PlayerData_Storage)bf.Deserialize(file);

                // string json = System.IO.File.ReadAllText(Application.persistentDataPath + "/playerData.json");
                // PlayerData_Storage data = JsonUtility.FromJson<PlayerData_Storage>(json);

                //coin = data.coin;
                //attack = data.attack;
                //level = data.level;
                //health = data.health;

                file.Close();
            }
            else
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

                PlayerData_Storage data = new PlayerData_Storage();

                //data.coin = 0;
                //data.attack = 5;
                //data.level = 1;
                //data.health = 100;

                Debug.Log("Save Game");

                bf.Serialize(file, data);
                file.Close();

                // string json = JsonUtility.ToJson(data);
                // File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);

                //coin = data.coin;
                //attack = data.attack;
                //level = data.level;
                //health = data.health;
            }
        }
        catch
        {
            Debug.Log("c");
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage data = new PlayerData_Storage();

        //data.coin = coin;
        //data.attack = attack;
        //data.level = level;
        //data.health = health;

        // string json = JsonUtility.ToJson(data);
        // File.WriteAllText(Application.persistentDataPath + "playerData.json", json);

        Debug.Log("Save Game");

        bf.Serialize(file, data);
        file.Close();
    }

    public void SelectLevel(int level)
    {
        levelSelected = level;
    }

    //dont forget add this too from line no 10
    [Serializable]
    class PlayerData_Storage
    {
        public int isRewardClaimed;
        //public int coin;
        //public int attack;
        //public int level;
        //public int health;
    }

    public void SetRewardClaimed_Level_1(bool claimed)
    {
        level_1_RewardClaimed = claimed;

        if (level_1_RewardClaimed)
        {
            Debug.Log("Reward claimed!");
            // Lakukan tindakan lain yang sesuai setelah klaim reward
        }
    }
}