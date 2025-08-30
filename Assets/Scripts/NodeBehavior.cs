using UnityEngine;
using System.Collections.Generic;
using ARiskyGame.Types;
public class Node : MonoBehaviour
{
    public Sprite ActiveSprite;
    public Sprite InactiveSprite;
    public LevelStateController ExpeditionState;
    public NodeState State = NodeState.Inactive;
    public bool visited = false;

    public SpriteRenderer sprite;
    public List<Node> connectedNodes = new();

    public void SetState(NodeState state)
    {
        State = state;
        SetSprite();
   }

    private void SetSprite()
    {
        try
        {

            if (State != NodeState.Inactive)
            {
                sprite.sprite = ActiveSprite;
                return;
            }
            sprite.sprite = InactiveSprite;

        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error setting sprite: {ex.Message}");
        }
 
    }
 
    public void TravelHere()
    {
        ExpeditionState.SetActiveNode(this);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
