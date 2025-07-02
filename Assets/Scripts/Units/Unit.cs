using UnityEngine;

public class Unit : MonoBehaviour, IAttacking
{
    public e_UnitType UnitType;
    public e_Team Team;
    public int DamageRange = 1;
    private int _damage = 50;
    [SerializeField] private int _MaxMovementPoints = 1;
    private int _movementPoints;

    private HexGrid _hexGrid;

    private TurnManager _turnManager;

    void Awake()
    {
        _hexGrid = FindAnyObjectByType<HexGrid>();
        _turnManager = FindAnyObjectByType<TurnManager>();

        _turnManager.OnTurnEnd.AddListener(ResetMovementPoints);

        _movementPoints = _MaxMovementPoints;
    }
    public int GetMovementPoints()
    {
        return _movementPoints;
    }
    
    public void ResetMovementPoints()
    {
        _movementPoints = _MaxMovementPoints;
    }
    public void TakeMovementPoints(int pointsToTake)
    {
        _movementPoints -= pointsToTake;
    }
    public void Attack(Unit thingToAttack)
    {
        if(_hexGrid.DistanceBetweenTiles(_hexGrid.GetIntCordsFromPosition(this.gameObject.transform.position), _hexGrid.GetIntCordsFromPosition(thingToAttack.gameObject.transform.position)) > DamageRange)
        {
            return;
        }
        thingToAttack.gameObject.gameObject.GetComponent<Health>().TakeDamage(_damage);
    }

}
