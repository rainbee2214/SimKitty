using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KittenController : MonoBehaviour
{
    public static KittenController controller;

    public List<GameObject> kittens;
    public int kittensSleeping;
    public int kittensEating;
    public int kittensPlaying;

    GameObject kittenPrefab;

    void Awake()
    {
        if (controller == null) controller = this;
    }

    void Start()
    {
        kittenPrefab = Resources.Load("Prefabs/Kitten", typeof(GameObject)) as GameObject;

        kittens = new List<GameObject>();
    }

    void Update()
    {
        UpdateKittenStatus();
    }

    public Transform GetRandomKitten()
    {
        if (kittens.Count < 1) return gameObject.transform;
        return kittens[Random.Range(0, kittens.Count-1)].transform;
    }

    public void GetNewKitten()
    {
        kittens.Add(Instantiate(kittenPrefab) as GameObject);
        kittens[kittens.Count - 1].name = "Kitten "+ (kittens.Count - 1);
    }

    void UpdateKittenStatus()
    {
        kittensEating = 0;
        kittensPlaying = 0;
        kittensSleeping = 0;
        foreach (GameObject kitten in kittens)
        {
            switch (kitten.GetComponent<Kitten>().currentStatus)
            {
                case Kitten.Status.DoingNothing: break;
                case Kitten.Status.Eating: kittensEating++; break;
                case Kitten.Status.Playing: kittensPlaying++; break;
                case Kitten.Status.Sleeping: kittensSleeping++; break;
            }
        }
    }
}
