using UnityEngine;
using System.Collections.Generic;

public class CurrentLevelInfo : MonoBehaviour {

    public MushroomScript.MushroomName desiredMushroom;
    public Dictionary<MushroomScript.MushroomName, int> pickedMushrooms;
    public int levelNumber = 1;
    public int mushroomCount = 0;
    public int grassPicked = 0;
    public int leavesBrushed = 0;
    public Vector3 mushroomRatio = new Vector3(10, 0, 0);

    void Awake()
    {
        if(FindObjectsOfType<CurrentLevelInfo>().Length > 1)
        {
            Destroy(this.gameObject);
        }
        pickedMushrooms = new Dictionary<MushroomScript.MushroomName, int>();
        desiredMushroom = MushroomScript.MushroomName.BUTTON;
        pickedMushrooms.Add(MushroomScript.MushroomName.BUTTON, 3);
        pickedMushrooms.Add(MushroomScript.MushroomName.DEATHCAP, 2);
        grassPicked = 20;
        leavesBrushed = 15;
        mushroomCount = 5;
        levelNumber = 1;
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddMushroom(MushroomScript.MushroomName mushroomName)
    {
        if (pickedMushrooms.ContainsKey(mushroomName))
        {
            pickedMushrooms[mushroomName]++;
        }else
        {
            pickedMushrooms.Add(mushroomName, 1);
        }mushroomCount++;
    }

    public Dictionary<MushroomScript.MushroomName, int> GetPickedMushrooms()
    {
        return pickedMushrooms;
    }

    public int GetNumberOfDesiredMushrooms()
    {
        if (pickedMushrooms.ContainsKey(desiredMushroom))
        {
            return pickedMushrooms[desiredMushroom];
        }
        else
        {
            return 0;
        }
    }

    public MushroomScript.MushroomName GetDesiredMushroom()
    {
        return desiredMushroom;
    }
}
