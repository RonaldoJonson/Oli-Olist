using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

public class RegisterProduct : MonoBehaviour
{
    public TestProduct product;

    private void Start()
    {
        product.description += product.CustomDescription;
        Register(product.id, product.description);
    }

    public void Register(string id, string description)
    {
        description = description.Replace(" : ", ":");
        string[] content = description.Split(':');
        Item item = new Item();
        item.ID = id;

        for (int i = 0; i < content.Length - 1; i++)
        {
            if (i % 2 == 0)
            {
                content[i] = TextCleaner.CleanText(content[i]);
                content[i] = TextCleaner.RemovePonctuation(content[i]);

                item.AddIntent(content[i], content[i + 1]);
            }
        }

        item.SaveItem();
    }
}

public static class TextCleaner
{
    public static string CleanText(string text)
    {
        string newText = text;
        newText = newText.Replace("Modelo do", " ");
        newText = newText.Replace("Modelo da", " ");
        newText = newText.Replace("modelo do", " ");
        newText = newText.Replace("modelo da", " ");
        newText = newText.Replace("do ", "");
        newText = newText.Replace(" e ", " ");
        newText = newText.Replace("É ", " ");
        newText = newText.Replace(" a ", " ");
        newText = newText.Replace(" á ", " ");
        newText = newText.Replace(" à ", " ");
        newText = newText.Replace(" de ", " ");
        newText = newText.Replace(" do ", " ");
        newText = newText.Replace(" di ", " ");
        newText = newText.Replace(" da ", " ");
        newText = newText.Replace(" por ", " ");
        newText = newText.Replace(" com ", " ");
        newText = newText.Replace(" em ", " ");
        newText = newText.Replace("Com ", "");
        newText = newText.Replace("Numero ", " ");
        newText = newText.Replace("Nome ", " ");

        newText = newText.TrimStart(' ');
        newText = newText.TrimEnd(' ');

        return newText;
    }

    public static string RemovePonctuation(string text)
    {
        StringBuilder sbReturn = new StringBuilder();
        var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();

        foreach (char letter in arrayText)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                sbReturn.Append(letter);
        }

        return sbReturn.ToString();
    }
}
