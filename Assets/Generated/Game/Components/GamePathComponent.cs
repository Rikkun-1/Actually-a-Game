//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PathComponent path { get { return (PathComponent)GetComponent(GameComponentsLookup.Path); } }
    public bool hasPath { get { return HasComponent(GameComponentsLookup.Path); } }

    public void AddPath(Roy_T.AStar.Paths.Path newPath, int newCurrentIndex) {
        var index = GameComponentsLookup.Path;
        var component = (PathComponent)CreateComponent(index, typeof(PathComponent));
        component.path = newPath;
        component.currentIndex = newCurrentIndex;
        AddComponent(index, component);
    }

    public void ReplacePath(Roy_T.AStar.Paths.Path newPath, int newCurrentIndex) {
        var index = GameComponentsLookup.Path;
        var component = (PathComponent)CreateComponent(index, typeof(PathComponent));
        component.path = newPath;
        component.currentIndex = newCurrentIndex;
        ReplaceComponent(index, component);
    }

    public void RemovePath() {
        RemoveComponent(GameComponentsLookup.Path);
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

    static Entitas.IMatcher<GameEntity> _matcherPath;

    public static Entitas.IMatcher<GameEntity> Path {
        get {
            if (_matcherPath == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Path);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPath = matcher;
            }

            return _matcherPath;
        }
    }
}
