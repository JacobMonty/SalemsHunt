using System;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public Canvas BookMenu;
    public TextMeshProUGUI BookTitle;
    public TextMeshProUGUI BookDescription;

    private UnityEngine.UI.Image hiddenBook = null;

    public class bookInformation {
        public string title;
        public string description;
        public string[] words;

        public bookInformation(string title, string description, string[] words) {
            this.title = title;
            this.description = description;
            this.words = words;
        }
    }

    Dictionary<string,bookInformation> books = new Dictionary<string,bookInformation>();
    

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
        
    }

    public void CloseBook() {
        hiddenBook.enabled = true;
        hiddenBook = null;
        BookMenu.enabled = false;
    }

    void Start() {
        BookMenu.enabled = false;

        books.Add("SpinningTheWeb",new bookInformation("Spinning the Web", "Learn the ins and outs of spiningn tales, information, or anything you can desire to share to your fellow witches and wizards!", Array.Empty<String>()));
        books.Add("SecurityInMagicking", new bookInformation("Security In Magicking", "Magic can be difficult, but don't let bad actors intercept good magic! Keep your practice safe with Magic Security!", Array.Empty<String>()));
    }
}
