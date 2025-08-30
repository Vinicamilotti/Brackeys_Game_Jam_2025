using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Planet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int week;
    public int fuelGeneration;
    public int food;
    public int spareParts;
    public int qntAutmaticFarm;
    public int combatPower;
    // Update is called once per frame
    void Update()
    {

    }

    public void BuyFuelGeneration()
    {
        if (spareParts >= 10)
        {
            fuelGeneration += 5;
            spareParts -= 10;
        }
    }
    public void BuyCombatPower()
    {
        if (spareParts >= 20)
        {
            combatPower += 1;
            spareParts -= 15;
        }
    }
    public void BuyBeatGame()
    {
        if (spareParts >= 200)
        {
            SceneManager.LoadScene("Win");
        }
    }
    public void BuyAutomaticFarm()
    {
        if (spareParts >= 20)
        {
            qntAutmaticFarm += 1;
            spareParts -= 20;
        }
    }
    public void StartExpedition()
    {

        food -= 10 + week - qntAutmaticFarm;
        week += 1;
        SceneManager.LoadScene("Expedition");
    }
    public void EndExpedition(int CollectedFood, int CollectedSpareParts)
    {
        food += CollectedFood;
        spareParts += CollectedSpareParts;
        if (food < 0)
        {
            SceneManager.LoadScene("GameOver");
            ResetPlanet();
            return;
        }

        SceneManager.LoadScene("Planet");
        

    }

    public void ResetPlanet()
    {
        week = 0;
        fuelGeneration = 10;
        food = 0;
        spareParts = 0;
        qntAutmaticFarm = 0;
    }
}
