using UnityEngine;
using UnityEngine.UI;

public class DebugDisplay : MonoBehaviour
{
    public Text debugText; // 显示调试信息的 Text 组件

    private static DebugDisplay instance; // 单例模式方便其他脚本调用

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // 确保只有一个实例
        }
    }

    // 更新 Debug 信息
    public static void UpdateDebug(string message)
    {
        if (instance != null && instance.debugText != null)
        {
            instance.debugText.text = message;
        }
    }
}
