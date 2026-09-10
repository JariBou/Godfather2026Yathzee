using UnityEngine;

public class finalScore : MonoBehaviour
{
    public float finalMultiplier = 1f;

    
    public float[] diceScore = { 0, 0, 0, 0, 0 };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float FinalScore()
    {
        // float score = diceScore.Sum() * finalMultiplier;
        // Objects goatSkull = null;
        //
        // foreach (Objects obj in GetComponent<inventoryBehaviour>().ObjectsInventory)
        // {
        //     if (obj is goatSkull) 
        //     {
        //         goatSkull = (goatSkull)obj;
        //         continue;
        //     }
        //     score = obj.ApplyEffect(null);
        // }
        // if (goatSkull != null) 
        // {
        //     score = goatSkull.ApplyEffect(null);
        // }
        //
        //
        // return score;
        return 0;
    }
}
