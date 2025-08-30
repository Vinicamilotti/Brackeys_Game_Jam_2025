using ARiskyGame.Types;
using Assets.Types;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExpeditionController : MonoBehaviour
{
    public List<GameObject> LevelPrefabs;
    public GalaxyDepth GalaxyDepth;
    public GameObject LevelContainer;
    public int Fuel;
    public int CombatPower;
    public int Health;
    public int CollectedFood;
    public int CollectedSpareParts;

    void Awake()
    {
        LevelContainer = GameObject.Find("LevelContainer");
        Fuel = PlanetSingleton.Instance.fuelGeneration;
        CombatPower = PlanetSingleton.Instance.combatPower;
        Health = 5;
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
    public bool Travel()
    {
        Fuel--;
        if (Fuel < 0)
        {
            FailMission();
            return false;
        }
        return true;
    }
    public void TakeDamage(int dmg)
    {
        Health -= dmg;
        if (Health <= 0)
        {
            FailMission();
        }
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
                Fuel += quant;
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
                Fuel -= quant;
                break;
        }
    }

    public void FailMission()
    {
        CollectedFood = 0;
        CollectedSpareParts = 0;
        EndExpedition();
    }
    public void EndExpedition()
    {
        ClearLevel();
        PlanetSingleton.Instance.EndExpedition(CollectedFood, CollectedSpareParts);
        CollectedFood = 0;
        CollectedSpareParts = 0;
    }
}
