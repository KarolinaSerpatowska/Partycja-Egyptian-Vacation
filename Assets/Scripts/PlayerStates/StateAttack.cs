using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : IState
{
    GameObject owner;

    private Animator anim;

    [SerializeField] Camera cam;
    [SerializeField] Interactable focusedOn = null;
    [SerializeField] Transform playerTransform;
    bool isFocus = false;

    public StateAttack(GameObject owner) { this.owner = owner; }

    public void Enter()
    {
        Debug.Log("entering attack state");
        anim = owner.GetComponent<Animator>();
        anim.SetBool("IsAttack", true);
        cam = Camera.main;
        playerTransform = PlayerManger.instance.player.transform;
    }

    public void Execute()
    {
        owner.GetComponent<Player>().Attack();
        RightButtonClick();
        Debug.Log("updating attack state");
    }

    public void Exit()
    {
        anim.SetBool("IsAttack", false);
        Debug.Log("exiting attack state");
    }

    void RightButtonClick()
    {
         Ray ray = cam.ScreenPointToRay(Input.mousePosition);
         RaycastHit hit;
         LayerMask layer = 0;
         layer = LayerMask.NameToLayer("Interacable");
         if (layer == 0)
               Debug.LogError("Problem with layer");
         if (Physics.Raycast(ray, out hit, layer))
         {
             Interactable interactable = hit.collider.GetComponent<Interactable>();
             if (!interactable)
                 Debug.LogWarning("no Interactable hit.");
             else
             {
                 SetFocusOn(interactable);
                 interactable.OnFocused(playerTransform);
             }
         }
    }

    void SetFocusOn(Interactable interactable)
    {
        focusedOn = interactable;
        isFocus = true;
    }

    void Removefocus()
    {
        focusedOn = null;
        isFocus = false;
    }


}
