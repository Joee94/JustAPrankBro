using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    public Button networkButton;
    public Button cameraButton;
    public Text millionText;

    private int cameraMuliplier = 1337;
    private bool millionaire = false;

    public int viewsPower;

    // Use this for initialization
    void Start ()
    {
        if (Stats.stats.money > 1000000)
        {
            millionaire = true;
            millionText.text = "Congratulations, you are a millionaire and you only had to hit " + Stats.stats.CountPeopleHit() + " people. What a guy";
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Just some random-ish multipliers so people
        if (Stats.stats.totalViews > ((Stats.stats.networkLevel +1)* Mathf.Pow(viewsPower, Stats.stats.networkLevel)) && Stats.stats.networkLevel < 51)
            networkButton.interactable = true;
        else
            networkButton.interactable = false;
        
        if ((Stats.stats.money > ((Stats.stats.cameraLevel +1) * cameraMuliplier)) && Stats.stats.cameraLevel < 51)
            cameraButton.interactable = true;
        else
            cameraButton.interactable = false;

        networkButton.GetComponentInChildren<Text>().text = ("Upgrade Network: \n" + (Stats.stats.networkLevel +1) * Mathf.Pow(viewsPower, Stats.stats.networkLevel)).ToString() + " views";
        cameraButton.GetComponentInChildren<Text>().text = ("Buy New Camera: \n£" + (Stats.stats.cameraLevel+1) * cameraMuliplier).ToString();

        if(millionaire)
        {
            millionText.color = Color.red;
        }

    }

    public void Load()
    {
        Stats.stats.Load();
    }

    public void Save()
    {
        Stats.stats.Save();
    }

    public void UpgradeNetworkLevel()
    {
        Stats.stats.networkLevel += 1;
        Stats.stats.NetworkMoney();
    }

    public void UpgradeCameraLevel()
    {
        Stats.stats.money -= (Stats.stats.cameraLevel+1) * cameraMuliplier;
        Stats.stats.cameraLevel += 1;
    }



}
