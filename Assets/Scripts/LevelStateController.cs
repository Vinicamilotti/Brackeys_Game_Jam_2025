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
    public GameObject DialogScreen;
    Node ActiveNode;
    public ExpeditionController ExpeditionController;
    public void InitializeLevel(GalaxyDepth depth)
    {
            Depth = depth;
            InitialNode.SetState(NodeState.CanTravel);
            SetActiveNode(InitialNode);
    }
    public void ClearNodes()
    {
        foreach (var node in FindObjectsByType<Node>(FindObjectsSortMode.InstanceID))
        {
            DestroyImmediate(node.gameObject);
            if (node is not null)
            {
                DestroyImmediate(node);
            }
        }
        Nodes.Clear();
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

        if (!ExpeditionController.Travel())
        {
            return;
        }
        ;
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
        if (ActiveNode.visited)
        {
            return;
        }

        if (ActiveNode == InitialNode)
        {
            return;
        }

        ActiveNode.visited = true;
        var selectEvent = EventFactory.GetEvent();
        if (ActiveNode == LastNode)
        {
            selectEvent = ScriptableObject.CreateInstance<LastNodeEvent>();
        }

        PerformEvent(selectEvent);
    }

    public void PerformEvent(ExpeditionEvent evnt)
    {
        evnt.InitiateEvent(this, GameObject.Find("UI"), DialogScreen);
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
        GetExpeditionController();
        InitiateMap();
    }

    void InitiateMap()
    {
        Nodes = FindObjectsByType<Node>(FindObjectsSortMode.InstanceID).ToList();
        Inject();
    }
    void Start()
    {
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
