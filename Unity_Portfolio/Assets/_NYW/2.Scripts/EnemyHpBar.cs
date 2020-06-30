using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    public GameObject hpBar = null;

    List<Transform> enemyPoint = new List<Transform>();
    List<GameObject> enemyHpBarList = new List<GameObject>();

    Camera mainCam = null;

    public static EnemyHpBar instance;

    private void Awake()
    {
        EnemyHpBar.instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i=0; i<objects.Length;i++)
        {
            enemyPoint.Add(objects[i].transform);
            GameObject t_hpbar = Instantiate(hpBar, objects[i].transform.position, Quaternion.identity, transform);
            enemyHpBarList.Add(t_hpbar);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<enemyPoint.Count;i++)
        {
            enemyHpBarList[i].transform.position = mainCam.WorldToScreenPoint(enemyPoint[i].position + new Vector3(0, 1.15f, 0));

        }
    }
}
