using UnityEngine;

public class SkyboxBlender : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField] private GameObject starDome;
    [SerializeField] private float rotationSpeed = 1f;

    [SerializeField] private float fadeSpeed = 1f;
    private Material _starMat;
    private float _currentAlpha = 0f;

    private void Start()
    {
        _starMat = starDome.GetComponent<Renderer>().material;
        Color c = _starMat.GetColor("_TintColor");
        c.a = 0f;                // iniciar invisible
        _starMat.SetColor("_TintColor", c);

        starDome.SetActive(true); // está activo siempre, pero invisible
    }

    private void Update()
    {
        // Rotar el sol
        sun.transform.Rotate(Vector3.right * (rotationSpeed * Time.deltaTime));

        float sunAngle = sun.transform.eulerAngles.x;

        // SISTEMA DE TRANSICIÓN
        bool isNight = (sunAngle > 180f);

        // FADE-IN de estrellas
        if (isNight)
            _currentAlpha = Mathf.Lerp(_currentAlpha, 1f, fadeSpeed * Time.deltaTime);
        else
            _currentAlpha = Mathf.Lerp(_currentAlpha, 0f, fadeSpeed * Time.deltaTime);

        Color c = _starMat.GetColor("_TintColor");
        c.a = _currentAlpha;
        _starMat.SetColor("_TintColor", c);
    }
}