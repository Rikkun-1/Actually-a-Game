using System.Collections.Generic;
using System.Linq;
using Entitas;
using Shouldly;

public class describe_health_and_damage_systems: entitas_tests
{
    private void describe_apply_damage_to_health_system()
    {
        GameEntity   e           = null;
        List<Damage> damage      = null;
        int          startHealth = 100;
        
        before = () =>
        {
            Setup();
            systems.Add(new ApplyDamageToHealthSystem(contexts));
            e = CreateEntity();
            e.AddHealth(startHealth);
            
            damage = new List<Damage>
            {
                new Damage(0, 25),
                new Damage(0, 50),
                new Damage(0, 75)
            };
        };

        it["applies damage to entity health"] = () =>
        {
            e.AddDamage(damage);
            systems.Execute();
            e.health.value.ShouldBe(startHealth - damage.Sum(elem => elem.damage));
        };

        it["does not applies damage to entity without damage"] = () =>
        {
            systems.Execute();
            e.health.value.ShouldBe(startHealth);
        };

        it["does not applies damage to indestructible entity"] = () =>
        {
            e.isIndestructible = true;
            e.AddDamage(damage);
            systems.Execute();
            e.health.value.ShouldBe(startHealth);
        };
    }

    private void describe_destroy_entity_on_zero_health_system()
    {
        GameEntity e = null;
        
        before = () =>
        {
            Setup();
            systems.Add(new DestroyEntityOnZeroHealthSystem(contexts));
            e = CreateEntity();
        };

        it["destroys entity on zero health"] = () =>
        {
            e.AddHealth(0);
            systems.Execute();
            e.isDestroyed.ShouldBeTrue();
        };

        it["destroys entity on health bellow zero"] = () =>
        {
            e.AddHealth(-5); 
            systems.Execute();
            e.isDestroyed.ShouldBeTrue();
        };

        it["does not destroy entity if health above zero"] = () =>
        {
            e.AddHealth(10);
            systems.Execute();
            e.isDestroyed.ShouldBeFalse();
        };

        it["does not destroy entity without health"] = () =>
        {
            systems.Execute();
            e.isDestroyed.ShouldBeFalse();
        };
    }

    private void describe_remove_damage_system()
    {
        GameEntity   e      = null;
        List<Damage> damage = null;
        
        before = () =>
        {
            Setup();
            systems.Add(new RemoveDamageSystem(contexts));
            e = CreateEntity();
            
            damage = new List<Damage>
            {
                new Damage(0, 25),
                new Damage(0, 50),
                new Damage(0, 75)
            };
        };

        it["removes damage from entity"] = () =>
        {
            e.AddDamage(damage);
            systems.Cleanup();
            e.hasDamage.ShouldBeFalse();
        };
    }
}
