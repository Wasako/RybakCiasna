using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int enemyHealth;
    public int enemyDamageOutput;
    public int enemyO2DrainOutput;
    [SerializeField] private float damageFlashDuration;
    [SerializeField] private Sprite enemyNormalSprite;
    [SerializeField] private Sprite enemyFlashingSprite;
    [SerializeField] private GameObject deathPrefarb;

    [SerializeField]private float damageFlashTimer;

    public void TakeDamage(int weaponDamage)
    {
        damageFlashTimer = 0;
        enemyHealth -= weaponDamage;
        gameObject.GetComponent<SpriteRenderer>().sprite = enemyFlashingSprite;

        if (enemyHealth <= 0)
            EnemyDeath();
    }

    void Update()
    {
        damageFlashTimer += Time.deltaTime;
        if (damageFlashTimer >= damageFlashDuration) {
            damageFlashTimer = 0;
            gameObject.GetComponent<SpriteRenderer>().sprite = enemyNormalSprite;
        }
    }

    void EnemyDeath()
    {
        gameObject.SetActive(false);
        if (deathPrefarb != null)
        {
            Instantiate(deathPrefarb, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    //Thats just a quick system untill someone makes some attacks for the enemies
    void OnCollisionEnter2D(Collision2D target)
    {
        // todo
    }

}
