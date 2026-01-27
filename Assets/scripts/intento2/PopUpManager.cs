using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.Collections;

using TMPro; // Para TextMeshPro

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance;

    [Header("Referencias UI")]
    [SerializeField] private GameObject panelPopUp;
    [SerializeField] private TextMeshProUGUI textoMensaje; // Cambiado a TextMeshPro

    [Header("Configuración")]
    [SerializeField] private float tiempoVisible = 2f;

    private void Awake()
    {
        // Singleton para acceder desde cualquier script
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Asegurarse de que el pop-up esté oculto al inicio
        if (panelPopUp != null)
            panelPopUp.SetActive(false);
    }

    private void Start()
    {
        // Si no asignaste el texto manualmente, intenta encontrarlo automáticamente
        if (textoMensaje == null && panelPopUp != null)
        {
            textoMensaje = panelPopUp.GetComponentInChildren<TextMeshProUGUI>();
            if (textoMensaje != null)
                Debug.Log("Texto encontrado automáticamente!");
        }
    }

    public void MostrarMensaje(string mensaje)
    {
        if (panelPopUp == null)
        {
            Debug.LogError("Falta el Panel en el PopUpManager!");
            return;
        }

        if (textoMensaje == null)
        {
            Debug.LogError("Falta el Texto en el PopUpManager!");
            return;
        }

        textoMensaje.text = mensaje;
        panelPopUp.SetActive(true);

        // Ocultar después de un tiempo
        StartCoroutine(OcultarDespuesDeTiempo());
    }

    private IEnumerator OcultarDespuesDeTiempo()
    {
        yield return new WaitForSeconds(tiempoVisible);
        panelPopUp.SetActive(false);
    }

    public void CerrarPopUp()
    {
        panelPopUp.SetActive(false);
    }
}