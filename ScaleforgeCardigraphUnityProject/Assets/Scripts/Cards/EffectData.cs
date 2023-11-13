using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[CreateAssetMenu(fileName = "NewEffect", menuName = "Effect/Create new Effect Data")]
public class EffectData : ScriptableObject
{
    [field: SerializeField]
    public string Name
    {get; private set;}

    [field: SerializeField]
    public Sprite Icon
    {get; private set;}
    /// <summary>
    /// How many rounds will this effect occur.
    /// </summary>
    [field: SerializeField]
    public int RoundDuration
    {get; private set;}


    #region Effects on cards

    [field: SerializeField]
    public float BoostHealing
    { get; private set; } = 1;

    [field: SerializeField]
    public float BoostAttack
    { get; private set; } = 1;

    [field: SerializeField]
    public float BoostArmor
    { get; private set; } = 1;

    [field: SerializeField]
    public float BoostCrush
    { get; private set; } = 1;

    [field: SerializeField]
    #endregion

    #region Effects on start of turn
    public bool SkipRound
    { get; private set; } = false;

    /// <summary>
    /// This amount of health will be added on start of turn
    /// </summary>
    [field: SerializeField]
    public int OneTimeHeal
    { get; private set; } = 0;

    /// <summary>
    /// This amount of armor will be added on start of turn
    /// </summary>
    [field: SerializeField]
    public int OneTimeArmor
    { get; private set; } = 0;

    #endregion

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append($"Will add effect <b>{Name}</b>. ");
        if(BoostHealing > 1)
        {
            sb.Append("Will increase healing. ");
        }
        else if (BoostHealing < 1)
        {
            sb.Append("Will decrease healing. ");
        }

        if (BoostArmor > 1)
        {
            sb.Append("Actions for adding armor are increased. ");
        }
        else if (BoostArmor < 1)
        {
            sb.Append("Actions for adding armor are decreased. ");
        }

        if (BoostCrush > 1)
        {
            sb.Append("Actions for removing enemy armor are increased. ");
        }
        else if (BoostCrush < 1)
        {
            sb.Append("Actions for removing enemy armor are decreased. ");
        }

        if (BoostAttack > 1)
        {
            sb.Append("Attack strength is increased. ");
        }
        else if (BoostAttack < 1)
        {
            sb.Append("Attack strength is decreased. ");
        }

        if(SkipRound)
        {
            sb.Append("Target needs to skip round. ");
        }

        if(OneTimeArmor > 0)
        {
            sb.Append($"Armor will be increased by {OneTimeArmor} every round. ");
        }
        else if (OneTimeArmor < 0)
        {
            sb.Append($"Armor will be decreased by {Mathf.Abs(OneTimeArmor)} every round. ");
        }

        if (OneTimeHeal > 0)
        {
            sb.Append($"Health will be increased by {OneTimeHeal} every round. ");
        }
        else if (OneTimeHeal < 0)
        {
            sb.Append($"Health will be decreased by {Mathf.Abs(OneTimeHeal)} every round. ");
        }

        sb.Append($"({RoundDuration} Rounds.)");

        return sb.ToString();
    }
}
