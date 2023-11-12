using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickCardsBase : MonoBehaviour
{
    public abstract FightCard PickCard(List<FightCard> deck);
    public abstract ConversationCard PickCard(List<ConversationCard> deck);

    // Todo implement all?
}
