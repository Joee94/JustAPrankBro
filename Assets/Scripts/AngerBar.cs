using UnityEngine;
using System.Collections;

public class AngerBar : MonoBehaviour {

    private float maxAnger;
    private float anger;

	// Use this for initialization
	void Start ()
    {
        anger = 0;
        maxAnger = transform.parent.gameObject.GetComponentInParent<Enemy>().getMaxAnger();
        transform.localScale = (new Vector3(0, 1f, 1f));
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (anger > maxAnger)
            anger = maxAnger;
        if (anger < 0)
            anger = 0;
        anger = transform.parent.gameObject.GetComponentInParent<Enemy>().getAnger();
        transform.localScale = (new Vector3(anger/maxAnger, 1f, 1f));
	}
}
