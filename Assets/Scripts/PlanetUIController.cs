using UnityEngine;

public class PlanetUIController : MonoBehaviour
{
    bool initialized = false;
    public TMPro.TextMeshProUGUI WeekText;
    public TMPro.TextMeshProUGUI FuelGenerationText;
    public TMPro.TextMeshProUGUI FoodText;
    public TMPro.TextMeshProUGUI SparePartsText;
    public TMPro.TextMeshProUGUI QntAutomaticFarmText;
    public TMPro.TextMeshProUGUI CombatPowerText;
    public Planet planet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        planet = PlanetSingleton.Instance;
    }

    public void OnClickStartExpedition()
    {
        planet.StartExpedition();
    }

    public void OnClickBuyAutomaticFarm()
    {
        planet.BuyAutomaticFarm();
    }
    public void OnClickBuyCombatPower()
    {
        planet.BuyCombatPower();
    }
    public void OnClickBuyFuelGeneration()
    {
        planet.BuyFuelGeneration();
    }
    public void OnClickBeatGame()
    {
        planet.BuyBeatGame();
    }

    // Update is called once per frame
    void Update()
    {

        WeekText.SetText("Week: " + planet.week.ToString());
        FuelGenerationText.SetText("Fuel generation: " + planet.fuelGeneration.ToString());
        FoodText.SetText(planet.food.ToString());
        SparePartsText.SetText(planet.spareParts.ToString());
        QntAutomaticFarmText.SetText("Automatic Farms: " + planet.qntAutmaticFarm.ToString());
        CombatPowerText.SetText(planet.combatPower.ToString());
    }
}
