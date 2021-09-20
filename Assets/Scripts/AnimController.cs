using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public List<Transform> ObjAnim;
    public float speedAnim;
    private bool tydaobratno = true;
    private int AnimCount;
    private int Kostil;
    [SerializeField] private float speedArrow;
    private bool isActivator = false;
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
            if (ObjAnim[i].position == GameObject.Find("PlaceElectrod1").transform.position &&
            GameObject.Find("KolbaCylinder").transform.position == GameObject.Find("PlaceKolbaCylinder").transform.position)
            {
                isActivator = true;
                AnimCount = 3;
                transport = ObjAnim[i];
            }
            if (ObjAnim[i].position == GameObject.Find("PlaceElectrod2").transform.position &&
            GameObject.Find("KolbaCylinder1").transform.position == GameObject.Find("PlaceKolbaCylinder1").transform.position)
            {

                isActivator = true;
                AnimCount = 3;
                transport = ObjAnim[i];
            }
            if (ObjAnim[i].gameObject == GameObject.Find("Arrow"))
            {
                if (GameObject.Find("KolbaCylinder").transform.position == GameObject.Find("PlaceKolbaCylinder").transform.position &&
                GameObject.Find("KolbaCylinder1").transform.position == GameObject.Find("PlaceKolbaCylinder1").transform.position &&
                GameObject.Find("CylinderLiq").activeInHierarchy && Kostil == 3)
                {
                    Quaternion targetArrow = Quaternion.Euler(ObjAnim[i].eulerAngles.x, ObjAnim[i].eulerAngles.y, 70);
                    ObjAnim[i].rotation = Quaternion.Lerp(ObjAnim[i].rotation, targetArrow, speedArrow * Time.deltaTime);
                }
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
                var target = new Vector3(obj.position.x, 1.45f, obj.position.z);
                var children = obj.GetComponentInChildren<Transform>();
                obj.position = Vector3.MoveTowards(obj.position, target, Time.deltaTime * speedAnim);

                foreach (Transform child in children)
                {
                    if (child.name == "Left")
                    {
                        child.localEulerAngles = new Vector3(60, 0, 0);
                    }
                    if (child.name == "Right")
                    {
                        child.localEulerAngles = new Vector3(-60, 0, 0);
                    }
                }
                if (obj.position == target)
                {
                    tydaobratno = false;
                    isActivator = false;
                    Kostil += 1;
                }
                break;
            case 2:
                var target1 = new Vector3(obj.position.x, 1.365f, obj.position.z);
                var back = new Vector3(obj.position.x, 1.457f, obj.position.z);
                var children1 = obj.GetComponentsInChildren<Transform>();

                if (tydaobratno) //перемещаем в нужную позицию
                {
                    obj.position = Vector3.MoveTowards(obj.position, target1, Time.deltaTime * speedAnim);

                    if (obj.position == target1)
                    {
                        foreach (Transform child in children1)
                        {
                            child.gameObject.GetComponent<Renderer>().material.color = Color.red;
                        }
                        obj.GetComponent<Renderer>().material.color = Color.red;
                        tydaobratno = false;
                    }
                }
                else if (!tydaobratno)
                {
                    obj.position = Vector3.MoveTowards(obj.position, back, Time.deltaTime * speedAnim);

                    if (obj.position == back) //перемещаем обратно
                    {
                        tydaobratno = true;
                        obj.name = "WetPaper";
                        isActivator = false;
                    }
                }
                break;
            case 3:
                var target2 = new Vector3(obj.position.x, 1.471f, obj.position.z);

                obj.position = Vector3.MoveTowards(obj.position, target2, Time.deltaTime * speedAnim);

                if (obj.position == target2)
                {
                    tydaobratno = false;
                    isActivator = false;
                    Kostil += 1;
                }
                break;
        }
    }
}

