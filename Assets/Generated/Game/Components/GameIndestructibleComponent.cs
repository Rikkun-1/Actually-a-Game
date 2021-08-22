//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly IndestructibleComponent indestructibleComponent = new IndestructibleComponent();

    public bool isIndestructible {
        get { return HasComponent(GameComponentsLookup.Indestructible); }
        set {
            if (value != isIndestructible) {
                var index = GameComponentsLookup.Indestructible;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : indestructibleComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherIndestructible;

    public static Entitas.IMatcher<GameEntity> Indestructible {
        get {
            if (_matcherIndestructible == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Indestructible);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherIndestructible = matcher;
            }

            return _matcherIndestructible;
        }
    }
}
