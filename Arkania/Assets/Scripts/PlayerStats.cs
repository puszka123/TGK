using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{

    private float maxHealth = 100;
    private float currentHealth = 100;
    private float maxMana = 100;
    private float currentMana = 100;
    private float maxStamina = 100;
    private float currentStamina = 100;

    private float barWidth = 100;
    private float barHeight = 100;

    private float canHeal = 0.0f;
    private float canRegenerate = 0.0f;

    private CharacterController chCont;
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsC;
    private Vector3 lastPosition;

    public Texture2D healthTexture;
    public Texture2D manaTexture;
    public Texture2D staminaTexture;

    public float walkSpeed = 10.0f;
    public float runSpeed = 20.0f;

    public float AttackDistance  = 3.2f;
    public float AttackDelay = 0.5f;
    public float AttackDamage = 10f;
    private RaycastHit hit;
    private Vector3 fwd;
    private float time = 0f;
    bool isSword = false;
    float lastPressed;
    public GameObject Sword;
    Animator animator;
    public GameObject animation;
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController controller;
    bool isWalking;

    void Awake()
    {
        barHeight = Screen.height * 0.02f;
        barWidth = barHeight * 10.0f;

        chCont = GetComponent<CharacterController>();
        fpsC = gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();

        lastPosition = transform.position;
    }
   
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width - barWidth - 10,
                                 Screen.height - barHeight * 3 - 30,
                                 currentHealth * barWidth / maxHealth,
                                 barHeight),
                        healthTexture);
        GUI.DrawTexture(new Rect(Screen.width - barWidth - 10,
                                 Screen.height - barHeight * 2 - 20,
                                 currentMana * barWidth / maxMana,
                                 barHeight),
                        manaTexture);
        GUI.DrawTexture(new Rect(Screen.width - barWidth - 10,
                                 Screen.height - barHeight - 10,
                                 currentStamina * barWidth / maxStamina,
                                 barHeight),
                        staminaTexture);
    }

    void Start()
    {
        animator = animation.GetComponent<Animator>();
        Rect currentRes = new Rect(-Screen.width * 0.5f,
                                   -Screen.height * 0.5f,
                                   Screen.width,
                                   Screen.height);
    }

    void Update()
    {
        if (canHeal > 0.0f)
        {
            canHeal -= Time.deltaTime;
        }
        if (canRegenerate > 0.0f)
        {
            canRegenerate -= Time.deltaTime;
        }

        if (canHeal <= 0.0f && currentHealth < maxHealth)
        {
            regenerate(ref currentHealth, maxHealth);
        }
        if (canRegenerate <= 0.0f && currentStamina < maxStamina)
        {
            regenerate(ref currentStamina, maxStamina);
        }

        fwd = transform.TransformDirection(Vector3.forward);

        if (Input.GetKeyDown(KeyCode.Mouse0) && isSword && !isWalking)
        {
            if (time <= 0f)
            {
                animator.SetTrigger("attack");
                StartCoroutine(StopAttack());
                time = AttackDelay;
                if (Physics.Raycast(transform.position, fwd, out hit, AttackDistance))
                {
                    hit.transform.gameObject.SendMessage("TakeHit", AttackDamage);
                }
            }
        }

        if(Input.GetKey(KeyCode.Z))
        {       
            if(lastPressed <= 0f)
            {
                lastPressed = 0.2f;
                isSword = !isSword;
                Sword.SetActive(isSword);
            }
        }

        time -= Time.deltaTime;
        lastPressed -= Time.deltaTime;
    }

    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("stopAttack");
    }

    void FixedUpdate()
    {
        float speed = walkSpeed;

        if (chCont.isGrounded && Input.GetKey(KeyCode.LeftShift) && lastPosition != transform.position && currentStamina > 0)
        {
            lastPosition = transform.position;
            speed = runSpeed;
            currentStamina -= 1;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            canRegenerate = 1.0f;
        }

        if (currentStamina > 0)
        {
            fpsC.CanRun = true;
        }
        else
        {
            fpsC.CanRun = false;
        }
    }

    void takeHit(float damage)
    {

        //Destroy(Instantiate(hitTexture), 0.15f);

        currentHealth -= damage;

        if (currentHealth < maxHealth)
        {
            canHeal = 10.0f;
        }

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    void regenerate(ref float currentStat, float maxStat)
    {
        currentStat += maxStat * 0.01f;
        Mathf.Clamp(currentStat, 0, maxStat);
    }

    public void IsWalking(bool value)
    {
        isWalking = value;
    }

}