using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ClickManager : MonoBehaviour
{
    public bool playerWalking;
    public Transform character;
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void GoToItem(ItemData item)
    {
        StartCoroutine(gameManager.MoveToPoint(character, item.goToPoint.position));
        playerWalking = true;
        TryGettingItem(item);
        StartCoroutine(updateSceneAfterAction(item));
    }

    private void TryGettingItem(ItemData item)
    {
        if (item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID))
        {
            GameManager.collectedItems.Add(item.itemID);
        }
    }

    private IEnumerator updateSceneAfterAction(ItemData item)
    {
        while (playerWalking) //wait for player to reach the target
        {
            yield return new WaitForSeconds(1.5f);
        }
        
        foreach (GameObject g in item.objectsToRemove)
            Destroy(g);
        Debug.Log("item collected");

    }
}
