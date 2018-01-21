using UnityEngine;

public class Fruit : MonoBehaviour {
    public FruitType type;
	public GameObject fruitSlicedPrefab;
    public GameObject fruitParticlesPrefab;
	public float startForce = 15f;
    public int points = 1;
   
	Rigidbody2D rb;

    private ApplicationManager applicationManager;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.AddForce(transform.up * startForce, ForceMode2D.Impulse);
        applicationManager = ApplicationManager.instance;
    }

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Blade")
		{
			Vector3 direction = (col.transform.position - transform.position).normalized;

			Quaternion rotation = Quaternion.LookRotation(direction);

			GameObject slicedFruit = Instantiate(fruitSlicedPrefab, transform.position, rotation);
			Destroy(slicedFruit, 3f);
			Destroy(gameObject);
            InstantiateFruitParticles();
            applicationManager.SetScore(points);
		}
	}

    void InstantiateFruitParticles()
    {
        GameObject particles = Instantiate(fruitParticlesPrefab, transform.position, Quaternion.identity);
        Destroy(particles, 1f);
    }

}
