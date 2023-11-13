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
    public FightCardCollection InitialDeck { get; private set; }
    private Deck<FightCard> participantDeck = null;
    public Deck<FightCard> ParticipantDeck => participantDeck ?? (new Deck<FightCard>(InitFightCards(InitialDeck)));

    [field: SerializeField]
    public List<EffectData> ParticipantInitEffects { get; private set; }
    private List<Effect> participantEffects = null;
    public List<Effect> ParticipantEffects => participantEffects ?? (participantEffects = InitEffectList());

    [SerializeField]
    private PickCardsBase cardPicker;
    [SerializeField] 
    private PickNpcBase npcPicker;

    public FightCard PickFightCard => cardPicker.PickCard(ParticipantDeck?.GetComplete);
    public FightParticipant PickNpcTargetForFight(List<FightParticipant> npcs, FightCardData card) => npcPicker.PickNpc(npcs, card);


    public List<FightCard> InitFightCards(FightCardCollection initDeck)
    {
        if (InitialDeck.FightCardsToAddToDeck == null)
        {
            Health = -1; // TODO no Cards given
            return null;
        }
        var cards = new List<FightCard>();
        foreach (var cardData in InitialDeck.FightCardsToAddToDeck)
        {
            cards.Add(new FightCard(cardData));
        }
        return cards;
    }

    public List<Effect> InitEffectList()
    {
        List<Effect> initEffects = new List<Effect>();
        foreach (var item in ParticipantInitEffects)
        {
            initEffects.Add(new Effect(item));
        }
        return initEffects;
    }

    public void UseFightCard(FightCard card)
    {
        if (!card.Data.StaysInDeck)
        {
            ParticipantDeck.Remove(card.Id);
        }
    }

    public void TargetOfCard(FightCard card, List<Effect> attackerEffects)
    {
        // Remove effects of cards to target
        if (card.Data.RemoveCardEffects != null)
        {
            foreach (var effect in card.Data.RemoveCardEffects)
            {
                Debug.Log($"The effect {effect.name} will be removed from {name}");
                ParticipantEffects.RemoveAll(x => x.Data == effect);
            }
        }

        // Give effects of cards to target
        if (card.Data.GiveCardEffects != null)
        {
            foreach (var effect in card.Data.GiveCardEffects)
            {
                Debug.Log($"The effect {effect.name} will be added to {name}");
                ParticipantEffects.Add(new Effect(effect));
            }
        }

        // Add the targets effects to the card values multiplier
        float effectorAttack = 1;
        float effectorHealing = 1;
        float effectorCrush = 1;
        float effectorArmor = 1;
        foreach (var effect in ParticipantEffects)
        {
            effectorAttack *= effect.Data.BoostAttack;
            effectorHealing *= effect.Data.BoostHealing;
            effectorArmor *= effect.Data.BoostArmor;
            effectorCrush *= effect.Data.BoostCrush;
        }

        // Add the attackers effects to the card values multiplier
        if (attackerEffects != null)
        {
            foreach (var effect in attackerEffects)
            {
                effectorAttack *= effect.Data.BoostAttack;
                effectorHealing *= effect.Data.BoostHealing;
                effectorArmor *= effect.Data.BoostArmor;
                effectorCrush *= effect.Data.BoostCrush;
            }
        }

        // Add card values with effects to target
        AddHealth(card.Healing * effectorHealing);
        AddHealth(-(card.Attack * effectorAttack));
        AddArmor(card.Armor * effectorArmor);
        AddArmor(-(card.Crush * effectorCrush));


        // Check if target ist dead
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
        ParticipantEffects.RemoveAll(effect =>
        {
            return effect.NextRound();
        });
    }

    public void Died()
    {
        // loses all effects when dead
        participantEffects = new List<Effect>();
        Debug.Log("Is dead");
    }

}
