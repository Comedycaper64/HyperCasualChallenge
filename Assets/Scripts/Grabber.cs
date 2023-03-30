using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    [SerializeField] private ColourType colourType;

    [SerializeField] private GrabberHead grabberHead;
    [SerializeField] private Collider2D activateCollider;

    [SerializeField] private Transform startLocation;
    [SerializeField] private Transform endLocation;

    [SerializeField] private float outwardsSpeed;
    [SerializeField] private float inwardsSpeed;
    //Grabber type variable, which is set in grabber head

    private bool movingOut;
    private bool movingIn;
    private bool canActivate = true;


    private void Awake() 
    {
        grabberHead.SetColourType(colourType);    
    }

    private void Update() 
    {
        if (!GameStartManager.isGameStarted) {return;}

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (activateCollider.OverlapPoint(mousePosition) && canActivate)
            {
                ActivateGrabber();
            }
        }

        if (movingOut)
        {
            grabberHead.transform.position = Vector3.MoveTowards(grabberHead.transform.position, endLocation.position, outwardsSpeed * Time.deltaTime);
            if ((grabberHead.transform.position - endLocation.position).magnitude < 0.5f)
            {
                RetractGrabber();
            }
        }
        else if (movingIn)
        {
            grabberHead.transform.position = Vector3.MoveTowards(grabberHead.transform.position, startLocation.position, inwardsSpeed * Time.deltaTime);
            if ((grabberHead.transform.position - startLocation.position).magnitude < 0.5f)
            {
                StopGrabber();
            }
        }
    }

    private void ActivateGrabber()
    {
        movingOut = true;
        canActivate = false;
        grabberHead.ToggleCollider(true);
    }

    private void RetractGrabber()
    {
        movingIn = true;
        movingOut = false;
        grabberHead.ToggleCollider(false);
    }

    private void StopGrabber()
    {
        movingIn = false;
        canActivate = true;
    }
}
