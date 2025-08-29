using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ARiskyGame.Types;
using System.Linq;
public class LevelStateController : MonoBehaviour
{
    int Depth;
    public List<Node> Nodes;
    public Node InitialNode;
    public Node LastNode;
    public GameObject PlayerObj;
    public Player Player;
    Node ActiveNode;
    public ExpeditionController ExpeditionController;
    public void InitializeLevel(int depth)
    {
            Depth = depth;
    }
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
        if (node.State != NodeState.CanTravel) 
        {
            return;
        }
        ResetNodes();
        PlayerObj.transform.position = node.transform.position;
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
        PlayerObj = GameObject.Find("Player");
        Player = PlayerObj.GetComponent<Player>();
        InitiateMap();
    }

    void InitiateMap()
    {
        Nodes = FindObjectsByType<Node>(FindObjectsSortMode.InstanceID).ToList();
        Inject();
    }
    void Start()
    {
        GetExpeditionController();
        SetActiveNode(InitialNode);
    }

    void GetExpeditionController()
    {
        ExpeditionController = GameObject.Find("ExpeditionController").GetComponent<ExpeditionController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
