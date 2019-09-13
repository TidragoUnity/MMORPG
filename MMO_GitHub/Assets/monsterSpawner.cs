using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterSpawner : MonoBehaviour
{
    public GameObject prefab;

    
    public Vector3 center;
    public Vector3 size;
    public Color color;



    int mobCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q)) Spawn(); return;

    }
    private void FixedUpdate()
    {
        mobCount = transform.childCount;
    }

    public void Spawn()
    {
        if(mobCount < 10)
        {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));

           // Instantiate(prefab, pos, Quaternion.identity);
           GameObject t = ((GameObject)Instantiate(prefab, pos, Quaternion.identity));
           t.transform.parent = transform;

            mobCount++;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        Gizmos.DrawCube(center, size);
        
    }
}
