using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIK : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float distanceToground = 0.1f;
    [SerializeField]
    private LayerMask layerMask;

    private void Start()
    {
        // Animator bileşenini al, eğer atanmadıysa
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Debug.Log("IK Called");
        //if (animator)
        //{
        //    // IK ağırlığını ayarla
        //    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
        //    animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);

        //    // Sol ayak için raycast
        //    RaycastHit hit;
        //    Ray ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up * 0.1f, Vector3.down); // Yukarıdan aşağıya ray gönder

        //    if (Physics.Raycast(ray, out hit, distanceToground + 1f, layerMask))
        //    {
        //        if (hit.transform.CompareTag("Walkable")) // "Walkable" etiketiyle kontrol et
        //        {
        //            Vector3 footPosition = hit.point;
        //            footPosition.y += distanceToground;
        //            animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
        //        }
        //    }
        //}
    }
}
