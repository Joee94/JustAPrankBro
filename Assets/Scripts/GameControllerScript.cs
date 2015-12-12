using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

    private int noOfBGs;
    public GameObject background;
    public GameObject floor;
    public GameObject Enemy;
    private Vector3 initialBGPosition;
    private Vector3 initialFloorPosition;
    public float gap;//The space between backgrounds, should be half the width roughly
    public float floorGap;//The space between floors, should be half the width roughly

    private float enemyTimer;
    public float enemyTimerMax;

    private GameObject floor1;
    private GameObject floor2;

    // Use this for initialization
    void Start ()
    {
        enemyTimer = 0;
        initialBGPosition = new Vector3(0, 4f, 40f);
        initialFloorPosition = new Vector3(0, -2f, 0f);
        noOfBGs = Random.Range(5, 15);
        for(int i = 0; i< noOfBGs; i++)
        {
            Instantiate(background, new Vector3((i * gap) + initialBGPosition.x , initialBGPosition.y, initialBGPosition.z), this.transform.rotation);
        }
        for (int i = 0; i < noOfBGs * 5; i++)
        {
            Instantiate(floor, new Vector3(initialFloorPosition.x + (i * floorGap), initialFloorPosition.y, initialFloorPosition.z), this.transform.rotation);
        }
        

    }
	
	// Update is called once per frame
	void Update ()
    {
        enemyTimer += Time.deltaTime;
        if(enemyTimer >= enemyTimerMax)
        {
            if (Random.Range(0, 3) == 1)
            {
                Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
                Instantiate(Enemy, new Vector3(playerPos.x + 30f, playerPos.y, playerPos.z), this.transform.rotation);
            }
            enemyTimer = 0;
        }

	}
}
