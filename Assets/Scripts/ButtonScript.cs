using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    public Button networkButton;
    public Button cameraButton;

    private int cameraMuliplier = 1337;

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Just some random-ish multipliers so people
        if (Stats.stats.totalViews > ((Stats.stats.networkLevel +1)* Mathf.Pow(9, Stats.stats.networkLevel)) && Stats.stats.networkLevel < 51)
            networkButton.interactable = true;
        else
            networkButton.interactable = false;


        if ((Stats.stats.money > ((Stats.stats.cameraLevel +1) * cameraMuliplier)) && Stats.stats.cameraLevel < 51)
            cameraButton.interactable = true;
        else
            cameraButton.interactable = false;

        networkButton.GetComponentInChildren<Text>().text = ("Upgrade Network: \n" + (Stats.stats.networkLevel +1) * Mathf.Pow(9, Stats.stats.networkLevel)).ToString() + " views";
        cameraButton.GetComponentInChildren<Text>().text = ("Buy New Camera: \n£" + (Stats.stats.cameraLevel+1) * cameraMuliplier).ToString();
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
