using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    [SerializeField]
    private Color normalColor;
    [SerializeField]
    private Color highlightColor;

    private Material material;

    public bool Active
    {
        get { return gameObject.activeSelf; }
        set { gameObject.SetActive(value); }
    }

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        SetHighlight(false);    
    }

    public void SetHighlight(bool isOn)
    {
        material.color = isOn ? highlightColor : normalColor;
    }
}
