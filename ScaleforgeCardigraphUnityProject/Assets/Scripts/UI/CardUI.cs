using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardUI : MonoBehaviour
{
    // TODO visualise corruption
    [SerializeField]
    private Image bg;
    [SerializeField]
    private TMP_Text explain_text;
    [SerializeField]
    private RectTransform mainTransform;
    private float baseWidth;

    [SerializeField]
    private Image healing;
    [SerializeField]
    private Image armor;
    [SerializeField]
    private Image attack;
    [SerializeField]
    private Image crush;

    [SerializeField]
    private TMP_Text heal_text;
    [SerializeField]
    private TMP_Text armor_text;
    [SerializeField]
    private TMP_Text attack_text;
    [SerializeField]
    private TMP_Text crush_text;


    public void Select()
    {
        var size = mainTransform.sizeDelta;
        size.x = 1000f;
        mainTransform.sizeDelta = size;
    }

    public void DeSelect()
    {
        var size = mainTransform.sizeDelta;
        size.x = baseWidth;
        mainTransform.sizeDelta = size;
    }

    public void DrawCard(FightCard card)
    {
        baseWidth = mainTransform.sizeDelta.x;
        var baseRotation = mainTransform.eulerAngles;
        baseRotation.z += Random.Range(-5f, 5f);
        gameObject.transform.eulerAngles = baseRotation;

        bg.sprite = card.Data.Picture;
        explain_text.text = card.Data.ToString();

        healing.gameObject.SetActive(card.Healing > 0);
        heal_text.text = card.Healing.ToString();
        heal_text.gameObject.SetActive(card.Healing > 0);

        armor.gameObject.SetActive(card.Armor > 0);
        armor_text.text = card.Armor.ToString();
        armor_text.gameObject.SetActive(card.Armor > 0);

        attack.gameObject.SetActive(card.Attack > 0);
        attack_text.text = card.Attack.ToString();
        attack_text.gameObject.SetActive(card.Attack > 0);

        crush.gameObject.SetActive(card.Crush > 0);
        crush_text.text = card.Crush.ToString();
        crush_text.gameObject.SetActive(card.Crush > 0);
    }
}
