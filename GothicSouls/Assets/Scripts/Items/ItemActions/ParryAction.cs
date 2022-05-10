using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(menuName = "Item Actions/Parry Action")]
    public class ParryAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            if (player.isInteracting)
            {
                return;
            }

            WeaponItem parryingWeapon = player.playerInventoryManager.currentItemBeingUsed as WeaponItem;

            if (parryingWeapon.weaponType == WeaponType.SmallShield)
            {
                player.playerAnimatorManager.PlayTargetAnimation("Parry", true);
            }
            else if (parryingWeapon.weaponType != WeaponType.Shield)
            {
                player.playerAnimatorManager.PlayTargetAnimation("Parry", true);
            }
        }
    }
}
