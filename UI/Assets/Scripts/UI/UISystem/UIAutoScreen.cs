using UnityEngine;
using UnityEngine.UI;

/**
 * @brief UIAutoScreen
 * @detail 해상도 비율로 matchWidthOrHeight값을 변경한다.
 * @date 2024-02-20
 * @version 1.0.0
 * @author kij
 */

[ExecuteInEditMode]
public class UIAutoScreen : MonoBehaviour
{
    public float baseScreen_height = 1280f;
    public float baseScreen_width = 720f;
    public CanvasScaler canvasScaler;

    private void Start()
    {
        ResetScreen();
    }

    void ResetScreen()
    {       
        float ScreenRatio = baseScreen_width / baseScreen_height;
        float currentRatio = Screen.width / (float)Screen.height;

        if (ScreenRatio < currentRatio)
        {
            canvasScaler.matchWidthOrHeight = 1f;
        }
        else
        {
            canvasScaler.matchWidthOrHeight = 0f;
        }
    }

#if UNITY_EDITOR
    void Update()
    {
        ResetScreen();
    }
#endif
}
