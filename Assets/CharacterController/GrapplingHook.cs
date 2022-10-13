using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    // Code base by Dave / Game Development on YouTube

    [Header("Input")]
    public KeyCode swingKey = KeyCode.Mouse1;

    [Header("References")]
    public LineRenderer lr;
    public Transform gunTip, cam, player;
    public LayerMask whatIsGrappleable;
    public PlayerMovement pm;

    [Header("Swinging")]
    public float maxSwingDistance = 25f;
    private Vector3 swingPoint, currentGrapplePosition;
    private SpringJoint joint;

    [Header("Camera Effects")]
    public PlayerCam FovCam;
    public float grappleFov;

    private void Update()
    {
        if (Input.GetKeyDown(swingKey)) StartSwing();
        if (Input.GetKeyUp(swingKey)) StopSwing();
    }

    private void LateUpdate()
    {
        DrawRope();
    }
    
    private void StartSwing()
    {

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxSwingDistance, whatIsGrappleable))
        {
            FovCam.DoFov(grappleFov);
            pm.swinging = true;
            swingPoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

            // the distance grapple will try to keep from the grapple point
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            // customize values as you like
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;



        }
    }

    private void StopSwing()
    {
        pm.swinging = false;
        pm.moveSpeed = pm.desiredMoveSpeed;

        FovCam.DoFov(80f);        

        lr.positionCount = 0;
        Destroy(joint);
    }

    private void DrawRope()
    {
        // if not grappling, don't draw rope 
        if(!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 80f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, swingPoint);
    }
}
