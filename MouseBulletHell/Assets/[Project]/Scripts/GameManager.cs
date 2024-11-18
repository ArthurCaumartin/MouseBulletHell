using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //! definire un global game speed et voila
    public static GameManager instance;
    [SerializeField] private float _currentScore;
    [SerializeField] private float _scoreToLoseOnHit = 50;
    public UnityEvent _onPlayerHit;

    public float Score
    {
        get { return _currentScore; }
        set
        {
            _currentScore = value;
            if (_currentScore <= 0) _currentScore = 0;
            CanvansManager.instance.SetScoreText(_currentScore);
        }
    }

    private void Awake()
    {
        Score += 10;

        if (instance) Destroy(gameObject);
        instance = this;
    }

    void Start()
    {
        Score = 0;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnPlayerHit()
    {
        Score -= _scoreToLoseOnHit;
        _onPlayerHit.Invoke();
    }
}
