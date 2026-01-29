using UnityEngine;
using UnityEngine.UI;

public class MiradaInteraccion : MonoBehaviour
{
    public float distanciaRayo = 10f;
    public float tiempoParaActivar = 1.5f;

    // --- NUEVO: Referencia al GameManager ---
    public GameManager gameManager; 
    // ----------------------------------------

    [Header("Paneles de Texto (UI)")]
    public GameObject panelMensajeGrafica;
    public GameObject panelMensajePlacaBase;
    public GameObject panelMensajeFuenteAlimentacion;
    public GameObject panelMensajeVentilador;
    public GameObject panelMensajeRefrigeracionLiquida;
    public GameObject panelMensajeRam;

    [Header("Modelos 3D")]
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
        // Apagar modelos al inicio
        if(Grafica) Grafica.SetActive(false);
        if(PlacaBase) PlacaBase.SetActive(false);
        if(FuenteAlimentacion) FuenteAlimentacion.SetActive(false);
        if(Ventilador) Ventilador.SetActive(false);
        if(RefrigeracionLiquida) RefrigeracionLiquida.SetActive(false);
        if(Ram) Ram.SetActive(false);
    }

    void Update()
    {
        Ray rayo = new Ray(transform.position, transform.forward);
        RaycastHit golpe;

        Debug.DrawRay(transform.position, transform.forward * distanciaRayo, Color.red);

        if (Physics.Raycast(rayo, out golpe, distanciaRayo))
        {
            string tagGolpe = golpe.collider.tag;

            // --- NUEVO: FILTRO DEL JUEGO ---
            // Si el GameManager existe y NO es el objeto correcto, ignoramos
            if (gameManager != null)
            {
                // Solo permitimos procesar si el GameManager dice que es el correcto
                // PERO: Si es el "Ordenador" (para revelar piezas), dejamos que pase siempre si no se ha revelado
                if (tagGolpe != "Ordenador" && !gameManager.ComprobarHallazgo(tagGolpe))
                {
                   ResetearMirada();
                   return; 
                }
            }
            // -------------------------------

            if (tagGolpe == "Grafica" || tagGolpe == "PlacaBase" || tagGolpe == "FuenteAlimentacion" || 
                tagGolpe == "Ventilador" || tagGolpe == "RefrigeracionLiquida" || tagGolpe == "Ram")
            {
                ProcesarMirada(tagGolpe);
            }
            else if (tagGolpe == "Ordenador")
            {
                // Lógica del ordenador (primera fase)
                 if (!componentesYaRevelados)
                 {
                    cronometro += Time.deltaTime;
                    if (cronometro >= tiempoParaActivar)
                    {
                        RevelarComponentes();
                        // Avisamos al juego que hemos empezado
                        if(gameManager != null) gameManager.ComprobarHallazgo("Ordenador");
                    }
                 }
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
            // Avisamos al Manager que ya hemos completado la visualización
            if(gameManager != null) gameManager.ComprobarHallazgo(tagObjeto);
        }
    }

    void ResetearMirada()
    {
        cronometro = 0f;
        objetoActual = "";
        OcultarTodosLosPaneles();
    }

    // --- NUEVO: Función pública para que el GameManager cierre todo ---
    public void ForzarCierrePaneles()
    {
        OcultarTodosLosPaneles();
        cronometro = 0f;
        objetoActual = "";
    }
    // ----------------------------------------------------------------

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