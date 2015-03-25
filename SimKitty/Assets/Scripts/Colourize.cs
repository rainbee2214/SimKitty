using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Colourize : MonoBehaviour
{
    public Color[] colors;
    public int currentColor = 0;
    public float speed = 1f;
    public float changeDelay = 1f;
    public float nextChangeTime;

    Text text;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        if (colors.Length <= 0)
        {
            colors = new Color[10];
            colors[0] = Color.black;
            colors[1] = Color.black;
            colors[2] = Color.black;
            colors[3] = Color.black;
            colors[4] = Color.black;
            colors[5] = Color.white;
            colors[6] = Color.white;
            colors[7] = Color.white;
            colors[8] = Color.white;
            colors[9] = Color.white;
        }
        nextChangeTime = Time.time;

        currentColor = Random.Range(0, colors.Length);
        text.color = colors[currentColor];
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextChangeTime)
        {
            currentColor++;
            if (currentColor >= colors.Length) currentColor = 0;
            nextChangeTime = Time.time + changeDelay;
        }
            text.color = Color.Lerp(text.color, colors[currentColor], Time.deltaTime * speed);
    }
}
