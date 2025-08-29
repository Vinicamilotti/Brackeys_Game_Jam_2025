using ARiskyGame.Types;
using Mono.Cecil;
using UnityEngine;

public class ResourceRoll
{
    public ARiskyGame.Types.ResourceType ResourceType;
    public string Title;
    public string Description;
    public int Min, Max;

}
public class ResourceEncounter : ExpeditionEvent
{
    ResourceRoll Food()
    {
        
        int type = Random.Range(0, 3);

        if (type == 0)
        {
            return new ResourceRoll
            {
                ResourceType = ARiskyGame.Types.ResourceType.Food,
                Title = "You found Food!",
                Description = "You found a bunch of Spicy Fruits",
                Min = 1,
                Max = 3
            };
        }
        if (type == 1)
        {
            return new ResourceRoll
            {
                ResourceType = ARiskyGame.Types.ResourceType.Food,
                Title = "You found Food!",
                Description = "You found a herd of alien cows",
                Min = 2,
                Max = 4
            };
        }
        return new ResourceRoll
        {
            ResourceType = ARiskyGame.Types.ResourceType.Food,
            Title = "You Found Food!",
            Description = "You abducted alien cows from an alien farmer — now the locals think he’s crazy.",
            Min = 3,
            Max = 5,

        };
    }
    public override void Init()
    {
        // Determine resource type based on random value
        ARiskyGame.Types.ResourceType resourceType = (ARiskyGame.Types.ResourceType)Random.Range(0, 3);

        ResourceRoll rollResult = resourceType switch
        {
            ARiskyGame.Types.ResourceType.Food => Food(),
            ARiskyGame.Types.ResourceType.SpareParts => new ResourceRoll
            {
                ResourceType = ARiskyGame.Types.ResourceType.SpareParts,
                Title = "You found Spare Parts!",
                Description = "You found a deactivated industrial planet",
                Min = 2,
                Max = 4
            },
            ARiskyGame.Types.ResourceType.Fuel => new ResourceRoll
            {
                ResourceType = ARiskyGame.Types.ResourceType.Fuel,
                Title = "You found Fuel",
                Description = "There is a fuel station on this planet",
                Min = 2,
                Max = 2,
            },
            _ => Food(),
        };

        var depth = (int)Expedition.ExpeditionController.GalaxyDepth;



        var amount = Random.Range(rollResult.Min, rollResult.Max + 1);

        if(rollResult.ResourceType == ARiskyGame.Types.ResourceType.Fuel)
        {
            amount += depth; // Fuel scales with depth
        }
    

        Title = rollResult.Title;
        Steps.Enqueue(new()
        {
            Description = $"{rollResult.Description}\n\nYou gain {amount}x {resourceType}",
            StepAction = new()
            {
                new()
                {
                    Description = "Take",
                    StepAction = () =>
                    {
                        Expedition.ExpeditionController.AddLoot(resourceType, amount);
                        End();
                    }
                }
            }
        });
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
}
