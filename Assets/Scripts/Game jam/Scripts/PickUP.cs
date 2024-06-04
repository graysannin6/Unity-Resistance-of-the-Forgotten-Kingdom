using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUP : MonoBehaviour
{
    private enum PickUpType
    {
        Coin,
        Health
    }

    [SerializeField] private PickUpType pickUpType;
    [SerializeField] private float pickUpDistance = 5f;
    [SerializeField] private float accelarationRate = .2f;
    [SerializeField] private float movSpeed = 3f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 1f;
    private Vector3 moveDir;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(PopUp());
    }

    private void Update()
    {
        Vector3 playerPos = Player.Instance.transform.position;

        if (Vector3.Distance(playerPos, transform.position) < pickUpDistance)
        {
            moveDir = (playerPos - transform.position).normalized;
            movSpeed += accelarationRate;
        }
        else
        {
            moveDir = Vector3.zero;
            movSpeed = 0f;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * movSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            DetectPickUpType();
            Destroy(gameObject);
        }
    }

    private IEnumerator PopUp()
    {
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-1f, 1f);

        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while (timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }

    }

    private void DetectPickUpType()
    {
        switch (pickUpType)
        {
            case PickUpType.Coin:
                LevelUpController.Instance.AddExperience(1);
                break;
            case PickUpType.Health:
                PlayerHealthManager.Instance.HealPlayer();
                break;
            default:
                break;
        }
    }
}
