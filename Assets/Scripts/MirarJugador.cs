using UnityEngine;

public class MirarAlJugador : MonoBehaviour
{
    // Arrastra aquí a tu objeto "JugadorVR" o "PadreCamaras"
    public Transform cabezaJugador; 

    void Update()
    {
        if (cabezaJugador != null)
        {
            // Hacemos que el panel gire para mirar a tu cabeza
            // Al mirar a la cabeza y no a un ojo, los dos ojos lo ven bien centrado
            transform.LookAt(transform.position + cabezaJugador.rotation * Vector3.forward,
                             cabezaJugador.rotation * Vector3.up);
        }
    }
}