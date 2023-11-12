using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickCards
{
    public FightCard PickCard(List<FightCard> deck);
}
