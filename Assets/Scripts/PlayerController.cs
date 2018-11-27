using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public Slider healthBar;
    public Camera mainCamera;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;


    private void Awake()
    {
        Screen.lockCursor = true;
        currentHealth = maxHealth;

        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        mainCamera = Camera.main;

    }
        void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        transform.Translate(x, 0, z);
        healthBar.value = currentHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Dead!");
        }
    }
}