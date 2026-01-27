using UnityEngine;
using UnityEngine.UI;

public class GazeSystem : MonoBehaviour
{
    public float gazeTime = 2f;
    public GameObject infoPanel;
    public Text infoText;

    private float timer;
    private GameObject currentTarget;

    void Start()
    {
        infoPanel.SetActive(false);
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10f))
        {
            if (hit.collider.gameObject != currentTarget)
            {
                currentTarget = hit.collider.gameObject;
                timer = 0f;
            }

            timer += Time.deltaTime;

            if (timer >= gazeTime)
            {
                ShowInfo(hit.collider.tag);
            }
        }
        else
        {
            currentTarget = null;
            timer = 0f;
            infoPanel.SetActive(false);
        }
    }

    void ShowInfo(string tag)
    {
        infoPanel.SetActive(true);

        if (tag == "ordenador")
        {
            infoText.text = "ORDENADOR\n\nDispositivo electrónico que procesa información.\n\nComponentes principales:\n- CPU (cerebro)\n- RAM (memoria)\n- GPU (gráficos)\n- Disco duro (almacenamiento)";
        }
    }
}