using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    { 
        public int maxHealth = 100;
        public float startPHealth = 1f;

        private int _currentHealth;
        public int currentHealth
        {
            get { return _currentHealth; }
            set { _currentHealth = Mathf.Clamp (value, 0, maxHealth);}
        }

        public void OnServerInitialized()
        {
            currentHealth = maxHealth;
        }
    }

    public EnemyStats stats = new EnemyStats();

    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    // Start is called before the first frame update
    void Start()
    {
        stats.OnServerInitialized();

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.currentHealth, stats.maxHealth);
        }
    //    currentHealth = maxHealth;
    }


    public void TakeDamage(int damage)
    {
        stats.currentHealth -= damage;

        //hurt animation

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.currentHealth, stats.maxHealth);
        }

        if (stats.currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        Debug.Log("Enemy died!");
        //die animation

        //disable enemy
        GetComponent<Collider2D>().enabled = false;
        ((EnemyAI)gameObject.GetComponent<EnemyAI>()).enabled = false;

        StartCoroutine(Begone());

        IEnumerator Begone()
        {
            yield return new WaitForSeconds(2);
            Destroy(this.gameObject);
            this.enabled = false;
        }


    } 
}
