using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManagerUI : MonoBehaviour
{
    [SerializeField]
    private UICardPool fightCardPool;

    public void ShowCardFightDeck(List<FightCard> fightCards)
    {
        if (fightCards == null) return;

        foreach (var card in fightCards)
        {
            var newCard = fightCardPool.Get();
            newCard.DrawCard(card);
        }
    }
}
