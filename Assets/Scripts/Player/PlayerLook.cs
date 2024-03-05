using Unity.Netcode;
using UnityEngine;

public class PlayerLook : NetworkBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float xSensitivity = 30f;
    [SerializeField] private float ySensitivity = 30f;
    private float xRotation = 0f;
    
    private void LateUpdate()
    {
        if(!IsOwner) return;
        ProcessLook();
    }

    private void ProcessLook(){
        Vector2 input = InputManager.Instance.GetLookVectorNormalized();

        float mouseX = input.x;
        float mouseY = input.y;

        //Calculate camera rotation for looking up and down
        xRotation -= mouseY * Time.deltaTime * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        //apply this to our camera transform.
        cam.transform.localRotation = Quaternion.Euler(xRotation,0,0);

        //rotate player to look left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    public void ChangeSensitivity(bool adsMode){
        // if(adsMode){
        //     xSensitivity = 10f;
        //     ySensitivity = 10f;
        // }
        // else{
        //     xSensitivity = 30f;
        //     ySensitivity = 30f;
        // }
        xSensitivity = ySensitivity = adsMode ? 10f : 30f;
        print(new Vector2(xSensitivity,ySensitivity));
    }

    
}
