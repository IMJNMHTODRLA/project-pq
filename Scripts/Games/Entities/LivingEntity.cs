using System;
using Godot;

namespace ProjectPQ.Scripts.Games.Entities;

public abstract partial class LivingEntity : Entity
{
    [Signal] public delegate void HpChangedEventHandler(long changeHp);

    [Signal] public delegate void OnHealEventHandler(long hp);
    [Signal] public delegate void OnDamageEventHandler(long hp);

    protected LivingEntity(long maxHealth, long health)
    {
        MaxHealth = maxHealth;
        Health = health;
    }

    public long Health
    {
        get => field;
        set
        {
            long changeHp = Math.Clamp(value, 0, MaxHealth);

            field = changeHp;
            EmitSignal(SignalName.HpChanged, changeHp);
        }
    }

    public void Heal(long value)
    {
        value = Math.Clamp(value, 0, MaxHealth);

        Health += value;
        EmitSignal(SignalName.OnHeal, value);
    }
    
    public void Damage(long value)
    {
        value = Math.Clamp(value, 0, MaxHealth);

        Health -= value;
        EmitSignal(SignalName.OnDamage, value);
    }

    public long MaxHealth
    {
        get => field;
        set => field = Math.Max(value, 1);
    }

    public override bool IsDead() =>
        Health <= 0;
}