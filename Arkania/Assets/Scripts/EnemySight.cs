using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        Transform playerTransform = player.transform;
        Vector3 targetDir = playerTransform.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);

        if (angle < 30.0f)
            Debug.Log("close");
    }
}
