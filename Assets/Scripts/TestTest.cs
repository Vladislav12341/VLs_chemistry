using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTest : MonoBehaviour
{
    public List<Transform> ObjAnim;
    public float speedAnim;
    private bool tydaobratno = true;
    [SerializeField] private int AnimCount;
    public bool isActivator = false;
    private Transform transport;
    private void FixedUpdate()
    {
        for (int i = 0; i < ObjAnim.Count; i++)
        {
            if (ObjAnim[i].position == GameObject.Find("PlacePaper").transform.position && GameObject.Find("WaterVanna").activeInHierarchy &&
            GameObject.Find("bath").transform.position == GameObject.Find("PlaceBath").transform.position)
            {
                isActivator = true;
                AnimCount = 2;
                transport = ObjAnim[i];
            }
            if (ObjAnim[i].position == GameObject.Find("PlaceWetPaper").transform.position && GameObject.Find("CylinderLiq").activeInHierarchy &&
            GameObject.Find("KolbaCylinder").transform.position == GameObject.Find("PlaceKolbaCylinder").transform.position &&
            GameObject.Find("KolbaCylinder1").transform.position == GameObject.Find("PlaceKolbaCylinder1").transform.position)
            {
                isActivator = true;
                AnimCount = 1;
                transport = ObjAnim[i];
            }
        }
        if (isActivator)
        {
            Anim(transport, AnimCount);
        }
    }

    private void Anim(Transform obj, int playcount)
    {
        switch (playcount)
        {
            case 1:
                var target = new Vector3(obj.position.x, 1.365f, obj.position.z);
                obj.position = Vector3.MoveTowards(obj.position, target, Time.deltaTime * speedAnim);

                if (obj.position == target)
                {
                    playcount = 1;
                    tydaobratno = false;
                    isActivator = false;
                }
                break;
            case 2:
                var target1 = new Vector3(obj.position.x, 1.365f, obj.position.z);
                var back = new Vector3(obj.position.x, 1.457f, obj.position.z);
                var children = obj.GetComponentsInChildren<Transform>();

                if (tydaobratno) //перемещаем в нужную позицию
                {
                    obj.position = Vector3.MoveTowards(obj.position, target1, Time.deltaTime * speedAnim);

                    if (obj.position == target1)
                    {
                        playcount = 1;
                        tydaobratno = false;
                    }
                }
                else if (!tydaobratno)
                {
                    obj.position = Vector3.MoveTowards(obj.position, back, Time.deltaTime * speedAnim);

                    if (obj.position == back) //перемещаем обратно
                    {
                        playcount = 2;
                        tydaobratno = true;
                        obj.name = "WetPaper";
                        isActivator = false;
                    }
                }
                break;
        }
    }
}
