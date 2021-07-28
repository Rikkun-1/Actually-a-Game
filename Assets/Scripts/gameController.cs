using UnityEngine;

public class GameController : MonoBehaviour
{
    RootSystems _systems;

    // Start is called before the first frame update
    void Start()
    {
        _systems = new RootSystems(Contexts.sharedInstance);
        _systems.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        _systems.Execute(); 
    }
}
