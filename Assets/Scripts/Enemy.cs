using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float maxAnger;
    private float anger;
    private float punchesReceived;
    public float angerMultiplyer;
    private float timer;
    public float initialTimer;

    private bool collided;

	// Use this for initialization
	void Start ()
    {
        timer = 0;
        anger = 0;
        punchesReceived = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (collided)
            timer += Time.deltaTime;

        if (anger < maxAnger && anger >= 0)
        {
            anger += Time.deltaTime * punchesReceived * angerMultiplyer;
        }

        if(anger >= maxAnger)
        {
            Debug.Log("Punch Back");
        }


        if(timer >= initialTimer && punchesReceived == 0)
        {
            fightWon();
        }
    }

    public void receivePunch()
    {
        punchesReceived += 1;
    }

    public void calmDown(float amount)
    {
        if (anger - amount > 0)
            anger -= amount;
        else
            fightWon();

    }

    public float getMaxAnger()
    {
        return maxAnger;
    }

    public float getAnger()
    {
        return anger;
    }

    void fightWon()
    {
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        collided = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        collided = false;
    }
}
