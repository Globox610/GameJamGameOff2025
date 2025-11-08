using UnityEngine;

public class CooldownTest : MonoBehaviour
{
    float attackCooldownDuration = 3;
    CooldownTimer timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = new CooldownTimer(attackCooldownDuration);
    }

    // Update is called once per frame
    void Update()
    {

        if (!timer.IsActive)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Pressing A");
                timer.Start();
            }
        }

        Debug.Log(timer.TimeRemaining);

        timer.Update(Time.deltaTime);
    }
}
