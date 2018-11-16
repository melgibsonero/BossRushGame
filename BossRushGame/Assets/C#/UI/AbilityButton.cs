using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityButton : MonoBehaviour {

    BattleSystem_v2 _battleSystem;
    UnitHighlight _unitHighlight;
    BattleUnitPlayer _player;
    Button _thisButton;
    ButtonNameToText _nameText;

    public GameObject Ability;

    [SerializeField]
    private int ManaCost;

    Color _originalNormal;
    Color _originalHighlight;

	// Use this for initialization
	void Start () {
        _battleSystem = FindObjectOfType<BattleSystem_v2>();
        _unitHighlight = FindObjectOfType<UnitHighlight>();
        _player = FindObjectOfType<BattleUnitPlayer>();
        _thisButton = GetComponent<Button>();
        _nameText = GetComponent<ButtonNameToText>();

        ManaCost = Ability.GetComponent<BaseAbility>().ManaCost;

        _originalNormal = _thisButton.colors.normalColor;
        _originalHighlight = _thisButton.colors.highlightedColor;
	}
	
	// Update is called once per frame
	void Update () {
        _nameText.manaCost = ManaCost;

        var colors = _thisButton.colors;
        if (ManaCost > _player.CombatValues.CurrentMP)
        {
            colors.normalColor = Color.red;
            colors.highlightedColor *= Color.red;
        }
        else
        {
            colors.normalColor = _originalNormal;
            colors.highlightedColor = _originalHighlight;
        }
        _thisButton.colors = colors;
	}

    public void UseAbility()
    {
        if (ManaCost <= _player.CombatValues.CurrentMP)
        {            
            _unitHighlight.SetAbility(Ability);
        }
        else
        {
            Debug.Log("Not enough mana");
        }
    }
}
