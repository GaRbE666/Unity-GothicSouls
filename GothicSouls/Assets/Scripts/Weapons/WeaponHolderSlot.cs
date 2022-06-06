using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JS
{
    public class WeaponHolderSlot : MonoBehaviour
    {
        public Transform parentOverride;
        public Transform shieldParentOverride;
        public Transform staffParentOverride;
        public WeaponItem currentWeapon;
        public bool isLeftHandSlot;
        public bool isRightHandSlot;
        public bool isBackSlot;

        public GameObject currentWeaponModel;

        public void UnloadWeapon()
        {
            if (currentWeaponModel != null)
            {
                currentWeaponModel.SetActive(false);
            }
        }

        public void UnloadWeaponAndDestroy()
        {
            if (currentWeaponModel != null)
            {
                Destroy(currentWeaponModel);
            }
        }

        public void LoadWeaponModel(WeaponItem weaponItem)
        {
            UnloadWeaponAndDestroy();

            if (weaponItem == null)
            {
                UnloadWeapon();
                return;
            }

            GameObject model = Instantiate(weaponItem.modelPrefab) as GameObject;

            if (model != null)
            {
                if (model.name.Equals("Shield(Clone)"))
                {
                    if (shieldParentOverride != null)
                    {
                        model.transform.parent = shieldParentOverride;
                    }
                    else
                    {
                        model.transform.parent = transform;
                    }
                }
                else if (model.name.Equals("Staff(Clone)"))
                {
                    if (staffParentOverride != null)
                    {
                        model.transform.parent = staffParentOverride;
                    }
                    else
                    {
                        model.transform.parent = transform;
                    }
                }
                else
                {
                    if (parentOverride != null)
                    {
                        model.transform.parent = parentOverride;
                    }
                    else
                    {
                        model.transform.parent = transform;
                    }
                }



                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }

            currentWeaponModel = model;
        }
    }
}
