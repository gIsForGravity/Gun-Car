using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Laps : MonoBehaviour
{

    private int _lapCount = 1;
    private int _checks = 0;

    public TMP_Text lapText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lapText.SetText("Laps: " + _lapCount);
    }

    public void lapIncr()
    {
        if (_lapCount == _checks)
        {
            _lapCount++;
        }
    }

    public void checkIncr()
    {
        if (_checks == _lapCount - 1)
        {
            _checks++;
        }
    }
}
