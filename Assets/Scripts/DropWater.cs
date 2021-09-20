using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWater : MonoBehaviour
{
    public List<Transform> WaterInContainer;
    public GameObject Container;
    // public List<GameObject> WaterGrows = new List<GameObject>();
    public Vector3 target;
    // public List<GameObject> Kolbs = new List<GameObject>();
    public GameObject ParticleWaterObj;
    private float speed = 0.001f;
    public float radiusSphere;
    private void FixedUpdate()
    {
        float angle = Mathf.Round(transform.rotation.eulerAngles.x);
        SphereAroundObj(transform.position, radiusSphere);

        // var Ohmy = Container.GetComponent<RotatedObject>();

        // for (int i = 0; i < Ohmy.Kostil.Length; i++)
        // {
        //     Debug.Log(Ohmy.Kostil[i] + "   " + Ohmy.GetComponentInParent<GameObject>().name); //Такое тупое решение, но я хлебушек, у меня лапки
        // }

        if (angle >= 270 && angle < 360)
        {
            GameObject.Find(ParticleWaterObj.name).GetComponent<ParticleSystem>().Stop(true);
        }
        else if (angle >= 0 && angle < 60)
        {
            GameObject.Find(ParticleWaterObj.name).GetComponent<ParticleSystem>().Play(true);
        }
        // if (GameObject.Find(ParticleWaterObj.name).GetComponent<ParticleSystem>().isPlaying)
        // {
        //     Invoke("WaterUp", 2.1f);
        // }
    }
    void SphereAroundObj(Vector3 center, float radius)//сфера триггер для обработки приближающихся объектов
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (GameObject.Find(ParticleWaterObj.name).GetComponent<ParticleSystem>().isPlaying)
            {
                // Invoke("WaterUp", 2.1f); //почему то не инвокается?
                WaterUp(hitColliders[i]);
            }
        }
    }
    private void WaterUp(Collider comingObj) //возможна замена на список объектов куда налить можно
    {
        List<GameObject> ComingObj = new List<GameObject>();
        
        for (int i = 0; i < 10; i++)
        {
            ComingObj.Add(comingObj.gameObject);
            for (int j = 0; j <= WaterInContainer.Count; j++)
            {
                // Debug.Log(ComingObj[i].name);
                if (ComingObj[i] == GameObject.Find("bath"))
                {
                    var Liquid = ComingObj[i].transform.Find(WaterInContainer[j].name);
                    Debug.Log(j + "1");
                    Debug.Log(Liquid.name);
                    Liquid.transform.localPosition = Vector3.MoveTowards(Liquid.localPosition, target, speed * Time.deltaTime);
                }
                if (ComingObj[i] == GameObject.Find("KolbaCylinder") && j==1)
                {
                    Debug.Log(j + "2");
                    var Liquid = ComingObj[i].transform.Find(WaterInContainer[j].name); //большой вопрос что не так с прогоном
                    Debug.Log(Liquid.name);
                    Liquid.transform.localPosition = Vector3.MoveTowards(Liquid.localPosition, target, speed * Time.deltaTime);
                }
            }
        }
    }
}
