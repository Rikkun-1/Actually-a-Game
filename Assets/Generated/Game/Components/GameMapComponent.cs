//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity mapEntity { get { return GetGroup(GameMatcher.Map).GetSingleEntity(); } }
    public MapComponent map { get { return mapEntity.map; } }
    public bool hasMap { get { return mapEntity != null; } }

    public GameEntity SetMap(UnityEngine.Vector2Int newMapSize) {
        if (hasMap) {
            throw new Entitas.EntitasException("Could not set Map!\n" + this + " already has an entity with MapComponent!",
                "You should check if the context already has a mapEntity before setting it or use context.ReplaceMap().");
        }
        var entity = CreateEntity();
        entity.AddMap(newMapSize);
        return entity;
    }

    public void ReplaceMap(UnityEngine.Vector2Int newMapSize) {
        var entity = mapEntity;
        if (entity == null) {
            entity = SetMap(newMapSize);
        } else {
            entity.ReplaceMap(newMapSize);
        }
    }

    public void RemoveMap() {
        mapEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public MapComponent map { get { return (MapComponent)GetComponent(GameComponentsLookup.Map); } }
    public bool hasMap { get { return HasComponent(GameComponentsLookup.Map); } }

    public void AddMap(UnityEngine.Vector2Int newMapSize) {
        var index = GameComponentsLookup.Map;
        var component = (MapComponent)CreateComponent(index, typeof(MapComponent));
        component.mapSize = newMapSize;
        AddComponent(index, component);
    }

    public void ReplaceMap(UnityEngine.Vector2Int newMapSize) {
        var index = GameComponentsLookup.Map;
        var component = (MapComponent)CreateComponent(index, typeof(MapComponent));
        component.mapSize = newMapSize;
        ReplaceComponent(index, component);
    }

    public void RemoveMap() {
        RemoveComponent(GameComponentsLookup.Map);
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

    static Entitas.IMatcher<GameEntity> _matcherMap;

    public static Entitas.IMatcher<GameEntity> Map {
        get {
            if (_matcherMap == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Map);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMap = matcher;
            }

            return _matcherMap;
        }
    }
}
