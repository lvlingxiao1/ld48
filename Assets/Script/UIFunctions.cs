using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctions : MonoBehaviour
{
    public void RespawnPlayer(){
        FindObjectOfType<PlayerController>().Respawn();
    }
}
