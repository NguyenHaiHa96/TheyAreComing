using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FinishLine : MonoBehaviour, ISubcribers
{
    [SerializeField] private GameObject[] finishedMinions;
    [SerializeField] private Transform standPoint;
    [SerializeField] private float spacing;

    private PlayerMain playerMain;
    private List<Vector3> positions;
    private List<int> selectedIndex;
    private float newXPosition;
    private float newZPosition;
    private int rows;
    private int columns;
    private bool reached;

    private void Start()
    {
        InitializeVariables();
        SubscribeEvent();
    }

    private void OnDisable()
    {
        UnsubscribeEvent();
    }

    private void InitializeVariables()
    {
        playerMain = PlayerMain.Instance;
        positions = new List<Vector3>();
        selectedIndex = new List<int>();
        columns = 10;
        reached = false;
    }

    public void SubscribeEvent()
    {
        playerMain.OnReachedFinishLine += GenerateRandomPositions;
    }

    public void UnsubscribeEvent()
    {
        playerMain.OnReachedFinishLine -= GenerateRandomPositions;
    }


    private void Update() 
    {
        if(!reached)
        {
            return;
        }
        MinionLindingUp();
    }

    public void GenerateRandomPositions(GameObject[] minions)
    {
        finishedMinions = minions;
        if (finishedMinions.Length % columns == 0)
        {
            rows = finishedMinions.Length / columns;
        }
        else
        {
            rows = finishedMinions.Length / columns + 1;
        }
        newXPosition = standPoint.position.x;
        newZPosition = standPoint.position.z;
        for (int i = 0; i < rows; i++)
        {
            if (i == rows - 1) 
            {
                int lastColumn = finishedMinions.Length - i * columns;
                for (int j = 0; j < lastColumn; j++)
                {
                    Vector3 newPosition = new Vector3(newXPosition, standPoint.position.y, newZPosition);
                    RandomMinionPosition(finishedMinions, newPosition);
                    positions.Add(newPosition);
                    newXPosition += spacing;
                }
            }
            else
            {
                for (int j = 0; j < columns; j++)
                {
                    Vector3 newPosition = new Vector3(newXPosition, standPoint.position.y, newZPosition);
                    RandomMinionPosition(finishedMinions, newPosition);
                    positions.Add(newPosition);
                    newXPosition += spacing;
                }
            }
            newXPosition = standPoint.position.x;
            newZPosition -= spacing;
        }
        reached = true;
    }

    private void MinionLindingUp() 
    {
        for (int i = 0; i < finishedMinions.Length; i++)
        {
            SmoothTransform(finishedMinions[i].transform.position, finishedMinions[i].transform.position, positions[i], 3f);
        }
        reached = false;
    }

    private void RandomMinionPosition(GameObject[] minions, Vector3 newPosition)
    {        
        int randomIndex;  
        bool contained;    
        do
        {
            randomIndex = Random.Range(0, finishedMinions.Length);
            contained = selectedIndex.Contains(randomIndex);
            if (!contained)
            {
                selectedIndex.Add(randomIndex);
            }
        }
        while (contained);

        Vector3 minionNewPosition = new Vector3(newPosition.x, minions[randomIndex].transform.position.y, newPosition.z);
        minions[randomIndex].transform.position = minionNewPosition;
    }

    public void SmoothTransform(Vector3 minionPosition, Vector3 startPosition, Vector3 endPosition, float desiredDuration) 
    {
        float elapsedTime = 0f;
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / desiredDuration;
        Vector3 minionNewPosition = new Vector3(endPosition.x, minionPosition.y, endPosition.z);
        minionPosition = Vector3.Lerp(startPosition, minionNewPosition, percentageComplete);
    }
}
