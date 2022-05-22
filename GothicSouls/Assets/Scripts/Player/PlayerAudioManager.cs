using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerAudioManager : CharacterAudioManager
    {
        [Header("Inventory")]
        public AudioClip openInventory;
        public AudioClip equipArmor;

        [Header("Spells")]
        public AudioClip healMagic;
        public AudioClip fireMagic;

        [Header("Weapon")]
        public AudioClip drawSword;
        public AudioClip shieldBlock;

        [Header("Items")]
        public AudioClip drinkEstus;
        public AudioClip illusionaryWall;
        public AudioClip openChest;
        public AudioClip explosion;

        #region INVENTORY
        public void PlayOpenInventory()
        {
            audioSource.PlayOneShot(openInventory);
        }

        public void PlayEquipArmor()
        {
            audioSource.PlayOneShot(equipArmor);
        }
        #endregion

        #region SPELLS
        public void PlayHealMagic()
        {
            audioSource.PlayOneShot(healMagic);
        }

        public void PlayFireMagic()
        {
            audioSource.PlayOneShot(fireMagic);
        }
        #endregion

        #region WEAPON
        public void PlayDrawSword()
        {
            audioSource.PlayOneShot(drawSword);
        }

        public void PlayShieldBlock()
        {
            audioSource.PlayOneShot(shieldBlock);
        }
        #endregion

        #region ITEMS
        public void PlayDrinkEstus()
        {
            audioSource.PlayOneShot(drinkEstus);
        }

        public void PlayIllusionaryWall()
        {
            audioSource.PlayOneShot(illusionaryWall);
        }

        public void PlayOpenChest()
        {
            audioSource.PlayOneShot(openChest);
        }

        public void PlayExplosion()
        {
            audioSource.PlayOneShot(explosion);
        }
        #endregion
    }
}
