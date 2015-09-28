using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class UGUIFollowTarget : MonoBehaviour
{
    public RectTransform canvasRT;
    public Transform target;
    public Image hpBar;

    void Update()
    {
        transform.position = target.position;
    }
}