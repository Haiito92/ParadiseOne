using UnityEngine;

public class BoatStretchEffect : MonoBehaviour
{
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private float stretchAmount;
    [SerializeField] private float stretchDuration;
    [SerializeField] private int bounceCount;

    private Vector3 originalScale;
    private float stretchTimer;
    private bool isStretching;

    private void Awake()
    {
        if (spriteTransform == null)
            spriteTransform = transform;

        originalScale = spriteTransform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isStretching = true;
        stretchTimer = 0f;
    }

    private void Update()
    {
        if (!isStretching) return;

        stretchTimer += Time.deltaTime;
        float t = stretchTimer / stretchDuration;

        if (t >= 1f)
        {
            spriteTransform.localScale = originalScale;
            isStretching = false;
            return;
        }

        float bounce = Mathf.Sin(t * Mathf.PI * bounceCount) * (1f - t);

        float x = originalScale.x + stretchAmount * bounce;
        float y = originalScale.y - stretchAmount * bounce;
        spriteTransform.localScale = new Vector3(x, y, originalScale.z);
    }
}
