using UnityEngine;

public class ThoughtBubble : MonoBehaviour
{
    public SpriteRenderer itemToDisplay;

    public void SetItemToDisplay(Objective item)
    {
        itemToDisplay.sprite = item.objectiveSprite;
    }
}
