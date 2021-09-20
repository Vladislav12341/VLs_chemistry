using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlace : MonoBehaviour
{
    public GameObject Standed;
    public List<string> NamesObj;
    private void FixedUpdate()
    {
        var Used = GameObject.Find("Main Camera").GetComponent<NewDragNDrop>().Selected;


        if (Used != null && Used == Standed)//появление ключевой позиции при претаскивании предмета
        {
            for (int i = 0; i < NamesObj.Count; i++)
            {
                if (Used.name == NamesObj[i])
                {
                    transform.gameObject.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
        else transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Standed)
        {
            Standed.transform.position = transform.position; //установка при триггере объекта в позицию стойки
            Standed.transform.rotation = transform.rotation; //установка положения вращения объетку 
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Standed.GetComponent<Rigidbody>().isKinematic = true;
    }
}
