using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Inventario : MonoBehaviour
{
    public List<string> itens = new List<string>();

    [Header("Interface (UI)")]
    public GameObject painelInventario;
    public TextMeshProUGUI textoItens;
    public TextMeshProUGUI textoMensagem;

    [Header("Poção de cura")]
    public string nomePocao = "Poção";
    public int curaDaPocao = 30;

    [Header("Espada")]
    public string nomeEspada = "Espada";
    public GameObject espadaNaMao;
    private bool espadaEquipada = false;

    private vida vida;

    void Start()
    {
        vida = GetComponent<vida>();
        painelInventario.SetActive(false);
        espadaNaMao.SetActive(false);
        AtualizarTela();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            painelInventario.SetActive(!painelInventario.activeSelf);
        }


        if (Input.GetKeyDown(KeyCode.H))
        {
            UsarPocao();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            EquiparEspada();
        }
    }

    public void AdicionarItem(string nomeDoItem)
    {
        itens.Add(nomeDoItem);
        Avisar("Pegou: " + nomeDoItem);
        AtualizarTela();
    }

    void UsarPocao()
    {
        if (!itens.Contains(nomePocao))
        {
            Avisar("Você não tem poção!");
            return;
        }

        if (vida.EstaComVidaCheia())
        {
            Avisar("Sua vida já está cheia!");
            return;
        }

        itens.Remove(nomePocao);
        vida.Curar(curaDaPocao);
        Avisar("Usou uma poção! +" + curaDaPocao + " de vida");
        AtualizarTela();
    }

    void EquiparEspada()
    {
        if (!itens.Contains(nomeEspada))
        {
            Avisar("Você não tem espada!");
            return;
        }
        espadaEquipada = !espadaEquipada;
        espadaNaMao.SetActive(espadaEquipada);

        if (espadaEquipada) Avisar("Espada equipada!");
        else Avisar("Espada guardada.");

        AtualizarTela();
    }

    void Avisar(string mensagem)
    {
        Debug.Log(mensagem);
        if (textoMensagem != null)
        {
            textoMensagem.text = mensagem;
            CancelInvoke(nameof(LimparMensagem));
            Invoke(nameof(LimparMensagem), 2f);
        }
    }

    void LimparMensagem()
    {
        textoMensagem.text = "";
    }

    void AtualizarTela()
    {
        string texto = "INVENTÁRIO (" + itens.Count + " itens)\n\n";

        if (itens.Count == 0)
        {
            texto += "Vazio\n";
        }

        foreach (string item in itens)
        {
            texto += "- " + item;
            if (item == nomeEspada && espadaEquipada) texto += " (equipada)";
            texto += "\n";
        }

        texto += "\nH: usar poção   Q: espada";
        textoItens.text = texto;
    }
}