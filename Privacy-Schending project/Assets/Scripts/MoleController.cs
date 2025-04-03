using UnityEngine;
using System.Collections;

public class MoleController : MonoBehaviour
{
    public float timeVisible = 2.5f;
    public float timeHidden = 7f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

        StartCoroutine(MoleRoutine());
    }

    public IEnumerator MoleRoutine()
    {
        while (true)
        {
            // Shows the mole
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(timeVisible);

            // Hides the mole
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(timeHidden);
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Mole Whacked!");
        spriteRenderer.enabled = false;
    }
}