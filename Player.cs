using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

    int health = 100;
    public bool damaged = false;
    public bool invunerable = false;
    public bool levelFinished = false;

    public Rigidbody2D rb2d;

    float maxSpeed;
    float accel;

    float jumpForce;

    public bool grounded;
    public bool wallJumpLeft;
    public bool wallJumpRight;
    bool hasSideJumped = false;

    bool allowShot = true;

    int jumpTimer = 0;
    int sideJumpTimer = 0;

    public Animator anim;

    public GameObject projectile;

    // Use this for initialization
    void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        maxSpeed = 4f;
        accel = 0.5f;
        jumpForce = 150f;

        anim = gameObject.GetComponent<Animator>();
	}

    public void TakeDamage(Vector2 dir, int dmg)
    {
        if (!invunerable)
        {
            damaged = true;
            health = health - dmg;
            rb2d.velocity = new Vector2(0f, 0f);
            anim.SetBool("damaged", true);

            if(health <= 0)
            {
                GameObject c = GameObject.Find("Main Camera");
                c.GetComponent<MainCameraScript>().LevelFail();
                Destroy(gameObject);
            }

            if (dir.x > 0)
            {
                rb2d.AddForce(new Vector2(100, 170));
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                rb2d.AddForce(new Vector2(-100, 170));
                GetComponent<SpriteRenderer>().flipX = false;
            }

            invunerable = true;
            Invoke("MakeVunerable", 1f);
            Invoke("RegainControl", 0.6f);
        }
    }

    void RegainControl()
    {
        damaged = false;
        anim.SetBool("damaged", false);
    }

    void MakeVunerable()
    {
        invunerable = false;
    }

    void Shoot()
    {
        GameObject bullet;
        Vector2 facing;
        if (GetComponent<SpriteRenderer>().flipX == false) {
            facing = Vector2.right;
            bullet = Instantiate(projectile, new Vector2(rb2d.position.x+0.2f, rb2d.position.y), Quaternion.identity) as GameObject;
        } else {
            facing = Vector2.left;
            bullet = Instantiate(projectile, new Vector2(rb2d.position.x-0.2f, rb2d.position.y), Quaternion.identity) as GameObject;
        }
        allowShot = false;

        if (!grounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * 170);
        }
        

        Invoke("AllowShot", 0.6f);
        bullet.GetComponent<Rigidbody2D>().AddForce(facing * 200);
    }

    void AllowShot()
    {
        allowShot = true;
    }

    void MovementControls()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (rb2d.velocity.x > -maxSpeed && sideJumpTimer==0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x - accel, rb2d.velocity.y);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (rb2d.velocity.x < maxSpeed && sideJumpTimer == 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x + accel, rb2d.velocity.y);
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (grounded && jumpTimer == 0)
            {
                rb2d.AddForce(new Vector2(0f, jumpForce));
                jumpTimer = 1;
               
            } else if (wallJumpLeft && !hasSideJumped)
            {
                rb2d.AddForce(new Vector2(-jumpForce, jumpForce*2));
                sideJumpTimer = 1;
                hasSideJumped = true;
            } else if (wallJumpRight && !hasSideJumped)
            {
                rb2d.AddForce(new Vector2(jumpForce, jumpForce*2));
                sideJumpTimer = 1;
                hasSideJumped = true;
            }

            if (jumpTimer > 0 && jumpTimer < 15)
            {
                rb2d.AddForce(new Vector2(0f, jumpForce/10));
                jumpTimer++;
            }
        }

        if (Input.GetKeyUp(KeyCode.W)){
            jumpTimer = 0;
            hasSideJumped = false;
        }

        if (sideJumpTimer > 0)
        {
            sideJumpTimer++;
            if (sideJumpTimer == 15)
            {
                sideJumpTimer = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && allowShot){
            Shoot();
        }

        anim.SetFloat("hspeed", Mathf.Abs(rb2d.velocity.x));  

    }

    void FixedUpdate()
    {
        
        if (!damaged)
        {
            MovementControls();
        }

        anim.SetBool("inAir", !grounded);
        if (!anim.GetBool("onWall") && !anim.GetBool("damaged"))
        {
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

    public void FinishLevel()
    {
        GameObject c = GameObject.Find("Main Camera");
        c.GetComponent<MainCameraScript>().LevelComplete();
        Destroy(gameObject);    
    }

}
