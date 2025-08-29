using ARiskyGame.Types;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EventControllerScript : MonoBehaviour
{
    public TextMeshProUGUI TxtTitle;
    public TextMeshProUGUI TxtDescription;
    public GameObject ButtonPanel;
    public GameObject ButtonPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetTitle(string title)
    {
        TxtTitle.SetText(title);
    }

    public void SetStep(EventStep step)
    {
        SetDescription(step.Description);
        foreach (Object item in ButtonPanel.transform)
        {
            Destroy(item.GameObject().gameObject);
        }
        foreach(var stepAction in step.StepAction)
        {
            var btnInstance = Instantiate(ButtonPrefab, ButtonPanel.transform);
            var btn = btnInstance.GetComponent<Button>();
            var txt = btnInstance.transform.Find("Txt").GetComponent<TextMeshProUGUI>();
            btn.onClick.AddListener(delegate { stepAction.StepAction(); });
            txt.SetText(stepAction.Description);
        }
    }
    public void SetDescription(string description)
    {
        TxtDescription.SetText(description);
    }
}
