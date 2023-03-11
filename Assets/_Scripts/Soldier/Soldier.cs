using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Soldier : MonoBehaviour
{
    protected int isSelected;
    public int populationOccupied;
    protected Node currentNode;
    public abstract void Move();

}
