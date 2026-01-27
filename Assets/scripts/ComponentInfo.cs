    using UnityEngine;

    public class ComponentInfo : MonoBehaviour
    {
        [Header("Datos del componente")]
        public string nombre;            // Nombre del componente
        [TextArea]
        public string descripcion;       // Descripción del componente

        [Header("Tooltip")]
        public GameObject tooltipPrefab; // Prefab que creaste
        private GameObject tooltipActual;

        public void MostrarTooltip()
        {
            if (tooltipActual == null)
            {
                // Busca el Canvas de la escena
                Canvas canvas = FindObjectOfType<Canvas>();
                tooltipActual = Instantiate(tooltipPrefab, canvas.transform);

                // Posición del panel sobre el objeto en pantalla
                Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.3f, 0));
                tooltipActual.transform.position = screenPos;

                tooltipActual.GetComponent<Tooltip3D>().EstablecerInfo(nombre, descripcion);
            }
        }


        public void OcultarTooltip()
        {
            if (tooltipActual != null)
                Destroy(tooltipActual);
        }
    }
