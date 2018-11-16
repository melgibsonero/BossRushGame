using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayerCombatValues : MonoBehaviour {

    private enum CCV
    {
        HP,
        MP,
        XP,
        AP,
        DP
    }

    [SerializeField]
    private CCV Stat;

    [SerializeField]
    private Color TextColor;

    private string ccv_stat { get { return Stat.ToString(); } }

    private TextMeshProUGUI Text;
    private CharCombatValues CombatValues;

    private void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
        CombatValues = FindObjectOfType<BattleUnitPlayer>().GetComponent<CharCombatValues>();
    }

    private void Update()
    {
        Text.color = TextColor;

        if (Stat == CCV.HP) Text.SetText(ccv_stat + ": " + CombatValues.CurrentHP + "/" + CombatValues.maxHP);
        if (Stat == CCV.MP) Text.SetText(ccv_stat + ": " + CombatValues.CurrentMP + "/" + CombatValues.maxMP);
        if (Stat == CCV.AP) Text.SetText(ccv_stat + ": " + CombatValues.CurrentAP);
        if (Stat == CCV.DP) Text.SetText(ccv_stat + ": " + CombatValues.CurrentDP);
        if (Stat == CCV.XP) Text.SetText("");
    }


}
