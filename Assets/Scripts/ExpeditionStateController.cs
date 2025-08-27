using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ARiskyGame.Types;
using System.Linq;
public class ExpeditionStateController : MonoBehaviour
{
    public Node InitialNode;
    public List<Node> Nodes;
    static Node ActiveNode;
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    public void ResetNodes()
    {
        foreach (var node in Nodes)
        {
            node.SetState(NodeState.Inactive);
        }
    }

    public void SetActiveNode(Node node)
    {
        ResetNodes();
        node.SetState(NodeState.Active);
        ActiveNode = node;
        foreach (var connected in node.connectedNodes)
        {
            connected.SetState(NodeState.CanTravel);
        }
    }
    void Inject()
    {
        foreach (var node in Nodes)
        {
            node.ExpeditionState = this;
        }
    }
    void Awake()
    {
        Nodes = FindObjectsByType<Node>(FindObjectsSortMode.InstanceID).ToList();
        Inject();
    }
    void Start()
    {
        SetActiveNode(InitialNode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
