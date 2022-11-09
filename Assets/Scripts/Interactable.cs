using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool useEvents;

    
    //message displayed to player when looking at interactable.
    public string promptMessage;

    public virtual string OnLook(){
        return promptMessage;
    }

    //this function will be called from our player.
    public void BaseInteract(){
        if(useEvents)
             GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }
    protected virtual void Interact(){
        //we won't have any code written in this function
        //this is a template function to be overridden by our subclass  
    }
}
