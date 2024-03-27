using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public event EventHandler<OnPlayerMovementEventArgs> OnPlayerMovement;

    public class OnPlayerMovementEventArgs : EventArgs
    {
        public float speed;
    }

    private PlayerController pController;
    private NavMeshAgent navMeshAgent;
    private HighlightManager highlightManager;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private AudioSource walkSound;
    private float rotateVelocity;

    public GameObject target;

    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        pController = GetComponent<PlayerController>();
        highlightManager = GetComponent<HighlightManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
        Sound();
        CheckTargetDistance();
        if (target == null) Movement();
    }

    private void Sound()
    {
        if(navMeshAgent.velocity.magnitude / navMeshAgent.speed > 0.2f)
        {
            walkSound.mute = false;
        }
        else
        {
            walkSound.mute = true;
        }
    }

    private void CheckTargetDistance()
    {
        
        if (target != null)
        {
            if (target.CompareTag("DeadEnemy"))
            {
                target = null;
                highlightManager.DeselectOutline();
                return;
            }
            if (Vector3.Distance(transform.position, target.transform.position) > pController.GetAttackRange())
            {
                navMeshAgent.SetDestination(target.transform.position);
            }
        }
    }

    private void Movement()
    {
        //RaycastHit hit;
        //if(Physics.Raycast(Camera.main.ScreenPointToRay(val), out hit, Mathf.Infinity))
        //{
        //    if (hit.transform.gameObject.CompareTag("Ground") || hit.transform.gameObject.CompareTag("Enemy"))
        //    {
        //        navMeshAgent.SetDestination(hit.point);
        //        navMeshAgent.stoppingDistance = 0;

        //        RotationLook(hit.point);

        //        if (target != null)
        //        {
        //            target = null;
        //            highlightManager.DeselectOutline();
        //        }
        //    }
        //}

        Vector3 destination = transform.position + Vector3.forward * moveInput.y + Vector3.right * moveInput.x;
        //transform.Rotate(0, moveInput.x * navMeshAgent.angularSpeed * Time.deltaTime, 0);
        navMeshAgent.SetDestination(destination);
        navMeshAgent.stoppingDistance = 0;
        if (moveInput != Vector2.zero) RotationLook(destination);
    }

    public void Move(Vector2 val)
    {
        moveInput = val;
        if (target != null)
        {
            target = null;
            highlightManager.DeselectOutline();
        }
    }

    public void MoveToTarget()
    {
        target = highlightManager.GetHit().gameObject;

        navMeshAgent.stoppingDistance = pController.GetAttackRange();
        highlightManager.SelectOutline();
        RotationLook(target.transform.position);
    }

    private void RotationLook(Vector3 position)
    {
        Quaternion rotationLook = Quaternion.LookRotation(position - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationLook.eulerAngles.y, ref rotateVelocity, rotateSpeed);
        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }


    private void Animate()
    {
        float speed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
        OnPlayerMovement?.Invoke(this, new OnPlayerMovementEventArgs
        {
            speed = speed
        });
    }
}
