using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public List<Transform> ObjAnim;
    public float speedAnim;
    private bool tydaobratno = true;
    private int AnimCount;
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
        var target = new Vector3(obj.position.x, 1.365f, obj.position.z);
        var back = new Vector3(obj.position.x, 1.457f, obj.position.z);
        var children = obj.GetComponentsInChildren<Transform>();

        if (tydaobratno) //перемещаем в нужную позицию
        {
            obj.position = Vector3.MoveTowards(obj.position, target, Time.deltaTime * speedAnim);

            if (obj.position == target)
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
                isActivator = false;
            }
        }
    }

    private void AnimType()
    {
        
    }
}
