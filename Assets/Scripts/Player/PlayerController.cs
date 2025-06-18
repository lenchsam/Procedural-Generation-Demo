using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public TurnManager TurnManager;
    public UnitManager UnitManager;
    public PathFinding PathFinding;
    public HexGrid HexGrid;

    private IState _currentState;

    private int _tileLayerMask;

    void Awake()
    {
        _tileLayerMask = LayerMask.GetMask("Tile");

    }
    void Start()
    {
        ChangeState(new DefaultState(this));
    }

    public void ChangeState(IState newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = newState;
        _currentState.Enter();

        Debug.Log($"PlayerController changed state to: {_currentState.GetType().Name}");
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        //only want the first moment its clicked
        if (!context.performed) { return; }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _tileLayerMask))
        {
            TileScript clickedTile = hit.collider.GetComponent<TileScript>();
            if (clickedTile != null)
            {
                _currentState.OnTileClicked(clickedTile);
            }
        }
    }
}
