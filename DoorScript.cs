using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerHitbox")
        {
            Player p = col.gameObject.GetComponentInParent<Player>();
            p.FinishLevel();
        }
    }


}
