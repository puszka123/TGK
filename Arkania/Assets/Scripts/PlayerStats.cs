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

    public float AttackDistance = 3.2f;
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
    float originalSpeed;
    float speedTimer;
    bool haveSecondChance = false;
    bool showRestartButton = false;
    bool menu = false;
    float pressed;
    bool alive = true;
    bool end = false;
    bool keyboard = false;
    GUIStyle guiStyle = new GUIStyle();

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
        if(end)
        {
            guiStyle.fontSize = 20;
            //guiStyle.normal.textColor = Color.red;
            guiStyle.fontStyle = FontStyle.Bold;
            var texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            texture.SetPixel(0, 0, new Color(0f, 0f, 0f, 1f));
            texture.Apply();
            string text = "Koniec!";
            GUIContent content = new GUIContent(text);
            Vector2 size = guiStyle.CalcSize(content);
            guiStyle.normal = new GUIStyleState { textColor = Color.white, background = texture };
            GUI.Label(new Rect(Screen.width / 2 - size.x/2, Screen.height/2 - 100, size.x, size.y), content, guiStyle);
            if (GUI.Button(new Rect(Screen.width / 2 - 70 / 2, Screen.height / 2, 70, 70), "Zakończ"))
            {
                Application.Quit();
            }
        }

        GUI.DrawTexture(new Rect(Screen.width - barWidth - 10,
                                 Screen.height - barHeight * 2 - 20,
                                 currentHealth * barWidth / maxHealth,
                                 barHeight),
                        healthTexture);
        /*
        GUI.DrawTexture(new Rect(Screen.width - barWidth - 10,
                                 Screen.height - barHeight * 2 - 20,
                                 currentMana * barWidth / maxMana,
                                 barHeight),
                        manaTexture);
                        */
        GUI.DrawTexture(new Rect(Screen.width - barWidth - 10,
                                 Screen.height - barHeight - 10,
                                 currentStamina * barWidth / maxStamina,
                                 barHeight),
                        staminaTexture);
        if (showRestartButton && !menu)
        {
            fpsC.enabled = false;
            chCont.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (GUI.Button(new Rect(Screen.width / 2 - 70 / 2, Screen.height / 2 - 30 / 2, 70, 70), "Powtórz"))
            {
                GameObject.FindGameObjectWithTag("storyobject").SendMessage("SecondChance");
                fpsC.enabled = true;
                fpsC.CanMove = true;
                chCont.enabled = true;
                Cursor.visible = false;
                showRestartButton = false;
                currentHealth = maxHealth;
                animator.SetTrigger("alive");
                alive = true;
                
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 70 / 2, Screen.height / 2 + 90 - 30 / 2, 70, 70), "Zakończ"))
            {
                Application.Quit();
            }
        }

        if (menu)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 70 / 2, Screen.height / 2 - 30 / 2, 70, 70), "Wznów"))
            {
                Cursor.visible = false;
                menu = false;
                fpsC.enabled = true;
                chCont.enabled = true;
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 70 / 2, Screen.height / 2 + 90 - 30 / 2, 80, 70), "Sterowanie"))
            {
                menu = false;
                keyboard = true;
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 70 / 2, Screen.height / 2 + 180 - 30 / 2, 70, 70), "Zakończ"))
            {
                Application.Quit();
            }
        }
        if(keyboard)
        {
            guiStyle.fontSize = 20;
            //guiStyle.normal.textColor = Color.red;
            guiStyle.fontStyle = FontStyle.Bold;
            var texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            texture.SetPixel(0, 0, new Color(0f, 0f, 0f, 1f));
            texture.Apply();
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            stringBuilder.AppendLine("WASD: Ruch postaci");
            stringBuilder.AppendLine("I: Dziennik misji");
            stringBuilder.AppendLine("G: Rzut przedmiotem");
            stringBuilder.AppendLine("Lewy shift: sprint");
            stringBuilder.AppendLine("Esc: Menu");
            stringBuilder.AppendLine("E: Rozmawiaj/Podnieś/Użyj");
            GUIContent content = new GUIContent(stringBuilder.ToString());
            Vector2 size = guiStyle.CalcSize(content);
            guiStyle.normal = new GUIStyleState { textColor = Color.white, background = texture };
            GUI.Label(new Rect(Screen.width / 2 - size.x / 2, Screen.height / 2 - size.y/2, size.x, size.y), content, guiStyle);

            if (GUI.Button(new Rect(Screen.width / 2 - 70 / 2, Screen.height / 2 - size.y / 2 + size.y + 10, 70, 70), "Wróć"))
            {
                menu = true;
                keyboard = false;
            }
        }
    }

    void Start()
    {
        animator = animation.GetComponent<Animator>();
        Rect currentRes = new Rect(-Screen.width * 0.5f,
                                   -Screen.height * 0.5f,
                                   Screen.width,
                                   Screen.height);
        originalSpeed = walkSpeed;
        speedTimer = 0.5f;
        fpsC.SendMessage("ChangeSpeed", walkSpeed);
        fpsC.SendMessage("ChangeRunSpeed", runSpeed);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && pressed < 0f)
        {
            pressed = 0.2f;
            menu = !menu;
            //fpsC.CanMove = !fpsC.CanMove;
            fpsC.enabled = !fpsC.enabled;
            chCont.enabled = !chCont.enabled;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = !Cursor.visible;
        }
        pressed -= Time.deltaTime;

        if (walkSpeed < originalSpeed)
        {
            if (speedTimer <= 0f)
            {
                walkSpeed++;
                runSpeed++;
                speedTimer = 0.5f;
                fpsC.SendMessage("ChangeSpeed", walkSpeed);
                fpsC.SendMessage("ChangeRunSpeed", runSpeed);
            }
            speedTimer -= Time.deltaTime;
        }

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

        if (Input.GetKey(KeyCode.Z))
        {
            if (lastPressed <= 0f)
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
        walkSpeed = 3.5f;
        runSpeed = 5.5f;
        if (currentHealth <= 0f && alive)
        {
            alive = false;
            Debug.Log("die");
            SendMessage("DieNow");
            fpsC.CanMove = false;
            if (haveSecondChance)
            {
                showRestartButton = true;
            }
        }
        fpsC.SendMessage("ChangeSpeed", walkSpeed);
        fpsC.SendMessage("ChangeRunSpeed", runSpeed);
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

    public void HaveSecondChance()
    {
        haveSecondChance = true;
    }

    public void EndTheGame()
    {
        fpsC.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        end = true;
    }
}