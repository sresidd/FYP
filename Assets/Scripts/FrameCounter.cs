using UnityEngine;
using TMPro;

public class FrameCounter : MonoBehaviour
{
    [SerializeField] 
    private float hudRefreshRate = 1f;
    private float timeForNextFrameUpdate;
    public TMP_Text frameRateUI;
    private const float ONE_SECOND = 1f;
    public void Update ()
    {
        if (Time.unscaledTime > timeForNextFrameUpdate)
        {
            int fps = (int)(ONE_SECOND / Time.unscaledDeltaTime);
            frameRateUI.text = "FPS: " + fps;
            timeForNextFrameUpdate = Time.unscaledTime + hudRefreshRate;
        }
    }
}
