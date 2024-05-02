using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static List<int> collectedItems = new List<int>();
    static float moveSpeed = 3.5f, moveAccuracy = 0.15f;

    public AnimationData[] playerAnimations;
    public RectTransform nameTag, hintBox;

    public Image blockingImage;
    
    public GameObject[] localScenes;

    int activeLocalScene = 0;

    public Transform[] playerStartPositions;

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
        nameTag.sizeDelta = item.nameTagSize;
        nameTag.localPosition = new Vector2(item.nameTagSize.x/2, -0.5f);
    }

    public void UpdateHintBox(ItemData item, bool playerFlipped)
    {
        if (item == null) 
        {
            // Hide hint Box
            hintBox.gameObject.SetActive(false);
            return;
        }
        // Show hint box
        hintBox.gameObject.SetActive(true);

        // Change name
        hintBox.GetComponentInChildren<TextMeshProUGUI>().text = item.hintMessage;
        
        hintBox.sizeDelta = item.hintBoxSize;
        
        // Move hint box
        if (playerFlipped ) 
        {
            hintBox.parent.localPosition = new Vector2(0, 0);
        }
        else
        {
            hintBox.parent.localPosition = Vector2.zero;
        }
    }

/*public void CheckSpecialConditions(ItemData item){
    case -11:
        StartCoroutine(ChangeScene(1,0));
        break;
    case -12:
        StartCoroutine(ChangeScene(2,0));
        break;
    case -1:
        StartCoroutine(ChangeScene(3,0));
        break; 
}

 public IEnumerator ChangeScene(int sceneNumber,float delay) //Not sure why public makes this break. I commented everything out so it'll work for the presentation.
    {
        yield return new WaitForSeconds(delay);

        //if end game remove player
        if (sceneNumber == localScenes.Length - 1)
        {
            FindObjectOfType<ClickManager>().player.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

        Color c = blockingImage.color;
        //screen goes black (in one second) and clicking is blocked
        blockingImage.enabled = true;
        while(blockingImage.color.a<1)
        {
            //increase color.a
            c.a += Time.deltaTime;
            yield return null;
            blockingImage.color = c;
        }

        //hide the old scene
        localScenes[activeLocalScene].SetActive(false);
        //show the new scene
        localScenes[sceneNumber].SetActive(true);
        //say which one is currently used
        activeLocalScene = sceneNumber;
        //teleport the player
        FindObjectOfType<ClickManager>().player.position = playerStartPositions[sceneNumber].position;
        //hide hint box
        UpdateHintBox(null, false);
        //hide name tag
        UpdateNameTag(null);
        //reset animations
        foreach (SpriteAnimator spriteAnimator in FindObjectsOfType<SpriteAnimator>())
            spriteAnimator.PlayAnimation(null);
        //show new scene and enable clicking
        while (blockingImage.color.a > 0)
        {
            //decrease color.a
            c.a -= Time.deltaTime;
            yield return null;
            blockingImage.color = c;
        }
        blockingImage.enabled = false;
        yield return null;
    }
*/ 
}
