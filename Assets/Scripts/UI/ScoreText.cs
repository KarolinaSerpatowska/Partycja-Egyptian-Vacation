using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public int counter;
    [SerializeField] Text text;

    // Start is called before the first frame update
    void Start()
    {
        counter = 5;
        text.text = "Enemies remain: " + counter.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter < 0) counter = 0;
        text.text = "Enemies remain: " + counter.ToString();
    }
}
