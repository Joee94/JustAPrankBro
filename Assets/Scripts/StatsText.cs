using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatsText : MonoBehaviour
{
    Text totalViews;
    Text subscribers;
    Text money;
    Text cameraLevel;
    Text moneyPerClick;
    Text networkLevel;
    Stats stats;

    // Use this for initialization
    void Start()
    {
        stats = GameObject.Find("StatsManager").GetComponent<Stats>();

        totalViews = GameObject.Find("Stats/TotalViews").GetComponent<Text>();
        subscribers = GameObject.Find("Stats/Subscribers").GetComponent<Text>();
        money = GameObject.Find("Stats/Money").GetComponent<Text>();
        cameraLevel = GameObject.Find("Stats/CameraLevel").GetComponent<Text>();
        moneyPerClick = GameObject.Find("Stats/Money Per Click").GetComponent<Text>();
        networkLevel = GameObject.Find("Stats/Network Level").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {

        totalViews.text = stats.totalViews.ToString();
        subscribers.text = stats.subscribers.ToString();
        money.text = stats.money.ToString();
        cameraLevel.text = stats.cameraLevel.ToString();
        moneyPerClick.text = stats.moneyPerClick.ToString();
        networkLevel.text = stats.networkLevel.ToString();
    }
}
