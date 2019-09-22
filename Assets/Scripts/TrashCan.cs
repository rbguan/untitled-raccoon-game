using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : InteractableObject
{
    [SerializeField] private BoxCollider blocker;
    public override void Awake()
    {
        //Debug.Log("subclass");
        base.Awake();
        blocker.enabled = false;
    }

    public override void DoAction(){
        base.DoAction();
        blocker.enabled = true;
    }
}
