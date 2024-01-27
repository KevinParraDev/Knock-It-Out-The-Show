using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
          spriteRenderer.enabled = true;
          Vector2 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          transform.position = mouseCursorPos;
    }
}
