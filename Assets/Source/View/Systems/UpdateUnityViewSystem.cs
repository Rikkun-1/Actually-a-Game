using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class UpdateUnityViewSystem : ReactiveSystem<GameEntity>
{
    private readonly Dictionary<string, GameObject> _categories = new Dictionary<string, GameObject>();
    private readonly GameObject                     _parent;

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

            if (HasViewPrefabName(e))
            {
                var prefabName = e.viewPrefab.prefabName;
                AddNewCategoryIfNotExist(prefabName);
                UnityViewHelper.LoadViewFromPrefab(e, prefabName, _categories[prefabName]);
            }
        }
    }

    private void AddNewCategoryIfNotExist(string newCategoryName)
    {
        if (_categories.ContainsKey(newCategoryName)) return;
        
        var newCategory = new GameObject(newCategoryName);
        newCategory.transform.SetParent(_parent.transform);

        _categories.Add(newCategoryName, newCategory);
    }

    private static bool HasViewPrefabName(GameEntity e)
    {
        return e.hasViewPrefab && !string.IsNullOrEmpty(e.viewPrefab.prefabName);
    }
}