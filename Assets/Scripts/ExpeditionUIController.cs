using TMPro;
using UnityEngine;

public class ExpeditionUIController : MonoBehaviour
{
    public TextMeshProUGUI FoodText;
    public TextMeshProUGUI SparePartsText;
    public TextMeshProUGUI FuelText;
    public ExpeditionController ExpeditionController;
    public TextMeshProUGUI EndText;
    public TextMeshProUGUI HealthText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FoodText.SetText(ExpeditionController.CollectedFood.ToString());
        SparePartsText.SetText(ExpeditionController.CollectedSpareParts.ToString());
        FuelText.SetText(ExpeditionController.Player.Fuel.ToString());
        HealthText.SetText(ExpeditionController.Player.Health.ToString());
        EndText.SetText($"End of Expedition ({(int)ExpeditionController.GalaxyDepth}) fuel)");
    }
}
