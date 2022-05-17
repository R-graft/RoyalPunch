using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Canvas")]
    #region"Canvas"
    [SerializeField]
    private Button _pause;

    [SerializeField]
    private Button _claim;

    [SerializeField]
    private Button _start;

    [SerializeField]
    private Text _level;

    [SerializeField]
    private Text _money;

    [SerializeField]
    private Text _succsess;

    [SerializeField]
    private Text _playerHealthValue;

    [SerializeField]
    private Text _enemyHealthValue;

    [SerializeField]
    private Scrollbar _healthPlayer;

    [SerializeField]
    private Scrollbar _healthEnemy;

    [SerializeField]
    private Joystick _joystick;

    [SerializeField]
    private GameObject _pausePanel;

    [SerializeField]
    private GameObject _gamePanel;
    #endregion 

    [Header("Game")]

    [SerializeField]
    private Transform _player;

    [SerializeField]
    private Animator _playerAnimator;

    [SerializeField]
    private GameObject enemy;

    private Enemy _enemy;


    private void Awake()
    {
        _enemy = enemy.GetComponent<Enemy>();

        GameEvens.playerDamage.AddListener(PlayerHealthDecrement);
        GameEvens.enemyDamage.AddListener(EnemyHealthDecrement);
    
    }
    void Start()
    {
        MenuMode();
    }

    public void MenuMode()
    {
        CameraMove.Instance.MenuPosition();

        _player.position = new Vector3(0, 0.1f, -8f);

        _gamePanel.SetActive(false);
        _pausePanel.SetActive(false);

        _start.enabled = true;

        _playerAnimator.SetBool("StartBehaveur", true); 
    }
    public void GameMode()
    {
        CameraMove.Instance.StartCoroutine("MoveAway");

        _gamePanel.SetActive(true);

        _start.enabled = false;
       
        _playerAnimator.SetBool("StartBehaveur", false);

        _joystick.gameObject.SetActive(true);

        _enemy.CoroutineStart(true);

    }
    public void PauseMenu(bool value)
    {
        _enemy.CoroutineStart(!value);
        _pausePanel.SetActive(value);
        _joystick.gameObject.SetActive(!value);
    }
    void PlayerHealthDecrement()
    {

        _healthPlayer.size -= 0.002f;

    }
    void EnemyHealthDecrement()
    {

        _healthEnemy.size -= 0.002f;

    }


    void Update()
    {

    }
}
