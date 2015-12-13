using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;

    private bool enemyCollided;
    private GameObject enemy;
    private int punchesLanded;
    private int enemiesPunched;
    private int pranks;

    public float calmAmount; //Maybe make this an upgrade or something
    public GameObject speechBubble;
    private List<GameObject> SpeechBubble;

    public Sprite walking;
    public Sprite punching;
    public Sprite surrendering;

    private int animationState;

    // Use this for initialization
    void Start ()
    {
        punchesLanded = 0;
        enemyCollided = false;
        SpeechBubble = new List<GameObject>();
        animationState = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(animationState == 0)
            gameObject.GetComponent<SpriteRenderer>().sprite = walking;
        if (animationState == 1)
            gameObject.GetComponent<SpriteRenderer>().sprite = punching;
        if (animationState == 2)
            gameObject.GetComponent<SpriteRenderer>().sprite = surrendering;

        //Move right until collided with an enemy
        if (enemyCollided == false)
        {
            transform.Translate(new Vector3(Time.deltaTime * speed, 0, 0));
        }
        //Stop to Attack etc.
        else
        {
            if (Input.GetButtonUp("Attack"))    //Attack
            {
                animationState = 1;
                Debug.Log("Attack");
                Attack();
            }

            else if (Input.GetButtonUp("JustAPrankBro"))    //Yell "It's just a prank bro"
            {
                animationState = 2;
                Debug.Log("JustAPrankBro");
                JustAPrankBro();
            }
        }
            
	}
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyCollided = true;
            enemy = other.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyCollided = false;
            pranks = 0;
            for (int i= 0; i< SpeechBubble.Count; i++)
            {
                Destroy(SpeechBubble[i]);
            }
            animationState = 0;
        }
    }

    void Attack()
    {
        punchesLanded += 1;
        ((Enemy)enemy.GetComponent(typeof(Enemy))).receivePunch();
    }

    void JustAPrankBro()
    {
         pranks += 1;
        Debug.Log(pranks);
        SpeechBubble.Add((GameObject)Instantiate(speechBubble, new Vector3(transform.position.x + 1.5f + (pranks / Random.Range(2, 10)), (transform.position.y + 4.5f) +(pranks /  Random.Range(2, 10)), transform.position.z), transform.rotation));
        ((Enemy)enemy.GetComponent(typeof(Enemy))).calmDown(calmAmount);
    }

    public int GetPunchesLanded()
    {
        return punchesLanded;
    }

    public int GetEnemiesPunched()
    {
        return enemiesPunched;
    }

    public void increaseEnemiesPunched()
    {
        enemiesPunched += 1;
    }
}
