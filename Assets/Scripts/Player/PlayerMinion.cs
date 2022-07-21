using System.Collections;
using UnityEngine;

public class PlayerMinion : PlayerUnit, ISubcribers
{
    private void OnEnable()
    {
        InitializeVariables();
        InitializeModel();
        SubscribeEvent();
    }

    private void OnDisable()
    {
        UnsubscribeEvent();
    }

    public void SubscribeEvent()
    {
        playerMain.OnPickedUpNewWeapon += InitializeWeapon;
        playerMain.OnReachedFinishLine += ChangeRigidBodyType;
    }

    public void UnsubscribeEvent()
    {
        playerMain.OnPickedUpNewWeapon -= InitializeWeapon;
        playerMain.OnReachedFinishLine -= ChangeRigidBodyType;
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
    }

    private void ChangeRigidBodyType(GameObject[] gameObjects)
    {
        //rb.isKinematic = true;
    }

    private void Move()
    {
        controller.Move(playerMain.MoveSpeed * Time.deltaTime * playerMain.Direction.normalized);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RightWall"))
        {
            playerMain.HasTouchedRightWall = true;
        }

        if (collision.gameObject.CompareTag("LeftWall"))
        {
            playerMain.HasTouchedLeftWall = true;
        }
        if (collision.gameObject.CompareTag("PlayerMinion"))
        {
            touched = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("RightWall"))
        {
            playerMain.HasTouchedRightWall = true;
        }

        if (collision.gameObject.CompareTag("LeftWall"))
        {
            playerMain.HasTouchedLeftWall = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("RightWall"))
        {
            playerMain.HasTouchedRightWall = false;
        }

        if (collision.gameObject.CompareTag("LeftWall"))
        {
            playerMain.HasTouchedLeftWall = false;
        }

        if (collision.gameObject.CompareTag("PlayerMinion"))
        {
            touched = false;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject);
        }
    }
}
