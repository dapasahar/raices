using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musica : MonoBehaviour
{
    Musica instancia;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = GetComponent<Musica>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
}
