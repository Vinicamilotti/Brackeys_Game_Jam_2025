using UnityEngine;

public class PlanetInitiator : MonoBehaviour
{
    public GameObject PlanetPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var instance = Instantiate(PlanetPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    
}
