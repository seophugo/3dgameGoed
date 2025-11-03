using UnityEngine;
using TMPro;

public class EnemyCounterTMP : MonoBehaviour
{
    [Header("Instellingen")]
    [Tooltip("Selecteer hier de layer waar je vijanden op zitten")]
    public LayerMask enemyLayer;

    [Header("UI Element")]
    public TextMeshProUGUI enemyCountText;

    [Header("Instellingen")]
    public float checkRadius = 1000f; // hoe groot het gebied is waarin hij zoekt

    private void Update()
    {
        // Zoek alle vijanden binnen de opgegeven radius
        Collider[] enemies = Physics.OverlapSphere(Vector3.zero, checkRadius, enemyLayer);

        // Update de tekst
        enemyCountText.text = "Enemies left: " + enemies.Length;
    }
}
