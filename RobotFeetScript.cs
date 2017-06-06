using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFeelScript : MonoBehaviour
{

    Robot enemy;

    // Use this for initialization
    void Start()
    {
        enemy = gameObject.GetComponentInParent<Robot>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        enemy.grounded = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        enemy.grounded = false;
    }

}
