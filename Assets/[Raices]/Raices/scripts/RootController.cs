using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{

    [SerializeField] float tickTime = .5f;

    [SerializeField] RootFragment normalPrefab;
    [SerializeField] RootFragment turnLeftPrefab;
    [SerializeField] RootFragment turnRightPrefab;
    [SerializeField] RootFragment forkPrefab;

    public Transform leftLimit;
    public Transform rightLimit;

    public List<RootFragment> fragmentosColocados = new();
    public List<RootFragment> tFragments = new();

    public bool run = false;

    [SerializeField] int tickCounter = 0;

    [SerializeField] int turnCounter = 5;

    RootFragment[] specialFragments;

    public float TopLimit => transform.position.y;
    public float LefLimit => leftLimit.position.x;
    public float RightLimit => rightLimit.position.x;

    static int a = 0;

    IEnumerator Start()
    {
        specialFragments = new RootFragment[] { 
            turnLeftPrefab, turnRightPrefab, 
            turnLeftPrefab, turnRightPrefab, 
            forkPrefab };

        while (!run)
        {
            yield return null;
        }

        AddFragment(transform, null, false);

        while (run)
        {
            // 1.- Esperar tickTime
            yield return new WaitForSeconds(tickTime);

            // 2.- Obtener puntas que se tiene que detener
            QuitarPuntasDetenidas();

            // 3.- Obtener fragmento que ya han terminado su ciclo
            QuitarFragmentosCompletos();

            // 4.- actualizar fragmentos colocados
            foreach (var item in fragmentosColocados)
            {
                item.Next();
            }

            // 5.- colocar nuevos fragmentos


            //Debug.Log($"Tick {tickCounter}");
            fragmentosColocados.AddRange(tFragments);
            tFragments = new List<RootFragment>();
            tickCounter++;
            //Debug.Break();

            if (tickCounter > turnCounter)
            {
                tickCounter = 0;
                turnCounter = Random.Range(5, 8);
            }
        }


    }

    private void QuitarFragmentosCompletos()
    {
        List<RootFragment> completos = new();
        foreach (var item in fragmentosColocados)
        {
            if (item.Completo)
            {
                completos.Add(item);
            }
        }
        foreach (var item in completos)
        {
            fragmentosColocados.Remove(item);
        }
    }

    private void QuitarPuntasDetenidas()
    {
        List<RootFragment> puntasDetenidas = new();
        foreach (var item in fragmentosColocados)
        {
            if (item.Detenida)
            {
                puntasDetenidas.Add(item);
                puntasDetenidas.Add(item.father);
            }
        }

        foreach (var item in puntasDetenidas)
        {
            fragmentosColocados.Remove(item);
        }
    }

    public RootFragment AddFragment(Transform spawner, RootFragment father, bool special)
    {

        RootFragment f = Instantiate(GetPrefab(spawner.rotation.eulerAngles.z, special),
            spawner.position, spawner.rotation);
        f.controller = this;
        f.father = father;
        f.name = "Fragment_" + a++;
        tFragments.Add(f);
        return f;
    }

    RootFragment GetPrefab(float rotation, bool special)
    {
        if (special)
        {
            switch (rotation)
            {
                case 0:
                    return GetRandomFragment();
                case 90:
                    return GetRandomFragment(turnLeftPrefab, turnLeftPrefab, forkPrefab);
                case 270:
                    return GetRandomFragment(turnRightPrefab, turnRightPrefab, forkPrefab);
                case 180:
                    return GetRandomFragment(turnLeftPrefab, turnLeftPrefab);
            }

        }
        return normalPrefab;
    }

    RootFragment GetRandomFragment(params RootFragment[] list)
    {
        return list[Random.Range(0, list.Length)];
    }

    RootFragment GetRandomFragment()
    {
        return specialFragments[Random.Range(0, specialFragments.Length)];
    }

}
