using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoard : MonoBehaviour
{
    public List<QuestionStructure> questions;
    public APICommands api;

    int index;

    public void AddQuestion(QuestionStructure newQuestion)
    {
        bool duplicate = false;

        newQuestion.pergunta_texto = TextCleaner.RemovePonctuation(newQuestion.pergunta_texto);

        for (int i = 0; i < questions.Count; i++)
        {
            if(questions[i].pergunta_id == newQuestion.pergunta_id)
            {
                duplicate = true;
                break;
            }
        }

        if (!duplicate)
            questions.Add(newQuestion);
    }

    public QuestionStructure GetQuestion()
    {
        if (questions.Count <= 0)
            return null;

        QuestionStructure current = questions[index];
        return current;
    }

    public void SendResponse(string text)
    {
        api.Respond(questions[index].pergunta_id, text);
        questions.Remove(questions[index]);

        Next();
    }

    public void Next()
    {
        index++;
        if (index > questions.Count - 1)
            index = 0;
    }
}
