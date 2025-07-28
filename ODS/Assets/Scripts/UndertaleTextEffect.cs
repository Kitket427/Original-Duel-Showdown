using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
struct TextLine
{
    public string text;
    public float speed, timeAlpha, timeNext;
    public ActiveOrNot[] activeOrNot;
}
[System.Serializable]
struct ActiveOrNot
{
    public GameObject obj;
    public bool active;
}
public class UndertaleTextEffect : MonoBehaviour
{
    [SerializeField] private Text textObject;
    [SerializeField] private float time;
    [SerializeField] private TextLine[] messages;
    private int currentMessageIndex = 0;
    private string currentText = "";
    private AudioSource voice;
    private void Start()
    {
        voice = GetComponent<AudioSource>();
        textObject.text = "";
        Invoke(nameof(StartText), -time);
    }
    private void Update()
    {
        if (messages[currentMessageIndex].timeNext != 0) time += Time.deltaTime;
        if (messages[currentMessageIndex].timeAlpha - time < 1) textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, messages[currentMessageIndex].timeAlpha - time);
        else textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, 1);
        if (transform.lossyScale.x <0) transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    void StartText()
    {
        StartCoroutine(ShowText());
        for (int i = 0; i < messages[currentMessageIndex].activeOrNot.Length; i++)
        {
            messages[currentMessageIndex].activeOrNot[i].obj.SetActive(messages[currentMessageIndex].activeOrNot[i].active);
        }
    }
    IEnumerator ShowText()
    {
        time = 0;
        string message = messages[currentMessageIndex].text;
        if (messages[currentMessageIndex].timeNext != 0) messages[currentMessageIndex].timeAlpha += message.Length / messages[currentMessageIndex].speed;
        for (int i = 0; i <= message.Length; i++)
        {
            voice.Play();
            currentText = message.Substring(0, i);
            textObject.text = currentText;
            yield return new WaitForSeconds(1/messages[currentMessageIndex].speed);
        }
        if(messages[currentMessageIndex].timeNext != 0) Invoke(nameof(NextMessage), messages[currentMessageIndex].timeNext);
    }
    private void NextMessage()
    {
        for (int i = 0; i < messages[currentMessageIndex].activeOrNot.Length; i++)
        {
            messages[currentMessageIndex].activeOrNot[i].obj.SetActive(!messages[currentMessageIndex].activeOrNot[i].active);
        }
        if (currentMessageIndex < messages.Length - 1)
        {
            currentMessageIndex++;
            textObject.text = "";
            StartCoroutine(ShowText());
            for (int i = 0; i < messages[currentMessageIndex].activeOrNot.Length; i++)
            {
                messages[currentMessageIndex].activeOrNot[i].obj.SetActive(messages[currentMessageIndex].activeOrNot[i].active);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}