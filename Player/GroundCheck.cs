﻿using UnityEngine;
using Photon.Pun;
[ExecuteInEditMode]
public class GroundCheck : MonoBehaviourPunCallbacks
{
    public float distanceThreshold = .15f;
    public bool isGrounded = true;

    public event System.Action Grounded;

    const float OriginOffset = .2f;
    Vector3 RaycastOrigin => transform.position + Vector3.up * OriginOffset;
    float RaycastDistance => distanceThreshold + OriginOffset;


    void LateUpdate()
    {
        if (photonView.IsMine)
        {
            bool isGroundedNow = Physics.Raycast(RaycastOrigin, Vector3.down, distanceThreshold * 2);

            if (isGroundedNow && !isGrounded)
            {
                Grounded?.Invoke();
            }

            isGrounded = isGroundedNow;
        }
    }

    void OnDrawGizmosSelected()
    {
        Debug.DrawLine(RaycastOrigin, RaycastOrigin + Vector3.down * RaycastDistance, isGrounded ? Color.white : Color.red);
    }
}
