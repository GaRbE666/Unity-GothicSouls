using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class ArmsModelChanger : MonoBehaviour
    {
        public List<GameObject> armsModels;

        private void Awake()
        {
            GetAllArmsModels();
        }

        private void GetAllArmsModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++)
            {
                armsModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnEquipAllArmsModels()
        {
            foreach (GameObject armModel in armsModels)
            {
                armModel.SetActive(false);
            }
        }

        public void EquipArmsModelByNAme(string armName)
        {
            for (int i = 0; i < armsModels.Count; i++)
            {
                if (armsModels[i].name == armName)
                {
                    armsModels[i].SetActive(true);
                }
            }
        }
    }
}
