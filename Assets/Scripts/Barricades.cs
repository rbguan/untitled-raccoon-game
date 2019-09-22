using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricades : InteractableObject
{
    [SerializeField] private BoxCollider blocker;
    [SerializeField] private MeshRenderer render;
    [SerializeField] private MeshRenderer render2;
    public override void Awake()
    {
        //Debug.Log("subclass");
        base.Awake();
        blocker.enabled = true;
        render.enabled = true;
        render2.enabled = true;
    }

    public override void DoAction(){
        Debug.Log("doing thing");
        base.DoAction();
        blocker.enabled = false;
        render.enabled = false;
        render2.enabled = false;
    }
}

