using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickCardsNpc : PickCardsBase
{
    public override FightCard PickCard(List<FightCard> deck)
    {
        if (deck == null || deck.Count == 0) return null;
        return deck[Random.Range(0, deck.Count)];
    }

    public override ConversationCard PickCard(List<ConversationCard> deck)
    {
        if (deck == null || deck.Count == 0) return null;
        return deck[Random.Range(0, deck.Count - 1)];
    }
}
