using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtFlash : MonoBehaviour
{
    //variables
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float duration;

    //references
    private SpriteRenderer bossSpriteRenderer;
    private Material originalBossMaterial;
    private Material insFlashMaterial;
    [SerializeField] private Material flashMaterial;

    private void Awake()
    {
        bossSpriteRenderer = GetComponent<SpriteRenderer>();

        originalBossMaterial = bossSpriteRenderer.material;

        insFlashMaterial = new Material(flashMaterial);
    }

    //call to indicate that player is hurt
    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    //hurt flash specs
    private IEnumerator FlashRoutine()
    {
        insFlashMaterial.color = flashColor;

        bossSpriteRenderer.material = insFlashMaterial;

        yield return new WaitForSeconds(duration);

        bossSpriteRenderer.material = originalBossMaterial;
    }
}
