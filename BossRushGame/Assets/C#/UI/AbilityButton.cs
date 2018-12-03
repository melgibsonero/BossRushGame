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
    ButtonOnClickSetter _nameText;

    public GameObject Ability;

    public int ManaCost;

    Color _originalNormal;
    Color _originalHighlight;

	// Use this for initialization
	void Start () {
        _battleSystem = FindObjectOfType<BattleSystem_v2>();
        _unitHighlight = FindObjectOfType<UnitHighlight>();
        _thisButton = GetComponent<Button>();
        _nameText = GetComponent<ButtonOnClickSetter>();

        ManaCost = Ability.GetComponent<BaseAbility>().ManaCost;

        _originalNormal = _thisButton.colors.normalColor;
        _originalHighlight = _thisButton.colors.highlightedColor;
	}
	
	// Update is called once per frame
	void Update ()
    {
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

    public void SetPlayer(BattleUnitPlayer player)
    {
        _player = player;
    }
}
