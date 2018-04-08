using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueRaycast : MonoBehaviour {
    private RaycastHit hit;
    private Vector3 fwd;
    private float range = 999.0f;
    private bool visible = false;

    public DialogueWindow window;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        fwd = transform.TransformDirection(Vector3.forward);

        if (Input.GetKeyDown(KeyCode.E))
        {
            //pistolSparks.particleEmitter.Emit();
            //audio.Play();

            if (Physics.Raycast(transform.position, fwd, out hit))
            {
                if (hit.distance < range)
                {
                    if(hit.collider is CapsuleCollider)
                    {
                        window.OnTalkPrompt();

                    }
                    
                }
            }
        }
    }
}
