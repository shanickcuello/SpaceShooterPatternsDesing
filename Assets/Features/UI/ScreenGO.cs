using System.Collections;
using System.Collections.Generic;
using Features.Rounds;
using Features.UI;
using UnityEngine;

public class ScreenGO : IScreen
{
    Dictionary<Behaviour, bool> _before = new Dictionary<Behaviour, bool>();

    public Transform root;

    public ScreenGO(Transform root)
    {
        this.root = root;
    }
    
    public void Activate()
    {
        foreach (var keyValue in _before)
        {
            keyValue.Key.enabled = keyValue.Value;
        }
        root.GetComponentInChildren<RoundManager>().ContinueSpawn();
        _before.Clear();
    }

    public void Deactivate()
    {
        root.GetComponentInChildren<RoundManager>().StopSpawn();
        foreach (var b in root.GetComponentsInChildren<Behaviour>())
        {            
            _before[b] = b.enabled;
            b.enabled = false;
        }
    }

    public string Free()
    {
        GameObject.Destroy(root.gameObject);
        return "Deletie una pantalla jugable";
    }
}
