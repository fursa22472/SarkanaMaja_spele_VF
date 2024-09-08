using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class InteractiveCharacterController : MonoBehaviour
{
    [Header("Animation Settings")]
    public Animator animator;
    public string idleAnimation = "Idle";
    public string standUpAnimation = "StandUp";
    public string talkAnimation = "Talk";

    [Header("Audio Settings")]
    public AudioSource pianoAudioSource;
    public AudioClip pianoClip;
    public AudioSource dialogueAudioSource;
    public float maxAudioDistance = 10f;
    public float minVolume = 0.1f;

    [Header("Dialogue Settings")]
    public TextAsset inkJSONAsset;
    public Canvas canvas;
    public Text textPrefab;
    public Button buttonPrefab;
    public Transform cameraTargetPosition;

    [Header("Player Settings")]
    public Transform playerTransform;
    public CharacterMovement2 playerMovement;

    private Story story;
    private Collider characterCollider;
    private CameraTransition cameraTransition;
    private bool isDialogueActive = false;
    private Dictionary<string, AudioClip> dialogueAudioMap;
    private AudioClip currentDialogueClip;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        characterCollider = GetComponent<Collider>();

        // Set up the piano audio clip
        if (pianoAudioSource != null && pianoClip != null)
        {
            pianoAudioSource.clip = pianoClip;
            pianoAudioSource.loop = true; // Ensure piano music loops
            pianoAudioSource.Play();
        }

        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }

        InitializeDialogueAudioMap();

        // Find and assign the CameraTransition component
        cameraTransition = Camera.main.GetComponent<CameraTransition>();

        // Store original position and rotation
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        if (playerTransform != null && pianoAudioSource != null)
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            float volume = Mathf.Clamp01(1 - (distance / maxAudioDistance));
            volume = Mathf.Max(volume, minVolume);
            pianoAudioSource.volume = volume;
        }
    }

    private void OnMouseDown()
    {
        if (!isDialogueActive)
        {
            StartDialogue();
        }
        else
        {
            ForceTalkAnimation();
        }
    }

    public void StartDialogue()
    {
        // Stop piano music
        if (pianoAudioSource != null)
        {
            pianoAudioSource.Stop();
        }

        // Lock the player in place
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Disable character collider
        if (characterCollider != null)
        {
            characterCollider.enabled = false;
        }

        // Play stand up animation
        if (animator != null)
        {
            animator.ResetTrigger(idleAnimation);
            animator.SetTrigger(standUpAnimation);
            StartCoroutine(WaitForAnimation(standUpAnimation, () =>
            {
                PlayTalkingAnimation();
            }));
        }

        // Transition the camera to the target position
        if (cameraTransition != null && cameraTargetPosition != null)
        {
            cameraTransition.MoveToTarget(cameraTargetPosition);
        }

        // Initialize and start the story
        story = new Story(inkJSONAsset.text);
        if (canvas != null)
        {
            canvas.gameObject.SetActive(true);
        }
        RefreshView();
        isDialogueActive = true;
    }

    public void EndDialogue()
    {
        StartCoroutine(SmoothRotateAndMove(originalRotation, originalPosition, () =>
        {
            if (animator != null)
            {
                animator.ResetTrigger(standUpAnimation);
                animator.ResetTrigger(talkAnimation);
                animator.SetTrigger(idleAnimation);
            }

            if (characterCollider != null)
            {
                characterCollider.enabled = true;
            }

            if (canvas != null)
            {
                canvas.gameObject.SetActive(false);
            }

            if (cameraTransition != null)
            {
                cameraTransition.ResetCamera();
            }

            // Resume piano clip playback
            if (pianoAudioSource != null && pianoClip != null)
            {
                pianoAudioSource.clip = pianoClip;
                pianoAudioSource.Play();
            }

            // Unlock the player
            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }

            isDialogueActive = false;
        }));
    }

    void PlayTalkingAnimation()
    {
        if (animator != null)
        {
            animator.ResetTrigger(idleAnimation);
            animator.SetTrigger(talkAnimation);
        }

        if (playerTransform != null)
        {
            StartCoroutine(SmoothRotateToFacePlayer());
        }
    }

    void ForceTalkAnimation()
    {
        if (animator != null)
        {
            animator.ResetTrigger(idleAnimation);
            animator.SetTrigger(standUpAnimation);
            StartCoroutine(WaitForAnimation(standUpAnimation, () =>
            {
                animator.SetTrigger(talkAnimation);
            }));
        }
    }

    IEnumerator SmoothRotateToFacePlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
            yield return null;
        }

        transform.rotation = targetRotation;
    }

    IEnumerator SmoothRotateAndMove(Quaternion targetRotation, Vector3 targetPosition, Action onComplete)
    {
        float transitionTime = 1.0f;
        float elapsedTime = 0;

        Quaternion startingRotation = transform.rotation;
        Vector3 startingPosition = transform.position;

        while (elapsedTime < transitionTime)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / transitionTime);
            transform.rotation = Quaternion.Lerp(startingRotation, targetRotation, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;

        onComplete?.Invoke();
    }

    IEnumerator WaitForAnimation(string animationName, Action onComplete)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        while (stateInfo.IsName(animationName) && stateInfo.normalizedTime < 1.0f)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        onComplete?.Invoke();
    }

    void RefreshView()
    {
        RemoveChildren();

        while (story.canContinue)
        {
            string text = story.Continue().Trim();
            CreateContentView(text);
            PlayDialogueAudioFromTags(story.currentTags);
        }

        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim());

                button.onClick.AddListener(delegate {
                    OnClickChoiceButton(choice);
                });
            }
        }
        else
        {
            Button choice = CreateChoiceView("Close");
            choice.onClick.AddListener(delegate {
                EndDialogue();
            });
        }
    }

    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
    }

    void CreateContentView(string text)
    {
        Text storyText = Instantiate(textPrefab) as Text;
        storyText.text = text;
        storyText.transform.SetParent(canvas.transform, false);
    }

    Button CreateChoiceView(string text)
    {
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(canvas.transform, false);

        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

        return choice;
    }

    void RemoveChildren()
    {
        int childCount = canvas.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }

    void InitializeDialogueAudioMap()
    {
        dialogueAudioMap = new Dictionary<string, AudioClip>();

        // Example mapping (replace with your actual mappings)
        dialogueAudioMap.Add("Intro", Resources.Load<AudioClip>("Audio/bomzisAudio/Intro"));
        dialogueAudioMap.Add("AncientReligion", Resources.Load<AudioClip>("Audio/bomzisAudio/AncientReligion"));
        dialogueAudioMap.Add("CurrentLandscape", Resources.Load<AudioClip>("Audio/bomzisAudio/CurrentLandscape"));
        dialogueAudioMap.Add("PersonalStory", Resources.Load<AudioClip>("Audio/bomzisAudio/PersonalStory"));
        dialogueAudioMap.Add("RitualsAndTraditions", Resources.Load<AudioClip>("Audio/bomzisAudio/RitualsAndTraditions"));
        dialogueAudioMap.Add("AnotherStory", Resources.Load<AudioClip>("Audio/bomzisAudio/AnotherStory"));
        dialogueAudioMap.Add("End", Resources.Load<AudioClip>("Audio/bomzisAudio/End"));
    }

    void PlayDialogueAudioFromTags(List<string> tags)
    {
        foreach (string tag in tags)
        {
            if (tag.StartsWith("audio:"))
            {
                string audioTag = tag.Substring(6);
                PlayDialogueAudio(audioTag);
            }
        }
    }

    void PlayDialogueAudio(string audioTag)
    {
        if (dialogueAudioSource != null && dialogueAudioMap.TryGetValue(audioTag, out AudioClip clip))
        {
            if (currentDialogueClip != clip)
            {
                dialogueAudioSource.clip = clip;
                dialogueAudioSource.Play();
                currentDialogueClip = clip;
            }
        }
        else
        {
            Debug.LogError("Audio tag not found or audio source is null: " + audioTag);
        }
    }
}
