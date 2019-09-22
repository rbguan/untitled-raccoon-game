using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : InteractableObject
{
    [SerializeField] private BoxCollider blocker;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform trash;
    [SerializeField] private MeshRenderer render;
    
    public override void Awake()
    {
        //Debug.Log("subclass");
        base.Awake();
        blocker.enabled = false;
    }

    public override void DoAction(){
        audioSource.Play();
        base.DoAction();
        blocker.enabled = true;
        render.enabled = true;
        trash.transform.rotation = Quaternion.Euler(200,0,0);
    }
}
