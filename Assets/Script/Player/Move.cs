using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;

    Vector2 inputVec;
    float speed = 5;

    public GameObject myObject;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            spriteRenderer.flipX = false;
            OffBool();
            animator.SetBool("isRightMove", true);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            OffBool();
            animator.SetBool("isBackMove", true);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            spriteRenderer.flipX = true;
            OffBool();
            animator.SetBool("isRightMove", true);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            OffBool();
            animator.SetBool("isFrontMove", true);
        }
        else if (inputVec.x == 0 && inputVec.y == 0)
        {
            OffBool();
        }
    }

    private void FixedUpdate()
    {
        // 키입력을 벡터에 저장
        inputVec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 nextVec = inputVec.normalized * speed * Time.deltaTime;

        // 입력받은 벡터만큼 이동
        rigid.MovePosition(this.rigid.position + nextVec);
    }

    void OffBool()
    {
        animator.SetBool("isFrontMove", false);
        animator.SetBool("isBackMove", false);
        animator.SetBool("isRightMove", false);
    }
}
