using UnityEngine;

public class GrapplingGun : MonoBehaviour {

    // Base Code by Affax on YouTube and Dave / GameDevelopment on YouTube
    
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public PlayerMovement pm;
    public Transform gunTip, camera, player;
    public float maxDistance = 30f;
    private SpringJoint joint;
    private bool needToGCheck;

    [Header("OdmGear")]
    public Transform orientation;
    public Rigidbody rb;
    public float horizontalThrustForce;
    public float forwardThrustForce;

    [Header("CameraEffects")]
    public PlayerCam cam;
    public float grappleFov;

    [Header("SFX")]
    public AudioSource audioSource;
    public AudioClip grappleSFX;
    public float volume=0.5f;

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(1)) {
            StopGrapple();
        }

        if (joint != null) OdmGearMovement();
    }



    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable)) {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;
            pm.swinging = true;
            cam.DoFov(grappleFov);
            audioSource.PlayOneShot(grappleSFX, volume);

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple() {
        Destroy(joint);
        cam.DoFov(80f);
        pm.swinging = false;
        pm.moveSpeed = pm.walkSpeed;
    }

    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }

    private void OdmGearMovement()
    {
        // right
        if (Input.GetKey(KeyCode.D)) rb.AddForce(orientation.right * horizontalThrustForce * Time.deltaTime);
        // left
        if (Input.GetKey(KeyCode.A)) rb.AddForce(-orientation.right * horizontalThrustForce * Time.deltaTime);
        // forward
        if (Input.GetKey(KeyCode.W)) rb.AddForce(orientation.forward * forwardThrustForce * Time.deltaTime);
        // backwards
        if (Input.GetKey(KeyCode.S)) rb.AddForce(-orientation.forward * forwardThrustForce * Time.deltaTime);
    }
}