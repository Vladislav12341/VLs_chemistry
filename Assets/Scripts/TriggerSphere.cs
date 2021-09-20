using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSphere : MonoBehaviour
{
    public List<GameObject> Сontainer = new List<GameObject>(); //список объектов куда наливается
    public Vector3 BackPoint; //уровень возврата объекта в нужное положение 
    public GameObject ParticleWaterObj; // вода внутри колб и откуда наливается
    public List<GameObject> PrefabWater; //меш воды которая якобы наливается
    public float speed; //скорость наклона колбы
    public float speedLiq; // скорость жидкости
    public float target; // уровень подъема воды
    public float ClosePoint; //расстояние до триггера
    private bool isCorrectContainer; //правильность наполняемого сосуда
    private GameObject _triggerredObj; //правильно задетый объект

    private void OnTriggerEnter(Collider other)
    {
        var Used = GameObject.Find("Main Camera").GetComponent<NewDragNDrop>().Selected;

        for (int i = 0; i < Сontainer.Count; i++)
        {
            if (other.gameObject == Сontainer[i] && Used == transform.gameObject)
            {
                _triggerredObj = other.gameObject;
                isCorrectContainer = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        float angleY = Mathf.Round(transform.rotation.eulerAngles.y);
        float angleX = Mathf.Round(transform.rotation.eulerAngles.x);

        for (int i = 0; i < Сontainer.Count; i++)
        {
            if (isCorrectContainer)
            {
                Vector3 direction = Сontainer[i].transform.position - transform.position;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), speed * Time.deltaTime);

                if (angleY >= 0 && angleY < 60 || angleX >= 0 && angleX < 60)
                {
                    GameObject.Find(ParticleWaterObj.name).GetComponent<ParticleSystem>().Play(true);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var Used = GameObject.Find("Main Camera").GetComponent<NewDragNDrop>().Selected;

        for (int i = 0; i < Сontainer.Count; i++)
        {
            if (other.gameObject == Сontainer[i] && Used == transform.gameObject)
            {
                transform.rotation = Quaternion.Euler(BackPoint); //временно замена нормальному методу
                GameObject.Find(ParticleWaterObj.name).GetComponent<ParticleSystem>().Stop(true);
                isCorrectContainer = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if (GameObject.Find(ParticleWaterObj.name).GetComponent<ParticleSystem>().isPlaying)
        {
            WaterAnim(_triggerredObj.transform);
        }
    }
    private void WaterAnim(Transform _TriggeredObj)
    {
        var _liquid = _TriggeredObj.GetChild(0);
        _liquid.gameObject.SetActive(true);
        var LvlLiq = new Vector3(_liquid.localPosition.x, _liquid.localPosition.y, target);
        _liquid.localPosition = Vector3.MoveTowards(_liquid.localPosition, LvlLiq, speedLiq * Time.deltaTime);
       
    }
}