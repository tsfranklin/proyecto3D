using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI textoInstrucciones; 
    
    [Header("Configuración")]
    public MiradaInteraccion scriptMirada; 
    public float tiempoLectura = 5.0f; 

    // Los datos iniciales
    private string[] ordenTags = { "Ordenador", "PlacaBase", "Ram", "Grafica", "FuenteAlimentacion", "RefrigeracionLiquida", "Ventilador" };
    private string[] nombresMostrar = { "el Ordenador Central", "la Placa Base", "la Memoria RAM", "la Tarjeta Gráfica", "la Fuente de Alimentación", "la Refrigeración Líquida", "los Ventiladores" };

    private int indiceActual = 0;
    private bool esperandoLectura = false; 

    void Start()
    {
        //Barajamos antes de empezar el juego.
        BarajarLista();

        // 2. Empezamos el juego
        ActualizarInstruccion();
    }

    void BarajarLista()
    {
        // El ordenador tiene que ser el primero obligatoriamente para revelar las piezas.
        for (int i = 1; i < ordenTags.Length; i++)
        {
            // Elegimos una posición al azar (entre el 1 y el final)
            int aleatorio = Random.Range(1, ordenTags.Length);

            // Intercambiamos los TAGS
            string tempTag = ordenTags[i];
            ordenTags[i] = ordenTags[aleatorio];
            ordenTags[aleatorio] = tempTag;

            // Intercambiamos los NOMBRES (para que coincidan con su tag)
            string tempNombre = nombresMostrar[i];
            nombresMostrar[i] = nombresMostrar[aleatorio];
            nombresMostrar[aleatorio] = tempNombre;
        }
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

    public bool EsElObjetoCorrecto(string tagMirado)
    {
        if (indiceActual >= ordenTags.Length) return true;

        if (esperandoLectura)
        {
            return tagMirado == ordenTags[indiceActual];
        }

        return tagMirado == ordenTags[indiceActual];
    }

    public void ConfirmarHallazgo()
    {
        if (esperandoLectura) return;

        if (indiceActual < ordenTags.Length)
        {
            StartCoroutine(SecuenciaAcierto());
        }
    }

    IEnumerator SecuenciaAcierto()
    {
        esperandoLectura = true;
        textoInstrucciones.text = "¡Correcto! Lee la información.";
        
        yield return new WaitForSeconds(tiempoLectura);

        scriptMirada.ForzarCierrePaneles(); 
        
        indiceActual++;
        esperandoLectura = false;
        ActualizarInstruccion();
    }
}