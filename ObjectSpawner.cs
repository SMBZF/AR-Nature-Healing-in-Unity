using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    [Tooltip("List of objects to spawn on horizontal surfaces.")]
    public List<GameObject> horizontalObjectPrefabs; 

    [SerializeField]
    [Tooltip("List of objects to spawn on vertical surfaces.")]
    public List<GameObject> verticalObjectPrefabs; 

    public void TrySpawnObject(Vector3 position, Vector3 normal)
    {
        if (Vector3.Dot(normal, Vector3.up) > 0.9f || Vector3.Dot(normal, Vector3.down) > 0.9f)
        {
            SpawnRandomObjectFromList(horizontalObjectPrefabs, position, normal);
        }
        else if (Mathf.Abs(Vector3.Dot(normal, Vector3.right)) > 0.9f ||
                 Mathf.Abs(Vector3.Dot(normal, Vector3.forward)) > 0.9f ||
                 Mathf.Abs(Vector3.Dot(normal, Vector3.back)) > 0.9f)
        {
            SpawnRandomObjectFromList(verticalObjectPrefabs, position, normal);
        }
        else
        {
            DebugDisplay.UpdateDebug("Unknown surface normal, no object will be spawned.");
        }
    }

    private void SpawnRandomObjectFromList(List<GameObject> objectPrefabs, Vector3 position, Vector3 normal)
    {
        if (objectPrefabs != null && objectPrefabs.Count > 0)
        {
            int randomIndex = Random.Range(0, objectPrefabs.Count);
            GameObject selectedObject = objectPrefabs[randomIndex];

            Quaternion rotation = Mathf.Abs(Vector3.Dot(normal, Vector3.up)) > 0.9f
                ? Quaternion.LookRotation(Vector3.forward, Vector3.up)
                : Quaternion.LookRotation(-normal, Vector3.up);

            GameObject spawnedObject = Instantiate(selectedObject, position, rotation);
            DebugDisplay.UpdateDebug($"Spawned {selectedObject.name} at {position}");

            // Trigger the plant's growth animation
            var flowerControl = spawnedObject.GetComponent<FlowerControlWithMPB>();
            if (flowerControl != null)
            {
                flowerControl.ResetGrow();
                flowerControl.StartGrowing();
            }

            // ≤•∑≈“Ù–ß
            var audioSource = spawnedObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
        else
        {
            DebugDisplay.UpdateDebug("Object Prefabs list is empty.");
        }
    }

}
