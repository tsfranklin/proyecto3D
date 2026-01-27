using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    [Header("Mensaje personalizado")]
    [TextArea(2, 4)]
    [SerializeField] private string mensajePersonalizado = "Has tocado este objeto";

    public string ObtenerMensaje()
    {
        return mensajePersonalizado;
    }
}