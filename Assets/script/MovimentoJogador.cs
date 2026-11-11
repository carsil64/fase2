using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovimentoJogador : MonoBehaviour
{
    public float velocidade = 3f;
    public float sensibilidade = 2f;
    public Transform cameraJogador;

    // --- NOVO: variáveis do pulo ---
    public float alturaPulo = 1.2f;
    public float gravidade = -20f;
    private float velocidadeVertical = 0f;

    private Animator animator;
    private CharacterController controle;
    private float rotacaoVertical = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        controle = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- Olhar com o mouse ---
        float mouseX = Input.GetAxis("Mouse X") * sensibilidade;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidade;

        transform.Rotate(0f, mouseX, 0f);

        rotacaoVertical -= mouseY;
        rotacaoVertical = Mathf.Clamp(rotacaoVertical, -80f, 80f);
        cameraJogador.localRotation = Quaternion.Euler(rotacaoVertical, 0f, 0f);

        // --- Andar na direção para onde está olhando ---
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direcao = transform.right * h + transform.forward * v;
        if (direcao.magnitude > 1f) direcao.Normalize();

        // --- NOVO: pulo e gravidade ---
        bool noChao = controle.isGrounded;

        if (noChao && velocidadeVertical < 0f)
        {
            velocidadeVertical = -2f; // mantém o personagem "colado" no chão
        }

        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            velocidadeVertical = Mathf.Sqrt(alturaPulo * -2f * gravidade);
        }

        velocidadeVertical += gravidade * Time.deltaTime;

        // --- Junta andar + pulo em um único movimento ---
        Vector3 movimento = direcao * velocidade;
        movimento.y = velocidadeVertical;

        controle.Move(movimento * Time.deltaTime);

        animator.SetFloat("Velocidade", direcao.magnitude);

        // ESC solta o mouse
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}