using System.Collections.Specialized;
using System.Numerics;
using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public Text actorName;
    public Text messageText;
    public RectTransform backgroundBox;
    
    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    
    public static bool isActive = false;

    public void OpenDialogue(Message[] messages, Actor[] actors) 
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;

        DisplayMessage();
        backgroundBox.LeanScale(UnityEngine.Vector3.one, 0.5f).setEaseInOutExpo();
    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;

        AnimateTextColor();
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length){
            DisplayMessage();
        } else {
            backgroundBox.LeanScale(UnityEngine.Vector3.zero, 0.5f).setEaseInOutExpo();
            isActive = false;
        }       
    }

    void AnimateTextColor()
    {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);
    }

    private void Update() {
        {
            if (Input.GetKeyDown(KeyCode.Space) && isActive == true)
            {
                NextMessage();
            }
        }
    }

    void Start() {
        {
            backgroundBox.transform.localScale = UnityEngine.Vector3.zero;
        }
    }
}
