using UnityEngine;

[System.Serializable]
public class DungeonGeneratorData
{
    public bool UseRandomSeed;
    public int CurrentSeed;

    [Space]
    [Range(12, 36)] public int TargetRoomCount;

    public int GetSeed()
    {
        if (UseRandomSeed)
            CurrentSeed = Random.Range(-10000, 10000);

        return CurrentSeed;
    }

    public int GetRandomSeed()
    {
        CurrentSeed = Random.Range(-10000, 10000);
        return CurrentSeed;
    }
}