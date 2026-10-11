using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class QuizLogic: MonoBehaviour
{

    //Variables
    public TMP_Text questionText; //doubles as dialoguetext in the beginning

        //Randomize interviewer image
    public Image interviewerImage;
    public Sprite[] interviewerSprites;

        //For beginning dialogue
    public Button continueButton;
    private string[] dialogueLines = {
        "INTERVIEWER: Good morning! You have a fantastic resume",
        "YOU: Willy Wonka!",
        "INTERVIEWER: Take deep breaths and you'll be fine.",
        "YOU: I'll do my best!",
        "INTERVIEWER: Listen kid. I'm not playing around today. Either you know the answers or you don't.",
        "YOU: Greetings!",
    };
    private List<string> availableDialogue = new List<string>();
    private int dialogueCount = 0;
    private int dialogueLimit = 3;

        //For the questions and answers
    public Button[] answerButtons;
    public Text[] answerTexts;
    private int questionsAsked = 0;
    public int questionLimit = 3;
    private List<int> availableQuestions = new List<int>();
    private List<string> questions = new List<string>()
    {
        "HOW DO YOU PRINT TEXT IN C?",
        "WHICH OF THE FOLLOWING CHECKS IF TWO VALUES ARE EQUAL IN C?",
        "SELECT THE DATA TYPE THAT STORES WHOLE NUMBERS IN C!",
        "WHAT IS A BOOLEAN DATA TYPE IN C USED FOR?",
        "WHICH FUNCTION IS USED TO GET USER INPUT IN C?"
    };
    private List<AnswerSet> answers = new List<AnswerSet>()
    {
        new AnswerSet( new[] {"println()", "Debug.log()", "printf()"}, 2),
        new AnswerSet( new[] {"=", "==", "<>"}, 1),
        new AnswerSet( new[] {"int", "double", "char"}, 0),
        new AnswerSet( new[] {"Storing characters", "Storing true or false values", "Storing floating-point numbers"}, 1),
        new AnswerSet( new[] {"printf()", "sgets()", "scanf()"}, 2)
    };

    private class AnswerSet
    {
        public string[] choices;
        public int correctIndex;
        public AnswerSet(string[] c, int correct) {choices = c; correctIndex = correct; }
    }

    private int currentIndex; 


    void Start()
    {

        ShowRandomInterviewer();

        for(int i = 0; i < questions.Count; i++)
        {
            availableQuestions.Add(i);
        }

        for(int i = 0; i < answerButtons.Length; i++)
        {
            int idx = i;
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(idx));
            
            //Hide answers until the quiz begins
            answerButtons[i].gameObject.SetActive(false);
        }

        //Connect the continue button
        continueButton.onClick.AddListener(ShowNextDialogue);
        availableDialogue.AddRange(dialogueLines);
        ShowNextDialogue();

    }


    void ShowNextDialogue()
    {
        if(dialogueCount >= dialogueLimit)
        {
            //Dialogue's finished, begin MCQ
            continueButton.gameObject.SetActive(false);
            questionText.text = "Interview begins!";
            
            ShowRandomQuestion();

            foreach(Button button in answerButtons)
            {
                button.gameObject.SetActive(true);
                button.interactable = true;
            }
            return;
        }

        int randomIndex = Random.Range(0, availableDialogue.Count);
        questionText.text = availableDialogue[randomIndex];
        availableDialogue.RemoveAt(randomIndex);
        dialogueCount++;

        TMP_Text buttonLabel = continueButton.GetComponentInChildren<TMP_Text>();
        buttonLabel.text = dialogueCount >= dialogueLimit ? "Start Interview!" : "Continue";

    }


    void ShowRandomInterviewer()
    {
        int randomIndex = Random.Range(0, interviewerSprites.Length);
        interviewerImage.sprite = interviewerSprites[randomIndex];
    }


    void ShowRandomQuestion()
    {
        int randomPosition = Random.Range(0, availableQuestions.Count);
        currentIndex = availableQuestions[randomPosition];
        availableQuestions.RemoveAt(randomPosition);

        //Show question (TMP)
        questionText.text = questions[currentIndex];

        //Show three answers (regular text)
        var set = answers[currentIndex];
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerTexts[i].text = set.choices[i];
        }
    }


    void OnAnswerSelected(int buttonIndex)
    {
        var set = answers[currentIndex];
        if (buttonIndex == set.correctIndex)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log($"Wrong - correct answer: {set.choices[set.correctIndex]}");
        }

        questionsAsked++;

        if(questionsAsked >= questionLimit)
        {
            questionText.text = "Interview Complete!";
            foreach(Button button in answerButtons)
            {
                button.interactable = false;
            }
            Debug.Log("Interview finished!");
            return;
        }
            
        ShowRandomQuestion();
    }


}
