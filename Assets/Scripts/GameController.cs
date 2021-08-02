using UnityEngine;

public class GameController : MonoBehaviour
{
    RootSystems systems;

    void Start()
    {
        this.systems = new RootSystems(Contexts.sharedInstance);
        this.systems.Initialize();
    }

    void Update()
    {
        this.systems.Execute();
        this.systems.Cleanup();
    }
}
