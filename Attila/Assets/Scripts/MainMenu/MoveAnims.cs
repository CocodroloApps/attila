using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnims : MonoBehaviour
{
    public float speed = 5f;
    public float maxSpeed = 10f;
    public GameObject hun;
    public GameObject roman;

    private Rigidbody2D hunRb2d;
    private Animator hunAnim;
    private Animator romanAnim;
    private float hunDirection;

    // Start is called before the first frame update
    void Start()
    {
        hunDirection = 0;
        hunRb2d = hun.GetComponent<Rigidbody2D>();
        hunAnim = hun.GetComponent<Animator>();
        romanAnim = roman.GetComponent<Animator>();
        StartCoroutine(StartIdle());
    }

    IEnumerator StartIdle()
    {
        yield return new WaitForSeconds(1f);        
        hunAnim.SetBool("Walk", true);
        StartCoroutine(StartWalkR());
    }

    IEnumerator StartWalkR()
    {
        yield return new WaitForSeconds(1f);
        hunDirection = 0.3f;        
    }

    private void OnTriggerEnter2D (Collider2D hit)
    {
        hunRb2d.velocity = new Vector2(0, 0);
        if (hit.gameObject.name == "HunLimitR")
        {
            hunDirection = -0.3f;
        }
        if (hit.gameObject.name == "HunLimitL")
        {
            hunDirection = 0.3f;
        }
        if (hit.gameObject.name == "Roman")
        {
            hunDirection = hunDirection * -1;
            int nAux = Mathf.RoundToInt(UnityEngine.Random.Range(1f, 2f));
            if (nAux == 1)
            {
                hunAnim.SetTrigger("Attack");                
                int nAux2 = Mathf.RoundToInt(UnityEngine.Random.Range(1f, 2f));
                if (nAux2 == 1)
                {
                    romanAnim.SetTrigger("Hurt");
                }                
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        hunRb2d.AddForce(Vector2.right * speed * hunDirection);
        float limitedSpeed = Mathf.Clamp(hunRb2d.velocity.x, -maxSpeed, maxSpeed);
        hunRb2d.velocity = new Vector2(limitedSpeed, hunRb2d.velocity.y);
    }
}
