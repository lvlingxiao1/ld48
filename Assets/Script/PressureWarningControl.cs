using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureWarningControl : MonoBehaviour
{
    Transform player;
    int level = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
