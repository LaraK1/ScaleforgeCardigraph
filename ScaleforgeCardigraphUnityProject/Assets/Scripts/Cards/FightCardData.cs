using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFightCard", menuName = "Cards/FightCard")]
public class FightCardData : BaseCardData
{
    /// <summary>
    /// If IsAttack this card is used on the opposing team. If not, this card can be used on your own team.
    /// </summary>
    [field: SerializeField]
    public bool IsAttack
    { get; private set; }

    [SerializeField]
    public int Healing { get; private set; }

    [SerializeField]
    public int Attack { get; private set; }

    [SerializeField]
    public int Armor { get; private set; }

    /// <summary>
    /// Amount of armor that gets removed on enemy.
    /// </summary>
    [SerializeField]
    public int Crush { get; private set; }

    [field: SerializeField]
    public EffectData[] GiveCardEffects
    { get; private set; }

    [field: SerializeField]
    public EffectData[] RemoveCardEffects
    { get; private set; }

    public FightCardData()
    {
        CardType = CardType.Fight;
    }
}
