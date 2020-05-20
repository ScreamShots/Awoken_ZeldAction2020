using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script involve support of Pegase for enemies
/// </summary>

public class PegaseSupport : MonoBehaviour
{
    #region Inspector Settings
    [Header("Element in Zone")]
    [Space]
    [SerializeField]

    List<GameObject> protectedEnemies = new List<GameObject>();
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Enemy"))
        {
            GameObject detectedEnemy = collision.transform.root.gameObject;

            if (!protectedEnemies.Contains(detectedEnemy) && detectedEnemy != transform.root.gameObject)
            {
                if (detectedEnemy.GetComponent<EnemyHealthSystem>() != null)
                {
                    detectedEnemy.GetComponent<EnemyHealthSystem>().ActivatePegaseProtection();
                    protectedEnemies.Add(detectedEnemy);
                }
            }                    
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Enemy"))
        {
            GameObject outEnemy = collision.transform.root.gameObject;

            if (protectedEnemies.Contains(outEnemy) && outEnemy != transform.root.gameObject)
            {
                if(outEnemy.GetComponent<EnemyHealthSystem>() != null)
                {
                    outEnemy.GetComponent<EnemyHealthSystem>().DesactivatePegaseProtection();
                    protectedEnemies.Remove(outEnemy);
                }
            }
        }
    }

    private void OnDestroy()
    {
        foreach (GameObject enemy in protectedEnemies)
        {
            enemy.GetComponent<EnemyHealthSystem>().DesactivatePegaseProtection();
        }
    }


}

