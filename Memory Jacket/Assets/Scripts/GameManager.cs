using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<int> collectedItems = new List<int>();
    static float moveSpeed = 3.5f, moveAccuracy = 0.15f;

    public IEnumerator MoveToPoint(Transform MyObject, Vector2 point)
    {
        Vector2 positionDifference = point - (Vector2)MyObject.position; // Set Direction
        while (positionDifference.magnitude > moveAccuracy) // Stop when we're close to the point (accuracy)
        {
            MyObject.Translate(moveSpeed * positionDifference.normalized * Time.deltaTime); // Move in direction frame after frame
            positionDifference = point - (Vector2)MyObject.position;
            yield return null;
        }

        MyObject.position = point;
        if (MyObject == FindObjectOfType<ClickManager>().character)
            FindObjectOfType<ClickManager>().playerWalking  = false;
        yield return null;
    }
}
