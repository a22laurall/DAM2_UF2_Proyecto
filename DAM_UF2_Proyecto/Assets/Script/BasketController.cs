using UnityEngine;
using System.Collections;

public class BasketController : MonoBehaviour
{
    public GameObject[] fruitsObj;
    [SerializeField] int speed;
    private float limitXLeft;
    private float limitXRight;
    static public Vector2 basketXPosition; //posicion actual de la canasta
    private int actualFruit;
    private Rigidbody2D currentFruitRb;
    private GameObject currentFruit;
    public static BasketController Instance;  // Instancia estática para el Singleton
    private bool isThrowed = false;
    public AudioSource audioSource;
    public AudioClip caidaSoundClip;
    public KeyCode keyToPress = KeyCode.Space;

    void Awake()
    {
        // Establece la instancia estática del Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);  // Asegura que solo haya una instancia
        }
    }


    void Start()
    {
        GameObject leftLimit = GameObject.Find("LeftLimit");
        GameObject rightLimit = GameObject.Find("RightLimit");

        BoxCollider2D leftCollider = leftLimit.GetComponent<BoxCollider2D>();
        BoxCollider2D rightCollider = rightLimit.GetComponent<BoxCollider2D>();

        // se calculan los bordes reales de los rectángulos gracias a boxcollider
        limitXLeft = leftCollider.bounds.max.x;   //se asigna el borde derecho del rectángulo izquierdo al límite izquierdo
        limitXRight = rightCollider.bounds.min.x; //se asigna el borde izquierdo del rectángulo derecho al límite derecho

        createFruit();
    }

    void Update()
    {

        if (Input.GetKey("left") && transform.position.x > limitXLeft)
        {
            transform.position += Vector3.left * speed * Time.deltaTime; //Vector.left mueve canasta izq, Time.deltaTime independiente fps (= v)

            if (!isThrowed)
            {
                currentFruit.transform.position = transform.position;
            }


        }

        if (Input.GetKey("right") && transform.position.x < limitXRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (!isThrowed)
            {
                currentFruit.transform.position = transform.position;
            }

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentFruitRb != null)
            {
                currentFruitRb.gravityScale = 1;
                currentFruitRb.AddForce(Vector2.down * 8f, ForceMode2D.Impulse);

                audioSource.PlayOneShot(caidaSoundClip); // Reproduce el sonido

            }

            isThrowed = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (isThrowed)
            {
                createFruit();

            }
        }

        basketXPosition = transform.position; //posición del objeto al que está adjunto el script
    }




    public void createFruit()
    {
        actualFruit = Random.Range(0, fruitsObj.Length);
        currentFruit = Instantiate(fruitsObj[actualFruit], transform.position, Quaternion.identity, null); //crea un objeto random del array 

        currentFruitRb = currentFruit.GetComponent<Rigidbody2D>();
        currentFruitRb.gravityScale = 0;
        currentFruit.transform.rotation = Quaternion.identity; // Esto asegura que la rotación sea cero
        isThrowed = false;

    }

}
