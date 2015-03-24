using UnityEngine;
using System.Collections;

public class Kitten : MonoBehaviour
{
    int MAX = 200;
    int LOCAL_MAX = 10;
    float FLOOR_MAX = 30f;
    Color maleColor = new Color(0.5f,0.6f,1,255), femaleColor = new Color(1f,0.5f,0.85f,255);
    public enum Status
    {
        DoingNothing,
        Eating,
        Playing,
        Sleeping
    }
    [Range(0, 10)]
    public int hungryness, sleepyness, playfulness;

    [Range(0, 100)]
    public int angryness, funness, cuteness, lazyness, cuddlyness, fatness;

    int personalityPoints = 200;

    bool female = true;
    public string Gender
    {
        get 
        {
            if (female) return "Female";
            else return "Male";
        }
    }
    int firstBonus, secondBonus;
    bool negation;

    public Status currentStatus;

    float minuteDelay = 60f;
    float halfMinDelay = 30f;
    float quickDelay = 5f;


    float nextActionTime;
    float nextUpdateTime;
    float nextWalktime;
    float currentTime;
    Vector3 newPosition;
    public float walkSpeed = 0.2f;

    void Start()
    {
        CreateKitten();
    }

    // Update is called once per frame
    void Update()
    {
        AdjustAttributes();
        float currentTime = Time.time;
        if (currentTime > nextActionTime) TakeAction();
        if (currentTime > nextUpdateTime) UpdateAttributes();

        //If it's not doing anything, walk around
        if (currentStatus == Status.DoingNothing)
        {
            if (Time.time > nextWalktime) WalkAround();
            Vector3 currPosition = transform.position;
            if (currPosition.x > FLOOR_MAX) currPosition.x = FLOOR_MAX;
            else if (currPosition.x < -FLOOR_MAX) currPosition.x = -FLOOR_MAX;

            if (currPosition.z > FLOOR_MAX) currPosition.y = FLOOR_MAX;
            else if (currPosition.z < -FLOOR_MAX) currPosition.y = -FLOOR_MAX;

            currPosition.y = 0f;
            transform.position = Vector3.Lerp(transform.position,newPosition, Time.deltaTime*walkSpeed);
        }
    }

    void WalkAround()
    {
        newPosition = new Vector3(Random.Range(-1f, 1f)*FLOOR_MAX, 0f, Random.Range(-1f, 1f)*FLOOR_MAX);
        nextWalktime = Time.time + halfMinDelay - quickDelay;
    }

    bool TakeAction()
    {
        //1/5 of the time, the cat will just continue to do what it is doing
        switch (Random.Range(1,100) % 5)
        {
            case 0: 
          //      Debug.Log(name + " is still " + currentStatus);
                return false;
        }

        if (hungryness == 10) Eat();
        else if (playfulness == 10) Play();
        else if (sleepyness == 10) Sleep();

        if (currentStatus == Status.DoingNothing)
        {
            switch (Random.Range(0, 1000) % 10)
            {
                case 0: Eat(true); break;
                case 1: Play(true); break;
                case 2: Sleep(true); break;
                default: break;
            }
        }
        else
        {
            currentStatus = Status.DoingNothing;
        }
        nextActionTime = Time.time + quickDelay;
        //Debug.Log(name + " is now " + currentStatus);
        return true; //the kitten made a new action
    }

    void Play(bool random = false)
    {
        if (random && playfulness >= 9) funness++;
        currentStatus = Status.Playing;
        switch (Random.Range(0, 100) % 2)
        {
            case 0: playfulness--; break;
            case 1: if (playfulness != 10) playfulness++; break;
        }
    }

    void Eat(bool random = false)
    {
        if (random && hungryness >= 9) fatness++;
        hungryness--;
        currentStatus = Status.Eating;
    }

    void Sleep(bool random = false)
    {
        if (random && sleepyness >= 9) lazyness++;
        currentStatus = Status.Sleeping;
        sleepyness -= 2;
    }

    void UpdateAttributes()
    {
        nextUpdateTime += minuteDelay;
        switch (Random.Range(0, 100) % 6)
        {
            case 0: hungryness++; break;
            case 1: playfulness++; break;
            case 2: sleepyness++; break;
        }
    }
    
