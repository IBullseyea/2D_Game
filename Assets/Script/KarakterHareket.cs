using UnityEngine;

public class KarakterHareket : MonoBehaviour
{
    //hareket için gerekli değişkenler
    public float hareketHizi = 5f;
    private float yatayInput;

    private bool sagaBakiyor = true;

    //zıplama için gerekli değişkenler
    public float ziplamaGucu = 10f;
    private bool yerdeMi;

    //zemin kontrolü için gerekli değişkenler
    public Transform zeminKontrolNoktasi; //zemin kontrol noktası
    public LayerMask zeminLayer; //zemin katmanı

    private Rigidbody2D rb;// Karakterin fizik bileşeni

    private Animator animator; // Karakterin animatör bileşeni
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 

        animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //sağ sol hareketi için yatay input al
        yatayInput = Input.GetAxis("Horizontal");


        //zeminde olup olmadığını kontrol et
        yerdeMi = Physics2D.OverlapCircle(zeminKontrolNoktasi.position, 0.1f, zeminLayer);

        //zıplama işlemi
        if (yerdeMi == true && Input.GetKeyDown(KeyCode.Space))
        {
            // Rigidbody'nin dikey hızını (velocity.y) anında zıplama gücüne eşitle.
            // (Mevcut yatay hızı (velocity.x) koru)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, ziplamaGucu);
        }

        animator.SetBool("isMoving", yatayInput != 0);




        Vector2 yeniHiz = new Vector2(yatayInput * hareketHizi, rb.linearVelocity.y);
        rb.linearVelocity = yeniHiz;

       


    }

    void FixedUpdate()
    {

        // HAREKETİ FİZİKSEL OLARAK UYGULA
        // Rigidbody'nin yatay hızını (velocity.x) hesapla
        // (Dikey hızı (velocity.y) koru, zıplamayı bozmasın)
        rb.linearVelocity = new Vector2(yatayInput * hareketHizi, rb.linearVelocity.y);

        if (yatayInput > 0 && !sagaBakiyor)
        {
            Flip();
        }
        else if (yatayInput < 0 && sagaBakiyor)
        {
            Flip();
        }
    }

    void Flip()
    {
        //'sagaBakiyor' değişkeninin değerini tersine çevir (true ise false, false ise true yap)
        sagaBakiyor = !sagaBakiyor;
        // Karakterin ölçeğini al
        Vector3 karakterOlcek = transform.localScale;
        // X eksenindeki ölçeği -1 ile çarparak yönünü değiştir
        karakterOlcek.x *= -1;
        // Yeni ölçeği karaktere uygula
        transform.localScale = karakterOlcek;
    }
}
