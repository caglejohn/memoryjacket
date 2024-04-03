using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public int itemID, requiredItemID;
    public Transform goToPoint;
    public GameObject[] objectsToRemove;
    public string objectName;
    public Vector2 nameTageSize = new Vector2(3,0.65f);

    [TextArea(3, 3)]
    public string hintMessage;
    public Vector2 hintBoxSize = new Vector2(4, 0.65f);
}
