using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnims2 : MonoBehaviour
{
    public float speed = 5f;
    public float maxSpeed = 10f;
    public GameObject hun;
    public GameObject roman;

    private Rigidbody2D romanRb2d;
    private Animator romanAnim;
    private Animator hunAnim;
    private float romanDirection;

    // Start is called before the first frame update
    void Start()
    {
        romanDirection = 0;
        romanRb2d = roman.GetComponent<Rigidbody2D>();
        romanAnim = roman.GetComponent<Animator>();
        hunAnim = hun.GetComponent<Animator>();
        StartCoroutine(StartIdle());
    }

    IEnumerator StartIdle()
    {
        yield return new WaitForSeconds(1f);
        romanAnim.SetBool("Walk", true);
        StartCoroutine(StartWalkL());
    }

    IEnumerator StartWalkL()
    {
        yield return new WaitForSeconds(0.5f);
        romanDirection = -0.3f;
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        Debug.Log("COLLISION");
        Debug.Log(hit.gameObject.name);
        romanRb2d.velocity = new Vector2(0, 0);
        if (hit.gameObject.name == "RomanLimitR")
        {
            romanDirection = -0.3f;
        }
        if (hit.gameObject.name == "RomanLimitL")
        {
            romanDirection = 0.3f;
        }
        if (hit.gameObject.name == "Hun")
        {
            romanDirection = romanDirection * -1;
            int nAux = Mathf.RoundToInt(UnityEngine.Random.Range(1f, 2f));
            if (nAux == 1)
            {
                romanAnim.SetTrigger("Attack");                
                int nAux2 = Mathf.RoundToInt(UnityEngine.Random.Range(1f, 2f));
                if (nAux2 == 1)
                {
                    hunAnim.SetTrigger("Hurt");
                }                    
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        romanRb2d.AddForce(Vector2.right * speed * romanDirection);
        float limitedSpeed = Mathf.Clamp(romanRb2d.velocity.x, -maxSpeed, maxSpeed);
        romanRb2d.velocity = new Vector2(limitedSpeed, romanRb2d.velocity.y);
    }
}
