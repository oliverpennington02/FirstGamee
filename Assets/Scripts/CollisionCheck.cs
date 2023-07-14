using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public GameObject prefabToCheck;

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag(prefabToCheck.tag) && collider.gameObject != gameObject)
            {
                Destroy(collider.gameObject);
            }
        }
    }
}
