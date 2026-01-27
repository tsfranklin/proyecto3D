using UnityEngine;

public class CambiarColorAlTocar : MonoBehaviour
{
    [Header("Configuración de Color")]
    public Color colorOriginal = Color.white;
    public Color colorAlTocar = Color.green;

    private Renderer pelotaRenderer;

    void Start()
    {
        pelotaRenderer = GetComponent<Renderer>();

        if (pelotaRenderer != null)
        {
            pelotaRenderer.material = new Material(pelotaRenderer.material);
            pelotaRenderer.material.color = colorOriginal;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Detecta cualquier colisión, no solo el ordenador
        Debug.Log("¡Colisión detectada con: " + collision.gameObject.name);
        CambiarColor(colorAlTocar);
    }

    void CambiarColor(Color nuevoColor)
    {
        if (pelotaRenderer != null)
        {
            pelotaRenderer.material.color = nuevoColor;
        }
    }
}