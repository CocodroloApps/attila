using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowColliders : MonoBehaviour {

    private BoxCollider2D objectCollider2D;

    private void OnDrawGizmos()
    {
        objectCollider2D = GetComponent<BoxCollider2D>();

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(objectCollider2D.bounds.center, new Vector2(objectCollider2D.size.x, objectCollider2D.size.y));
    }

}
