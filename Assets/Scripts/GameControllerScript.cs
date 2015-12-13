using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

    private int noOfBGs;
    public GameObject background;
    public GameObject floor;
    public GameObject enemy;
    private GameObject player;
    private Vector3 initialBGPosition;
    private Vector3 initialFloorPosition;
    public float gap;//The space between backgrounds, should be half the width roughly
    public float floorGap;//The space between floors, should be half the width roughly


    private float enemyTimer;
    public float enemyTimerMax;

    private GameObject floor1;
    private GameObject floor2;

    private float levelTime;
    private float FinaLevelTime;
    private int batteryPower; //Have this affected by a stat e.g. camera level

    private int views; //Number of views on this video

    // Use this for initialization
    void Start ()
    {
        views = 0;
        batteryPower = Stats.stats.cameraLevel;
        enemyTimer = 0;
        initialBGPosition = new Vector3(0, 4f, 40f);
        initialFloorPosition = new Vector3(0, -2f, 0f);
        noOfBGs = Random.Range(batteryPower+5, (batteryPower+5) * 5);
        for(int i = 0; i< noOfBGs; i++)
        {
            Instantiate(background, new Vector3((i * gap) + initialBGPosition.x , initialBGPosition.y, initialBGPosition.z), this.transform.rotation);
        }
        for (int i = 0; i < noOfBGs * 5; i++)
        {
            Instantiate(floor, new Vector3(initialFloorPosition.x + (i * floorGap), initialFloorPosition.y, initialFloorPosition.z), this.transform.rotation);
        }

        levelTime = noOfBGs * (batteryPower + 1);
        player = GameObject.FindGameObjectWithTag("Player");

    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if (!((Enemy)enemy.GetComponent(typeof(Enemy))).isColliding())  //Only increment the timer if the enemy and player are not colliding 
        {
            enemyTimer += Time.deltaTime;
            if (enemyTimer >= enemyTimerMax)
            {
                //A chance to span an enemy
                if (Random.Range(0, 2) == 1)
                {
                    Vector3 playerPos = player.transform.position;
                    Instantiate(enemy, new Vector3(playerPos.x + 30f, playerPos.y, playerPos.z), this.transform.rotation);  //The 30 is an arbitrary number I could increase or decrease
                }
                enemyTimer = 0;
            }
        }

        if (levelTime > 0)
            levelTime -= Time.fixedDeltaTime;
        else
            Won();


        views += ((Stats.stats.subscribers + 1) * player.GetComponent<Player>().GetPunchesLanded() + Random.Range(0, 5)) / (noOfBGs - player.GetComponent<Player>().GetEnemiesPunched());

        GameObject.Find("Time Left").GetComponent<Text>().text = ("Time Left: " + levelTime.ToString());
        GameObject.Find("Views").GetComponent<Text>().text = ("Views: " + views.ToString());

    }

    public void Won()
    {
        FinaLevelTime = noOfBGs * batteryPower; //The initial time
        EndLevel();
    }

    public void Lost()
    {
        FinaLevelTime = (noOfBGs * batteryPower) - levelTime;   //The initial time, minus the time left
        Stats.stats.subscribers -= (int)(Stats.stats.subscribers * 0.1);
        EndLevel();
    }

    void EndLevel()
    {
        Stats.stats.AddVideo(views, player.GetComponent<Player>().GetEnemiesPunched(), FinaLevelTime);
        Stats.stats.CountViews();
        Stats.stats.CalculateMoney(views);
        Stats.stats.subscribers += (int)(views / Random.Range(10, 50));
        Application.LoadLevel("menu");

    }
}
