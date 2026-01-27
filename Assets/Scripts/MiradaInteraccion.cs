using UnityEngine;
using UnityEngine.UI; // Necesario para controlar la UI

public class MiradaInteraccion : MonoBehaviour
{
    public float distanciaRayo = 10f;     // Cuán lejos ve el jugador
    public float tiempoParaActivar = 2.0f; // Tiempo necesario mirando
    public GameObject panelMensaje;       // El Panel que creamos

    private float cronometro = 0f;
    private bool mirandoObjeto = false;

    void Update()
    {
        // Lanzamos un rayo desde el centro de la cámara hacia adelante
        Ray rayo = new Ray(transform.position, transform.forward);
        RaycastHit golpe;

        // Dibujamos el rayo en la escena (solo visible en el editor para pruebas)
        Debug.DrawRay(transform.position, transform.forward * distanciaRayo, Color.red);

        // Si el rayo choca con algo
        if (Physics.Raycast(rayo, out golpe, distanciaRayo))
        {
            // Verificamos si tiene la etiqueta "Objetivo"
            if (golpe.collider.CompareTag("Objetivo"))
            {
                mirandoObjeto = true;
                cronometro += Time.deltaTime; // Sumamos tiempo

                // Si superamos los 2 segundos
                if (cronometro >= tiempoParaActivar)
                {
                    MostrarMensaje();
                }
            }
            else
            {
                ResetearMirada(); // Miramos algo, pero no es el objetivo
            }
        }
        else
        {
            ResetearMirada(); // No miramos nada
        }
    }

    void ResetearMirada()
    {
        mirandoObjeto = false;
        cronometro = 0f;
        panelMensaje.SetActive(false); // Ocultamos el panel si dejamos de mirar
    }

    void MostrarMensaje()
    {
        panelMensaje.SetActive(true); // Mostramos el panel
    }
}