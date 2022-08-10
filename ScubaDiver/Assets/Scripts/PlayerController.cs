using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float moveSpeed = 10;
    public float jumpForce;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask jumpableSubmarine;
    private BoxCollider2D _collider;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image powerShot;
    
    //---------------------Audio Clips--------------------
    [SerializeField] private AudioClip jumpClip;


    private TrashCollect _collect;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _collect = GetComponent<TrashCollect>();
        powerShot.fillAmount = 0;
    }

    // private void Update()
    // {
    //     // if (GameManager.Singleton.gameOver)
    //     // {
    //     //     this.enabled = false;
    //     // }
    //
    //
    //     if (transform.position.x > 8.5)
    //     {
    //         transform.position = new Vector2(8.5f, transform.position.y);
    //     }
    //     else if (transform.position.x < -8.5)
    //     {
    //         transform.position = new Vector2(-8.5f, transform.position.y);
    //     }
    //
    // }

    private float _powerCharge;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _powerCharge += .25f * Time.deltaTime;
            powerShot.fillAmount = _powerCharge;
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Debug.Log($"Shot Power {_powerCharge}");
            _collect.ThrowTrash(_powerCharge);
            _powerCharge = 0;
            powerShot.fillAmount = _powerCharge;
            // shoot the trash out through projectile
        }
        // Debug.Log($" hold {Input.GetMouseButton(0)}");
        // Debug.Log($" released {Input.GetMouseButtonUp(0)}");
    }

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var jumpKey = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W);

        var jForce = _rb.velocity.y;
        if (IsGrounded() && jumpKey)
        {
            GameManager.Singleton.PlayAudio(jumpClip);
            jForce = jumpForce;
        }

        _rb.velocity = new Vector2(horizontal * moveSpeed, jForce);
    }

    private bool IsGrounded()
    {
        var etraHeight = .2f;
        var bounds = _collider.bounds;
        var rayCast = Physics2D.Raycast(bounds.center, Vector2.down, bounds.extents.y + etraHeight, jumpableGround);
        return rayCast.collider != null;
    }
}