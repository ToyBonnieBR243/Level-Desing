using UnityEngine;
using UnityEngine.Events;

public class ArenaManager : MonoBehaviour
{
    [Tooltip("Total de inimigos que o spawner vai criar")]
    public int enemiesToKill;

    private int currentKills = 0;

    [Tooltip("Coloque a porta de saída aqui")]
    public UnityEvent OnArenaCleared;

    public void AddKill()
    {
        currentKills++;
        if (currentKills >= enemiesToKill)
        {
            OnArenaCleared.Invoke();
        }
    }
}