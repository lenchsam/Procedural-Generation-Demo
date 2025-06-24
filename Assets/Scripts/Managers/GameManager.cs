using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("System References")]
    [SerializeField] private ProceduralGeneration _proceduralGeneration;
    [SerializeField] private TurnManager _turnManager;
    [SerializeField] private GameObject _startUnitPrefab;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private UIManager _uiManager;

    [Header("Prefabs")]
    [SerializeField] private GameObject _cityPrefab;

    void Start()
    {
        _proceduralGeneration.OnMapGenerated.AddListener(SetupNewGame);
        _playerController.OnCityFounded.AddListener(OnNewCityFound);
    }
    private void OnDestroy()
    {
        _proceduralGeneration.OnMapGenerated.RemoveListener(SetupNewGame);
        _playerController.OnCityFounded.RemoveListener(OnNewCityFound);
    }
    #region new game setup
    private void SetupNewGame()
    {
        Vector2Int[] startPositions = _proceduralGeneration.Points.ToArray();

        _turnManager.InitializePlayers(startPositions, _startUnitPrefab);
    }
    #endregion

    #region founding new city
    private void OnNewCityFound(Vector2Int location)
    {
        Debug.Log("New city Founded at" + location);

        GameObject.Instantiate(_cityPrefab, _playerController.HexGrid.GetTileFromIntCords(location).transform.position, _cityPrefab.transform.rotation);

        _playerController.ChangeState(new DefaultState(_playerController));
    }
    #endregion
}
