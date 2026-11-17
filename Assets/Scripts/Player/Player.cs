using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public PlayerStats stats;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int coinsNeeded;
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
            rb.AddForce(Vector3.up * stats.jumpForce, ForceMode.Impulse);
            audioSource.PlayOneShot(jumpSound);
        }
    }

    void OnFire()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + stats.fireCooldown;
            Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            audioSource.PlayOneShot(shootSound);
        }
    }

    void Update()
    {
        onGround = Physics.Raycast(transform.position + Vector3.up * stats.groundCheckHeight, Vector3.down, stats.groundCheckDistance);

        anim.SetFloat("Speed", move.magnitude);
        anim.SetBool("OnGround", onGround);

        if (transform.position.y < stats.fallLimit)
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

        rb.linearVelocity = new Vector3(direction.x * stats.moveSpeed, rb.linearVelocity.y, direction.z * stats.moveSpeed);

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public void AddCoin(int amount)
    {
        coins += amount;
        coinText.text = coins + " / " + coinsNeeded;
        audioSource.PlayOneShot(coinSound);
    }
}
