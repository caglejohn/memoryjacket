using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static List<int> collectedItems = new List<int>();
    static float moveSpeed = 3.5f, moveAccuracy = 0.15f;
    public RectTransform nameTag;

    int activeLocalScene = 0;

    public IEnumerator MoveToPoint(Transform MyObject, Vector2 point)
    {
        //Calculate position difference
        Vector2 positionDifference = point - (Vector2)MyObject.position;
        //Flip Object
        if(MyObject.GetComponentInChildren<SpriteRenderer>() && positionDifference.x != 0)
        {
            MyObject.GetComponentInChildren<SpriteRenderer>().flipX = positionDifference.x > 0;
        }
        //Stop when near the point
        while (positionDifference.magnitude > moveAccuracy)
        {
            //Move in direction frame
            MyObject.Translate(moveSpeed * positionDifference.normalized * Time.deltaTime);
            //Recalculate position difference
            positionDifference = point - (Vector2)MyObject.position;
            yield return null;
        }
        //Snap to point
        MyObject.position = point;

        //Tell Clickmanager that the player has arrived
        if (MyObject == FindObjectOfType<ClickManager>().player || activeLocalScene == 0)
        {
            FindAnyObjectByType<ClickManager>().playerWalking = false;
        }
        yield return null;
    }

    //public IEnumerator MoveToPoint(Transform MyObject, Vector2 point)  (OLD)
    //{
      //  Vector2 positionDifference = point - (Vector2)MyObject.position; // Set Direction
        //while (positionDifference.magnitude > moveAccuracy) // Stop when we're close to the point (accuracy)
        //{
          //  MyObject.Translate(moveSpeed * positionDifference.normalized * Time.deltaTime); // Move in direction frame after frame
            //positionDifference = point - (Vector2)MyObject.position;
            //yield return null;
        //}

        //MyObject.position = point;
        //if (MyObject == FindObjectOfType<ClickManager>().character)
            //FindObjectOfType<ClickManager>().playerWalking  = false;
        //yield return null;
    //}

    public void UpdateNameTag(ItemData item)
    {
        nameTag.GetComponentInChildren<TextMeshProUGUI>().text = item.objectName;
        nameTag.sizeDelta = item.nameTageSize;
        nameTag.localPosition = new Vector2(item.nameTageSize.x/2, -0.5f);
    }
}
