using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stats : MonoBehaviour
{
    [SerializeField]
    private int Health = 300;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int GetHealth()
    {
        return Health;
    }
    public void changeHealth(int value)
    {
        Health -= value;
    }
}
