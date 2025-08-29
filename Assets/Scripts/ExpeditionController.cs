using Assets.Types;
using System.Collections.Generic;
using UnityEngine;

public class ExpeditionController : MonoBehaviour
{
    public List<GameObject> LevelPrefabs;
    public GalaxyDepth GalaxyDepth;
    public GameObject LevelContainer;
    public Player Player;
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
        GameObject lvl = LevelPrefabs[(int)depth - 1];
        var instance = Instantiate(lvl, LevelContainer.transform);
        instance.transform.SetParent(LevelContainer.transform);
        instance.GetComponent<LevelStateController>().InitializeLevel(depth);
    }
}
