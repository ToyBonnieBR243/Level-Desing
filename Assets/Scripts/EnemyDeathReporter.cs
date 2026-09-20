using UnityEngine;

public class EnemyDeathReporter : MonoBehaviour
{
    public void ReportDeath()
    {
        // Procura o ArenaManager na cena ativa e avisa que morreu
        ArenaManager arena = Object.FindAnyObjectByType<ArenaManager>();
        if (arena != null)
        {
            arena.AddKill();
        }
    }
}