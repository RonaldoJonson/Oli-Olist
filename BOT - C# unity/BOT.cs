using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOT : MonoBehaviour
{
    public QuestionBoard board;
    List<string> tags = new List<string>();
    List<string> awnsers = new List<string>();

    private void Start()
    {
        StartCoroutine(GetQuestion());
    }

    IEnumerator GetQuestion()
    {
        WaitForSeconds wait = new WaitForSeconds(5);
        yield return wait;
        Item item = new Item();

        while (true)
        {
            QuestionStructure question = board.GetQuestion();
            awnsers.Clear();
            tags.Clear();
            item = new Item();

            if (question != null)
            {
                question.pergunta_texto = question.pergunta_texto.ToLower();
                question.pergunta_texto = TextCleaner.CleanText(question.pergunta_texto);

                Debug.Log("Respondendo: " + question.pergunta_texto);

                if (item.LoadItem(question.produto_nome))
                {
                    Debug.Log("Verficando Tags");

                    foreach (string key in item.intents.Keys)
                    {
                        if (question.pergunta_texto.Contains(key))
                        {
                            tags.Add(key);
                            Debug.Log("Tag Encontrada: " + key);
                            awnsers.Add(item.intents[key]);
                        }
                    }

                    if (awnsers.Count > 0)
                        CreateAwnser();
                    else
                    {
                        board.Next();
                    }
                }
                else
                {
                    Debug.Log("Item nao cadastrado");
                }

            }

            yield return wait;
        }
    }

    void CreateAwnser()
    {
        string awnserToSend = "Olá. Respondendo sua pergunta sobre \n";

        for (int i = 0; i < tags.Count; i++)
        {
            awnserToSend += "\n" + tags[i] + ": " + awnsers[i] + "";
        }

        awnserToSend += ".";

        awnserToSend += " Qualquer dúvida faça uma nova pergunta.";

        board.SendResponse(awnserToSend);
    }
}
