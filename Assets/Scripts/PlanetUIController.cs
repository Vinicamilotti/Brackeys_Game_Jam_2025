using UnityEngine;

public class PlanetUIController : MonoBehaviour
{
    bool initialized = false;
    public TMPro.TextMeshProUGUI WeekText;
    public TMPro.TextMeshProUGUI FuelGenerationText;
    public TMPro.TextMeshProUGUI FoodText;
    public TMPro.TextMeshProUGUI SparePartsText;
    public TMPro.TextMeshProUGUI QntAutomaticFarmText;
    public Planet planet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Initialize(Planet planet)
    {
        this.planet = planet;
        initialized = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(!initialized)
        {
            return;
        }
        WeekText.SetText(planet.week.ToString());
        FuelGenerationText.SetText(planet.fuelGeneration.ToString());
        FoodText.SetText(planet.food.ToString());
        SparePartsText.SetText(planet.spareParts.ToString());
        QntAutomaticFarmText.SetText(planet.qntAutmaticFarm.ToString());
    }
}
