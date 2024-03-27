using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneShrink : MonoBehaviour
{
    [SerializeField] float shrinkSpeed;
    [SerializeField] float shrinkStartDelay;
    [SerializeField] float shrinkStartSize;
    [SerializeField] float shrinkSmallestSize;
    [SerializeField] float height;

    private float currSize;
    private bool isShrinking;

    private void Start()
    {
        currSize = shrinkStartSize;
        transform.localScale = new Vector3(currSize, height, currSize);
        isShrinking = false;
        Invoke(nameof(StartShrink), shrinkStartDelay);
    }

    private void StartShrink()
    {
        isShrinking = true;
    }

    private void Update()
    {
        if (isShrinking && currSize > shrinkSmallestSize)
        {
            transform.localScale = new Vector3(currSize, height, currSize);
            currSize -= shrinkSpeed * Time.deltaTime;
        }
    }

}
