using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class Watch : MonoBehaviour
{
    public TMP_Text WatchText;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(WatchText);
    }

    // Update is called once per frame
    void Update()
    {
        WatchText.text = DateTime.UtcNow.ToString("H:mm:ss");
    }
}
