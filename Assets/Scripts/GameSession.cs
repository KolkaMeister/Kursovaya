using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public ObservalProperty<int> Points = new ObservalProperty<int>(0);

    public static int RecordPoints;
    public void AddPoints(int count)
    {
        Points.Value += count;
    }
    private void OnDestroy()
    {
        if(Points.Value > RecordPoints)
            RecordPoints=Points.Value;
    }
}
