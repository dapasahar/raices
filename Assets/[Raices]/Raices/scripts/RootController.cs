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

    public List<RootFragment> fragments = new();
    public List<RootFragment> tFragments = new();

    [SerializeField] int tickCounter = 0;

    [SerializeField] int turnCounter = 5;

    RootFragment[] specialFragments;

    public float TopLimit => transform.position.y;

    static int a = 0;

    IEnumerator Start()
    {
        specialFragments = new RootFragment[] { turnLeftPrefab, turnRightPrefab, forkPrefab };

        AddFragment(transform, null);

        while (true)
        {
            // 1.- Esperar tickTime
            yield return new WaitForSeconds(tickTime);

            // 2.- Obtener los actualizables


            // 3.- Actualizar los actualizables


            // 4.- Añadir los nuevos





            //Debug.Log($"Tick {tickCounter}");
            for (int i = fragments.Count - 1; i >= 0; i--)
            {
                fragments[i].Next();
            }
            fragments.AddRange(tFragments);
            tFragments = new List<RootFragment>();
            tickCounter++;
            //Debug.Break();
        }
    }

    public RootFragment AddFragment(Transform spawner, RootFragment father)
    {
        RootFragment f = Instantiate(GetPrefab(spawner.rotation.eulerAngles.z),
            spawner.position, spawner.rotation);
        f.controller = this;
        f.father = father;
        f.name = "Fragment_" + a++;
        tFragments.Add(f);
        return f;
    }

    RootFragment GetPrefab(float rotation)
    {
        if (tickCounter > turnCounter)
        {
            tickCounter = 0;
            turnCounter = Random.Range(5, 8);
            switch (rotation)
            {
                case 0:
                    return GetRandomFragment();
                case 90:
                    return GetRandomFragment(turnLeftPrefab, forkPrefab);
                case 270:
                    return GetRandomFragment(turnRightPrefab, forkPrefab);
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

    internal void RemoveFragment(RootFragment rootFragment)
    {
        fragments.Remove(rootFragment);
    }
}
