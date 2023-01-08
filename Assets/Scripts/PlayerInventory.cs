using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public TextMeshProUGUI seedsText;
    public TextMeshProUGUI plantsText;

    int seedsCount = 1;
    int collectedPlants = 0;

    void Start() {
        UpdatePlantsText();
        UpdateSeedsText();
    }

    public void AddSeedCount(int amount = 1) {
        seedsCount += amount;
        UpdateSeedsText();
    }
    public void RemoveSeedCount(int amount = 1) {
        seedsCount -= amount;
        UpdateSeedsText();
    }
    public void AddCollectedPlants() {
        collectedPlants++;
        UpdatePlantsText();
    }

    public int GetSeedsCount() {
        return seedsCount;
    }
    public int GetCollectedPlants() {
        return collectedPlants;
    }

    void UpdateSeedsText() {
        seedsText.SetText(seedsCount.ToString());
    }
    void UpdatePlantsText() {
        plantsText.SetText(collectedPlants.ToString());
    }
}
