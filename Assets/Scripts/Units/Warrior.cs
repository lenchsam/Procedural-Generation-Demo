using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Warrior : Melee, IAttacking
{
    HexGrid _hexGrid;
    protected override void Awake()
    {
        base.Awake();
        _hexGrid = FindAnyObjectByType<HexGrid>();
    }

    // Update is called once per frame
    protected override void Start()
    {
        base.Start();
    }
    public void attack(GameObject thingToAttack){
        //if thing is x tiles away then attack
        Vector2Int startCords = _hexGrid.GetTileScriptFromPosition(gameObject.transform.position).IntCoords;
        Vector2Int targetCords = _hexGrid.GetTileScriptFromPosition(thingToAttack.transform.position).IntCoords;

        //Debug.Log();
        

        int tileDistance = _hexGrid.DistanceBetweenTiles(startCords, targetCords);
        if(tileDistance <= _maxAttackDistance){
            thingToAttack.GetComponent<IDamageable>().takeDamage(_damage);
        }else{
            Debug.Log("too far away");
        }
        //thingToAttack.GetComponent<IDamageable>().takeDamage(damage);
    }
}
