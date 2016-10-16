using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnScreenText : MonoBehaviour {

    private Text messageText;
    private RectTransform messageTransform;
    public RectTransform messageDisplayLocation;
    private Vector2 textStartPosition, textEndPosition;
    public float scrollToSpeed = 0.05f;
    public float messageStayTime = 0.5f;
    public float scrollOffSpeed = 0.05f;

	// Use this for initialization
	void Start () {
        messageText = gameObject.GetComponent<Text>();
        messageTransform = gameObject.GetComponent<RectTransform>();
        textStartPosition = messageTransform.anchoredPosition;
        textEndPosition = new Vector2(textStartPosition.x, 0);
	}
	
	public void DisplayMessage(string message)
    {
        messageText.text = message;
        StartCoroutine(ScrollMessage());
    }

    public IEnumerator ScrollMessage()
    {
        float duration = 1.0f;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; //0 means the animation just started, 1 means it finished
            messageTransform.anchoredPosition = Vector2.Lerp(textStartPosition, textEndPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }yield return new WaitForSeconds(1f);
        messageTransform.anchoredPosition = textStartPosition;
        /*Vector2 newPosition = new Vector2();
        while (messageTransform.anchoredPosition.y < messageDisplayLocation.anchoredPosition.y)
        {
            newPosition = messageTransform.anchoredPosition;
            newPosition.y += (scrollToSpeed * Time.deltaTime);
            messageTransform.anchoredPosition = newPosition;
        }yield return new WaitForSeconds(messageStayTime);
        while (messageTransform.anchoredPosition.y < 1)
        {
            newPosition = messageTransform.anchoredPosition;
            newPosition.y += (scrollToSpeed * Time.deltaTime);
            messageTransform.anchoredPosition = newPosition;
        }
        newPosition.y = 0;
        messageTransform.anchoredPosition = newPosition;*/
    }
}
