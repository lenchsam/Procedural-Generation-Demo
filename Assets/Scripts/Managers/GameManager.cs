using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("System References")]
    [SerializeField] private ProceduralGeneration _proceduralGeneration;
    [SerializeField] private TurnManager _turnManager;
    [SerializeField] private GameObject _startUnitPrefab;
    void Start()
    {
        _proceduralGeneration.OnMapGenerated.AddListener(SetupNewGame);
    }
    private void SetupNewGame()
    {
        Vector2Int[] startPositions = _proceduralGeneration.Points.ToArray();

        _turnManager.InitializePlayers(startPositions, _startUnitPrefab);
    }

    private void OnDestroy()
    {
        _proceduralGeneration.OnMapGenerated.RemoveListener(SetupNewGame);
    }
}
