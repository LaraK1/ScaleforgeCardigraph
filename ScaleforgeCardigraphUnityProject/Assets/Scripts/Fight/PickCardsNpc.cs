using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCardsNpc : MonoBehaviour, IPickCards
{
    public FightCard PickCard(List<FightCard> deck)
    {
        return deck[Random.Range(0, deck.Count-1)];
    }
}
