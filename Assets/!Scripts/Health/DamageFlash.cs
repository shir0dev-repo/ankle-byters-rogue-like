using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public Health health;
    private SpriteRenderer spriteRenderer;
    private Color origColor;
    public float flashTime = 0.15f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        origColor = spriteRenderer.color; 

        if (health != null)
        {
            health.OnHealthChanged += HandleHealthChanged;
        }
    }
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        FlashStart();
    //    }
    //}
    private void OnDestroy()
    {
        if (health != null)
        {
            health.OnHealthChanged -= HandleHealthChanged;
        }
    }

    private void HandleHealthChanged(int currentHealth)
    {
        FlashStart();
    }

    public void FlashStart()
    {
        Debug.Log("FlashStart triggered.");
        spriteRenderer.color = Color.red; 
        Invoke("FlashStop", flashTime);
    }

    public void FlashStop()
    {
        spriteRenderer.color = origColor; 
    }
}
