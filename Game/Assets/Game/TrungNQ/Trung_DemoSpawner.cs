using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trung_DemoSpawner : MonoBehaviour

{
    [SerializeField] private NN.PathFinding.Grid mainGrid;
    [SerializeField] private Trung_DemoHeroes CubePrefab;
    private void Awake()
    {
        Trung_DemoGridContainer.mainGrid = mainGrid;
    }
    public void SpawnRandom()
    {
        int randomX = Random.Range(0, Trung_DemoGridContainer.mainGrid.GridSizeX);
        int randomY = Random.Range(0, Trung_DemoGridContainer.mainGrid.GridSizeY);
        if (Trung_DemoGridContainer.IsOccupied(randomX, randomY)) return;


        Trung_DemoHeroes cube = Instantiate(CubePrefab, new Vector3(Trung_DemoGridContainer.mainGrid.GetNode(randomX, randomY).GridLocalPosX, 0.5f, Trung_DemoGridContainer.mainGrid.GetNode(randomX, randomY).GridLocalPosY), Quaternion.identity);
        cube.SetAttribute(new Trung_DemoHeroCombatAttribute() { heroClass = Random.Range(1, 7), heroStar = 1 });
        Trung_DemoGridContainer.OccupyIndex(randomX, randomY, cube);

    }
}

