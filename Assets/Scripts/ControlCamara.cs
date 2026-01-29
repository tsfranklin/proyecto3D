using UnityEngine;
using System.Collections;

public class ControlCamara : MonoBehaviour
{
    private GameObject camParent;
    public float velocidadSuavizado = 5.0f;
    
    // Variable para corregir si mira muy arriba o abajo manualmente (en grados)
    // Si sigue mirando abajo, prueba a poner aquí -10 o -20. Si mira arriba, pon 10 o 20.
    public float correccionVertical = 0f; 

    void Start()
    {
        // Crear el padre para ro tarlo y corregir la dirección
        camParent = new GameObject("PadreCamara");
        camParent.transform.position = this.transform.position;
        this.transform.parent = camParent.transform;

        Input.gyro.enabled = true;

        // Iniciamos la calibración (esperamos un poco a que el giroscopio se despierte)
        StartCoroutine(CalibrarInicio());
    }

    IEnumerator CalibrarInicio()
    {
        yield return new WaitForSeconds(0.5f); // Esperamos medio segundo

        // 1. Reseteamos la rotación del padre
        camParent.transform.rotation = Quaternion.identity;

        // 2. Leemos hacia dónde está mirando la cámara ahora mismo (solo en horizontal, eje Y)
        float rotacionActualY = transform.localEulerAngles.y;

        // 3. Giramos al PADRE en sentido contrario para que la cámara quede mirando al frente (Z = 0)
        camParent.transform.rotation = Quaternion.Euler(0, -rotacionActualY, 0);
    }

    void Update()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Quaternion rotacionDispositivo = Input.gyro.attitude;
            
            // Calculamos la rotación base
            Quaternion rotacionObjetivo = Quaternion.Euler(90 + correccionVertical, 0, 0) * new Quaternion(rotacionDispositivo.x, rotacionDispositivo.y, -rotacionDispositivo.z, -rotacionDispositivo.w);

            // Aplicamos movimiento suave
            this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, rotacionObjetivo, velocidadSuavizado * Time.deltaTime);
        }
    }
}