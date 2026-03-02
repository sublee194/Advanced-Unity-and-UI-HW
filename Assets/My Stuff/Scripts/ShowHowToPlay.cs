using UnityEngine;
using UnityEngine.UI;

public class ShowHowToPlay : MonoBehaviour
{
    public Image howToPlayMessage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        howToPlayMessage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMessage()
    {
        howToPlayMessage.gameObject.SetActive(true);
    }

    public void HideMessage()
    {
        howToPlayMessage.gameObject.SetActive(false);
    }
}
