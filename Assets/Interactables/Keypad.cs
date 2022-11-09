using UnityEngine;

public class Keypad : Interactable
{
    [SerializeField]private GameObject door;
    private bool doorOpen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //For Interaction   
    protected override void Interact(){
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("IsOpen",doorOpen);
        print("Interacted with" + gameObject.name);
    }
}
