using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvansManager : MonoBehaviour
{
    public static CanvansManager instance;
    [SerializeField] private Image _dashCooldownImage;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        instance = this;
    }

    public void SetScoreText(float value)
    {
        _scoreText.text = value.ToString().Split('.')[0];
    }

    public void SetDashCooldownFill(float value)
    {
        _dashCooldownImage.fillAmount = value;
    }
}
