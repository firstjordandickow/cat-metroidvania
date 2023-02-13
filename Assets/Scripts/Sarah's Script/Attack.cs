using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    bool attacking;
    // Start is called before the first frame update
    void Start()
    {

        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("X was pressed");
            attacking = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && attacking == true)
        {
            Destroy(collision.gameObject);
            attacking = false;
        }

    }
}

   
  
    




