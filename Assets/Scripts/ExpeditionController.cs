using ARiskyGame.Types;
using Assets.Types;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpeditionController : MonoBehaviour
{
    public List<GameObject> LevelPrefabs;
    public GalaxyDepth GalaxyDepth;
    public GameObject LevelContainer;
    public Player Player;

    public int CollectedFood;
    public int CollectedSpareParts;

    void Awake()
    {
        LevelContainer = GameObject.Find("LevelContainer");
    }

    void ClearLevel()
    {
        foreach (Transform child in LevelContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public void StartLevel(GalaxyDepth depth)
    {
        ClearLevel();
        GalaxyDepth = depth;
        GameObject lvl = LevelPrefabs[(int)depth - 1];
        var instance = Instantiate(lvl, LevelContainer.transform);
        instance.GetComponent<LevelStateController>().InitializeLevel(depth);
    }

    public void AddLoot(ResourceType res, int quant)
    {
        switch (res)
        {
            case ResourceType.Food:
                CollectedFood += quant;
                break;
            case ResourceType.SpareParts:
                CollectedSpareParts += quant;
                break;
            case ResourceType.Fuel:
                Player.Fuel += quant;
                break;
        }
    }
    public void RemoveLoot(ResourceType res, int quant)
    {
        switch (res)
        {
            case ResourceType.Food:
                CollectedFood -= quant;
                break;
            case ResourceType.SpareParts:
                CollectedSpareParts -= quant;
                break;
            case ResourceType.Fuel:
                Player.Fuel -= quant;
                break;
        }
    }
}
