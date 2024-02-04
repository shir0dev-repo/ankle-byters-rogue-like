using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInsanity : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite[] sprites;
    [SerializeField] float driftSpeed, runSpeed, driftRadius;
    SpriteRenderer currentSprite;

    bool isDrifting;
    private void OnEnable()
    {
        InsanityManager.OnInsanityChanged += GhostBehaviour;
        currentSprite = GetComponentInChildren<SpriteRenderer>();
        isDrifting = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrifting)
        {
            DriftToPlayer();
        }

    }

    void GhostBehaviour(int insanity)
    {
        Debug.Log("insanity: " + insanity);
        if (insanity > 10 && insanity <= 50)
        {
            currentSprite.sprite = sprites[1];
        }
        else if (insanity > 10 && insanity < 100)
        {
            isDrifting = true;
        }
        else
        {

        }
    }

    void DriftToPlayer()
    {
        Vector3 playerPosition = GameManager.Instance.PlayerManager.GetPlayerPosition();
        if (Vector3.Distance(gameObject.transform.position, playerPosition) < driftRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, driftSpeed * Time.deltaTime);
        }
    }
}
