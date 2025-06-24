using UnityEngine;

public class Unit : MonoBehaviour, IAttacking
{
    public e_UnitType UnitType;
    public e_Team Team;
    public int DamageRange = 1;
    private int _damage = 50;
    [SerializeField] private int _movementPoints = 1;

    private HexGrid _hexGrid;

    void Awake()
    {
        _hexGrid = FindAnyObjectByType<HexGrid>();

        UnitType = e_UnitType.Settler;
    }
    public int GetMovementPoints()
    {
        return _movementPoints;
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
