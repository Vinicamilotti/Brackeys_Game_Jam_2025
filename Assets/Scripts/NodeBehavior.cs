using UnityEngine;
using System.Collections.Generic;
using ARiskyGame.Types;
public class Node : MonoBehaviour
{
    public ExpeditionStateController ExpeditionState;
    public NodeState State = NodeState.Inactive;
    bool visited;

    public SpriteRenderer sprite;
    public List<Node> connectedNodes = new();

    public void SetState(NodeState state)
    {
        switch (state)
        {
            case NodeState.Active:
                sprite.color = Color.black;
                break;
            case NodeState.CanTravel:
                sprite.color = Color.yellow;
                break;
            case NodeState.Inactive:
                sprite.color = Color.white;
                break;
               
        }
    }
    public void TravelHere()
    {
        if (State != NodeState.CanTravel)
        {
            return;
        }

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
