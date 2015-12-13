using System;
using UnityEngine;

[Serializable]
public class Video {

    public int views { get; set; }
    public int peoplePunched { get; set; }
    public float time { get; set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void initValues(int views, int peoplePunched, float time)
    {
        this.views = views;
        this.peoplePunched = peoplePunched;
        this.time = time;
    }
}
