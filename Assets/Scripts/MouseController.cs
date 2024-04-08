using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//gesick
public class MouseController : MonoBehaviour
{

    public float jetpackForce = 75.0f;
    //added part1
    Rigidbody2D rbody2D;

    public float forwardMovementSpeed = 3.0f;

    public Transform groundCheckTransform;

    private bool grounded;

    public LayerMask groundCheckLayerMask;

    Animator animator;

    public ParticleSystem jetpack;

    private bool dead = false;

    private int coins = 0;
    public Texture2D coinIconTexture;
    public AudioClip coinCollectSound;
    public AudioSource jetpackAudio;

    public AudioSource footstepsAudio;
    //	public ParallaxScroll parallax;
    public ParallaxScript parallax;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        bool jetpackActive = Input.GetButton("Fire1");

        jetpackActive = jetpackActive && !dead;
        rbody2D = GetComponent<Rigidbody2D>();

        if (jetpackActive)
        {

            rbody2D.AddForce(new Vector2(0, jetpackForce));
        }
        if (!dead)
        {
            Vector2 newVelocity = rbody2D.velocity;
            newVelocity.x = forwardMovementSpeed;
            rbody2D.velocity = newVelocity;
        }
        UpdateGroundedStatus();
        AdjustJetpack(jetpackActive);
        AdjustFootstepsAndJetpackSound(jetpackActive);
        parallax.offset = transform.position.x;
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("coin"))
            CollectCoin(collider);
        else
            HitByLaser(collider);
    }
    void CollectCoin(Collider2D coinCollider)
    {
        coins++;

        Destroy(coinCollider.gameObject);
        AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);
    }
    void HitByLaser(Collider2D laserCollider)
    {
        if (!dead)
        {
            //laserCollider.gameObject.audio.Play();
            //GameObject go=laserCollider.GetComponent<GameObject>();
            AudioSource audioClip = laserCollider.GetComponent<AudioSource>();
            audioClip.Play();
        }
        dead = true;
        animator.SetBool("dead", true);
    }
    void UpdateGroundedStatus()
    {
        //1
        grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);

        //2
        animator.SetBool("grounded", grounded);
    }
    void AdjustJetpack(bool jetpackActive)
    {
        jetpack.enableEmission = !grounded;
        jetpack.emissionRate = jetpackActive ? 300.0f : 75.0f;
    }
    void OnGUI()
    {
        DisplayCoinsCount();
        DisplayRestartButton();
    }
    void DisplayCoinsCount()
    {
        Rect coinIconRect = new Rect(10, 10, 32, 32);
        GUI.DrawTexture(coinIconRect, coinIconTexture);

        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.yellow;

        Rect labelRect = new Rect(coinIconRect.xMax, coinIconRect.y, 60, 32);
        GUI.Label(labelRect, coins.ToString(), style);
    }
    void DisplayRestartButton()
    {
        if (dead && grounded)
        {
            Rect buttonRect = new Rect(Screen.width * 0.35f, Screen.height * 0.45f, Screen.width * 0.30f, Screen.height * 0.1f);
            if (GUI.Button(buttonRect, "Tap to restart!"))
            {
                SceneManager.LoadScene("RocketMouse");
                // Application.LoadLevel (Application.loadedLevelName);
            };
        }
    }
    void AdjustFootstepsAndJetpackSound(bool jetpackActive)
    {
        footstepsAudio.enabled = !dead && grounded;

        jetpackAudio.enabled = !dead && !grounded;
        jetpackAudio.volume = jetpackActive ? 1.0f : 0.5f;
    }
}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class MouseController : MonoBehaviour
//{
//    public float jetpackForce = 75.0f;
//    private Rigidbody2D playerRigidbody;
//    public float forwardMovementSpeed = 3.0f;
//    public Transform groundCheckTransform;
//    private bool isGrounded;
//    public LayerMask groundCheckLayerMask;
//    private Animator mouseAnimator;
//    public ParticleSystem jetpack;
//    private bool isDead = false;
//    private uint coins = 0;
//    public Text coinsCollectedLabel;
//    public Button restartButton;
//    public AudioClip coinCollectSound;
//    public AudioSource jetpackAudio;
//    public AudioSource footstepsAudio;
//    public ParallaxScript parallax;

//    // Start is called before the first frame update
//    void Start()
//    {
//        playerRigidbody = GetComponent<Rigidbody2D>();
//        mouseAnimator = GetComponent<Animator>();

//    }
//    public void RestartGame()
//    {
//        SceneManager.LoadScene("RocketMouse");
//    }
//    void AdjustFootstepsAndJetpackSound(bool jetpackActive)
//    {
//        footstepsAudio.enabled = !isDead && isGrounded;
//        jetpackAudio.enabled = !isDead && !isGrounded;
//        if (jetpackActive)
//        {
//            jetpackAudio.volume = 1.0f;
//        }
//        else
//        {
//            jetpackAudio.volume = 0.5f;
//        }
//    }

//    void OnTriggerEnter2D(Collider2D collider)
//    {
//        if (collider.gameObject.CompareTag("Coins"))
//        {
//            CollectCoin(collider);
//        }
//        else
//        {
//            HitByLaser(collider);
//        }

//    }

//    void HitByLaser(Collider2D laserCollider)
//    {
//        if (!isDead)
//        {
//            AudioSource laserZap = laserCollider.gameObject.GetComponent<AudioSource>();
//            laserZap.Play();
//        }

//        isDead = true;
//        mouseAnimator.SetBool("isDead", true);

//    }

//    void AdjustJetpack(bool jetpackActive)
//    {
//        var jetpackEmission = jetpack.emission;
//        jetpackEmission.enabled = !isGrounded;
//        if (jetpackActive)
//        {
//            jetpackEmission.rateOverTime = 300.0f;
//        }
//        else
//        {
//            jetpackEmission.rateOverTime = 75.0f;
//        }
//    }
//    void CollectCoin(Collider2D coinCollider)
//    {
//        coins++;
//        coinsCollectedLabel.text = coins.ToString();
//        Destroy(coinCollider.gameObject);
//        AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);

//    }

//    void UpdateGroundedStatus()
//    {
//        //1
//        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);
//        //2
//        print("isGrounded" + isGrounded);
//        print("isDead" + isDead);
//        mouseAnimator.SetBool("isGrounded", isGrounded);
//    }

//    void FixedUpdate()
//    {
//        bool jetpackActive = Input.GetButton("Fire1");
//        jetpackActive = jetpackActive && !isDead;

//        if (jetpackActive)
//        {
//            playerRigidbody.AddForce(new Vector2(0, jetpackForce));
//        }

//        if (!isDead)
//        {
//            Vector2 newVelocity = playerRigidbody.velocity;
//            newVelocity.x = forwardMovementSpeed;
//            playerRigidbody.velocity = newVelocity;
//        }

//        UpdateGroundedStatus();
//        AdjustJetpack(jetpackActive);
//        if (isDead && isGrounded)
//        {
//            restartButton.gameObject.SetActive(true);
//        }
//        AdjustFootstepsAndJetpackSound(jetpackActive);
//        parallax.offset = transform.position.x;



//    }


//}