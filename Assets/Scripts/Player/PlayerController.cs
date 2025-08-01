using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public TurnManager TurnManager;
    public UnitManager UnitManager;
    public PathFinding PathFinding;
    public UIManager UIManager;
    public HexGrid HexGrid;

    public bool IsPointerOverUI = false;

    private IState _currentState;

    private int _tileLayerMask;

    [HideInInspector] public Unit SelectedUnitComponent;

    [HideInInspector] public UnityEvent<Unit> OnUnitSelected = new UnityEvent<Unit>();
    [HideInInspector] public UnityEvent<Vector2Int> OnCityFounded = new UnityEvent<Vector2Int>();

    void Awake()
    {
        _tileLayerMask = LayerMask.GetMask("Tile");

    }
    void Start()
    {
        ChangeState(new DefaultState(this));
    }
    private void Update()
    {
        IsPointerOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
    public void ChangeState(IState newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = newState;
        _currentState.Enter();
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

    public void OnBuildUnitRequested(CraftablesScriptableObject craftable)
    {
        _currentState.OnBuildUnitRequested(craftable);
    }

    public void NotifyUnitSelected(Unit selectedUnitComponent)
    {
        SelectedUnitComponent = selectedUnitComponent;
        OnUnitSelected.Invoke(selectedUnitComponent);
    }
    public void NotifyCityFounded()
    {
        OnCityFounded.Invoke(HexGrid.GetIntCordsFromPosition(SelectedUnitComponent.transform.position));
    }
}
