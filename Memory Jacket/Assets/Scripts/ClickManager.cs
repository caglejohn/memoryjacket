using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ClickManager : MonoBehaviour
{
    float moveSpeed = 3.5f, moveAccuracy = 0.15f;
    public Transform character;
    public void GoToItem(ItemData item)
    {
        StartCoroutine(MoveToPoint(item.goToPoint.position));
        TryGettingItem(item);
    }

    public IEnumerator MoveToPoint(Vector2 point)
    {
        Vector2 positionDifference = point - (Vector2)character.position; // Set Direction
        while(positionDifference.magnitude > moveAccuracy) // Stop when we're close to the point (accuracy)
        {
            character.Translate(moveSpeed * positionDifference.normalized * Time.deltaTime); // Move in direction frame after frame
            positionDifference = point - (Vector2)character.position;
            yield return null;
        }
        
        character.position = point;
        yield return null;
    }

    private void TryGettingItem(ItemData item)
    {
        if (item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID))
        {
            GameManager.collectedItems.Add(item.itemID);
            Debug.Log("item collected");
        }
    }
}
