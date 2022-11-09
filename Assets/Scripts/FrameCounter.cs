using UnityEngine;
using TMPro;

public class FrameCounter : MonoBehaviour
{
    [SerializeField] 
    private float _hudRefreshRate = 1f;
    private float _timer;
    public TMP_Text display_TMP_Text;
    public void Update ()
    {
        if (Time.unscaledTime > _timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            display_TMP_Text.text = "FPS: " + fps;
            _timer = Time.unscaledTime + _hudRefreshRate;
        }
    }
    
}
