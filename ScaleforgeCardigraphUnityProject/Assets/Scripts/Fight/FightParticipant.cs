using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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

    // Must be seperate because cant be otherwise initialized through inspector
    [SerializeField]
    private List<FightCard> FightCardsToAddToDeck = new List<FightCard>();
    [SerializeField]
    private List<CardEffect> EffectsToAddToFighter = new List<CardEffect>();

    public GDictManager<FightCard> ParticipantDeck { get; private set; }

    // TODO just make it a list dont overcomplicate that shit
    public GDictManager<CardEffect> ParticipantEffects { get; private set; }

    public void Init()
    {
        if (FightCardsToAddToDeck != null)
        {
            foreach (var card in FightCardsToAddToDeck)
            {
                ParticipantDeck.Add(card, card.Id);
            }
            FightCardsToAddToDeck = null;
        }

        if (EffectsToAddToFighter != null)
        {
            foreach (var effect in EffectsToAddToFighter)
            {
                ParticipantEffects.Add(effect, effect.Id);
            }
            EffectsToAddToFighter = null;
        }

    }

    private IPickCards cardPicker = new PickCardsNpc();
    private IPickNpc npcPicker = new PickNpcNpc();

    public FightCard PickFightCard => cardPicker.PickCard(ParticipantDeck.GetComplete);
    public FightParticipant PickNpcTargetForFight(List<FightParticipant> npcs, FightCard card) => npcPicker.PickNpc(npcs, card);

    public void UseFightCard(FightCard card)
    {
        card.UseCard();
        if (!card.StaysInDeck)
        {
            ParticipantDeck.Remove(card.Id);
        }
    }

    public void TargetOfCard(FightCard card, List<CardEffect> agressorEffects)
    {
        // Give effects of cards to target
        if (card.GiveCardEffects != null)
        {
            foreach (var effect in card.GiveCardEffects)
            {
                ParticipantEffects.Add(effect, effect.Id);
            }
        }

        // Remove effects of cards to target
        if (card.RemoveCardEffects != null)
        {
            foreach (var effect in card.RemoveCardEffects)
            {
                ParticipantEffects.Remove(effect.Id);
            }
        }

        float effectorAttack = 1;
        float effectorHealing = 1;
        float effectorCrush = 1;
        float effectorArmor = 1;
        var targetEffects = ParticipantEffects.GetComplete;
        foreach (var effect in targetEffects)
        {
            effectorAttack *= effect.BoostAttack;
            effectorHealing *= effect.BoostHealing;
            effectorArmor *= effect.BoostArmor;
            effectorCrush *= effect.BoostCrush;
        }

        if (agressorEffects != null)
        {
            foreach (var effect in targetEffects)
            {
                effectorAttack *= effect.BoostAttack;
                effectorHealing *= effect.BoostHealing;
                effectorArmor *= effect.BoostArmor;
                effectorCrush *= effect.BoostCrush;
            }
        }

        Health += Mathf.RoundToInt(card.Healing * effectorHealing);
        Health -= Mathf.RoundToInt(card.Attack * effectorAttack);
        Armor  += Mathf.RoundToInt(card.Armor * effectorArmor);
        Armor  -= Mathf.RoundToInt(card.Crush * effectorCrush);

        if(IsDead)
        {
            Died();
        }
    }

    public void UpdateEffects()
    {
        foreach (var effect in ParticipantEffects.Collection)
        {
            effect.Value.NextRound();
        }
    }

    public void Died()
    {
        Debug.Log("Is dead");
    }

}
