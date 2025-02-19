using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControllerBackButton : MonoBehaviour
{
    [SerializeField]
    private InputActionProperty m_XButtonAction; // 绑定 X 按钮的 InputAction

    [SerializeField]
    private InputActionProperty m_BButtonAction; // 绑定 B 按钮的 InputAction

    void OnEnable()
    {
        // 监听 X 和 B 按钮按下事件
        m_XButtonAction.action.performed += OnButtonPressed;
        m_BButtonAction.action.performed += OnButtonPressed;
    }

    void OnDisable()
    {
        // 移除事件监听
        m_XButtonAction.action.performed -= OnButtonPressed;
        m_BButtonAction.action.performed -= OnButtonPressed;
    }

    void OnButtonPressed(InputAction.CallbackContext context)
    {
        BackToMenu();
    }

    void BackToMenu()
    {
        string menuSceneName = "Scenes/MetaMenu"; // 替换为你的实际场景路径
        if (Application.CanStreamedLevelBeLoaded(menuSceneName))
        {
            Debug.Log($"Returning to scene: {menuSceneName}");
            SceneManager.LoadScene(menuSceneName, LoadSceneMode.Single);
        }
    }
}
