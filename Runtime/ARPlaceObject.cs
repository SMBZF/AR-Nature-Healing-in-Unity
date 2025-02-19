using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARSubsystems;
using System;

#if BURST_PRESENT
using Unity.Burst;
#endif
using Unity.Collections;
//using Unity.Mathematics;

using UnityEngine.Assertions;
using UnityEngine.EventSystems;

using UnityEngine.XR.Interaction.Toolkit.UI;
using UnityEngine.XR.Interaction.Toolkit.Utilities;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Curves;
using Slider = UnityEngine.UI.Slider;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// Subscribes to an <see cref="ARRaycastHitEventAsset"/>. When the event is raised, the
    /// <see cref="prefabToPlace"/> is instantiated at, or moved to, the hit position.
    /// </summary>
    public class ARPlaceObject : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The list of prefabs to randomly choose from.")]
        List<GameObject> m_PrefabsToPlace;  // 存储多个可以放置的物体

        [SerializeField]
        [Tooltip("Maximum number of objects to randomly place.")]
        int maxObjectsToPlace = 10;  // 限制最多放置的物体数量

        [SerializeField]
        [Tooltip("The Scriptable Object Asset that contains the ARRaycastHit event.")]
        ARRaycastHitEventAsset m_RaycastHitEvent;

        List<GameObject> placedObjects = new List<GameObject>(); // 记录所有已放置的物体

        void OnEnable()
        {
            if (m_RaycastHitEvent == null || m_PrefabsToPlace == null || m_PrefabsToPlace.Count == 0)
            {
                Debug.LogWarning($"{nameof(ARPlaceObject)} component on {name} has null or empty inputs.", this);
                return;
            }

            m_RaycastHitEvent.eventRaised += PlaceRandomObjectAt; // 注册事件
        }

        void OnDisable()
        {
            if (m_RaycastHitEvent != null)
                m_RaycastHitEvent.eventRaised -= PlaceRandomObjectAt; // 注销事件
        }

        void PlaceRandomObjectAt(object sender, ARRaycastHit hitPose)
        {
            // 检查是否超出最大放置数量
            if (placedObjects.Count >= maxObjectsToPlace)
            {
                Debug.Log("Max objects placed. No more objects will be placed.");
                return;
            }

            // 随机选择一个 Prefab
            int randomIndex = Random.Range(0, m_PrefabsToPlace.Count);
            GameObject randomPrefab = m_PrefabsToPlace[randomIndex];

            // 实例化物体
            GameObject newObject = Instantiate(randomPrefab, hitPose.pose.position, hitPose.pose.rotation, hitPose.trackable.transform.parent);

            // 添加到已放置物体列表
            placedObjects.Add(newObject);

            Debug.Log($"Placed {randomPrefab.name} at position {hitPose.pose.position}.");
        }
    }

}
