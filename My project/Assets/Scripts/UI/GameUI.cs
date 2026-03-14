using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI bounty;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI nextRollTimer;

    private float elapsedTime;
    private float nextRollTime;
    public float score;

    void Start()
    {
        nextRollTime = 300;

    }

    // Update is called once per frame
    void Update()
    {
        bounty.text = "$" + score.ToString();
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");

        nextRollTime -= Time.deltaTime;

        int minutesRoll = Mathf.FloorToInt(nextRollTime / 60);
        int secondsRoll = Mathf.FloorToInt(nextRollTime % 60);

        nextRollTimer.text = minutesRoll.ToString("00") + ":" + secondsRoll.ToString("00");
    }
}
