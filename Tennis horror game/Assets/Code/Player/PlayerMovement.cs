using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public Vector2 moveInput;

    [SerializeField, Range(0f, 50f)]
    public float maxSpeed = 2.5f;

    public GameObject model;

    Vector3 velocity;
    [SerializeField]
    private Transform cameraTransform;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.GetComponent<Transform>();
    }

    void Update()
    {
        moveInput.x = 0f;
        moveInput.y = 0f;

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        moveInput.x = Input.GetAxis("Horizontal");

        if(moveInput.x > 0)
        {
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 1f, ref turnSmoothVelocity, turnSmoothTime);
            model.transform.rotation = Quaternion.Euler(0f, 0f, -angle);
        }
        else if (moveInput.x < 0)
        {
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, -1f, ref turnSmoothVelocity, turnSmoothTime);
            model.transform.rotation = Quaternion.Euler(0f, 0f, -angle);
        }
        else if (moveInput.x == 0)
        {
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 0f, ref turnSmoothVelocity, turnSmoothTime);
            model.transform.rotation = Quaternion.Euler(0f, 0f, -angle);
        }

        Vector3 forwardRelative = moveInput.y * camForward;
        Vector3 rightRelative = moveInput.x * camRight;

        Vector3 relativeMoveDir = forwardRelative + rightRelative;

        moveInput = Vector2.ClampMagnitude(relativeMoveDir, 1f);

        Vector3 velocity = new Vector3(relativeMoveDir.x, 0f) * maxSpeed;
        Vector3 moveDir = velocity * Time.deltaTime;
        transform.localPosition += moveDir;
    }
}
