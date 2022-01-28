//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public VisionListenerComponent visionListener { get { return (VisionListenerComponent)GetComponent(GameComponentsLookup.VisionListener); } }
    public bool hasVisionListener { get { return HasComponent(GameComponentsLookup.VisionListener); } }

    public void AddVisionListener(System.Collections.Generic.List<IVisionListener> newValue) {
        var index = GameComponentsLookup.VisionListener;
        var component = (VisionListenerComponent)CreateComponent(index, typeof(VisionListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceVisionListener(System.Collections.Generic.List<IVisionListener> newValue) {
        var index = GameComponentsLookup.VisionListener;
        var component = (VisionListenerComponent)CreateComponent(index, typeof(VisionListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveVisionListener() {
        RemoveComponent(GameComponentsLookup.VisionListener);
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

    static Entitas.IMatcher<GameEntity> _matcherVisionListener;

    public static Entitas.IMatcher<GameEntity> VisionListener {
        get {
            if (_matcherVisionListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.VisionListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherVisionListener = matcher;
            }

            return _matcherVisionListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public void AddVisionListener(IVisionListener value) {
        var listeners = hasVisionListener
            ? visionListener.value
            : new System.Collections.Generic.List<IVisionListener>();
        listeners.Add(value);
        ReplaceVisionListener(listeners);
    }

    public void RemoveVisionListener(IVisionListener value, bool removeComponentWhenEmpty = true) {
        var listeners = visionListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveVisionListener();
        } else {
            ReplaceVisionListener(listeners);
        }
    }
}
