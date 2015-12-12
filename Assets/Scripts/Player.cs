using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;

    private bool enemyCollided;
    private GameObject enemy;

    public float calmAmount; //Maybe make this an upgrade or something

	// Use this for initialization
	void Start ()
    {
        enemyCollided = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
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
                Debug.Log("Attack");
                Attack();
            }

            else if (Input.GetButtonUp("JustAPrankBro"))    //Yell "It's just a prank bro"
            {
                Debug.Log("JustAPrankBro");
                JustAPrankBro();
            }
        }
            
	}
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyCollided = true;
            enemy = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyCollided = false;
        }
    }

    void Attack()
    {
        ((Enemy)enemy.GetComponent(typeof(Enemy))).receivePunch();
    }

    void JustAPrankBro()
    {
        ((Enemy)enemy.GetComponent(typeof(Enemy))).calmDown(calmAmount);
    }
}
