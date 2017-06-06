using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Destroy", 3f);
	}

    void Destroy()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "PlayerHitbox")
        {
            Player p = col.gameObject.GetComponentInParent<Player>();
            Vector2 v = gameObject.GetComponent<Rigidbody2D>().velocity;
            p.TakeDamage(v,20);

            Destroy(gameObject);
        }
        else if (col.tag == "Ground" || col.tag == "Projectile")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
		
	}

}
