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
    [SerializeField] private TerrainGeneration _mainTerrainGen;

    private Rigidbody2D _playerRB;
    private GameState _state;
    private float _o2Level;
    private WaitForSeconds _wait = new(.1f);
    private GameObject _HUD;
    private GameObject _ShopUI;
    private Camera _mainCamera;

    public enum GameState
    {
        Shop,
        Underwater,
        Total
    }

    private void Start()
    {
        // assign all the necessary references
        _playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        _HUD = GameObject.Find("HUD");
        _ShopUI = GameObject.Find("Shop UI");
        _mainTerrainGen = GameObject.FindObjectOfType<TerrainGeneration>();
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // set state to underwater
        _state = GameState.Shop;
        ChangeState(GameState.Underwater);
    }

    public void ChangeState(GameState newState)
    {
        switch (_state, newState)
        {
            case (GameState.Shop, GameState.Underwater):
                SetupUnderwater();
                break;
            case (GameState.Underwater, GameState.Shop):
                SetupShop();
                break;
        }
        _state = newState;
    }

    private void SetupUnderwater()
    {
        _ShopUI.gameObject.SetActive(false); // Hide shop UI
        _mainTerrainGen.ButtonNewTerrain(); // generate new random terrain with random seed

        if (_playerRB == null) // if player needs to be spawned
        {
            _playerRB = Instantiate(_playerPrefab, _spawnPosition, Quaternion.identity).GetComponent<Rigidbody2D>(); // spawn the player
            _mainCamera.gameObject.transform.SetPositionAndRotation(new Vector3(_spawnPosition.x, _spawnPosition.y + 5f, -10f), Quaternion.identity); // reset camera position
            _mainCamera.gameObject.GetComponent<CameraMovement>().TargetPlayer(); // reset target of camera to player
        }

        _o2Level = _o2Parameter.Value; // start the o2 meter
        StartCoroutine(DrainO2Co());
        _HUD.gameObject.SetActive(true); // show the HUD
    }

    private void SetupShop()
    {
        Destroy(_playerRB.gameObject);
        _mainTerrainGen.ButtonClearTerrain(); // clear terrain
        _HUD.gameObject.SetActive(false); // hide HUD
        StopCoroutine(DrainO2Co());
        _ShopUI.gameObject.SetActive(true); // Show shop UI
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
        yield return _wait;

        while (_state == GameState.Underwater)
        {
            _o2Level -= _idleO2DrainRate;                                   // Drain o2 over time
            _o2Level -= _playerRB.velocity.magnitude * _movingO2DrainRate;  // Drain o2 when moving

            if (_o2Level <= 0f) ChangeState(GameState.Shop); // Player dies of lack of o2

            yield return _wait;
        }
    }

    public float GetO2() {return _o2Level<=0 ? 0 : _o2Level;}
}
