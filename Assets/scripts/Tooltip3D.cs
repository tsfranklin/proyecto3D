using TMPro;
using UnityEngine;

public class Tooltip3D : MonoBehaviour
{
    public TextMeshProUGUI tituloText;       // <- debe ser TextMeshProUGUI
    public TextMeshProUGUI descripcionText;  // <- debe ser TextMeshProUGUI

    public void EstablecerInfo(string titulo, string descripcion)
    {
        if (tituloText != null)
            tituloText.text = titulo;
        if (descripcionText != null)
            descripcionText.text = descripcion;
    }

    void Update()
    {
        if (Camera.main != null)
            transform.LookAt(Camera.main.transform);
    }
}
