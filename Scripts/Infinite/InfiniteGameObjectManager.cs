using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ALUN;
public class InfiniteGameObjectManager : MonoBehaviour
{
    public int maxObjects = 20;  // The maximum number of objects
    public float checkInterval = 1f;  // The interval between checks
    public float spawnRadius = 50f; // 设置生成半径
    public List<InfiniteGameObject> allObjects = new List<InfiniteGameObject>();  // The list of all objects
    public float distanceByCamera, howLongCanCheck = 1000f;
    private float lastCheckTime;
    private int lastObjectCount;
    bool getAllObjectsDone;
    void Start()
    {
        if (GetComponent<Terrain>() == null) this.enabled = false;
        lastCheckTime = Time.time;
        lastObjectCount = allObjects.Count;
    }
    void Update()
    {

        if (getAllObjectsDone) return;
        // Check if 5 seconds have passed since the last check
        if (Time.time - lastCheckTime >= 5f)
        {
            // Check if the number of objects has increased
            if (allObjects.Count > lastObjectCount)
            {
                // Update lastObjectCount and lastCheckTime
                lastObjectCount = allObjects.Count;
                lastCheckTime = Time.time;
            }
            else
            {
                // If the number of objects has not increased, start the coroutine
                StartCoroutine(CheckObjects());

                // Disable this script to stop further updates
                getAllObjectsDone = true;
            }
        }
    }
    // Call this function to add a new object
    public void AddObject(InfiniteGameObject obj)
    {
        if (!allObjects.Contains(obj))
        {
            allObjects.Add(obj);
        }
    }
    public int spawnedCountSave = 0;
    // The coroutine for checking the number of objects
    IEnumerator CheckObjects()
    {
        while (true)
        {
            distanceByCamera = Vector3.Distance(Camera.main.transform.position, transform.position);
            yield return new WaitForSeconds(checkInterval);
            if (distanceByCamera < howLongCanCheck)
            {// 计算已经生成的物体数量
                int spawnedCount = 0;
                foreach (var obj in allObjects)
                {
                    if (obj.spawnNew != null)
                    {
                        spawnedCount++;
                        yield return new WaitForSeconds(0.1f);
                    }
                }
                if (spawnedCountSave != spawnedCount)
                    spawnedCountSave = spawnedCount;

                // 检查是否需要生成更多的物体
                if (spawnedCount < maxObjects)
                {
                    // 获取摄像机的位置
                    Vector3 cameraPos = Camera.main.transform.position;
                    List<InfiniteGameObject> unspawnedObjects = new List<InfiniteGameObject>(allObjects);
                    unspawnedObjects.RemoveAll(obj => obj.spawnNew != null || Vector3.Distance(cameraPos, obj.transform.position) > spawnRadius);
                    if (unspawnedObjects.Count > 0)
                    {
                        int randomIndex = Random.Range(0, unspawnedObjects.Count);
                        unspawnedObjects[randomIndex].Spawn();
                    }
                }
            }
        }
    }
}
