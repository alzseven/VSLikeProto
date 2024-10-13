using System;
using UnityEngine;

namespace Content.Data
{
    public class PlayerStat : MonoBehaviour
    {
        // can be modified with modifiers
        public float maxHealth;
        public int armor;
        public float recovery;
        public float moveSpeed;
        // public static float AttackDamage; TODO: should be DamageMultiplier or something
        public float additionalCoolDown;
        public int additionalProjectileAmount;
        public float additionalExp;
        
        // can be modified with interactions
        public float currentHealth;
        public Action<float, float> OnHealthChanged;

        public void ApplyStatModifier(StatModifier statModifier)
        {
            maxHealth += statModifier.maxHealthBonus;
            armor += statModifier.armorBonus;
            recovery += statModifier.recoveryBonus;
            moveSpeed += statModifier.moveSpeedBonus;
            additionalCoolDown += statModifier.additionalCoolDownBonus;
            additionalProjectileAmount += statModifier.additionalProjectileAmountBonus;
            additionalExp += statModifier.additionalExpBonus;
        }

        public void RemoveStateModifier(StatModifier statModifier)
        {
            maxHealth -= statModifier.maxHealthBonus;
            armor -= statModifier.armorBonus;
            recovery -= statModifier.recoveryBonus;
            moveSpeed -= statModifier.moveSpeedBonus;
            additionalCoolDown -= statModifier.additionalCoolDownBonus;
            additionalProjectileAmount -= statModifier.additionalProjectileAmountBonus;
            additionalExp -= statModifier.additionalExpBonus;
        }
    }
}