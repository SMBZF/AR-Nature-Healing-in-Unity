using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

namespace UnityEngine.XR.Interaction.Toolkit.Samples.ARStarterAssets
{
    [RequireComponent(typeof(Rigidbody))]
    public class ARContactSpawnTrigger : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The behavior to use to spawn objects.")]
        ObjectSpawner m_ObjectSpawner;

        public ObjectSpawner objectSpawner
        {
            get => m_ObjectSpawner;
            set => m_ObjectSpawner = value;
        }

        [SerializeField]
        [Tooltip("Whether to require that the AR Plane has an alignment of horizontal up to spawn on it.")]
        bool m_RequireHorizontalUpSurface;

        public bool requireHorizontalUpSurface
        {
            get => m_RequireHorizontalUpSurface;
            set => m_RequireHorizontalUpSurface = value;
        }

        [SerializeField]
        [Tooltip("Minimum distance to check for nearby objects before spawning a new one.")]
        float spawnCheckRadius = 10.0f;

        [SerializeField]
        [Tooltip("Cooldown time between spawns.")]
        float spawnCooldown = 1.5f;

        private float lastSpawnTime = -1f;

        // 新增：水平面和竖直面销毁的物体
        [SerializeField]
        [Tooltip("The object to destroy when touching a horizontal plane.")]
        GameObject horizontalObjectToDestroy;

        [SerializeField]
        [Tooltip("The object to destroy when touching a vertical plane.")]
        GameObject verticalObjectToDestroy;

        void Start()
        {
            if (m_ObjectSpawner == null)
#if UNITY_2023_1_OR_NEWER
                m_ObjectSpawner = FindAnyObjectByType<ObjectSpawner>();
#else
                m_ObjectSpawner = FindObjectOfType<ObjectSpawner>();
#endif
        }

        void OnTriggerEnter(Collider other)
        {
            // 检测碰撞是否为有效的生成平面
            if (!TryGetSpawnSurfaceData(other, out var surfacePosition, out var surfaceNormal))
                return;

            // 检查是否在冷却时间内
            if (Time.time - lastSpawnTime < spawnCooldown)
                return;

            // 检查生成点附近是否有其他物体
            if (IsObjectNear(surfacePosition))
            {
                Debug.Log("Nearby object found, skipping spawn.");
                return;
            }

            // 计算碰撞点在平面上的最近点
            var infinitePlane = new Plane(surfaceNormal, surfacePosition);
            var contactPoint = infinitePlane.ClosestPointOnPlane(transform.position);

            // 尝试生成物体
            m_ObjectSpawner.TrySpawnObject(contactPoint, surfaceNormal);

            // 更新全局 Shader 属性
            UpdateShaderWithWorldPosition(contactPoint);

            lastSpawnTime = Time.time;

            // 新增：根据触摸的平面类型销毁物体
            if (IsHorizontalPlane(surfaceNormal))
            {
                // 水平面
                DestroyObject(horizontalObjectToDestroy);
            }
            else if (IsVerticalPlane(surfaceNormal))
            {
                // 竖直面
                DestroyObject(verticalObjectToDestroy);
            }
        }

        private void UpdateShaderWithWorldPosition(Vector3 position)
        {
            // 使用全局属性设置
            Shader.SetGlobalVector("_GlobalSpawnPosition", position);
        }

        public bool TryGetSpawnSurfaceData(Collider objectCollider, out Vector3 surfacePosition, out Vector3 surfaceNormal)
        {
            surfacePosition = default;
            surfaceNormal = default;

            var arPlane = objectCollider.GetComponent<ARPlane>();
            if (arPlane == null)
                return false;

            // 如果要求水平面生成，但检测到的平面不是水平面，则返回 false
            if (m_RequireHorizontalUpSurface && arPlane.alignment != PlaneAlignment.HorizontalUp)
                return false;

            // 获取平面的法线和中心位置
            surfaceNormal = arPlane.normal;
            surfacePosition = arPlane.center;
            return true;
        }

        private bool IsObjectNear(Vector3 position)
        {
            // 检查生成点附近是否有已生成的物体
            Collider[] hitColliders = Physics.OverlapSphere(position, spawnCheckRadius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("SpawnedObject"))
                {
                    return true;
                }
            }
            return false;
        }

        private void DestroyObject(GameObject objectToDestroy)
        {
            if (objectToDestroy != null)
            {
                Destroy(objectToDestroy);  // 销毁指定的物体
            }
        }

        private bool IsHorizontalPlane(Vector3 surfaceNormal)
        {
            // 判断是否是水平面（法线方向接近 (0, 1, 0)）
            return Mathf.Abs(surfaceNormal.y) > Mathf.Abs(surfaceNormal.x) && Mathf.Abs(surfaceNormal.y) > Mathf.Abs(surfaceNormal.z);
        }

        private bool IsVerticalPlane(Vector3 surfaceNormal)
        {
            // 判断是否是竖直面（法线方向接近 (1, 0, 0) 或 (0, 0, 1)）
            return Mathf.Abs(surfaceNormal.x) > Mathf.Abs(surfaceNormal.y) && Mathf.Abs(surfaceNormal.x) > Mathf.Abs(surfaceNormal.z) ||
                   Mathf.Abs(surfaceNormal.z) > Mathf.Abs(surfaceNormal.x) && Mathf.Abs(surfaceNormal.z) > Mathf.Abs(surfaceNormal.y);
        }
    }
}
