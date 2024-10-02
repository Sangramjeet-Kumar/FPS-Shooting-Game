using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    public string promptmessage;
    public void BaseInteract() 
    {
        Interact();    
    }
    protected virtual void Interact()
    {
        //template method pattern
        
    }
}
