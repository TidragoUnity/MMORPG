﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mobname
{
    GhostTiger = 0,
    SpiderGreenMesh,
    TrollGiant,

}

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
          //  t.tag = "mob";
            mobCount++;
        }

    }
    public void SpawnMob(float x, float y, float z, int MobID, int type)
    {
        Vector3 pos = new Vector3(x, y, z);

        // Instantiate(prefab, pos, Quaternion.identity);
        GameObject t = ((GameObject)Instantiate(prefab, pos, Quaternion.identity));
        t.transform.parent = transform;
        t.GetComponent<stats>().MobID = MobID;
        t.GetComponent<stats>().type = type;
       // t.tag = "mob";

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        Gizmos.DrawCube(center, size);
        
    }
}
