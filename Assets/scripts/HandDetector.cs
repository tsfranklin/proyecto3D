using UnityEngine;

public class HandDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entró en trigger: " + other.name); // <- línea nueva
        if (other.TryGetComponent(out ComponentInfo info))
        {
            info.MostrarTooltip();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Salió del trigger: " + other.name); // <- línea nueva
        if (other.TryGetComponent(out ComponentInfo info))
        {
            info.OcultarTooltip();
        }
    }
}
