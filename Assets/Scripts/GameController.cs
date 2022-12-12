using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] private ParameterScriptableObject _o2Parameter;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Vector3 _spawnPosition;
    [SerializeField] private float _idleO2DrainRate;
    [SerializeField] private float _movingO2DrainRate;

    private Rigidbody2D _playerRB;
    private GameState _state;
    private float _o2Level;
    private WaitForSeconds _wait = new(.1f);

    public enum GameState
    {
        Shop,
        Underwater,
        Total
    }

    private void Start()
    {
        _state = GameState.Underwater;
        _playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        _o2Level = _o2Parameter.Value;
        StartCoroutine(DrainO2Co());
        StartCoroutine(PrintO2Level());
    }

    public void ChangeState(GameState newState)
    {
        switch (_state, newState)
        {
            case (GameState.Shop, GameState.Underwater):
                _playerRB = Instantiate(_playerPrefab, _spawnPosition, Quaternion.identity).GetComponent<Rigidbody2D>();
                _o2Level = _o2Parameter.Value;
                StartCoroutine(DrainO2Co());
                // Hide shop UI
                break;
            case (GameState.Underwater, GameState.Shop):
                Destroy(_playerRB.gameObject);
                // Show shop UI
                break;
        }
    }

    public void DrainO2(float drainAmount) => _o2Level -= drainAmount;

    private void Awake()
    {
        // Ensure there is only one instance of GameController
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private IEnumerator DrainO2Co()
    {
        while (_state == GameState.Underwater)
        {
            _o2Level -= _idleO2DrainRate; // Drain o2 over time
            _o2Level -= _playerRB.velocity.magnitude * _movingO2DrainRate; // Drain o2 when moving

            if (_o2Level <= 0f) ChangeState(GameState.Shop); // Player dies of lack of o2

            yield return _wait;
        }
    }

    public IEnumerator PrintO2Level()
    {
        while (_state == GameState.Underwater)
        {
            Debug.Log("current o2: " + _o2Level);
            yield return new WaitForSeconds(1f);
        }
    }
}
