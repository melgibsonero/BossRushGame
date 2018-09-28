using UnityEngine;

public class CharCombatValues : MonoBehaviour
{
    [Space(-10), Header("Health")]
    public int maxHP;
    public int currentHP;

    [Space(-10), Header("Mana")]
    public int maxMP;
    public int currentMP;

    [Space(-10), Header("Attack")]
    public int defaultAP;       // no buffs
    public int currentAP;       // with buffs

    [Space(-10), Header("Defence")]
    public int defaultDP;       // no buffs
    public int currentDP;       // with buffs

    private int _totalDamage;

    #region HP methods

    public void TakeDamage(int damage)
    {
        _totalDamage = damage - currentDP;

        if (_totalDamage > 0)
        {
            currentHP -= _totalDamage;

            if (currentHP < 0)
            {
                gameObject.SetActive(false);
            }
        }

        _totalDamage = 0;
    }

    public void HealUp(int amount)
    {
        currentHP += amount;

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
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
    }
}
