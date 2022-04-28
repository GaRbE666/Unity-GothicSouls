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

    private Transform GetNearestObject(Transform hit, Vector3 hitPosition)
    {
        System.Single closetPos = 100f;
        Transform closetbone = null;
        Transform childs = hit.GetComponentInChildren<Transform>();

        foreach (Transform child in childs)
        {
            System.Single distance = Vector3.Distance(child.position, hitPosition);

            if (distance < closetPos)
            {
                closetPos = distance;
                closetbone = child;
            }
        }

        System.Single distanceRoot = Vector3.Distance(hit.position, hitPosition);

        if (distanceRoot < closetPos)
        {
            closetPos = distanceRoot;
            closetbone = hit;
        }

        return closetbone;
    }

    public void InstantiateBlood(Transform hit)
    {
        float angle = Mathf.Atan2(hit.position.normalized.x, hit.position.normalized.z) * Mathf.Rad2Deg + 100;

        GameObject bloodClone = Instantiate(prefabs[GenerateRandomNum()], hit.position, Quaternion.Euler(0, angle + 90, 0));
        //bloodClone.transform.SetParent(bloodInstancePosition);
        BFX_BloodSettings settings = bloodClone.GetComponent<BFX_BloodSettings>();
        settings.LightIntensityMultiplier = directional.intensity;

        Transform nearestBone = GetNearestObject(hit.root, hit.position);
        if (nearestBone != null)
        {
            GameObject attackBloodInstance = Instantiate(bloodDecal);
            Transform bloodT = attackBloodInstance.transform;
            bloodT.position = hit.position;
            bloodT.localRotation = Quaternion.identity;
            bloodT.localScale = Vector3.one * Random.Range(0.75f, 1.2f);
            bloodT.LookAt(hit.position + hit.position.normalized, direction);
            bloodT.Rotate(90, 0, 0);
            bloodT.transform.parent = nearestBone;
        }

    }

    private int GenerateRandomNum()
    {
        return Random.Range(0, prefabs.Length);
    }
}
