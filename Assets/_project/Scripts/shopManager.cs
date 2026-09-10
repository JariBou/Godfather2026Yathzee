using _project.Scripts.Die;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class shopManager : MonoBehaviour
{
    public Dictionary<Objects, int> itemPool = new Dictionary<Objects, int>();
    public List<Objects> _itemPool = new List<Objects>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void refreshShop()
    {
        for (int i = 0; i < itemPool.Count; i++) 
        {
            
        }
    }

    public void closeShop()
    {

    }

}
