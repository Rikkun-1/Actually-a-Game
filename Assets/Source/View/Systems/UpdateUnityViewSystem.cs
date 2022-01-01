using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class UpdateUnityViewSystem : ReactiveSystem<GameEntity>
{
    private readonly GameObject                     _parent;
    private readonly Dictionary<string, GameObject> _categories = new Dictionary<string, GameObject>();

    public UpdateUnityViewSystem(Contexts contexts) : base(contexts.game)
    {
        _parent = new GameObject("Views");
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ViewPrefab.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            if (e.hasUnityView)
            {
                UnityViewHelper.DestroyView(e);
            }

            if (e.hasViewPrefab)
            {
                var prefabName = e.viewPrefab.prefabName;

                if (!_categories.ContainsKey(prefabName))
                {
                    var newCategory = new GameObject(prefabName);
                    newCategory.transform.SetParent(_parent.transform);
                    
                    _categories.Add(prefabName, newCategory);
                }
                
                UnityViewHelper.LoadViewFromPrefab(e, prefabName, _categories[prefabName]);
            }
        }
    }
}