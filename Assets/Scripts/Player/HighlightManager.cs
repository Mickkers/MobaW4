using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightManager : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private Color outlineColor;
    [SerializeField] private float highlightRange;

    private PlayerController pController;

    public Transform highlighted { get; private set; }
    private Transform selected;

    private Outline outline;
    private Collider[] hits;
    private Collider hit;

    private void Start()
    {
        pController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        HoverOutline();
    }

    private void HoverOutline()
    {
        //Ray ray = Camera.main.ScreenPointToRay(InputManager.GetMousePosition());

        if (outline != null) outline.OutlineColor = outlineColor;

        if (highlighted != null)
        {
            outline.enabled = false;
            highlighted = null;
        }

        hit = GetHit();

        if (hit != null)
        {
            highlighted = hit.transform;

            if (highlighted != selected)
            {
                outline = highlighted.GetComponent<ITakeDamage>().GetOutline();
                outline.enabled = true;
            }
            else
            {
                highlighted = null;
            }
        }
    }

    public Collider GetHit()
    {
        hits = Physics.OverlapSphere(transform.position, pController.GetAttackRange() + highlightRange);

        Collider tempHit = null;
        float closestDis = Mathf.Infinity;

        foreach (Collider colliderHit in hits)
        {
            if ((colliderHit.transform.CompareTag("Enemy") || colliderHit.transform.CompareTag("Player")) && colliderHit.gameObject != this.gameObject)
            {
                float distance = Vector3.Distance(transform.position, colliderHit.transform.position);
                if (distance < closestDis)
                {
                    tempHit = colliderHit;
                    closestDis = distance;
                }
            }
        }

        return tempHit;
    }

    public void SelectOutline()
    {
        if(highlighted == null)
        {
            return;
        }
        if (highlighted.CompareTag("Enemy") || highlighted.CompareTag("Player") && highlighted.gameObject != this.gameObject)
        {
            if (selected != null)
            {
                selected.gameObject.GetComponent<ITakeDamage>().GetOutline().enabled = false;
            }
            selected = highlighted;
            selected.gameObject.GetComponent<ITakeDamage>().GetOutline().enabled = true;

            outline.enabled = true;
            highlighted = null;
        }
    }

    public void DeselectOutline()
    {
        if(selected == null)
        {
            return;
        }
        selected.gameObject.GetComponent<ITakeDamage>().GetOutline().enabled = false;
        selected = null;
    }

    public GameObject GetTarget()
    {
        if (highlighted != null)
        {
            return highlighted.gameObject;
        }
        else
        {
            return null;
        }
    }
}
