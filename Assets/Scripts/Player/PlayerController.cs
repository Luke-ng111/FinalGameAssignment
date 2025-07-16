using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //note: movement is very floaty; we may want to change it later

    public float moveSpeed; //records the character's movement speed
    private bool isMoving; //checks for player movement
    private Vector2 input; //checks input

    //checks layers
    public LayerMask solidObjectsLayer;
    public LayerMask interactables;

    private void Update()
    {
        // if player isn't moving, check for input
        if(!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical"); //stores axes in input variable

            //if statement below disallows diagonal movement; may change this later
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                Vector3 targetPos = transform.position + new Vector3(input.x, input.y, 0f);

                // Optional: round to grid
                targetPos = new Vector3(Mathf.Round(targetPos.x), Mathf.Round(targetPos.y), 0f);

                if (isWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }

            }

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
        }

    }

    void Interact()
    {
        Debug.Log("interaction logged!");
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) //if the target position minus the movement value has a difference, movement is detected
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime); //moves the character
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    private bool isWalkable(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, targetPos);


        Vector2 boxSize = new Vector2(0.8f, 0.8f); // Adjust to your player's size
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxSize, 0f, direction, distance, solidObjectsLayer);

        return hit.collider == null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, transform.forward);
    }

}
