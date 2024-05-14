using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDir = 2f;
    public enum State
    {
        Roaming
    }

    private State state;
    private EnemyPathfinding pathfinding;
    private void Awake()
    {
        pathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        StartCoroutine(Roaming());
    }

    private IEnumerator Roaming()
    {
        while (state == State.Roaming)
        {
            Vector2 randomPosition = GetRandomPosition();
            pathfinding.MoveTo(randomPosition);
            yield return new WaitForSeconds(roamChangeDir);
        }
    }

    private Vector2 GetRandomPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
