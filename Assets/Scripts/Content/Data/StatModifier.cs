using System;

namespace Content.Data
{
    [Serializable]
    public class StatModifier
    {
        public float maxHealthBonus;
        public int armorBonus;
        public float recoveryBonus;
        public float moveSpeedBonus;
        // public static float AttackDamage; TODO: should be DamageMultiplier or something
        public float additionalCoolDownBonus;
        public int additionalProjectileAmountBonus;
        public float additionalExpBonus;
    }
}