    void CreateKitten()
    {
        switch (Random.Range(0,101) % 2)
        {
            case 0: female = false; break;
        }

        if (female)
            GetComponent<MeshRenderer>().material.color = femaleColor;
        else 
            GetComponent<MeshRenderer>().material.color = maleColor;
        
        currentStatus = Status.DoingNothing;
        hungryness = Random.Range(0, 11);
        sleepyness = Random.Range(0, 11);
        playfulness = Random.Range(0, 11);

        firstBonus = Random.Range(0, 6);
        secondBonus = Random.Range(0, 6);

        for (int i = personalityPoints; i > 0; i--)
        {
            switch (Random.Range(0, 107) % 6)
            {
                case 0:
                    {
                        angryness++;
                        if (firstBonus == 0) angryness += 2;
                        if (secondBonus == 0) angryness++;
                        if (Random.Range(0, 100) % 12 == 0) angryness--;
                        break;
                    }
                case 1:
                    {
                        funness++;
                        if (firstBonus == 1) funness += 2;
                        if (secondBonus == 1) funness++;
                        if (Random.Range(0, 100) % 12 == 0) funness--;

                        break;
                    }
                case 2: cuteness++;
                    {
                        cuteness++;
                        if (firstBonus == 2) cuteness += 2;
                        if (secondBonus == 2) cuteness++;
                        if (Random.Range(0, 100) % 12 == 0) cuteness--;
                        break;
                    }
                case 3: lazyness++;
                    {
                        lazyness++;
                        if (firstBonus == 3) lazyness += 2;
                        if (secondBonus == 3) lazyness++;
                        if (Random.Range(0, 100) % 12 == 0) lazyness--;
                        break;
                    }
                case 4: cuddlyness++;
                    {
                        cuddlyness++;
                        if (firstBonus == 4) cuddlyness += 2;
                        if (secondBonus == 4) cuddlyness++;
                        if (Random.Range(0, 100) % 12 == 0) cuddlyness--;
                        break;
                    }
                case 5: fatness++;
                    {
                        fatness++;
                        if (firstBonus == 4) fatness += 2;
                        if (secondBonus == 4) fatness++;
                        if (Random.Range(0, 100) % 12 == 0) fatness--;
                        break;
                    }
            }
        }
        walkSpeed += (lazyness/100f);
        if (walkSpeed > 1f) walkSpeed /= 4f;
        else if (walkSpeed > 0.5f) walkSpeed /= 3f;
        else if (walkSpeed > 0.3f) walkSpeed /= 2f;
        //Debug.Log("Hungryness: " + hungryness + "\nSleepyness: " + sleepyness + "\nPlayfulness: " + playfulness +
        //    "\n\nAngryness: " + angryness + "\nFunness: " + funness + "\nCuteness: " + cuteness +
        //    "\nLazyness: " + lazyness + "\nCuddlyness: " + cuddlyness + "\nFatness: " + fatness);
    }
    
    void AdjustAttributes()
    {
        if (hungryness < 0) hungryness = 0;
        else if (hungryness > LOCAL_MAX) hungryness = LOCAL_MAX;

        if (playfulness < 0) playfulness = 0;
        else if (playfulness > LOCAL_MAX) playfulness = LOCAL_MAX;

        if (sleepyness < 0) sleepyness = 0;
        else if (sleepyness > LOCAL_MAX) sleepyness = LOCAL_MAX;

        if (angryness < 0) angryness = 0;
        else if (angryness > MAX) angryness = MAX;

        if (lazyness < 0) lazyness = 0;
        else if (lazyness > MAX) lazyness = MAX;

        if (funness < 0) funness = 0;
        else if (funness > MAX) funness = MAX;

        if (cuteness < 0) cuteness = 0;
        else if (cuteness > MAX) cuteness = MAX;

        if (cuddlyness < 0) cuddlyness = 0;
        else if (cuddlyness > MAX) cuddlyness = MAX;

        if (fatness < 0) fatness = 0;
        else if (fatness > MAX) fatness = MAX;
    }
}
