using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPrefabs : MonoBehaviour
{
    [Tooltip("Decal que se muestra en el poropio modelo y se coloca como hijo del hueso colisionado")]
    [SerializeField] private GameObject bloodDecal;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Light directional;
    public Transform bloodInstancePosition;

    public Vector3 direction;

    public GameObject GetBlood(int index)
    {
        return prefabs[index];
    }

    public void InstantiateBlood(Transform hit)
    {
        float angle;

        if (hit != null)
        {
            angle = Mathf.Atan2(hit.position.normalized.x, hit.position.normalized.z) * Mathf.Rad2Deg + 100;
            GameObject bloodClone = Instantiate(prefabs[GenerateRandomNum()], hit.position, Quaternion.Euler(0, angle + 90, 0));
        }
        else
        {
            angle = 0;
            GameObject bloodClone = Instantiate(prefabs[GenerateRandomNum()], bloodInstancePosition.position, Quaternion.Euler(0, angle + 90, 0));
        }

        if (bloodInstancePosition != null)
        {
            GameObject attackBloodInstance = Instantiate(bloodDecal);
            Transform bloodT = attackBloodInstance.transform;
            bloodT.position = hit.position;
            bloodT.localRotation = Quaternion.identity;
            bloodT.localScale = Vector3.one * Random.Range(0.75f, 1.2f);
            bloodT.LookAt(hit.position + hit.position.normalized, direction);
            bloodT.Rotate(90, 0, 0);
            bloodT.transform.parent = bloodInstancePosition;
        }

    }

    private int GenerateRandomNum()
    {
        return Random.Range(0, prefabs.Length);
    }
}
