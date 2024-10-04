using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Smallbutton : Interaction
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact() {
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("Is Open", doorOpen);
    }
}
