using UnityEngine;
using Unity.Netcode;

public class NetworkCamera : NetworkBehaviour
{
    private Camera playerNetworkCamera;
    private AudioListener playerNetworkAudioListener;

    public override void OnNetworkSpawn()
    {
        playerNetworkCamera = GetComponent<Camera>();
        playerNetworkAudioListener = GetComponent<AudioListener>();
        if(!IsOwner)
        {
            playerNetworkCamera.enabled = false;
            playerNetworkAudioListener.enabled = false;
        }
        base.OnNetworkSpawn();
    }
}
