using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBall : MonoBehaviour
{
    [SerializeField]
    private Transform[] handTransforms;

    [SerializeField]
    private int right, left, middle;

    private bool[] whereRacket = new bool[3];

    PlayerMovement movement;

    public float range;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    public LayerMask ball;

    public float tennisRacketSpeed;

    public Transform aimTarget;
    public float force;
    public Vector3 offset;


    public GameObject tennisRacket;

    private void Awake()
    {
        whereRacket[0] = false;
        whereRacket[1] = false;
        whereRacket[2] = false;
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Hitting(whereRacket);
        }

        aimTarget.transform.position = new Vector3(-transform.position.x, transform.position.y + offset.y, transform.position.z + offset.z);

        if (movement.moveInput.x > 0)
        {
            whereRacket[0] = true;
            whereRacket[1] = false;
            whereRacket[2] = false;
            tennisRacket.transform.position = Vector3.Lerp(tennisRacket.transform.position, handTransforms[right].position, tennisRacketSpeed * Time.deltaTime);
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, -30f, ref turnSmoothVelocity, turnSmoothTime);
            tennisRacket.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else if (movement.moveInput.x < 0)
        {
            whereRacket[0] = false;
            whereRacket[1] = true;
            whereRacket[2] = false;
            tennisRacket.transform.position = Vector3.Lerp(tennisRacket.transform.position, handTransforms[left].position, tennisRacketSpeed * Time.deltaTime);
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, 30f, ref turnSmoothVelocity, turnSmoothTime);
            tennisRacket.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else if (movement.moveInput.x == 0)
        {
            whereRacket[0] = false;
            whereRacket[1] = false;
            whereRacket[2] = true;
            tennisRacket.transform.position = Vector3.Lerp(tennisRacket.transform.position, handTransforms[middle].position, tennisRacketSpeed * Time.deltaTime);
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.x, 135f, ref turnSmoothVelocity, turnSmoothTime);
            tennisRacket.transform.rotation = Quaternion.Euler(angle, 0f, 0f);
        }
    }

    private void Hitting(bool[] where)
    {
        Vector3 dir = aimTarget.transform.position - transform.position;
        
        if (where[0])
        {
            Collider[] col = Physics.OverlapSphere(handTransforms[right].transform.position, range, ball);
            col[0].GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 10f, 0f);
        }
        else if (where[1])
        {
            Collider[] col = Physics.OverlapSphere(handTransforms[left].transform.position, range, ball);
            col[0].GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 10f, 0f);
        }
        else if (where[2])
        {
            Collider[] col = Physics.OverlapSphere(handTransforms[middle].transform.position, range, ball);
            col[0].GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 10f, 0f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        foreach (Transform transform in handTransforms)
        {
            Gizmos.DrawWireSphere(transform.transform.position, range);
        }
    }
}
