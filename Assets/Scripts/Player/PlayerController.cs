using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //note: movement is very floaty; we may want to change it later

    public float moveSpeed; //records the character's movement speed
    private bool isMoving; //checks for player movement
    private Vector2 input; //checks input

    public LayerMask solidObjectsLayer; //checks layers

    private void Update()
    {
        // if player isn't moving, check for input
        if(!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical"); //stores axes in input variable

            //if statement below disallows diagonal movement; may change this later
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero) //if user inputs something that changes input value
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                //checks if layer has collision
                if (isWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }

            }

        }
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

    private bool isWalkable(Vector3 targetPos) //boolean to see if area is above or below
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null) //if layer is on top, ensure no walking
        {
            return false;
        }

        return true;
    }

}
