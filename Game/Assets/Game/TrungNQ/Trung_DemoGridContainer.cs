using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trung_DemoGridContainer : MonoBehaviour
{
    public static NN.PathFinding.Grid mainGrid;
    public static Trung_DemoHeroes[] heroes = new Trung_DemoHeroes[7 * 18];

    public static void OutOfIndex(int x, int y)
    {
        heroes[x * 7 + y] = null;
    }

    public static void OccupyIndex(int x, int y, Trung_DemoHeroes hero)
    {
        heroes[x * 7 + y] = hero;
    }

    public static bool IsOccupied(int x, int y)
    {
        return heroes[x * 7 + y] != null;
    }
    public static Trung_DemoHeroes GetHero(int x, int y)
    {
        return heroes[x * 7 + y];
    }

    public static Trung_DemoHeroes GetMergeableHero(Trung_DemoHeroes hero)
    {
        MergeLogic logic = new MergeLogic();
        for (int i = 0; i < heroes.Length; i++)
        {
            if (heroes[i] != null && heroes[i] != hero)
            {
                if (logic.CheckMerge(hero.GetAttribute(), heroes[i].GetAttribute())) return heroes[i];
            }
        }
        return null;
    }
}
