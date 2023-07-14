using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public Transform referenceObject;
    public float sortingOrderMultiplier = 100f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * moveSpeed;

        if (referenceObject != null)
        {
            spriteRenderer.sortingOrder = Mathf.RoundToInt(referenceObject.position.y * sortingOrderMultiplier);
        }
    }

    // Tag of the specific prefab
    public string prefabTag = "MyPrefab";
    public string prefabTag2 = "MyPrefab2";
    public string prefabTag3 = "MyPrefab3";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(prefabTag))
        {
            // Execute the action when the character collides with the specific prefab
            PerformClimb();
        }
        if (other.CompareTag(prefabTag2))
        {
            // Execute the action when the character collides with the specific prefab
            PerformWalk();
        }
        if (other.CompareTag(prefabTag3))
        {
            // Execute the action when the character collides with the specific prefab
            PerformSwim();
        }
    }

    private void PerformClimb()
    {
        // Perform your desired action here
        Debug.Log("Action executed on the specific prefab");
        transform.localScale = new Vector2(1.3f, 1.3f);
        moveSpeed = 1.5f;
        spriteRenderer.color = Color.white;
    }

    private void PerformWalk()
    {
        // Perform your desired action here
        Debug.Log("Action executed on the specific prefab");
        transform.localScale = new Vector2(1f, 1f);
        moveSpeed = 2;
        spriteRenderer.color = Color.white;
    }

    private void PerformSwim()
    {
        // Perform your desired action here
        Debug.Log("Action executed on the specific prefab");
        spriteRenderer.color = Color.cyan;
        moveSpeed = 1.5f;
    }
}
