using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class HealthPanel : MonoBehaviour
{
    public GameObject healthPrefab;
    
    public List<GameObject> _healthElements;

    private void Init()
    {
        _healthElements = new List<GameObject>();
    }

    public void Initialize(int showHeartCount)
    {
        Init();

        for (int i = 0; i < showHeartCount; i++)
        {
            var instantiatedHealthUIElement = Instantiate(healthPrefab, transform);
            _healthElements.Add(instantiatedHealthUIElement);
        }
    }

    public void UpdateView(int showHeartCount)
    {
        var availableHeartCount = _healthElements.Count;

        if (availableHeartCount < showHeartCount)
        {
            Debug.LogError("You are making a mistake!");
            return;
        }

        HideAllHearts();
        for (var i = 0; i < showHeartCount; i++)
        { 
            _healthElements[i].gameObject.SetActive(true);
        }

    }

    private void HideAllHearts()
    {
        foreach (var healthElement in _healthElements)
        {
            healthElement.gameObject.SetActive(false);
        }
    }
}