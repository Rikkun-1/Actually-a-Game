//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PathRequestComponent pathRequest { get { return (PathRequestComponent)GetComponent(GameComponentsLookup.PathRequest); } }
    public bool hasPathRequest { get { return HasComponent(GameComponentsLookup.PathRequest); } }

    public void AddPathRequest(UnityEngine.Vector2Int newFrom, UnityEngine.Vector2Int newTo) {
        var index = GameComponentsLookup.PathRequest;
        var component = (PathRequestComponent)CreateComponent(index, typeof(PathRequestComponent));
        component.from = newFrom;
        component.to = newTo;
        AddComponent(index, component);
    }

    public void ReplacePathRequest(UnityEngine.Vector2Int newFrom, UnityEngine.Vector2Int newTo) {
        var index = GameComponentsLookup.PathRequest;
        var component = (PathRequestComponent)CreateComponent(index, typeof(PathRequestComponent));
        component.from = newFrom;
        component.to = newTo;
        ReplaceComponent(index, component);
    }

    public void RemovePathRequest() {
        RemoveComponent(GameComponentsLookup.PathRequest);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherPathRequest;

    public static Entitas.IMatcher<GameEntity> PathRequest {
        get {
            if (_matcherPathRequest == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PathRequest);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPathRequest = matcher;
            }

            return _matcherPathRequest;
        }
    }
}