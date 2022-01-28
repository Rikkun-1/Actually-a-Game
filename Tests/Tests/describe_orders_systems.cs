

using Shouldly;
using UnityEngine;

public class describe_orders_systems : entitas_tests
{
    private void describe_look_systems()
    {
        context["describe delete old look at orders when new added system"] = () =>
        {
            GameEntity e = null;
            before = () =>
            {
                Setup();
                systems.Add(new DeleteOldLookOrdersWhenNewAddedSystem(contexts));
                e = CreateEntity();
            };

            // direction -> position
            it["deletes old look at direction order if new look at position order added "] = () =>
            {
                e.AddLookAtDirectionOrder(0);
                e.AddLookAtPositionOrder(new Vector2(0, 0));
                systems.Execute();
                e.hasLookAtDirectionOrder.ShouldBeFalse();
                e.hasLookAtPositionOrder.ShouldBeTrue();
            };
            
            // position -> direction
            xit["deletes old look at position order if new look at direction order added "] = () =>
            {
                e.AddLookAtPositionOrder(new Vector2(0, 0));
                e.AddLookAtDirectionOrder(0);
                systems.Execute();
                e.hasLookAtPositionOrder.ShouldBeFalse();
                e.hasLookAtDirectionOrder.ShouldBeTrue();
            };
            
            // direction -> entity
            it["deletes old look at direction order if new look at entity order added "] = () =>
            {
                e.AddLookAtDirectionOrder(0);
                e.AddLookAtEntityOrder(0);
                systems.Execute();
                e.hasLookAtDirectionOrder.ShouldBeFalse();
                e.hasLookAtEntityOrder.ShouldBeTrue();
            };
            
            // entity -> direction
            xit["deletes old look at entity order if new look at direction order added "] = () =>
            {
                e.AddLookAtEntityOrder(0);
                e.AddLookAtDirectionOrder(0);
                systems.Execute();
                e.hasLookAtEntityOrder.ShouldBeFalse();
                e.hasLookAtDirectionOrder.ShouldBeTrue();
            };
            
            // position -> entity
            it["deletes old look at position order if new look at entity order added "] = () =>
            {
                e.AddLookAtPositionOrder(new Vector2(0, 0));
                e.AddLookAtEntityOrder(0);
                systems.Execute();
                e.hasLookAtPositionOrder.ShouldBeFalse();
                e.hasLookAtEntityOrder.ShouldBeTrue();
            };
            
            // entity -> position
            xit["deletes old look at entity order if new look at position order added "] = () =>
            {
                e.AddLookAtEntityOrder(0);
                e.AddLookAtPositionOrder(new Vector2(0, 0));
                systems.Execute();
                e.hasLookAtEntityOrder.ShouldBeFalse();
                e.hasLookAtPositionOrder.ShouldBeTrue();
            };
        };
        
        context["describe delete old shoot at orders when new added system"] = () =>
        {
            GameEntity e = null;
            before = () =>
            {
                Setup();
                systems.Add(new DeleteOldShootOrdersWhenNewAddedSystem(contexts));
                e = CreateEntity();
            };

            // direction -> position
            it["deletes old Shoot at direction order if new Shoot at position order added "] = () =>
            {
                e.AddShootAtDirectionOrder(0);
                e.AddShootAtPositionOrder(new Vector2(0, 0));
                systems.Execute();
                e.hasShootAtDirectionOrder.ShouldBeFalse();
                e.hasShootAtPositionOrder.ShouldBeTrue();
            };
            
            // position -> direction
            xit["deletes old Shoot at position order if new Shoot at direction order added "] = () =>
            {
                e.AddShootAtPositionOrder(new Vector2(0, 0));
                e.AddShootAtDirectionOrder(0);
                systems.Execute();
                e.hasShootAtPositionOrder.ShouldBeFalse();
                e.hasShootAtDirectionOrder.ShouldBeTrue();
            };
            
            // direction -> entity
            it["deletes old Shoot at direction order if new Shoot at entity order added "] = () =>
            {
                e.AddShootAtDirectionOrder(0);
                e.AddShootAtEntityOrder(0);
                systems.Execute();
                e.hasShootAtDirectionOrder.ShouldBeFalse();
                e.hasShootAtEntityOrder.ShouldBeTrue();
            };
            
            // entity -> direction
            xit["deletes old Shoot at entity order if new Shoot at direction order added "] = () =>
            {
                e.AddShootAtEntityOrder(0);
                e.AddShootAtDirectionOrder(0);
                systems.Execute();
                e.hasShootAtEntityOrder.ShouldBeFalse();
                e.hasShootAtDirectionOrder.ShouldBeTrue();
            };
            
            // position -> entity
            it["deletes old Shoot at position order if new Shoot at entity order added "] = () =>
            {
                e.AddShootAtPositionOrder(new Vector2(0, 0));
                e.AddShootAtEntityOrder(0);
                systems.Execute();
                e.hasShootAtPositionOrder.ShouldBeFalse();
                e.hasShootAtEntityOrder.ShouldBeTrue();
            };
            
            // entity -> position
            xit["deletes old Shoot at entity order if new Shoot at position order added "] = () =>
            {
                e.AddShootAtEntityOrder(0);
                e.AddShootAtPositionOrder(new Vector2(0, 0));
                systems.Execute();
                e.hasShootAtEntityOrder.ShouldBeFalse();
                e.hasShootAtPositionOrder.ShouldBeTrue();
            };
        };
    }
}
