using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController controller;

    public float speed = 3f;
    public GameObject win;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * 0.0025f);
        transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);

    }

    void OnFinish(Collider collider)
    {
        if (collider.gameObject.tag == "Finish")
        {
            Debug.Log("hahaha");
        }

    }
}
