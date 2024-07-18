using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

    [Header("Reward Counter")]
    public int rewardPieces;

    [Header("Level 1 Progress")]
    public bool level_1_RewardClaimed;
    public bool level_1_completed;
    public int levelSelected;

    [Header("Level 2 Progress")]
    public bool level_2_RewardClaimed;
    public bool level_2_completed;

    [Header("Level 3 Progress")]
    public bool level_3_RewardClaimed;
    public bool level_3_completed;

    [Header("Level 4 Progress")]
    public bool level_4_RewardClaimed;
    public bool level_4_completed;

    [Header("Level 5 Progress")]
    public bool level_5_RewardClaimed;
    public bool level_5_completed;

    [Header("Level 6 Progress")]
    public bool level_6_RewardClaimed;
    public bool level_6_completed;

    [Header("Level 7 Progress")]
    public bool level_7_RewardClaimed;
    public bool level_7_completed;

    [Header("Level 8 Progress")]
    public bool level_8_RewardClaimed;
    public bool level_8_completed;

    [Header("Level 9 Progress")]
    public bool level_9_RewardClaimed;
    public bool level_9_completed;

    [Header("Level 10 Progress")]
    public bool level_10_RewardClaimed;
    public bool level_10_completed;

    [Header("Level 11 Progress")]
    public bool level_11_RewardClaimed;
    public bool level_11_completed;

    [Header("Level 12 Progress")]
    public bool level_12_RewardClaimed;
    public bool level_12_completed;

    [Header("Level 13 Progress")]
    public bool level_13_RewardClaimed;
    public bool level_13_completed;

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

                level_2_RewardClaimed = data.level_2_RewardClaimed;
                level_2_completed = data.level_2_completed;

                level_3_RewardClaimed = data.level_3_RewardClaimed;
                level_3_completed = data.level_3_completed;

                level_4_RewardClaimed = data.level_4_RewardClaimed;
                level_4_completed = data.level_4_completed;

                level_5_RewardClaimed = data.level_5_RewardClaimed;
                level_5_completed = data.level_5_completed;

                level_6_RewardClaimed = data.level_6_RewardClaimed;
                level_6_completed = data.level_6_completed;

                level_7_RewardClaimed = data.level_7_RewardClaimed;
                level_7_completed = data.level_7_completed;

                level_8_RewardClaimed = data.level_8_RewardClaimed;
                level_8_completed = data.level_8_completed;

                level_9_RewardClaimed = data.level_9_RewardClaimed;
                level_9_completed = data.level_9_completed;

                level_10_RewardClaimed = data.level_10_RewardClaimed;
                level_10_completed = data.level_10_completed;

                level_11_RewardClaimed = data.level_11_RewardClaimed;
                level_11_completed = data.level_11_completed;

                level_12_RewardClaimed = data.level_12_RewardClaimed;
                level_12_completed = data.level_12_completed;

                level_13_RewardClaimed = data.level_13_RewardClaimed;
                level_13_completed = data.level_13_completed;

                rewardPieces = data.rewardPieces;

                file.Close();
            }
            else
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

                PlayerData_Storage data = new PlayerData_Storage();

                data.level_1_RewardClaimed = false;
                data.level_1_completed = false;

                data.level_2_RewardClaimed = false;
                data.level_2_completed = false;

                data.level_3_RewardClaimed = false;
                data.level_3_completed = false;

                data.level_4_RewardClaimed = false;
                data.level_4_completed = false;

                data.level_5_RewardClaimed = false;
                data.level_5_completed = false;

                data.level_6_RewardClaimed = false;
                data.level_6_completed = false;

                data.level_7_RewardClaimed = false;
                data.level_7_completed = false;

                data.level_8_RewardClaimed = false;
                data.level_8_completed = false;

                data.level_9_RewardClaimed = false;
                data.level_9_completed = false;

                data.level_10_RewardClaimed = false;
                data.level_10_completed = false;

                data.level_11_RewardClaimed = false;
                data.level_11_completed = false;

                data.level_12_RewardClaimed = false;
                data.level_12_completed = false;

                data.level_13_RewardClaimed = false;
                data.level_13_completed = false;

                data.rewardPieces = 0;

                Debug.Log("Save Game");

                bf.Serialize(file, data);
                file.Close();

                level_1_RewardClaimed = data.level_1_RewardClaimed;
                level_1_completed = data.level_1_completed;

                level_2_RewardClaimed = data.level_2_RewardClaimed;
                level_2_completed = data.level_2_completed;

                level_3_RewardClaimed = data.level_3_RewardClaimed;
                level_3_completed = data.level_3_completed;

                level_4_RewardClaimed = data.level_4_RewardClaimed;
                level_4_completed = data.level_4_completed;

                level_5_RewardClaimed = data.level_5_RewardClaimed;
                level_5_completed = data.level_5_completed;

                level_6_RewardClaimed = data.level_6_RewardClaimed;
                level_6_completed = data.level_6_completed;

                level_7_RewardClaimed = data.level_7_RewardClaimed;
                level_7_completed = data.level_7_completed;

                level_8_RewardClaimed = data.level_8_RewardClaimed;
                level_8_completed = data.level_8_completed;

                level_9_RewardClaimed = data.level_9_RewardClaimed;
                level_9_completed = data.level_9_completed;

                level_10_RewardClaimed = data.level_10_RewardClaimed;
                level_10_completed = data.level_10_completed;

                level_11_RewardClaimed = data.level_11_RewardClaimed;
                level_11_completed = data.level_11_completed;

                level_12_RewardClaimed = data.level_12_RewardClaimed;
                level_12_completed = data.level_12_completed;

                level_13_RewardClaimed = data.level_13_RewardClaimed;
                level_13_completed = data.level_13_completed;

                rewardPieces = data.rewardPieces;
            }
        }
        catch
        {
            Debug.Log("Load failed.");
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage data = new PlayerData_Storage();

        data.level_1_RewardClaimed = level_1_RewardClaimed;
        data.level_1_completed = level_1_completed;

        data.level_2_RewardClaimed = level_2_RewardClaimed;
        data.level_2_completed = level_2_completed;

        data.level_3_RewardClaimed = level_3_RewardClaimed;
        data.level_3_completed = level_3_completed;

        data.level_4_RewardClaimed = level_4_RewardClaimed;
        data.level_4_completed = level_4_completed;

        data.level_5_RewardClaimed = level_5_RewardClaimed;
        data.level_5_completed = level_5_completed;

        data.level_6_RewardClaimed = level_6_RewardClaimed;
        data.level_6_completed = level_6_completed;

        data.level_7_RewardClaimed = level_7_RewardClaimed;
        data.level_7_completed = level_7_completed;

        data.level_8_RewardClaimed = level_8_RewardClaimed;
        data.level_8_completed = level_8_completed;

        data.level_9_RewardClaimed = level_9_RewardClaimed;
        data.level_9_completed = level_9_completed;

        data.level_10_RewardClaimed = level_10_RewardClaimed;
        data.level_10_completed = level_10_completed;

        data.level_11_RewardClaimed = level_11_RewardClaimed;
        data.level_11_completed = level_11_completed;

        data.level_12_RewardClaimed = level_12_RewardClaimed;
        data.level_12_completed = level_12_completed;

        data.level_13_RewardClaimed = level_13_RewardClaimed;
        data.level_13_completed = level_13_completed;

        data.rewardPieces = rewardPieces;

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

        public bool level_2_RewardClaimed;
        public bool level_2_completed;

        public bool level_3_RewardClaimed;
        public bool level_3_completed;

        public bool level_4_RewardClaimed;
        public bool level_4_completed;

        public bool level_5_RewardClaimed;
        public bool level_5_completed;

        public bool level_6_RewardClaimed;
        public bool level_6_completed;

        public bool level_7_RewardClaimed;
        public bool level_7_completed;

        public bool level_8_RewardClaimed;
        public bool level_8_completed;

        public bool level_9_RewardClaimed;
        public bool level_9_completed;

        public bool level_10_RewardClaimed;
        public bool level_10_completed;

        public bool level_11_RewardClaimed;
        public bool level_11_completed;

        public bool level_12_RewardClaimed;
        public bool level_12_completed;

        public bool level_13_RewardClaimed;
        public bool level_13_completed;

        public int rewardPieces;
    }

    public void SetRewardClaimed(int level, bool claimed)
    {
        switch (level)
        {
            case 1:
                level_1_RewardClaimed = claimed;
                break;
            case 2:
                level_2_RewardClaimed = claimed;
                break;
            case 3:
                level_3_RewardClaimed = claimed;
                break;
            case 4:
                level_4_RewardClaimed = claimed;
                break;
            case 5:
                level_5_RewardClaimed = claimed;
                break;
            case 6:
                level_6_RewardClaimed = claimed;
                break;
            case 7:
                level_7_RewardClaimed = claimed;
                break;
            case 8:
                level_8_RewardClaimed = claimed;
                break;
            case 9:
                level_9_RewardClaimed = claimed;
                break;
            case 10:
                level_10_RewardClaimed = claimed;
                break;
            case 11:
                level_11_RewardClaimed = claimed;
                break;
            case 12:
                level_12_RewardClaimed = claimed;
                break;
            case 13:
                level_13_RewardClaimed = claimed;
                break;
            default:
                Debug.Log("Invalid level");
                break;
        }
        if (claimed)
        {
            Debug.Log("Reward claimed for level " + level + "!");
            // Perform any additional actions needed after claiming a reward
        }
    }
    public void LevelCompleted(int levelComplete, bool complete)
    {
        switch (levelComplete)
        {
            case 1:
                level_1_completed = complete;
                break;
            case 2:
                level_2_completed = complete;
                break;
            case 3:
                level_3_completed = complete;
                break;
            case 4:
                level_4_completed = complete;
                break;
            case 5:
                level_5_completed = complete;
                break;
            case 6:
                level_6_completed = complete;
                break;
            case 7:
                level_7_completed = complete;
                break;
            case 8:
                level_8_completed = complete;
                break;
            case 9:
                level_9_completed = complete;
                break;
            case 10:
                level_10_completed = complete;
                break;
            case 11:
                level_11_completed = complete;
                break;
            case 12:
                level_12_completed = complete;
                break;
            case 13:
                level_13_completed = complete;
                break;
            default:
                Debug.LogWarning("Invalid level selected.");
                break;
        }
    }
}