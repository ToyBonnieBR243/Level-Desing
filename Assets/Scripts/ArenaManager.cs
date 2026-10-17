using Gamekit2D;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ArenaManager : MonoBehaviour
{
    [Header("Configurações da Arena")]
    [Tooltip("Total de inimigos que precisam ser derrotados para abrir a saída")]
    public int enemiesToKill = 20;

    private int currentKills = 0;

    [Header("Interface (UI)")]
    [Tooltip("Arraste o seu objeto TextMeshPro da UI aqui")]
    public TextMeshProUGUI remainingText;

    [Tooltip("Arraste o Canvas inteiro do texto aqui (para ele começar desligado e ligar ao passar no checkpoint)")]
    public GameObject canvasToActivate;

    [Header("Ativação por Checkpoint")]
    [Tooltip("Arraste o Checkpoint (ex: Checkpoint (1)) da sua Hierarchy para este campo")]
    public GameObject checkpointObject;
    private BoxCollider2D checkpointCollider;

    [Header("Eventos de Conclusão")]
    public UnityEvent OnArenaCleared;

    private bool arenaStarted = false;
    private Transform playerTransform;

    void Start()
    {
        // Garante que o texto/canvas começa escondido no início do jogo
        if (canvasToActivate != null)
        {
            canvasToActivate.SetActive(false);
        }

        // Localiza a Ellen automaticamente na cena
        PlayerCharacter player = Object.FindFirstObjectByType<PlayerCharacter>();
        if (player != null)
        {
            playerTransform = player.transform;
        }

        // Pega o colisor do checkpoint que você arrastou no Inspector
        if (checkpointObject != null)
        {
            checkpointCollider = checkpointObject.GetComponent<BoxCollider2D>();
        }

        UpdateUI();
    }

    void Update()
    {
        // Se a arena ainda não começou, verifica se a Ellen encostou na área do checkpoint
        if (!arenaStarted && playerTransform != null && checkpointCollider != null)
        {
            if (checkpointCollider.OverlapPoint(playerTransform.position))
            {
                ActivateArena();
            }
        }
    }

    void ActivateArena()
    {
        arenaStarted = true;

        // Liga o Canvas do texto na tela assim que passa pelo checkpoint
        if (canvasToActivate != null)
        {
            canvasToActivate.SetActive(true);
        }
    }

    public void AddKill()
    {
        // Só conta os abates se a arena já foi ativada pelo checkpoint
        if (!arenaStarted) return;

        currentKills++;
        if (currentKills > enemiesToKill)
        {
            currentKills = enemiesToKill;
        }

        UpdateUI();

        // Se atingiu o número necessário de abates, abre a porta de saída
        if (currentKills >= enemiesToKill)
        {
            if (remainingText != null)
                remainingText.text = "Porta Aberta!";

            OnArenaCleared.Invoke();
        }
    }

    void UpdateUI()
    {
        if (remainingText != null)
        {
            remainingText.text = "Elimine os inimigos: " + currentKills + "/" + enemiesToKill;
        }
    }
}