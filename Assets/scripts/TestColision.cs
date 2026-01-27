using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestColision : MonoBehaviour
{
    void Start()
    {
        Debug.Log("TestColision: Script iniciado en " + gameObject.name);
    }

    void Update()
    {
        Debug.Log("TestColision: Update funcionando - Frame: " + Time.frameCount);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLISIÓN DETECTADA con: " + collision.gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER DETECTADO con: " + other.gameObject.name);
    }
}