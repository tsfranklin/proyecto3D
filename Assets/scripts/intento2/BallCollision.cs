using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        string mensaje;

        // Intenta obtener el componente ObjectInfo del objeto colisionado
        ObjectInfo info = collision.gameObject.GetComponent<ObjectInfo>();

        if (info != null)
        {
            // Si tiene el script ObjectInfo, usa su mensaje personalizado
            mensaje = info.ObtenerMensaje();
        }
        else
        {
            // Si no tiene el script, usa el nombre del objeto
            string nombreObjeto = collision.gameObject.name;
            mensaje = "¡Has tocado el objeto: " + nombreObjeto + "!";
        }

        // Muestra el pop-up con el mensaje
        PopUpManager.Instance.MostrarMensaje(mensaje);

        Debug.Log("Colisión: " + mensaje);
    }
}
