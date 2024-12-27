using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class Flicker : MonoBehaviour
{
    [SerializeField] Text flicker;
    
    void Update()
    {
        flicker.color = Color.Lerp(Color.white ,Color.red, Mathf.Sin(Time.time * 5) * 1f);
    }
}
