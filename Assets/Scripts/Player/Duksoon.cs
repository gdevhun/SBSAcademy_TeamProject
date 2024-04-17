using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duksoon : PlayerBase
{
    [SerializeField] private GameObject CharmPrefab;
    [SerializeField] private float charmRotationSpeed;
    private List<GameObject> _charmWeaponList;
    protected override void CharSkill()
    {
        InitCharm(1);
    }

    private void InitCharm(int num)
    {
        //num 수만큼 Charm을 생성
        //Player의 자식으로 오브젝트를 생성해서 들어가게함.
        //플레이어 주변을 원을 그리며 따라다니게됨.
       
    }
    
}
