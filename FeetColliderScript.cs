using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetColliderScript : MonoBehaviour {

    Player player;

	// Use this for initialization
	void Start () {
        player = gameObject.GetComponentInParent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground" || col.tag == "Spikes")
        {
            player.grounded = true;
            player.rb2d.velocity = new Vector2(player.rb2d.velocity.x, 0);
        }
        
    }

    void OnTriggerExit2D(Collider2D col)
    {

            player.grounded = false;
        
    }

}
