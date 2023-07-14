using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingPrefab : MonoBehaviour
{
    public Transform referenceObject;
    private SpriteRenderer spriteRenderer;
    public float sortingOrderMultiplier = 100f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (referenceObject != null)
        {
            spriteRenderer.sortingOrder = Mathf.RoundToInt(referenceObject.position.y * sortingOrderMultiplier);
        }
    }
}
