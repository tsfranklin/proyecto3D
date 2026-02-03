using UnityEngine;
using UnityEngine.UI;

public class MiradaInteraccion : MonoBehaviour
{
    public float distanciaRayo = 10f;
    public float tiempoParaActivar = 1.5f;
    public GameManager gameManager; 

    [Header("Paneles")]
    public GameObject panelMensajeGrafica;
    public GameObject panelMensajePlacaBase;
    public GameObject panelMensajeFuenteAlimentacion;
    public GameObject panelMensajeVentilador;
    public GameObject panelMensajeRefrigeracionLiquida;
    public GameObject panelMensajeRam;

    [Header("Piezas 3D")]
    public GameObject Grafica;
    public GameObject PlacaBase;
    public GameObject FuenteAlimentacion;
    public GameObject Ventilador;
    public GameObject RefrigeracionLiquida;
    public GameObject Ram;

    private float cronometro = 0f;
    private string objetoActual = ""; 
    private bool componentesYaRevelados = false; 

    void Start()
    {
        OcultarTodosLosPaneles();
    }

    void Update()
    {
        Ray rayo = new Ray(transform.position, transform.forward);
        RaycastHit golpe;

        if (Physics.Raycast(rayo, out golpe, distanciaRayo))
        {
            string tag = golpe.collider.tag;

            // 1. Si es el ordenador y no está revelado, prioridad a este.
            if (tag == "Ordenador")
            {
                 if (!componentesYaRevelados)
                 {
                    cronometro += Time.deltaTime;
                    if (cronometro >= tiempoParaActivar)
                    {
                        RevelarComponentes();
                        if(gameManager) gameManager.ConfirmarHallazgo(); 
                    }
                 }
                 return;
            }

            // 2. Filtro del Juego
            if (gameManager != null)
            {
                // Solo procesamos si es el objeto correcto
                if (!gameManager.EsElObjetoCorrecto(tag))
                {
                   ResetearMirada();
                   return; 
                }
            }

            // 3. Lógica normal de abrir paneles
            if (tag == "Grafica" || tag == "PlacaBase" || tag == "FuenteAlimentacion" || 
                tag == "Ventilador" || tag == "RefrigeracionLiquida" || tag == "Ram")
            {
                ProcesarMirada(tag);
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
            // 1. Mostramos el panel
            MostrarMensaje(tagObjeto);
            
            // 2. Y AHORA avisamos al manager de que hemos ganado
            if(gameManager != null) gameManager.ConfirmarHallazgo();
        }
    }

    void ResetearMirada()
    {
        cronometro = 0f;
        objetoActual = "";
        OcultarTodosLosPaneles();
    }

    public void ForzarCierrePaneles()
    {
        OcultarTodosLosPaneles();
        cronometro = 0f;
        objetoActual = "";
    }

    void OcultarTodosLosPaneles()
    {
        if(panelMensajeGrafica) panelMensajeGrafica.SetActive(false);
        if(panelMensajePlacaBase) panelMensajePlacaBase.SetActive(false);
        if(panelMensajeFuenteAlimentacion) panelMensajeFuenteAlimentacion.SetActive(false);
        if(panelMensajeVentilador) panelMensajeVentilador.SetActive(false);
        if(panelMensajeRefrigeracionLiquida) panelMensajeRefrigeracionLiquida.SetActive(false);
        if(panelMensajeRam) panelMensajeRam.SetActive(false);
    }

    void MostrarMensaje(string tag)
    {
        if (tag == "Grafica") panelMensajeGrafica.SetActive(true);
        if (tag == "PlacaBase") panelMensajePlacaBase.SetActive(true);
        if (tag == "FuenteAlimentacion") panelMensajeFuenteAlimentacion.SetActive(true);
        if (tag == "Ventilador") panelMensajeVentilador.SetActive(true);
        if (tag == "RefrigeracionLiquida") panelMensajeRefrigeracionLiquida.SetActive(true);
        if (tag == "Ram") panelMensajeRam.SetActive(true);
    }

    void RevelarComponentes()
    {
        if(Grafica) Grafica.SetActive(true);
        if(PlacaBase) PlacaBase.SetActive(true);
        if(FuenteAlimentacion) FuenteAlimentacion.SetActive(true);
        if(Ventilador) Ventilador.SetActive(true);
        if(RefrigeracionLiquida) RefrigeracionLiquida.SetActive(true);
        if(Ram) Ram.SetActive(true);
        
        componentesYaRevelados = true;
    }
}