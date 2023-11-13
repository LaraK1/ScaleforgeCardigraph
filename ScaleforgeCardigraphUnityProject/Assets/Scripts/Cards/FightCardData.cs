using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[CreateAssetMenu(fileName = "NewFightCard", menuName = "Cards/FightCard")]
public class FightCardData : BaseCardData
{
    /// <summary>
    /// If IsAttack this card is used on the opposing team. If not, this card can be used on your own team.
    /// </summary>
    [field: SerializeField]
    public bool IsAttack
    { get; private set; }

    [field: SerializeField]
    public int Healing { get; private set; }

    [field: SerializeField]
    public int Attack { get; private set; }

    [field: SerializeField]
    public int Armor { get; private set; }

    /// <summary>
    /// Amount of armor that gets removed on enemy.
    /// </summary>
    [field: SerializeField]
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

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var effect in GiveCardEffects)
        {
            sb.Append(effect.ToString());
        }
        sb.AppendLine();
        foreach (var effect in RemoveCardEffects)
        {
            sb.Append($"Removes effect <b>{effect.name}</b>. ");
        }

        return sb.ToString();
    }

}
