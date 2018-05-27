using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public GameObject player;
    public bool CanSee { get; set; }

    private void Start()
    {
        CanSee = false;
    }

    void Update()
    {
        Transform playerTransform = player.transform;
        Vector3 targetDir = playerTransform.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);

        if (gameObject.tag != "zombie")
        {
            if (angle <= 60.0f)
            {
                CanSee = true;
            }
            else CanSee = false;
        }
        else
        {
            if (angle <= 60.0f && Vector3.Distance(player.transform.position, transform.position) < 30f)
            {
                CanSee = true;
            }
            else CanSee = false;
        }
    }
}
