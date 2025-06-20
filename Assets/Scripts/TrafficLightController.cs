using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public enum LightState
    {
        Green,
        Yellow,
        Red
    }

    public float greenLightDuration = 5.0f;
    public float yellowLightDuration = 2.0f;
    public float redLightDuration = 5.0f;

    public GameObject redLightObject;
    public GameObject yellowLightObject;
    public GameObject greenLightObject;

    private LightState currentLightState;
    private float timer;

    public bool IsRedLightActive => currentLightState == LightState.Red;
    public bool IsGreenLightActive => currentLightState == LightState.Green;
    public bool IsYellowLightActive => currentLightState == LightState.Yellow;


    void Start()
    {
        currentLightState = LightState.Green;
        timer = greenLightDuration;
        UpdateLightVisuals();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            switch (currentLightState)
            {
                case LightState.Green:
                    currentLightState = LightState.Yellow;
                    timer = yellowLightDuration;
                    break;
                case LightState.Yellow:
                    currentLightState = LightState.Red;
                    timer = redLightDuration;
                    break;
                case LightState.Red:
                    currentLightState = LightState.Green;
                    timer = greenLightDuration;
                    break;
            }
            UpdateLightVisuals();
        }
    }

    void UpdateLightVisuals()
    {
        if (redLightObject != null)
            redLightObject.SetActive(currentLightState == LightState.Red);
        if (yellowLightObject != null)
            yellowLightObject.SetActive(currentLightState == LightState.Yellow);
        if (greenLightObject != null)
            greenLightObject.SetActive(currentLightState == LightState.Green);
    }
}
