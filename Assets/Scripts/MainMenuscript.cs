using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuscript : MonoBehaviour
{

    public void Play() {

        SceneManager.LoadScene("MagicMirror");

    }

    public void Quit() {

        Application.Quit();

    }



}
