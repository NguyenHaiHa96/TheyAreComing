using System;
using UnityEngine;

public class EnemyMinion : MonoBehaviour, IDamageable, ISubcribers
{
    public event Action OnIdling = delegate { };
    public event Action OnDeath = delegate { };

    [SerializeField] private EnemyInfomation enemyInfo;
    [SerializeField] private Transform target;
    [SerializeField] private float currentHealth;
    [SerializeField] private float dersiredDuration;

    private CapsuleCollider capsuleCollider;
    private Rigidbody rb;
    private CharacterController controller;
    private GameManager gameManager;
    private Vector3 direction;
    private Vector3 turnDirection;
    private Vector3 leftDirection;
    private Vector3 rightDirection;
    private Quaternion enemyRotation;
    private Quaternion desiredRotation;
    private float elapsedTime;
    private float percentageComplete;
    private float moveSpeed;
    private bool turning;
    private bool turnedLeft;
    private bool turnRight;

    private void OnEnable()
    {
        InitializeVariables();     
        InstantiateModel();
        SubscribeEvent();
    }

    private void OnDisable()
    {
        UnsubscribeEvent();
    }

    private void Update()
    {
        CheckPlayerInRange();
        Move();
        if (turning)
        {
            SmoothTurning();
        }
    }

    public void SubscribeEvent()
    {
        gameManager.OnGameOver += StopMoving;
    }

    public void UnsubscribeEvent()
    {
        gameManager.OnGameOver -= StopMoving;
    }

    private void StopMoving()
    {
        OnIdling?.Invoke();
        moveSpeed = 0f;
    }

    private void InitializeVariables()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        gameManager = GameManager.Instance;
        moveSpeed = enemyInfo.moveSpeed;
        direction = Vector3.back;
        leftDirection = new Vector3(0, -90, 0);
        rightDirection = new Vector3(0, 90, 0);
        turning = false;
        currentHealth = enemyInfo.health;
    }

    private void SmoothTurning()
    {
        elapsedTime += Time.deltaTime;
        percentageComplete = elapsedTime / dersiredDuration;
        transform.rotation = Quaternion.Lerp(enemyRotation, desiredRotation, percentageComplete);
        if (transform.rotation == desiredRotation)
        {
            turning = false;
            elapsedTime = 0f;
            percentageComplete = 0f;
        }
    }

    private void InstantiateModel()
    {
        Instantiate(enemyInfo.model, this.transform);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) 
        {
            capsuleCollider.enabled = false;
            moveSpeed = 0f;
            OnDeath?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerMinion"))
        {
            Destroy(other.gameObject);
        }

        #region Cross
        if (other.CompareTag("Cross"))
        {
            if (other.TryGetComponent(out Cross cross))
            {
                turnDirection = cross.GetTurnDirection();
                if (turnDirection.Equals(leftDirection))
                {
                    turnedLeft = true;
                    turnRight = false;
                }

                if (turnDirection.Equals(rightDirection))
                {
                    turnedLeft = false;
                    turnRight = true;
                }
                enemyRotation = transform.rotation;
                desiredRotation = Quaternion.Euler(turnDirection);
                turning = true;
            }
        }
        #endregion
    }

    private void Move()
    {
        controller.Move(moveSpeed * Time.deltaTime * direction.normalized);
    }

    private void CheckPlayerInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, enemyInfo.radiusCheck);
        foreach (var hitCollider in hitColliders)
        {  
            if (hitCollider.gameObject.CompareTag("PlayerMinion"))
            {
                Vector3 newDirection = new Vector3(hitCollider.gameObject.transform.position.x - transform.position.x,
                0f,
                hitCollider.gameObject.transform.position.z - transform.position.z);
                direction = newDirection;     
            }
        }
    }
}