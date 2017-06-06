using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {

    public Rigidbody2D rb2d;
    Animator anim;
    public bool grounded;
    int action = 1;

    float speed;

    public GameObject projectile;

    Vector2 facing = new Vector2();

    bool dead = false;
    

	// Use this for initialization
	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        speed = 1;

        InvokeRepeating("AI", Random.Range(0f,3f), 2.0f);
    }

    void AI()
    {
        action = Random.Range(1, 4);
    }

    void Fire()
    {
        GameObject bullet = Instantiate(projectile, new Vector2(rb2d.position.x, rb2d.position.y + 0.3f), Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().AddForce(facing * 200);
    }
	
    public void Kill()
    {
        dead = true;
        Destroy(gameObject.transform.GetChild(0).gameObject);
        rb2d.velocity = new Vector2(0f, 0f);
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        rb2d.constraints = RigidbodyConstraints2D.None;
        rb2d.AddForce(new Vector2(50, 30));
        rb2d.AddTorque(-100f);
        Invoke("DestroyRobot", 1.5f);
    }

    void DestroyRobot()
    {
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (!dead)
        {

            if (action == 1)
            {
                Debug.DrawRay(new Vector2(transform.position.x + 0.25f, transform.position.y), new Vector2(0f, -0.5f));
                RaycastHit2D edgeLook = Physics2D.Raycast(new Vector2(transform.position.x + 0.25f, transform.position.y), new Vector2(0f, -0.5f),1f);
                if (edgeLook.collider == null || edgeLook.collider.tag != "Ground")
                {
                    action = 2;
                }
                else
                {
                    rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                    facing = Vector2.right;
                }

            }
            else if (action == 2)
            {
                RaycastHit2D edgeLook = Physics2D.Raycast(new Vector2(transform.position.x - 0.25f, transform.position.y), new Vector2(0f, -0.5f),1f);
                if (edgeLook.collider == null || edgeLook.collider.tag != "Ground")
                {
                    action = 1;
                } else {
                    rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
                    facing = Vector2.left;
                }

            }
            else
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                Fire();
                anim.SetTrigger("Fire");
                action = Random.Range(1, 3);
            }


            if (rb2d.velocity.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (rb2d.velocity.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }


}
