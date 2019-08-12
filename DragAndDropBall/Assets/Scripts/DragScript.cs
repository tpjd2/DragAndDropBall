using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : MonoBehaviour
{
    float deltaX, deltaY;
    bool movimentoHabilitado;
    Rigidbody2D rb2d;
    Collider2D collider;
    PhysicsMaterial2D material;
    Touch toque;
    Vector2 posicaoToque;

    // Start is called before the first frame update
    void Start()
    {
        movimentoHabilitado = false;
        rb2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        material = collider.sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            toque = Input.GetTouch(0);
            posicaoToque = Camera.main.ScreenToWorldPoint(toque.position);

            switch (toque.phase)
            {
                case TouchPhase.Began:
                    if (collider == Physics2D.OverlapPoint(posicaoToque))
                    {
                        deltaX = posicaoToque.x - transform.position.x;
                        deltaY = posicaoToque.y - transform.position.y;

                        movimentoHabilitado = true;

                        rb2d.freezeRotation = true;
                        rb2d.velocity = Vector2.zero;
                        rb2d.gravityScale = 0f;
                        collider.sharedMaterial = null;
                    }
                    break;
                case TouchPhase.Moved:
                    if (collider == Physics2D.OverlapPoint(posicaoToque) && movimentoHabilitado)
                    {
                        // CALCULA A NOVA POSICAO DO PLAYER
                        // BASEADA NA POSICAO DO TOQUE,
                        // RESPEITANDO A DIFERENCA (OFFSET) DA POSICAO DO TOQUE PARA O PLAYER
                        Vector2 v = new Vector2(posicaoToque.x - deltaX, posicaoToque.y - deltaY);
                        rb2d.MovePosition(v);
                    }
                    break;
                case TouchPhase.Ended:
                    movimentoHabilitado = false;
                    rb2d.freezeRotation = false;
                    rb2d.gravityScale = 2f;
                    collider.sharedMaterial = material;
                    break;
            }
        }
    }
}
