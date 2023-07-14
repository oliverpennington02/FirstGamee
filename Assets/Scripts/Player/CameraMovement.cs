using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //movement
    public float moveSpeed = 5f;
    private Vector2 lastMovementDirection = Vector2.zero; // Store the last movement direction
    float moveHorizontal;
    float moveVertical;
    //rigidbody
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    //sorting layer
    public Transform referenceObject;
    public float sortingOrderMultiplier = 100f;

    public float interactDistance = 2f;
    public float interactionOffset = 1f; // Offset in front of the player
    public LayerMask interactableLayer;

    private void Start()
    {
        //retrieve components
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        /*Vector2 movementDirection = new Vector2(moveHorizontal, moveVertical).normalized;

        if (movementDirection != Vector2.zero)
        {
            lastMovementDirection = movementDirection;
        }
        //check if space is pressed every frame
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //do this function
            Interact();
            Debug.Log("Space is pressed");
        }*/
    }

    private void FixedUpdate()
    {
        //grab movement into float
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        //use rb to add velocity based on movement floats
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * moveSpeed;
        //change sorting order if your y position isnt null
        if (referenceObject != null)
        {
            spriteRenderer.sortingOrder = Mathf.RoundToInt(referenceObject.position.y * sortingOrderMultiplier);
        }

        Vector2 movementDirection = new Vector2(moveHorizontal, moveVertical).normalized;

        if (movementDirection != Vector2.zero)
        {
            lastMovementDirection = movementDirection;
        }
        //check if space is pressed every frame
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //do this function
            Interact();
            Debug.Log("Space is pressed");
        }
    }

    private void Interact()
    {
        Debug.Log("Interact is played");
        // Calculate the position in front of the player
        Vector3 interactionPosition = transform.position + new Vector3(lastMovementDirection.x, lastMovementDirection.y, 0f).normalized * interactionOffset;

        RaycastHit2D hit = Physics2D.Raycast(interactionPosition, lastMovementDirection, interactDistance, interactableLayer);

        Debug.DrawLine(interactionPosition, interactionPosition + new Vector3(lastMovementDirection.x, lastMovementDirection.y, 0f) * interactDistance, Color.red, 1f);

        if (hit.collider != null)
        {
            Debug.Log("Collider Hit");
            InteractablePrefab interactable = hit.collider.GetComponent<InteractablePrefab>();
            if (interactable != null)
            {
                interactable.Interact();
                // Optionally, you can pass additional values by calling interactable.Interact(5) or any other desired value
            }
        }
    }
}
