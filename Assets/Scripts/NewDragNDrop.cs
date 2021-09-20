using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDragNDrop : MonoBehaviour
{
    public GameObject prefab;
    public List<GameObject> AllDragNDropObj;
    public float undertabledist;
    private bool flag = true;
    private GameObject sphere;
    public GameObject Selected;
    private bool Dragged;

    private void FixedUpdate()
    {
        var target = Cursor();

        CanAndCantDragChecker(Dragged, target);
        
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Dragged = true;
            Invoke("DragBody", 0.04f);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Dragged = false;
            if (Selected != null) //просто чтобы не мозолили ошибки в дебагере
            {
                Selected.GetComponent<Rigidbody>().isKinematic = false;
                Selected.gameObject.layer = LayerMask.NameToLayer("Default");
            }
            Selected = null;
        }
    }
    public Transform Cursor() // метод сферы которая как бы 3d курсор ползающий по поверхности коллайдеров за курсором мыши и выдающая объект в который попадает
    {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition); //луч триггер
        RaycastHit hit; //регистрация попадания
        Transform objectHit = null;//объект попадания

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1, QueryTriggerInteraction.Ignore))
        {
            objectHit = hit.transform;

            Debug.DrawLine(ray.origin, hit.point, Color.green); //просто видеть в инспекторе сам луч зеленый когда на чем то
            if (flag) //не придумал как исправить, но это можно
            {
                flag = false;
                sphere = Instantiate(prefab, hit.point, prefab.transform.rotation); //создание курсора из префаба 
            }
            sphere.transform.position = ray.origin + ray.direction.normalized * hit.distance; //положение курсора на поверхности коллайдеров 
        }
        else
        {
            flag = true;
            Destroy(sphere);
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.red);//красный когда не на чем то
        }
        return objectHit;
    }
    private void CanAndCantDragChecker(bool isdragged, Transform kostil) //метод проверяющий теги на возможность переноса и обратно
    {
        Transform[] children;
        if (isdragged)
        {
            for (int i = 0; i < AllDragNDropObj.Count; i++)
            {
                AllDragNDropObj[i].tag = "UnDragable";  //отключение тегов у всех возможных для таскания предметов
                AllDragNDropObj[i].gameObject.layer = LayerMask.NameToLayer("Ignore Raycast"); //отключаем возможность луча попадать в остальны объеты во время таскания
                children = AllDragNDropObj[i].GetComponentsInChildren<Transform>();
                foreach (var child in children)
                {
                    child.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                }
                if (kostil.GetComponent<Rigidbody>()) //проверка на выбранном объекте компонента физики.
                {
                    kostil.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");//отключение луча для объекта
                    Selected = kostil.gameObject; // переопределение выбранного объекта в переменную для проверок и вызова в других скриптах
                }
            }
        }
        else
        {
            for (int i = 0; i < AllDragNDropObj.Count; i++)
            {
                children = AllDragNDropObj[i].GetComponentsInChildren<Transform>();
                foreach (var child in children)
                {
                    child.gameObject.layer = LayerMask.NameToLayer("Default");
                }
                AllDragNDropObj[i].tag = "Dragable"; //Включение тегов у всех возможных для таскания предметов
                AllDragNDropObj[i].gameObject.layer = LayerMask.NameToLayer("Default"); // включаем обратно возможность попадать
            }
        }
    }
    private void DragBody() //таскание предмета по предметам за 3д кусрором
    {
        for (int i = 0; i < AllDragNDropObj.Count; i++)
        {
            if (Selected == AllDragNDropObj[i]) // только объекты, которые есть в списке таскаемых
            {
                Selected.GetComponent<Rigidbody>().isKinematic = true;
                Selected.transform.position = new Vector3(sphere.transform.position.x, sphere.transform.position.y + undertabledist, sphere.transform.position.z);
            }
        }
    }
}
