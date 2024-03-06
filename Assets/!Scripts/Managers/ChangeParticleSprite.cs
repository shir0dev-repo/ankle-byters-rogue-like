using UnityEngine;

public class ChangeParticleSystem : MonoBehaviour
{
    public GameObject particleSystem1;
    public GameObject particleSystem2;
    public GameObject particleSystem3;
    public GameObject particleSystem4;
    public Transform playerTransform;
    public float followSpeed = 5f;

    public EnemySpawner enemySpawner;
    public bool enemiesSpawned = false;

    private void OnEnable()
    {
        InsanityManager.OnInsanityChanged += UpdateParticleSystems;
    }

    private void Start()
    {
        ActivateParticleSystem(particleSystem1);
        DeactivateParticleSystem(particleSystem2);
        DeactivateParticleSystem(particleSystem3);
        DeactivateParticleSystem(particleSystem4);
    }
    private void OnDisable()
    {
        InsanityManager.OnInsanityChanged -= UpdateParticleSystems;
    }

    private void UpdateParticleSystems(int currentInsanity)
    {
        if (currentInsanity >= 100 && !enemiesSpawned)
        {
            enemiesSpawned = true;
            Debug.LogWarning("Spawning Enemies");

            ActivateParticleSystem(particleSystem2);
            ActivateParticleSystem(particleSystem3);
            ActivateParticleSystem(particleSystem4);
            //DeactivateParticleSystem(particleSystem1);
        }
        if (currentInsanity >= 60 && currentInsanity < 100)
        {
            ActivateParticleSystem(particleSystem2);
            ActivateParticleSystem(particleSystem3);
            ActivateParticleSystem(particleSystem4);
            //DeactivateParticleSystem(particleSystem1);
        }
        if (currentInsanity < 60)
        {
            DeactivateParticleSystem(particleSystem2);
            DeactivateParticleSystem(particleSystem3);
            DeactivateParticleSystem(particleSystem4);
            ActivateParticleSystem(particleSystem1);
        }
    }

    private void ActivateParticleSystem(GameObject particleSystem)
    {
        if (particleSystem != null)
        {
            particleSystem.SetActive(true);
        }
    }

    private void DeactivateParticleSystem(GameObject particleSystem)
    {
        if (particleSystem != null)
        {
            particleSystem.SetActive(false);
        }
    }
}