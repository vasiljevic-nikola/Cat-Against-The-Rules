using UnityEngine;
using TMPro;

public class TextBounce : MonoBehaviour
{
    public float bounceDuration = 0.5f; // How long the bounce lasts
    public float bounceScale = 1.2f; // How much it grows

    private Vector3 originalScale;
    private float timer;
    private bool bouncing;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    void OnEnable()
    {
        // Triggers a bounce every time the object is activated
        bouncing = true;
        timer = 0f;
        transform.localScale = originalScale;
    }

    void Update()
    {
        if (!bouncing) return;

        timer += Time.unscaledDeltaTime; // We use unscaled so it doesn't depend on Time.timeScale
        float progress = timer / bounceDuration;

        if (progress < 0.5f)
        {
            // First half of the animation - growing
            transform.localScale = Vector3.Lerp(originalScale, originalScale * bounceScale, progress * 2);
        }
        else if (progress < 1f)
        {
            // Second half of animation - returns to normal
            transform.localScale = Vector3.Lerp(originalScale * bounceScale, originalScale, (progress - 0.5f) * 2);
        }
        else
        {
            
            transform.localScale = originalScale;
            bouncing = false;
        }
    }
}

