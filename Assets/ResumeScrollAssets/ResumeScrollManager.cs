using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ResumeScrollManager : MonoBehaviour
{
    public Canvas BookMenu;
    public GameObject KeywordsPanel;
    public TextMeshProUGUI BookTitle;
    public TextMeshProUGUI BookDescription;
    public UnityEngine.UI.Button DraggableKeywordItem;

    private UnityEngine.UI.Image hiddenBook = null;
    private string[] skills = new string[]{"", "", ""};

    public class bookInformation {
        public string title;
        public string description;
        public string[] keyWords;

        public bookInformation(string title, string description, string[] keyWords) {
            this.title = title;
            this.description = description;
            this.keyWords = keyWords;
        }
    }

    Dictionary<string,bookInformation> books = new Dictionary<string,bookInformation>();

    
    private void InitializeWords(string[] keyWords)
    {
        float initialy = DraggableKeywordItem.transform.position.y;

        for (int i = 0; i < keyWords.Length; i++)
        {
            float yvalue = initialy - (i * 30);
            string wordText = keyWords[i];
            Debug.Log(wordText);

            UnityEngine.UI.Button DraggableWord = Instantiate(DraggableKeywordItem,  new Vector3(DraggableKeywordItem.transform.position.x, 
                 yvalue, DraggableKeywordItem.transform.position.z), Quaternion.identity);
            DraggableWord.transform.parent = KeywordsPanel.transform;
            DraggableWord.GetComponentInChildren<TextMeshProUGUI>().text = keyWords[i];
            DraggableWord.enabled = true;

        }
    }
    


    // Open BookMenu and hide associated clicked item
    public void OpenBook(UnityEngine.UI.Image book) {
        // Debug.Log(book.name + " has been pressed!");
        // Debug.Log(BookMenu.name + " Has been initialized!");
        if (hiddenBook) return;

        hiddenBook = book;
        book.enabled = false;
        BookMenu.enabled = true;

        BookTitle.text = books[book.name].title;
        BookDescription.text = books[book.name].description;

        InitializeWords(books[book.name].keyWords);
        
    }

    public void CloseBook() {
        foreach (Transform child in KeywordsPanel.transform) 
        {
            Destroy(child.gameObject); 
        }


        hiddenBook.enabled = true;
        hiddenBook = null;
        BookMenu.enabled = false;
    }

    public void setSkill(int num, string skill)
    {
        skills[num] = skill;

        foreach(string skillString in skills)
        {
            Debug.Log(skillString);
        }
    }

    void Start() {
        BookMenu.enabled = false;
        DraggableKeywordItem.enabled = false;

        books.Add("SpinningTheWeb", new bookInformation("Spinning the Web", "Learn the ins and outs of spinning tales, information, or anything you can desire to share to your fellow witches and wizards!", new string[] { "Fullstack Web Development", "Web Devlopment" }));
        books.Add("SecurityInMagicking", new bookInformation("Security In Magicking", "Magic can be difficult, but don't let bad actors intercept good magic! Keep your practice safe with Magic Security!", new string[] { "Magic Security" }));
    }
}
