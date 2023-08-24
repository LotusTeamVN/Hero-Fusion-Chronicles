using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trung_DemoHeroes : MonoBehaviour, IMergeableHero
{
    private Trung_DemoHeroCombatAttribute attribute;
    [Header("Color")]
    [SerializeField] private Color dong;
    [SerializeField] private Color bac;
    [SerializeField] private Color vang;
    [SerializeField] private Color kimcuong;

    [SerializeField] private Color type1;
    [SerializeField] private Color type2;
    [SerializeField] private Color type3;
    [SerializeField] private Color type4;
    [SerializeField] private Color type5;
    [SerializeField] private Color type6;

    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private MeshRenderer cubeRenderer;

    private Vector3 mousePosition;
    private NN.PathFinding.Grid mainGrid;
    private int CurrentSlotX;
    private int CurrentSlotY;

    private float LastClick = -999;

    public Trung_DemoHeroCombatAttribute GetAttribute()
    {
        return attribute;
    }

    public void SetAttribute(Trung_DemoHeroCombatAttribute newAttribute)
    {
        attribute = newAttribute;
        UpdateStarUI();
        UpdateClassUI();
    }

    #region Drag Drop
    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        CurrentSlotX = (int)transform.position.x;
        CurrentSlotY = (int)transform.position.z;
        mousePosition = Input.mousePosition - GetMousePos();

        if (Time.time - LastClick < 0.5f)
        {
            if (Trung_DemoGridContainer.GetMergeableHero(this) != null)
            {
                Merge(Trung_DemoGridContainer.GetMergeableHero(this), new MergeLogic());
            }
            LastClick = -999;
        }
        else
        {
            LastClick = Time.time;
        }
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);

        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
    }
    private void OnMouseUp()
    {
        try
        {

            if (mainGrid == null) mainGrid = Trung_DemoGridContainer.mainGrid;
            NN.PathFinding.Node currentNode = mainGrid.GetNode(transform.position);
            if (CurrentSlotX == currentNode.GridLocalPosX && CurrentSlotY == currentNode.GridLocalPosY)
                return;

            if (Trung_DemoGridContainer.IsOccupied(currentNode.GridLocalPosX, currentNode.GridLocalPosY))
            {
                Merge(Trung_DemoGridContainer.GetHero(currentNode.GridLocalPosX, currentNode.GridLocalPosY), new MergeLogic());

            }
            else
            {
                Trung_DemoGridContainer.OutOfIndex(CurrentSlotX, CurrentSlotY);
                Trung_DemoGridContainer.OccupyIndex(currentNode.GridLocalPosX, currentNode.GridLocalPosY, this);
                CurrentSlotX = currentNode.GridLocalPosX;
                CurrentSlotY = currentNode.GridLocalPosY;
                transform.position = new Vector3(currentNode.GridLocalPosX, 0.5f, currentNode.GridLocalPosY);
            }
        }
        catch (System.Exception e)
        {

        }
    }


    #endregion

    #region UI
    private void UpdateStarUI()
    {
        star1.gameObject.SetActive(true);
        star2.gameObject.SetActive(true);
        switch (GetAttribute().heroStar)
        {
            case 1:
                star1.color = dong;
                star2.gameObject.SetActive(false);
                break;
            case 2:
                star1.color = dong;
                star2.color = dong;
                break;

            case 3:
                star1.color = bac;
                star2.gameObject.SetActive(false);
                break;
            case 4:
                star1.color = bac;
                star2.color = bac;
                break;
            case 5:
                star1.color = vang;
                star2.gameObject.SetActive(false);
                break;
            case 6:
                star1.color = vang;
                star2.color = vang;
                break;
            case 7:
                star1.color = kimcuong;
                star2.gameObject.SetActive(false);
                break;

        }
    }
    private void UpdateClassUI()
    {
        switch (GetAttribute().heroClass)
        {
            case 1:
                cubeRenderer.material.color = type1;
                break;
            case 2:
                cubeRenderer.material.color = type2;
                break;

            case 3:
                cubeRenderer.material.color = type3;
                break;
            case 4:
                cubeRenderer.material.color = type4;
                break;
            case 5:
                cubeRenderer.material.color = type5;
                break;
            case 6:
                cubeRenderer.material.color = type6;
                break;

        }
    }
    #endregion

    #region Merge
    public void Merge(IMergeableHero other, MergeLogic logic)
    {
        MergeLogic mergeLogic = new MergeLogic();
        bool canMerge = mergeLogic.CheckMerge(attribute, other.GetAttribute());

        if (canMerge)
        {
            Trung_DemoGridContainer.OutOfIndex(CurrentSlotX, CurrentSlotY);
            other.SetAttribute(mergeLogic.Merge(attribute, other.GetAttribute()));
            Destroy(gameObject);
            return;
        }
        else
        {
            transform.position = new Vector3(CurrentSlotX, 0.5f, CurrentSlotY);
        }
    }
    #endregion
}
