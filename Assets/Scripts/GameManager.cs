using UnityEngine;
using TMPro; // Si usas TextMeshPro
using UnityEngine.UI; // Si usas Texto normal (Legacy)
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI textoInstrucciones; // Cambia a 'Text' si usas Legacy
    
    [Header("Configuración")]
    public MiradaInteraccion scriptMirada; // Referencia a tu script de mirar
    public float tiempoLectura = 5.0f; // Tiempo que dejamos el panel abierto antes de pasar al siguiente

    // Lista de misiones: {TAG del objeto, Nombre para mostrar}
    private string[] ordenTags = { "Ordenador", "PlacaBase", "Ram", "Grafica", "FuenteAlimentacion", "RefrigeracionLiquida", "Ventilador" };
    private string[] nombresMostrar = { "el Ordenador Central", "la Placa Base", "la Memoria RAM", "la Tarjeta Gráfica", "la Fuente de Alimentación", "la Refrigeración Líquida", "los Ventiladores" };

    private int indiceActual = 0;
    private bool esperandoLectura = false;

    void Start()
    {
        // Iniciamos el primer objetivo
        ActualizarInstruccion();
    }

    void ActualizarInstruccion()
    {
        if (indiceActual < ordenTags.Length)
        {
            textoInstrucciones.text = "Busca " + nombresMostrar[indiceActual];
        }
        else
        {
            textoInstrucciones.text = "¡Juego Completado!";
        }
    }

    // Esta función la llamará tu script de MiradaInteraccion
    public bool ComprobarHallazgo(string tagMirado)
    {
        // Si estamos esperando que el usuario lea, bloqueamos todo
        if (esperandoLectura) return false;

        // Si el juego ha terminado, no hacemos nada
        if (indiceActual >= ordenTags.Length) return true; // Dejamos interactuar libremente al final

        // Comprobamos si lo que mira es lo que toca
        if (tagMirado == ordenTags[indiceActual])
        {
            StartCoroutine(SecuenciaAcierto());
            return true; // Le decimos al otro script: "Sí, muestra el panel"
        }

        return false; // "No, no muestres nada, no es lo que busco"
    }

    IEnumerator SecuenciaAcierto()
    {
        esperandoLectura = true;
        textoInstrucciones.text = "¡Correcto!";
        
        // Esperamos X segundos para que el usuario lea el panel del componente
        yield return new WaitForSeconds(tiempoLectura);

        // Cerramos los paneles (llamamos a tu script para que resetee)
        scriptMirada.ForzarCierrePaneles(); // AÑADIREMOS ESTO A TU SCRIPT
        
        // Pasamos al siguiente
        indiceActual++;
        esperandoLectura = false;
        ActualizarInstruccion();
    }
}