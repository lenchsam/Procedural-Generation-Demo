using UnityEngine;

public interface IState
{
    void Enter();
    void Exit();
    void OnTileClicked(TileScript tile);
    void OnBuildUnitRequested(CraftablesScriptableObject craftable);
}
