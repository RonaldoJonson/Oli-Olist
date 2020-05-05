using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class APICommands : MonoBehaviour
{
    const string URL = "https://api-ml-test.herokuapp.com/";

    const string GetQuestionsURL = "list-questions.php";
    const string PostAwnserURL = "answer-questions.php";
    const string GetTokenURL = "token.php";

    string token;
    public QuestionBoard board;

    private void Awake()
    {
        StartCoroutine(GetToken());
    }

    IEnumerator GetToken()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post(URL + GetTokenURL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                token = www.downloadHandler.text;
            }
        }

        StartCoroutine(GetQuestions());
    }

    IEnumerator GetQuestions()
    {
        while (true)
        {
            WWWForm form = new WWWForm();
            form.AddField("access_token", token);

            using (UnityWebRequest www = UnityWebRequest.Post(URL + GetQuestionsURL, form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);

                    if (www.downloadHandler.text.Contains("{"))
                    {
                        QuestionRequisiton question = JsonUtility.FromJson<QuestionRequisiton>("{\"questions\":" + www.downloadHandler.text + "}");

                        for (int i = 0; i < question.questions.Length; i++)
                        {
                            board.AddQuestion(question.questions[i]);
                        }
                    }
                }
            }

            yield return new WaitForSeconds(15);
        }   
    }

    public void Respond(string question_id, string awnser)
    {
        StartCoroutine(RespondQuestion(question_id, awnser));
    }

    IEnumerator RespondQuestion(string question_id, string _awnser)
    {
        WWWForm form = new WWWForm();
        form.AddField("question_id", question_id);
        form.AddField("text", _awnser);
        form.AddField("access_token", token);

        Debug.Log(_awnser);
        yield return null;

        using (UnityWebRequest www = UnityWebRequest.Post(URL + PostAwnserURL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
