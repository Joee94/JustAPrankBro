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

    public Sprite walking;
    public Sprite punching;

    private int animationState;

    // Use this for initialization
    void Start ()
    {
        animationState = 0;
        timer = 0;
        anger = 0;
        punchesReceived = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (animationState == 0)
            gameObject.GetComponent<SpriteRenderer>().sprite = walking;
        if (animationState == 1)
            gameObject.GetComponent<SpriteRenderer>().sprite = punching;

        if (collided)
            timer += Time.deltaTime;

        if (anger < maxAnger && anger >= 0)
        {
            anger += Time.deltaTime * punchesReceived * angerMultiplyer;
        }

        if(anger >= maxAnger)
        {
            Debug.Log("Punch Back");
            animationState = 1;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>().Lost();
        }


        if(timer >= initialTimer && punchesReceived == 0)
        {
            fightWon(true);
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
            fightWon(false);

    }

    public float getMaxAnger()
    {
        return maxAnger;
    }

    public float getAnger()
    {
        return anger;
    }

    void fightWon(bool passive)
    {
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
        if(!passive)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().increaseEnemiesPunched();
        }
        Destroy(gameObject, 1f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        collided = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        collided = false;
    }

    public bool isColliding()
    {
        return collided;
    }
}
