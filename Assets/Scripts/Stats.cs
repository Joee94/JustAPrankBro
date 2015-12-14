using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public class Stats : MonoBehaviour
{

    public int totalViews { get; set; }
    public int subscribers { get; set; }
    public float money { get; set; }
    public int cameraLevel { get; set; }        //This affects how long your videos can last.
    public float moneyPerClick { get; set; }
    public int networkLevel { get; set; }

    private List<Video> videos;

    public static Stats stats;

    // Use this for initialization
    void Awake()
    {
        if (stats == null)
        {
            DontDestroyOnLoad(gameObject);
            stats = this;
        }
        else if (stats != this)
        {
            Destroy(gameObject);
        }

            videos = new List<Video>();
    }

    public void AddVideo(int videoViews, int peoplePunched, float time)
    {
        videos.Add(new Video());
        videos[videos.Count - 1].initValues(videoViews, peoplePunched, time);
    }

    public void CountViews()
    {
        for (int i = 0; i < videos.Count; i++)
        {
            totalViews += videos[i].views;
        }
    }

    public int CountPeopleHit()
    {
        int totalPeopleHit = 0;
        for (int i = 0; i < videos.Count; i++)
        {
            totalPeopleHit += videos[i].peoplePunched;
        }
        return totalPeopleHit;
    }

    public void CalculateMoney(int views)
    {
        if(moneyPerClick > 0 && views > 0)
            money += (float)(views * moneyPerClick);
    }

    public void NetworkMoney()
    {
        moneyPerClick = (float)networkLevel / 100;
    }


    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerStats.dat");

        PlayerData data = new PlayerData();
        data.TotalViews = totalViews;
        data.Subscribers = subscribers;
        data.Money = money;
        data.CameraLevel = cameraLevel;
        data.MoneyPerClick = moneyPerClick;
        data.Videos = videos;
        data.NetworkLevel = networkLevel;
        Debug.Log(data.TotalViews);

        bf.Serialize(file, data);
        file.Close();
        
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerStats.dat"))
        {
            Debug.Log("Test");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerStats.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);

            file.Close();

            totalViews = data.TotalViews;
            subscribers = data.Subscribers;
            money = data.Money;
            cameraLevel = data.CameraLevel;
            moneyPerClick = data.MoneyPerClick;
            videos = data.Videos;
            networkLevel = data.NetworkLevel;

        }
    }

    public void LoadLevel()
    {
        Application.LoadLevel("main");
    }
    

}

[Serializable]
class PlayerData
{

    public int TotalViews { get; set; }
    public int Subscribers { get; set; }
    public float Money { get; set; }
    public int CameraLevel { get; set; }
    public float MoneyPerClick { get; set; }
    public int NetworkLevel { get; set; }
    public List<Video> Videos { get; set; }
}