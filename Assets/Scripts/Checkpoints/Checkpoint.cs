using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private ParticleSystem particles;

    private void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CheckpointManager.CheckpointUpdateEvent.Invoke(this);
            
        }
    }

    public void PlayParticles()
    {
        particles.Play();
    }
}
