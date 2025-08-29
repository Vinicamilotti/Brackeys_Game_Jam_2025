using UnityEngine;

public class ExpeditionInitiator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<ExpeditionController>().StartLevel(Assets.Types.GalaxyDepth.Depth3);
    }

    // Update is called once per frame
}
