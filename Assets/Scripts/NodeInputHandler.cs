using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class NodeInputHandler : MonoBehaviour
{
    Camera MainCamera;
    public Node node;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        MainCamera = Camera.main;
    }

    // Update is called once per frame
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        
        var rayHit = Physics2D.GetRayIntersection(MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (rayHit.collider.gameObject != this.gameObject)
        {
            return;
        }
        node.TravelHere();
    }
}
