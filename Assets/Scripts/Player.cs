using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 7f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireCooldown = 0.4f;
    public int coinsNeeded = 5;
    public TextMeshProUGUI coinText;
    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip shootSound;

    public int coins;

    Rigidbody rb;
    Animator anim;
    AudioSource audioSource;
    Vector2 move;
    bool onGround;
    float nextFireTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        coinText.text = coins + " / " + coinsNeeded;
    }

    void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (onGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            audioSource.PlayOneShot(jumpSound);
        }
    }

    void OnFire()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireCooldown;
            Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            audioSource.PlayOneShot(shootSound);
        }
    }

    void Update()
    {
        onGround = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.3f);

        anim.SetFloat("Speed", move.magnitude);
        anim.SetBool("OnGround", onGround);

        if (transform.position.y < -10f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void FixedUpdate()
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0f;
        right.y = 0f;

        Vector3 direction = forward.normalized * move.y + right.normalized * move.x;

        rb.linearVelocity = new Vector3(direction.x * speed, rb.linearVelocity.y, direction.z * speed);

        if (direction.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public void AddCoin()
    {
        coins++;
        coinText.text = coins + " / " + coinsNeeded;
        audioSource.PlayOneShot(coinSound);
    }
}
