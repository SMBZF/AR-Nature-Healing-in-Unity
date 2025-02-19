using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MushroomGrabEffect : MonoBehaviour
{
    public GameObject targetVFX; // 需要销毁的 VFX 对象

    // 监听抓取事件
    public void OnGrabbed(SelectEnterEventArgs args)
    {
        if (targetVFX != null)
        {
            Destroy(targetVFX); // 销毁目标 VFX 对象
        }
    }
}
