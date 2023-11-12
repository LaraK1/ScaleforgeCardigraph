using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFightDeck", menuName = "Cards/FightDeck")]
[System.Serializable]
public class FightDeck : Deck<FightCard>
{
    // TODO this must be done differently

    /// <summary>
    /// The content of this list is only used for initializing the dictionary through the inspector.
    /// The content will be deleted afterwards.
    /// </summary>
    [SerializeField]
    private List<FightCard> FightCardsToAddToDeck = new List<FightCard>();

    protected override Dictionary<uint, FightCard> Init()
    {
        var newDict = new Dictionary<uint, FightCard>();
        if (FightCardsToAddToDeck != null)
        {
            foreach (var card in FightCardsToAddToDeck)
            {
                newDict[card.Id] = card;
            }
        }

        return newDict;
    }
}
