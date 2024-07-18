using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

    public bool level_1_RewardClaimed;
    public bool level_1_completed;
    public int levelSelected;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
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

                level_1_RewardClaimed = data.level_1_RewardClaimed;
                level_1_completed = data.level_1_completed;
                file.Close();
            }
            else
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

                PlayerData_Storage data = new PlayerData_Storage();

                data.level_1_RewardClaimed = false;
                data.level_1_completed = false;

                Debug.Log("Save Game");

                bf.Serialize(file, data);
                file.Close();

                level_1_RewardClaimed = data.level_1_RewardClaimed;
                level_1_completed = data.level_1_completed;
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

        data.level_1_RewardClaimed = level_1_RewardClaimed;
        data.level_1_completed = level_1_completed;
        Debug.Log("Save Game");

        bf.Serialize(file, data);
        file.Close();
    }

    public void SelectLevel(int level)
    {
        levelSelected = level;
    }

    [Serializable]
    class PlayerData_Storage
    {
        public bool level_1_RewardClaimed;
        public bool level_1_completed;
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
