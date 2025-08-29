using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ARiskyGame.Types;
using System.Linq;
using Assets.Types;
using JetBrains.Annotations;
using System.ComponentModel;
using TMPro;
public class LevelStateController : MonoBehaviour
{
    GalaxyDepth Depth;
    public List<Node> Nodes;
    public Node InitialNode;
    public Node LastNode;
    public GameObject PlayerObj;
    public Player Player;
    public GameObject DialogScreen;
    Node ActiveNode;
    public ExpeditionController ExpeditionController;
    public void InitializeLevel(GalaxyDepth depth)
    {
            Depth = depth;
            InitialNode.SetState(NodeState.CanTravel);
            SetActiveNode(InitialNode);
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
        PerformEvent();
    }

   void PerformEvent()
   {
        if(ActiveNode.visited)
        {
            return;
        }

        if(ActiveNode == InitialNode)
        {
            return;
        }


        var selectEvent = EventFactory.GetEvent();

        selectEvent.InitiateEvent(this, GameObject.Find("UI"), DialogScreen);

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
