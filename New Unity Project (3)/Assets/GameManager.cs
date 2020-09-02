using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Reflection;

public class GameManager : MonoBehaviour
{
    public Questions[] questions;
    private static List<Questions> unanswered;
    private Questions currentque;
    private static int score;

    [SerializeField]
    private Text QueText,opt1text,opt2text,opt3text,opt4text,Scoretxt;

    [SerializeField] private GameObject gameComplete;

    [SerializeField]
    private float timerbetweenscreen=1;

    private GameObject [] gamebutton;

  

    void Start()

    {
        gameComplete.SetActive(false);
        if (unanswered==null || unanswered.Count == 0)
        {
            unanswered = questions.ToList();

        }
        Scoretxt.text = "Score:" + score.ToString();
        GetRandomQuestion();
       
    }

    // Update is called once per frame
    void GetRandomQuestion()
    {
        int randomnum = Random.Range(0, unanswered.Count);
        currentque = unanswered[randomnum];
        QueText.text = currentque.quetext;
        opt1text.text = currentque.opt1;
        opt2text.text = currentque.opt2;
        opt3text.text = currentque.opt3;
        opt4text.text = currentque.opt4;
        

        unanswered.RemoveAt(randomnum);

    }

    public void CorrectAnswer(Button B)
    {
       Image img = GetComponent<Image>();
        string correctans = B.GetComponentInChildren<Text>().text;

        ColorBlock colors = B.GetComponent<Button>().colors;

        if (correctans== currentque.correctanswer)
        {
            Debug.Log("Opt selected correct answer"+ correctans);

            colors.normalColor = Color.green;
            colors.highlightedColor = new Color32(0, 255, 0, 0);
            B.GetComponent<Button>().colors = colors;
            B.GetComponent<Image>().color = Color.green;
            score = score + 1;
            Scoretxt.text = "Score:" + score.ToString();

        }

        else
        {
            Debug.Log("Opt selected wrong answer" + correctans);
            colors.normalColor = Color.red;
            colors.highlightedColor = new Color32(255, 0, 0, 255);
            B.GetComponent<Button>().colors = colors;
            B.GetComponent<Image>().color = Color.red;

        }
        if (unanswered.Count == 0)
        {
            gamebutton = GameObject.FindGameObjectsWithTag("Button");
            gameComplete.SetActive(true);
            QueText.enabled = false;
            opt1text.enabled = false;
            opt2text.enabled = false;
            opt3text.enabled = false;
            opt4text.enabled = false;
            foreach (GameObject b1 in gamebutton)
            {
                b1.SetActive(false);
            }


        }
        else
        {
            StartCoroutine(NextQuestion());
        }
        

    }

    IEnumerator NextQuestion()
    {
        unanswered.Remove(currentque);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

}
