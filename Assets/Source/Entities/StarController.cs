using UnityEngine;

public class StarController : MonoBehaviour
{
    private float currentAnimationState;

    private float currentScale = 1f;

    public GameObject EnemyPrefab;

    private bool growing = true;

    public GameObject StarCollectionParticleSystem;

    private void Start()
    {
    }

    private void Update()
    {
        currentAnimationState += Time.deltaTime;
        currentScale = growing
            ? Mathf.Lerp(0.75f, 1.35f, currentAnimationState)
            : Mathf.Lerp(1.35f, 0.75f, currentAnimationState);

        if (currentAnimationState >= 1f)
        {
            currentAnimationState = 0;
            growing = !growing;
        }

        transform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag.ToLower() == "player")
        {
            StarCollectionParticleSystem.transform.position = transform.position;
            StarCollectionParticleSystem.GetComponent<ParticleSystem>().Stop();
            StarCollectionParticleSystem.GetComponent<ParticleSystem>().Play();

            var screenBounds = Utility.GetScreenSpaceBounds();
            transform.position = new Vector3(Random.Range(screenBounds.x, screenBounds.x + screenBounds.width),
                Random.Range(screenBounds.y, screenBounds.y + screenBounds.height), 1);
        }
    }
}