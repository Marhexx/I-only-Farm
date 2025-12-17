using UnityEngine;

public class SkyboxBlender : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Light sun;
    [SerializeField] private GameObject starDome;

    [Header("Day/Night Settings")]
    [Tooltip("Duración de un día completo (24h) en minutos reales")]
    [SerializeField] private float dayDurationMinutes = 5f;

    [SerializeField] private float fadeSpeed = 1f;

    private Material _starMat;
    private float _currentAlpha = 0f;
    private float _degreesPerSecond;

    private void Start()
    {
        // Cálculo de velocidad del sol
        _degreesPerSecond = 360f / (dayDurationMinutes * 60f);

        _starMat = starDome.GetComponent<Renderer>().material;

        Color c = _starMat.GetColor("_TintColor");
        c.a = 0f;
        _starMat.SetColor("_TintColor", c);

        starDome.SetActive(true);
    }

    private void Update()
    {
        // Rotación del sol (24h completas)
        sun.transform.Rotate(Vector3.right * (_degreesPerSecond * Time.deltaTime));

        float sunAngle = sun.transform.eulerAngles.x;

        // Noche cuando el sol está debajo del horizonte
        bool isNight = sunAngle > 180f;

        // Fade de estrellas
        float targetAlpha = isNight ? 1f : 0f;
        _currentAlpha = Mathf.Lerp(_currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);

        Color c = _starMat.GetColor("_TintColor");
        c.a = _currentAlpha;
        _starMat.SetColor("_TintColor", c);
    }
}