using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    [Header("UI", order = 0)]

    [Header("UI Buttons", order = 1)]
    public GameObject panelGameOver;

    [Header("UI Knifes", order = 1)]
    public GameObject panelKnifes;
    public GameObject iconKnife;

    [Header("Knifes", order = 1)]
    public int countKnifes;
    public GameObject knifeObject;
    ///public int knifeSkin;

    [Header("Audios", order = 1)]
    public AudioClip AudioButton;
    public AudioClip AudioGameOver;

    private int knifeIconIndex = 0;
    private int scene;
    private AudioSource audioData;

    public void Start() {      
        Instance = this;
        audioData = GetComponent<AudioSource>();
        SetKnifeIcons(countKnifes);
        SpawnKnife();
    }

    //Spanws knife icons on panel knifes
    public void SetKnifeIcons(int count) {
        for (int i = 0; i < count; i++) {
            Instantiate(iconKnife, panelKnifes.transform);
        }
    }

    //Delete knife icons from panel knifes
    public void DeleteKnifeIcon() {
        panelKnifes.transform.GetChild(knifeIconIndex++).GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    //Check if has more knifes, else start win state
    public void KnifeHit() {
        if (countKnifes > 0) {
            SpawnKnife();
        } else {
            StartSceneManager(true);
        }
    }

    //Spawn knives and decrease count
    private void SpawnKnife() {
        countKnifes--;
        Instantiate(knifeObject, new Vector2(0, -3), Quaternion.identity);
    }

    //Start Coroutine to see if win or lose
    public void StartSceneManager(bool win) {
        StartCoroutine("WinOrLose", win);
    }

    //Check if wins, go to next stage, else show game over panel
    private IEnumerator WinOrLose(bool win) {
        if (win) {
            yield return new WaitForSecondsRealtime(1f);

            scene = int.Parse(SceneManager.GetActiveScene().name)+1;
            if (scene < 6) {
                SceneManager.LoadScene(scene);
            } else {
                SceneManager.LoadScene("Win");
            }

        } else {
            scene = 1;
            ShowGameOver();
        }
    }

    public void ShowGameOver() {
        audioData.PlayOneShot(AudioGameOver);
        panelGameOver.SetActive(true);
    }
    public void PlayGame() {
        audioData.PlayOneShot(AudioButton);
        SceneManager.LoadScene(1);
    }
    public void QuitGame() {
        audioData.PlayOneShot(AudioButton);
        SceneManager.LoadScene(0);
    }
    public void CloseGame() {
        audioData.PlayOneShot(AudioButton);
        Application.Quit();
    }
    public void ChangeKnife(int skin) {
        audioData.PlayOneShot(AudioButton);
        ///knifeSkin = skin;
    }
}
