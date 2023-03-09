using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform castPos;
    public Transform enemySight;
    public float castDist;

    private Rigidbody2D rb;
    public float chaseSpeed;
    public float moveSpeed;
    public bool LookRight;
    public LayerMask ground;

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (target == null)
        {
            if (IsHittingWall() || IsNearEdge())
            {
                if (LookRight)
                {
                    this.transform.localScale = new Vector3(-0.6f, 0.6f, 0.6f);
                    LookRight = false;
                }
                else if (!LookRight)
                {
                    this.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                    LookRight = true;
                }
            }

            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            if (!LookRight)
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
        }
        else if (target != null)
        {
            ChaseTarget();
        }
    }

    private void ChaseTarget()
    {
        if (Mathf.Abs(target.position.x - this.transform.position.x) > 3f)
        {
            if (target.position.x > transform.position.x)
            {
                LookRight = true;
                this.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                rb.velocity = new Vector2(chaseSpeed, rb.velocity.y);
            }
            else if (target.position.x < transform.position.x)
            {
                LookRight = false;
                this.transform.localScale = new Vector3(-0.6f, 0.6f, 0.6f);
                rb.velocity = new Vector2(-chaseSpeed, rb.velocity.y);
            }
        }

        else if (Mathf.Abs(target.position.x - this.transform.position.x) <= 3f)
        {
            rb.velocity = Vector2.zero;
        }
    }

    bool IsHittingWall()
    {
        bool value = false;

        float castRange = castDist;

        if (!LookRight)
        {
            castRange = -castDist;
        }
        else if (LookRight)
        {
            castRange = castDist;
        }

        Vector3 rayPos = castPos.position;
        rayPos.x += castRange;

        Debug.DrawLine(castPos.position, rayPos, Color.green);

        if (Physics2D.Linecast(castPos.position, rayPos, ground))
        {
            value = true;
        }
        else
        {
            value = false;
        }

        return value;
    }

    bool IsNearEdge()
    {
        bool value = true;

        float castRange = castDist;

        Vector3 rayPos = castPos.position;
        rayPos.y -= castRange;

        Debug.DrawLine(castPos.position, rayPos, Color.red);

        if (Physics2D.Linecast(castPos.position, rayPos, ground))
        {
            value = false;
        }
        else
        {
            value = true;
        }

        return value;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Mathf.Abs(collision.gameObject.transform.position.y - this.transform.position.y) < 0.3f)
            {
                target = collision.gameObject.transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            target = null;
    }
}
