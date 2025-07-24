using System;
using System.Collections;
using UnityEngine;

public class ZombiSpawner : MonoBehaviour
{

    public static event Action OnSpawnZombiEvent;
    public GameObject zombiPrefab;
    public Transform startPoint;
    public int zombiCount;
    public float spacing = 2f; // Distance between cubes
    public float radius = 2f;
    public LayerMask targetLayer;
    [SerializeField] ItemTag zombiTag;
    [SerializeField] ItemTag targetTag;
    [SerializeField] Transform container;

    GameObject[,] objectGrid;
    int count = 0;
    void FindAndRemove(Vector3 pos, float myRadius)
    {
        Vector3 point = pos;
        float radius = myRadius;

        Collider[] hits = Physics.OverlapSphere(point, radius * 5, targetLayer);

        foreach (Collider col in hits)
        {
            GameObject obj = col.gameObject;
            Destroy(obj);
        }
        StartCoroutine(CheckLastZombi());
    }

    IEnumerator CheckLastZombi()
    {
        yield return null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(zombiTag.ToString());

        if (enemies.Length == 0)
        {

            yield return new WaitForSeconds(1);
            OnSpawnZombiEvent?.Invoke();
            SpawnZombi();
        }
    }


    private void Start()
    {
        TargetStone.OnKnockDownToZombiEvent += TargetStone_OnKnockDownEvent;
        //TargetStone.OnHitByProjectile += TargetStone_OnHitByProjectile;

        SpawnZombi();
    }

    private void TargetStone_OnHitByProjectile(StoneType obj)
    {
        DestoryFrontLine(count);
        count++;
    }

    private void TargetStone_OnKnockDownEvent(Vector3 pos)
    {

        FindAndRemove(pos, radius);
    }

    public void SpawnZombi()
    {
        objectGrid = new GameObject[zombiCount, zombiCount];
        count = 0;
        int gridSize = Mathf.CeilToInt(Mathf.Pow(zombiCount, 1f / 3f)); // Cube root for 3D grid
        int y = 1;

        for (int x = 0; x < zombiCount; x++)
        {
            for (int z = 0; z < zombiCount; z++)
            {
                Vector3 position = new Vector3(x, y, z) * spacing;
                GameObject clone = Instantiate(zombiPrefab, position, Quaternion.identity);
                clone.transform.SetParent(container, false);
                objectGrid[x, z] = clone;
            }
        }
    }

    void DestoryFrontLine(int index)
    {
        for (int col = 0; col < zombiCount; col++)
        {
            GameObject obj = objectGrid[index, col];
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        CheckLastZombi();
    }

}

