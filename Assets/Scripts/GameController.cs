using UnityEngine;

public class GameController : MonoBehaviour
{
    private RootSystems _systems;

    private void Start()
    {
        _systems = new RootSystems(Contexts.sharedInstance);
        _systems.Initialize();
    }

    private void Update()
    {
        _systems.Execute();
        _systems.Cleanup();
    }

    private void OnDestroy()
    {
        _systems.TearDown();
    }
}