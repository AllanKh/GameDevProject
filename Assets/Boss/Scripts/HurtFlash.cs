using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtFlash : MonoBehaviour
{
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration;

    private SpriteRenderer bossSpriteRenderer;
    private Material originalBossMaterial;
    private Material insFlashMaterial;

    private void Awake()
    {
        bossSpriteRenderer = GetComponent<SpriteRenderer>();

        originalBossMaterial = bossSpriteRenderer.material;

        insFlashMaterial = new Material(flashMaterial);
    }

    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        insFlashMaterial.color = flashColor;

        bossSpriteRenderer.material = insFlashMaterial;

        yield return new WaitForSeconds(duration);

        bossSpriteRenderer.material = originalBossMaterial;
    }
}
