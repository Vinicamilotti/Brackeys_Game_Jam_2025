using UnityEditor;
using UnityEngine;

public class PlanetSingleton : MonoBehaviour
{
    static Planet _instance;
    public static Planet Instance
    {
        get
        {
            if (_instance is not null)
            {
                return _instance;
            }
            var prefab = Resources.Load<GameObject>("Prefabs/PlanetSingletonPrefab");
            var inScene = Instantiate<GameObject>(prefab);
            _instance = inScene.GetComponent<Planet>();
            if (!_instance)
            {
                _instance = inScene.AddComponent<Planet>();
            }

            DontDestroyOnLoad(_instance.transform.root.gameObject);
            return _instance;

        }

    }




}
