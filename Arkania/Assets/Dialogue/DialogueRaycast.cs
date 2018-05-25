using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueRaycast : MonoBehaviour {
    private RaycastHit hit;
    private Vector3 fwd;
    private float range = 999.0f;
    private bool visible = false;

    public DialogueWindow window;
    public GameObject StoryObject;

    float lastPressed;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        fwd = transform.TransformDirection(Vector3.forward);

        int layerMask = 1 << 8;

        layerMask = ~layerMask;
        if (Physics.Raycast(transform.position, fwd, out hit, Mathf.Infinity, layerMask))
        {
            DialogueActor actor = hit.transform.gameObject.GetComponent<DialogueActor>();
            if (actor != null)
            {
                StoryObject.SendMessage("ActivateTalk", actor.actorName);
            }
            else StoryObject.SendMessage("NoTalk");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (lastPressed + 0.2f < Time.time)
            {
                lastPressed = Time.time;
                if (Physics.Raycast(transform.position, fwd, out hit, Mathf.Infinity, layerMask))
                {
                    if (hit.distance < range)
                    {
                        if (hit.collider is CapsuleCollider)
                        {
                            DialogueWindow window = hit.transform.gameObject.GetComponent(typeof(DialogueWindow)) as DialogueWindow;
                            //Debug.Log(actor.actorName);
                            window.OnTalkPrompt();

                        }
                    }
                }
            }
        }
    }
}
