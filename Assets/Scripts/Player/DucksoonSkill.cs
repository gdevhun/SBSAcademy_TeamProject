using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DucksoonSkill : MonoBehaviour
{
    public GameObject rotatingCharmPrefab;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private int curCharmDmg;
    private List<GameObject> rotatingCharmList = new List<GameObject>();
    
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void SpawnRotatingCharm()
    {
        for (int i = 0; i < rotatingCharmList.Count; i++)
        {
            // 리스트에 있는 오브젝트 접근해서 수정
            GameObject rotatingCharm = rotatingCharmList[i];
            rotatingCharm.transform.localPosition = Vector3.zero;
            rotatingCharm.transform.localRotation = Quaternion.identity;
            rotatingCharm.GetComponent<Charm>().Damage = curCharmDmg;
            // 위치와 회전 설정
            float angle = i * 360f / rotatingCharmList.Count;
            rotatingCharm.transform.localPosition = 
                new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * 2f, Mathf.Sin(angle * Mathf.Deg2Rad) * 2f, 0f);
            rotatingCharm.transform.Rotate(Vector3.forward, angle);
        }
    }
    
    private void AddCharmNum()
    {
        //하나를 생성해서 리스트에 넣기
        GameObject rotatingCharm = Instantiate(rotatingCharmPrefab, Vector3.zero, Quaternion.identity);
        rotatingCharm.transform.SetParent(this.transform);
        rotatingCharm.GetComponent<Charm>().Damage = curCharmDmg;
        this.rotatingCharmList.Add(rotatingCharm);

        SpawnRotatingCharm();
    }


}
