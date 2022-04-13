using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{    public float JumpForce = 10;
     public float velocity = 10;
     private Rigidbody2D _rb;
     private Animator _animator;
     private SpriteRenderer _sr;
     private static readonly string ANIMATOR_STATE = "Estado";
     private static readonly int ANIMATION_QUIETO = 3;
     private static readonly int ANIMATION_MORIR = 2;
     private static readonly int ANIMATION_JUMP = 1;
     private static readonly int ANIMATION_RUN = 0;
     private static readonly int RIGHT = 1;
     private bool colision = false;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Desplazarse(RIGHT);
        if (colision == true)
        {
            Morir();
        }
        if (Input.GetKey(KeyCode.C))
        {
            Saltar();
        }
        if (Input.GetKeyUp(KeyCode.C)) //cada vez que suelto la tecla salta
        {
            _rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse); //ayuda para el tipo de fuerza
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "zombi")
        {
            Debug.Log("Entrar en colision: "+other.gameObject.name);
            colision = true;
        }
    }

    private void Morir()
    {
        _rb.velocity = new Vector2(0, 0);
        ChangeAnimation(ANIMATION_MORIR);
    }
    private void Desplazarse(int position)
    {
        _rb.velocity = new Vector2(velocity * position, _rb.velocity.y);
        ChangeAnimation(ANIMATION_RUN);
    }

    private void Saltar()
    {
        ChangeAnimation(ANIMATION_JUMP);
    }
    private void ChangeAnimation(int animation)
    {
        _animator.SetInteger(ANIMATOR_STATE,animation);
    }

}
