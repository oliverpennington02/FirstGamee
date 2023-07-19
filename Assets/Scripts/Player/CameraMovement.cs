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
    public GameObject yourPrefab;
    public GameObject raycastOrigin;
    public DemoScript demoScript;
    public bool canPlace;
    public InventoryItem inventoryItem;
    public InventoryManager inventoryManager;
    public InteractablePrefab interactablePrefab;
    private bool isInteracting = false;

    private void Start()
    {
        //retrieve components
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact")) // Change "Interact" to the name of your input button for interaction (e.g., "Space")
        {
            isInteracting = true; // Set the flag to indicate that the interaction button is pressed
            Interact(); // Call the Interact function on the first frame when the button is pressed
        }
        if (Input.GetMouseButtonDown(0)) // Change "Interact" to the name of your input button for interaction (e.g., "Space")
        {
            Hit(); // Call the Interact function on the first frame when the button is pressed
        }
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
    }
    public void Hit()
{
    Vector3 interactionPosition = raycastOrigin.transform.position + new Vector3(lastMovementDirection.x, lastMovementDirection.y, 0f).normalized * interactionOffset;
    RaycastHit2D hit = Physics2D.Raycast(interactionPosition, lastMovementDirection, interactDistance, interactableLayer);

    Debug.DrawLine(interactionPosition, interactionPosition + new Vector3(lastMovementDirection.x, lastMovementDirection.y, 0f) * interactDistance, Color.red, 1f);

    if (hit.collider != null)
    {
        Debug.Log("Collider Hit");
        InteractablePrefab interactable = hit.collider.GetComponent<InteractablePrefab>();
        if (interactable != null)
        {
            interactable.Interact();
        }
    }
    else
    {
        Debug.Log("Didnt Hit an interactable object.");
    }
}
    public void Interact()
{
    if ( isInteracting)
    {
        Item receivedItem = inventoryManager.GetSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log("Used item: " + receivedItem);
            Debug.Log("Interact is played");
            // Calculate the position in front of the player
            Vector3 interactionPosition = raycastOrigin.transform.position + new Vector3(lastMovementDirection.x, lastMovementDirection.y, 0f).normalized * interactionOffset;

            RaycastHit2D hit = Physics2D.Raycast(interactionPosition, lastMovementDirection, interactDistance, 1 << LayerMask.NameToLayer("world"));

        Debug.DrawLine(interactionPosition, interactionPosition + new Vector3(lastMovementDirection.x, lastMovementDirection.y, 0f) * interactDistance, Color.red, 1f);

        if (hit.collider != null)
        {
            Debug.Log("Collider Hit");
            Vector3 tileCenter = new Vector3(Mathf.Floor(hit.point.x) + 0.5f, Mathf.Floor(hit.point.y) + 0.5f, 0f); // Center of the tile
            // Instantiate your desired prefab centered on the tile
            Instantiate(yourPrefab, tileCenter, Quaternion.identity);
        }
        }
        else
        {
            Debug.Log("Didnt Use item");
        }
    
    }
    
}
}
