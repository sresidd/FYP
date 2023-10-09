using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private PlayerUI playerUI;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float distance = 3f;
    // Start is called before the first frame update

    private void Start()
    {
        playerUI = GetComponent<PlayerUI>();
    }

    // Update is called once per frame
    private void Update()
    {
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position,cam.transform.forward) ;
        Debug.DrawRay(ray.origin,ray.direction*distance);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray,out hitInfo, distance, mask)){

            if(hitInfo.collider.GetComponent<Interactable>()!=null){
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                if(InputManager.Instance.OnFoot.Interact.triggered){
                    interactable.BaseInteract();
                }
            }
        }
    }
}


