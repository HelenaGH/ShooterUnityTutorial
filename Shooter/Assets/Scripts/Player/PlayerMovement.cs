using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float brzina = 6f;
    Vector3 kretanje;
    Animator anim;
    Rigidbody igracevRigidbody;
    int podnaMaska;
    float camRayLength = 100f;

    void Awake()
    {
        podnaMaska = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        igracevRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        kretanje.Set(h, 0f, v);
        kretanje = kretanje.normalized * brzina * Time.deltaTime;
        igracevRigidbody.MovePosition(transform.position + kretanje);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit podPogodak;

        if (Physics.Raycast(camRay, out podPogodak, camRayLength, podnaMaska))
        {
            Vector3 odIgracaDoMisa = podPogodak.point - transform.position;
            odIgracaDoMisa.y = 0f;
            Quaternion novaRotacija = Quaternion.LookRotation(odIgracaDoMisa);
            igracevRigidbody.MoveRotation(novaRotacija);
        }
    }

    void Animating(float h, float v)
    {
        bool hoda = h != 0f || v != 0f;
        anim.SetBool("DaHoda", hoda);
    }
   
}
