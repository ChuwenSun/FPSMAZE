using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject yin;


    private void OnTriggerEnter(Collider other)
    {
        yin.SetActive(true);
    }
}
