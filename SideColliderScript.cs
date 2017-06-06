using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideColliderScript : MonoBehaviour {

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
        if (!player.damaged)
        {
            if(col.tag == "Ground")
            {
                player.rb2d.velocity = new Vector2(0f, 0f);
                player.wallJumpLeft = true;
                player.anim.SetBool("onWall", true);
                player.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (!player.damaged)
        {
            if (col.tag == "Ground")
            {
                player.rb2d.velocity = new Vector2(0f, 0f);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {

        if (col.tag == "Ground")
        {
            player.wallJumpLeft = false;
            player.anim.SetBool("onWall", false);
        }
    }

}
