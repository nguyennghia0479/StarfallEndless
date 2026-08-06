using System;
using UnityEngine;

public class AndroidImmersiveMode : MonoBehaviour
{
    void Start()
    {
        ApplyImmersiveMode();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
            ApplyImmersiveMode();
    }

    void OnApplicationPause(bool isPaused)
    {
        if (!isPaused)
            ApplyImmersiveMode();
    }

    private void ApplyImmersiveMode()
    {
#if UNITY_ANDROID

        if (Application.platform != RuntimePlatform.Android)
        {
            return;
        }

        try
        {
            int sdkInt = new AndroidJavaClass("android.os.Build$VERSION").GetStatic<int>("SDK_INT");

            if (sdkInt >= 19)
            {
                AndroidJavaClass cView = new AndroidJavaClass("android.view.View");
                AndroidJavaObject oAct = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");

                oAct.Call<AndroidJavaObject>
                    (
                        "findViewById",
                        new AndroidJavaClass("android.R$id").GetStatic<int>("content")
                    )
                    .Call
                    (
                        "setSystemUiVisibility",
                        cView.GetStatic<int>("SYSTEM_UI_FLAG_LAYOUT_STABLE") |
                        cView.GetStatic<int>("SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION") |
                        cView.GetStatic<int>("SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN") |
                        cView.GetStatic<int>("SYSTEM_UI_FLAG_HIDE_NAVIGATION") |
                        cView.GetStatic<int>("SYSTEM_UI_FLAG_FULLSCREEN") |
                        cView.GetStatic<int>("SYSTEM_UI_FLAG_IMMERSIVE_STICKY")
                    );
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

#endif
    }
}
