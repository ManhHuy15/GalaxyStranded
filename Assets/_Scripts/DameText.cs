using TMPro;
using UnityEngine;

public class DameText : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private RectTransform rectTransform;
    private Color defaultColor;
    private RectTransform defaultTransform;
    private float timeLive = 0.5f;
    private float speed = 100f;
    private float timeElapsed = 0.0f;
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        defaultColor = textMeshPro.color;
        defaultTransform = rectTransform;
    }


    void Update()
    {
        timeElapsed += Time.deltaTime;


        rectTransform.position += new Vector3(0, speed * Time.deltaTime,0);

        textMeshPro.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 1 -(timeElapsed / timeLive));
        if (timeElapsed > timeLive)
        {
            gameObject.SetActive(false);
            timeElapsed = 0f;
            textMeshPro.color = defaultColor;
            rectTransform = defaultTransform;
        }
    }
}
