

[System.Serializable]
public class QuestionRequisiton
{
    public QuestionStructure[] questions;
}


[System.Serializable]
public class QuestionStructure
{
    public string pergunta_id;
    public string pergunta_texto;
    public string produto_nome;
}
