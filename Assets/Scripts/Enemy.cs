using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float maxAnger;
    private float anger;
    private float punchesReceived;
    public float angerMultiplyer;

	// Use this for initialization
	void Start ()
    {
        anger = 0;
        punchesReceived = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        anger += Time.deltaTime * punchesReceived * angerMultiplyer;
        Debug.Log(anger);

        if(anger >= 100)
        {
            Debug.Log("Punch Back");
        }
    }

    public void receivePunch()
    {
        punchesReceived += 1;
    }

    public void calmDown(float amount)
    {
        anger -= amount;
    }

    public float getMaxAnger()
    {
        return maxAnger;
    }

    public float getAnger()
    {
        return anger;
    }
}
