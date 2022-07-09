using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dontdestory : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
