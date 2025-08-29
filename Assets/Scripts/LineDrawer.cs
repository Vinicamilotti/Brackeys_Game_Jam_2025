using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public LineRenderer lineRederer;
    HashSet<string> relationHash = new HashSet<string>();

    void Awake()
    {
        lineRederer = GetComponent<LineRenderer>();
    }
    public void DrawLines(List<Node> nodes)
    {
        foreach (var node in nodes)
        {
            foreach (var connected in node.connectedNodes)
            {
                string relation = node.name + connected.name;
                string reverseRelation = connected.name + node.name;
                if (!relationHash.Contains(relation) && !relationHash.Contains(reverseRelation))
                {
                    relationHash.Add(relation);
                    lineRederer.positionCount += 2;
                    lineRederer.SetPosition(lineRederer.positionCount, node.transform.position);
                    lineRederer.SetPosition(lineRederer.positionCount, connected.transform.position);
                }
            }
        }
    }
}
