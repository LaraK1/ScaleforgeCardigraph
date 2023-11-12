using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightParticipant : MonoBehaviour
{
    public int Team;

    [SerializeField]
    private int health = 100;
    public int Health
    {
        get => health;
        set
        {
            // Health can max be 100
            health = Mathf.Min(Helpers.MaxHealth, value);
        }
    }

    public bool IsDead => Health <= 0;

    [SerializeField]
    private int armor = 0;
    public int Armor
    {
        get => armor;
        set
        {
            // Armor can be max 100 an 
            armor = Mathf.Max(0, Mathf.Min(Helpers.MaxArmor, value));
        }
    }

    [field: SerializeField]
    public FightDeck ParticipantDeck { get; private set; }

    [field: SerializeField]
    public List<Effect> ParticipantEffects { get; private set; }

    [SerializeField]
    private PickCardsBase cardPicker;
    [SerializeField] 
    private PickNpcBase npcPicker;

    public FightCard PickFightCard => cardPicker.PickCard(ParticipantDeck.GetComplete);
    public FightParticipant PickNpcTargetForFight(List<FightParticipant> npcs, FightCard card) => npcPicker.PickNpc(npcs, card);

    public void UseFightCard(FightCard card)
    {
        if (!card.StaysInDeck)
        {
            ParticipantDeck.Remove(card.Id);
        }
    }

    public void TargetOfCard(FightCard card, List<Effect> agressorEffects)
    {
        // Give effects of cards to target
        if (card.GiveCardEffects != null)
        {
            foreach (var effect in card.GiveCardEffects)
            {
                Debug.Log($"The effect {effect.name} will be added to {name}");
                ParticipantEffects.Add(effect);
            }
        }

        // Remove effects of cards to target
        if (card.RemoveCardEffects != null)
        {
            foreach (var effect in card.RemoveCardEffects)
            {
                Debug.Log($"The effect {effect.name} will be removed from {name}");
                ParticipantEffects.Remove(effect);
            }
        }

        float effectorAttack = 1;
        float effectorHealing = 1;
        float effectorCrush = 1;
        float effectorArmor = 1;
        foreach (var effect in ParticipantEffects)
        {
            effectorAttack *= effect.BoostAttack;
            effectorHealing *= effect.BoostHealing;
            effectorArmor *= effect.BoostArmor;
            effectorCrush *= effect.BoostCrush;
        }

        if (agressorEffects != null)
        {
            foreach (var effect in agressorEffects)
            {
                effectorAttack *= effect.BoostAttack;
                effectorHealing *= effect.BoostHealing;
                effectorArmor *= effect.BoostArmor;
                effectorCrush *= effect.BoostCrush;
            }
        }

        AddHealth(card.Healing * effectorHealing);
        AddHealth(-(card.Attack * effectorAttack));
        AddArmor(card.Armor * effectorArmor);
        AddArmor(-(card.Crush * effectorCrush));

        if(IsDead)
        {
            Died();
        }
    }

    public void AddHealth(int amount)
    {
        Debug.Log($"{name} gets {amount} health.");
        Health += amount; 
    }

    public void AddHealth(float amount)
    {
        Debug.Log($"{name} gets {amount} health.");
        Health += Mathf.RoundToInt(amount);
    }

    public void AddArmor(int amount)
    {
        Debug.Log($"{name} gets {amount} armor.");
        Armor += amount; 
    }

    public void AddArmor(float amount)
    {
        Debug.Log($"{name} gets {amount} armor.");
        Armor += Mathf.RoundToInt(amount);
    }

    public void UpdateEffects()
    {
        foreach (var effect in ParticipantEffects)
        {
            effect.NextRound();
        }
    }

    public void Died()
    {
        // loses all effects when dead
        ParticipantEffects = new List<Effect>();
        Debug.Log("Is dead");
    }

}
