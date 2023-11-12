using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffect", menuName = "Effect/Create new Effect")]
public class Effect : ScriptableObject
{
    private static uint EffectCount = 0;
    [field: SerializeField]
    public uint Id  // Is the unique Id of the specific effect
    { get; private set; }

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

    /// <summary>
    /// Effect was used in the current round.
    /// </summary>
    /// <returns>If the effect should be removed.</returns>
    public bool NextRound()
    {
        RoundDuration -= 1;
        return RoundDuration <= 0;
    }

    public void Awake()
    {
        EffectCount++;
        Id = EffectCount;
    }
}
