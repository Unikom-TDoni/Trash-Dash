using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_GUITest : MonoBehaviour
{
    [SerializeField] Animator NPCAnim;
    bool toIdle, toWalking, toRunning, isStanding = true;

    void OnGUI()
    {
        GUI.Box(new Rect(10, 110, 100, 10 + (40 * 6)), "NPC Animator");

        if (GUI.Button(new Rect(20, 140, 80, 20), "Idle"))
        {
            toWalking = false; toRunning = false; isStanding = true;
            NPCAnim.SetBool("isTalking", false);
            //NPCAnim.SetFloat("movement", 0);
            toIdle = true;
        }

        if (GUI.Button(new Rect(20, 140 + 30, 80, 20), "Walking"))
        {
            toIdle = false; toRunning = false; isStanding = true;
            NPCAnim.SetBool("isTalking", false);
            //NPCAnim.SetFloat("movement", 0.5f);
            toWalking = true;
        }

        if (GUI.Button(new Rect(20, 140 + (30 * 2), 80, 20), "Running"))
        {
            toIdle = false; toWalking = false; isStanding = true;
            NPCAnim.SetBool("isTalking", false);
            //NPCAnim.SetFloat("movement", 1);
            toRunning = true;
        }

        if (GUI.Button(new Rect(20, 140 + (30 * 3), 80, 20), "Stand To Sit"))
        {
            toIdle = false; toWalking = false; toRunning = false; isStanding = false;
            NPCAnim.SetBool("isTalking", false);
            NPCAnim.SetTrigger("trigToSit");
        }

        if (GUI.Button(new Rect(20, 140 + (30 * 4), 80, 20), "Sit To Stand"))
        {
            toIdle = true; toWalking = false; toRunning = false; isStanding = true;
            NPCAnim.SetBool("isTalking", false);
            NPCAnim.SetTrigger("trigToStand");
        }

        if (GUI.Button(new Rect(20, 140 + (30 * 5), 80, 20), "Talking"))
        {
            toIdle = false; toWalking = false; toRunning = false; isStanding = true;
            int i = Random.value > 0.5f ? 0 : 1;
            NPCAnim.SetInteger("randomTalk", i);
            NPCAnim.SetBool("isTalking", true);
        }

        if (GUI.Button(new Rect(20, 140 + (30 * 6), 80, 20), "Disappointed"))
        {
            toIdle = false; toWalking = false; toRunning = false;
            if (isStanding)
            {
                NPCAnim.SetTrigger("throwStanding");
            }
            else
            {
                NPCAnim.SetTrigger("throwSitting");
            }
        }
    }

    void Update()
    {
        if (toIdle)
        {
            NPCAnim.SetFloat("movement", Mathf.Lerp(NPCAnim.GetFloat("movement"), 0, 5 * Time.deltaTime));
        }
        else if (toWalking)
        {
            NPCAnim.SetFloat("movement", Mathf.Lerp(NPCAnim.GetFloat("movement"), 0.5f, 5 * Time.deltaTime));
        }
        else if (toRunning)
        {
            NPCAnim.SetFloat("movement", Mathf.Lerp(NPCAnim.GetFloat("movement"), 1, 5 * Time.deltaTime));
        }
    }
}
