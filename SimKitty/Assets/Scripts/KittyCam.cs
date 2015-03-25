using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Skybox))]
public class KittyCam : MonoBehaviour
{
    Skybox skybox;
    Material[] skies;
    int currentSky = 0;

    ControlBackground mainCam;

    void Start()
    {
        LoadSkies();
        mainCam = Camera.main.GetComponent<ControlBackground>();
        skybox = GetComponent<Skybox>();
    }

    void Update()
    {
        if (currentSky != mainCam.currentSky)
        {
            currentSky = mainCam.currentSky;
            skybox.material = skies[currentSky];
        }
    }

    void LoadSkies()
    {
        skybox = Camera.main.GetComponent<Skybox>();
        skies = new Material[5];
        skies[0] = Resources.Load("Skyboxes/Skies/Morning", typeof(Material)) as Material;
        skies[1] = Resources.Load("Skyboxes/Skies/Afternoon", typeof(Material)) as Material;
        skies[2] = Resources.Load("Skyboxes/Skies/LateAfternoon", typeof(Material)) as Material;
        skies[3] = Resources.Load("Skyboxes/Skies/Evening", typeof(Material)) as Material;
        skies[4] = Resources.Load("Skyboxes/Skies/Night", typeof(Material)) as Material;
    }
}
