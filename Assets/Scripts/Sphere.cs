using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{

    // скорость вращения. Можно менять в Unity потому что public
    public float rotationSpeedX = 10;
    public float rotationSpeedY = 20;
    public float rotationSpeedZ = 30;

    public GameObject bang; // взрыв

    AudioSource source; // источник звука

    Color startColor;
    Renderer renderer;

    // Выполняется в начале игры
    void Start()
    {
        source = GetComponent<AudioSource>(); // получить компонент звука

        renderer = GetComponent<Renderer>();
        startColor = renderer.material.color;
    }

    // Выполняется каждый кадр
    void Update()
    {
        // Вращает объект
        transform.Rotate(rotationSpeedX * Time.deltaTime,
            rotationSpeedY * Time.deltaTime, rotationSpeedZ * Time.deltaTime);

    }


    public void SelfDestruction()
    {
        Instantiate(bang, transform.position, transform.rotation); // спавним взрыв

        GameObject[] knifesObjects = GameObject.FindGameObjectsWithTag("Knife"); // найти все ножи
        // Для каждого ножа
        foreach (var o in knifesObjects)
        {
            o.transform.SetParent(null);

            var r = o.GetComponent<Rigidbody>();
            r.isKinematic = false;
            r.useGravity = true; // включить гравитацию
            r.AddExplosionForce(800, transform.position, 100);
            var k = o.GetComponent<Knife>(); // получить компонент ножа
            k.speed = 0; // выключить скорость
            k.deactivate = true;
        }


        MeshRenderer[] pices = GetComponentsInChildren<MeshRenderer>();

        // откинуть каждый кусок
        foreach (var o in pices)
        {
            o.transform.SetParent(null);
            
            var r = o.GetComponent<Rigidbody>();
            r.isKinematic = false;
            r.useGravity = true; // включить гравитацию
            o.GetComponent<Collider>().enabled = true;
            o.enabled = true;
            r.AddExplosionForce(600, transform.position, 100);
        }

        source.Play(); // проиграть звук

        Destroy(gameObject.GetComponent<MeshRenderer>());

    }

    public void ChangeAfterHit()
    {
        StartCoroutine(HitChange());
    }

    IEnumerator HitChange()
    {
        renderer.material.color += new Color(0.6f, 0.6f, 0.6f);
        transform.localScale -= new Vector3(0.03f, 0.03f, 0.03f);
        yield return new WaitForSeconds(0.03f);
        renderer.material.color = startColor;
        transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);


    }




}
