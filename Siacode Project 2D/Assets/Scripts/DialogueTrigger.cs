using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    [Header("Visual Cue")] [SerializeField]
    private GameObject visualCue;

    [Header("Ink JSON")] [SerializeField] private TextAsset inkJSON;

    public bool interactableOnce;
    public bool interactionOnEnter;
    private bool isLockedVisualCueState;

    private bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
        if (interactableOnce)
        {
            visualCue.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if (!isLockedVisualCueState)
            {
                visualCue.SetActive(true);
            }

            if (_playerInputActions.Player.Interact.triggered)
            {
                Debug.Log("OK");
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            if (!isLockedVisualCueState)
            {
                visualCue.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            DialogueManager.GetInstance().DialogueTrigger = this;

            if (interactionOnEnter && !DialogueManager.GetInstance().dialogueIsPlaying)
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    public void disableDialogue()
    {
        gameObject.SetActive(false);
    }
}