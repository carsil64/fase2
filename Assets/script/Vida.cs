using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class vida : MonoBehaviour
{
    public int vidaMaxima = 100;
    private int vidaAtual;

    public TextMeshProUGUI textovida;
    void Start()
    {
        vidaAtual = vidaMaxima;
        AtualizarTela();
    }

    public void TomarDano(int dano)
    {
        vidaAtual -= dano;
        if (vidaAtual < 0) vidaAtual = 0;

        Debug.Log("Tomou " + dano + " de dano");
        AtualizarTela();

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    public void Curar(int cura)
    {
        vidaAtual += cura;
        if (vidaAtual > vidaMaxima) vidaAtual = vidaMaxima;
        Debug.Log("Curou " + cura + " de vida");
        AtualizarTela();
    }

    public bool EstaComVidaCheia()
    {
        return vidaAtual == vidaMaxima;
    }

    void Morrer()
    {
        Debug.Log("Morreu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void AtualizarTela()
    {
        textovida.text = "Vida: " + vidaAtual + "/" + vidaMaxima;
    }
}