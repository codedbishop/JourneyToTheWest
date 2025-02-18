using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class MoveAction : BaseAction
{
    HexTile targetTile;
    Vector3 targetPosition;

    [SerializeField] float stoppingDistance = .2f;
    bool isActive;

    public void Start()
    {
        isActive = false;
        targetPosition = this.transform.position;
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            isActive = false;
            Debug.Log("Reached Target");
        }

        float rotateSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }

    public void SetTarget(HexTile hexTile)
    {
        targetTile = hexTile;
        targetPosition = LevelSystem.Instance.GetHexWorldPosition(hexTile);
        isActive = true;
    }

    public void SetIsActiveToFalse()
    {
        isActive = false;
        
    }

}
