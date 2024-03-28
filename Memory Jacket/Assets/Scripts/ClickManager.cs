using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class ClickManager : MonoBehaviour
{
    public bool playerWalking;
    public Transform character;
    public Transform player;
     Camera myCamera;
    GameManager gameManager;

    Coroutine goToClickCoroutine;
    float goToClickMaxY = 4.71f;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        myCamera = GetComponent<Camera>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            goToClickCoroutine = StartCoroutine(GoToClick(Input.mousePosition));
        }
    }

public IEnumerator GoToClick(Vector2 mousePos)
{
    yield return new WaitForSeconds(0.05f);

    Vector2 targetPos = myCamera.ScreenToWorldPoint(mousePos);
    if (targetPos.y > goToClickMaxY || playerWalking)
    {
        yield break;
    }
    //gameManager.UpdateHintBox(null, false);   (Not Yet Implemented)
    playerWalking = true;
    StartCoroutine(gameManager.MoveToPoint(player,targetPos));
    //player.GetComponent<SpriteAnimator>().PlayAnimation(gameManager.playerAnimations[1]);
    StartCoroutine(CleanAfterClick());
    yield return null;
}

private IEnumerator CleanAfterClick()
    {
        while (playerWalking)
            yield return new WaitForSeconds(0.05f);
        //player.GetComponent<SpriteAnimator>().PlayAnimation(null);
        yield return null;
    }

    public void GoToItem(ItemData item)
    {
        StartCoroutine(gameManager.MoveToPoint(character, item.goToPoint.position));
        playerWalking = true;
        TryGettingItem(item);
    }

    private void TryGettingItem(ItemData item)
    {
        bool canGetItem = item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID);
        if (canGetItem)
        {
            GameManager.collectedItems.Add(item.itemID);
        }
         StartCoroutine(updateSceneAfterAction(item, canGetItem));
    }

    private IEnumerator updateSceneAfterAction(ItemData item, bool canGetItem)
    {
        //Prevent goToClick if going to Item
        yield return null;
        if(goToClickCoroutine != null){
            StopCoroutine(goToClickCoroutine);
        }
        while (playerWalking) //wait for player to reach the target
        {
            yield return new WaitForSeconds(1.5f);
        }
        if(canGetItem)
        {
            foreach (GameObject g in item.objectsToRemove)
            Destroy(g);
        Debug.Log("item collected");
        }

    }
}
