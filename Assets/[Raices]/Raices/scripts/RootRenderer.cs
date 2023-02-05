using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootRenderer : MonoBehaviour
{
    public const int FRAG_1 = 1;
    public const int FRAG_2 = 2;

    public GameObject fragmento1;

    public GameObject fragmento2;

    public void Render(int frag)
    {
        DestroyImmediate(transform.GetChild(0).gameObject);
        Instantiate(frag == 1 ? fragmento1 : fragmento2, transform);
    }
}
