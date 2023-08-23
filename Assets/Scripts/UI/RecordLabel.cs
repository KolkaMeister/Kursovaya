using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordLabel : MonoBehaviour
{
    [SerializeField] private Text _text;
    private void Start()
    {
        if (GameSession.RecordPoints>0)
             _text.text = GameSession.RecordPoints.ToString();
    }
}
