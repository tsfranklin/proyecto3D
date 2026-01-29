using UnityEngine;
using UnityEngine.UI;

public class MiradaInteraccion : MonoBehaviour
{
    public float distanciaRayo = 10f;
    public float tiempoParaActivar = 2.0f;

    // Solo los 4 paneles básicos
    public GameObject panelMensajeGrafica;
    public GameObject panelMensajePlacaBase;
    public GameObject panelMensajeFuenteAlimentacion;
    public GameObject panelMensajeVentilador;
    public GameObject panelMensajeRefrigeracionLiquida;
    public GameObject panelMensajeOrdenador;
    public GameObject panelMensajeRam;


    private float cronometro = 0f;
    private string objetoActual = ""; 

    void Update()
    {
        // Lanzamos el rayo
        Ray rayo = new Ray(transform.position, transform.forward);
        RaycastHit golpe;

        // Dibujo para debug (solo se ve en Scene)
        Debug.DrawRay(transform.position, transform.forward * distanciaRayo, Color.red);

        if (Physics.Raycast(rayo, out golpe, distanciaRayo))
        {
            if (golpe.collider.CompareTag("Grafica"))
            {
                ProcesarMirada("Grafica");
            }
            else if (golpe.collider.CompareTag("PlacaBase"))
            {
                ProcesarMirada("PlacaBase");
            }
            else if (golpe.collider.CompareTag("FuenteAlimentacion"))
            {
                ProcesarMirada("FuenteAlimentacion");
            }
            else if (golpe.collider.CompareTag("Ventilador"))
            {
                ProcesarMirada("Ventilador");
            }
            else if (golpe.collider.CompareTag("RefrigeracionLiquida"))
            {
                ProcesarMirada("RefrigeracionLiquida");
            }
            else if (golpe.collider.CompareTag("Ordenador"))
            {
                ProcesarMirada("Ordenador");
            }
            else if (golpe.collider.CompareTag("Ram"))
            {
                ProcesarMirada("Ram");
            }
            else
            {
                ResetearMirada();
            }
            
        }
        else
        {
            ResetearMirada();
        }
    }

    void ProcesarMirada(string tagObjeto)
    {
        if (objetoActual != tagObjeto)
        {
            cronometro = 0f;
            objetoActual = tagObjeto;
            OcultarTodosLosPaneles(); 
        }

        cronometro += Time.deltaTime;

        if (cronometro >= tiempoParaActivar)
        {
            MostrarMensaje(tagObjeto);
        }
    }

    void ResetearMirada()
    {
        cronometro = 0f;
        objetoActual = "";
        OcultarTodosLosPaneles();
    }

    void OcultarTodosLosPaneles()
    {
        if(panelMensajeGrafica) panelMensajeGrafica.SetActive(false);
        if(panelMensajePlacaBase) panelMensajePlacaBase.SetActive(false);
        if(panelMensajeFuenteAlimentacion) panelMensajeFuenteAlimentacion.SetActive(false);
        if(panelMensajeVentilador) panelMensajeVentilador.SetActive(false);
        if(panelMensajeRefrigeracionLiquida) panelMensajeRefrigeracionLiquida.SetActive(false);
        if(panelMensajeOrdenador) panelMensajeOrdenador.SetActive(false);
        if(panelMensajeRam) panelMensajeRam.SetActive(false);
    }

    void MostrarMensaje(string tag)
    {
        if (tag == "Grafica") panelMensajeGrafica.SetActive(true);
        if (tag == "PlacaBase") panelMensajePlacaBase.SetActive(true);
        if (tag == "FuenteAlimentacion") panelMensajeFuenteAlimentacion.SetActive(true);
        if (tag == "Ventilador") panelMensajeVentilador.SetActive(true);
        if (tag == "RefrigeracionLiquida") panelMensajeRefrigeracionLiquida.SetActive(true);
        if (tag == "Ordenador") panelMensajeOrdenador.SetActive(true);
        if (tag == "Ram") panelMensajeRam.SetActive(true);
    }
}