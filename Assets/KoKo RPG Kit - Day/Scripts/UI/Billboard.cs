using UnityEngine;
using System.Collections;

/// <summary>
/// Billboard script.
/// </summary>
public class Billboard : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}