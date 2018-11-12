using UnityEngine;
using System.Collections;
using TMPro;

public class CharCombatValues : MonoBehaviour
{
    
    public TextMeshPro textMesh;
    public TextMeshPro HealthText;

    [Space(-10), Header("Health")]
    public int maxHP = 10;
    private int currentHP;

    [Space(-10), Header("Mana")]
    public int maxMP = 5;
    private int currentMP;

    [Space(-10), Header("Attack")]
    public int defaultAP = 1;   // no buffs
    private int currentAP;       // with buffs

    [Space(-10), Header("Defence")]
    public int defaultDP = 0;   // no buffs
    private int currentDP;       // with buffs

    public bool IsDead { get { return currentHP <= 0; } }
    public bool IsPlayer { get { return GetComponent<BattleUnitPlayer>() != null; } }

    public int CurrentHP{get{return currentHP;}}

    public int CurrentMP{get{return currentMP;}}

    public int CurrentAP{get{return currentAP;}}

    public int CurrentDP{get{return currentDP;}}

    private int _totalDamage;

    #region HP methods

    public void TakeDamage(int damage)
    {
        _totalDamage = damage - currentDP;

        if (_totalDamage > 0)
            currentHP -= _totalDamage;
        else
            _totalDamage = 0;

        ShowDamageText(_totalDamage);

        _totalDamage = 0;
    }

    public void ShowDamageText(int damageTaken)
    {
        var damageText = Instantiate(textMesh, transform);
        if (damageTaken == 0)
        {
            damageText.text = "BLOCK";
        }
        else
        {
            damageText.text = "-" + damageTaken;
        }
        HealthText.text = currentHP + "/" + maxHP;
    }

    public void HealUp(int amount)
    {
        currentHP += amount;

        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    #endregion

    #region MP methods

    /// <summary>
    /// Returns true if mana was used.
    /// </summary>
    /// <param name="amount">Mana points to use.</param>
    /// <returns>True if mana was used.</returns>
    public bool UseMana(int amount)
    {
        if (currentMP < amount)
            return false;
        else
        {
            currentMP -= amount;
            Debug.Log("Used mana for " + amount);
            Debug.Log("Total mana: " + CurrentMP);
            return true;
        }
    }

    public void GetMana(int amount)
    {
        currentMP += amount;

        if (currentMP > maxMP)
        {
            currentMP = maxMP;
        }
    }

    #endregion

    #region Buff methods
    
    public void AttackBuff(int buffValue)
    {
        currentAP += buffValue;

        if (currentAP < 0)
            currentAP = 0;
    }

    public void DefenceBuff(int buffValue)
    {
        currentDP += buffValue;
    }

    #endregion

    private void Start()
    {
        currentHP = maxHP;
        currentMP = maxMP;
        currentAP = defaultAP;
        currentDP = defaultDP;

        HealthText = Instantiate(HealthText, transform);
        HealthText.text = currentHP + "/" + maxHP;
    }
}
