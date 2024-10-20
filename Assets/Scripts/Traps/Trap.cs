using UnityEngine;

public class Trap : MonoBehaviour
{

    [SerializeField] protected int spawnChance;
    

    protected virtual void Start()
    {
        if(spawnChance < Random.Range(0, 100))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null && !GameManager.instance.TookDamage)
        {
            GameManager.instance.TookDamage = true;
            Player player = collision.GetComponent<Player>();
            player.Damage();
        }
    }
}
