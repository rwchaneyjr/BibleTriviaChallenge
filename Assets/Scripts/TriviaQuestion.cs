using System;

[Serializable]
public class TriviaQuestion
{
    public string id;
    public string category;
    public string difficulty;
    public string question;
    public string[] answers;
    public int correctAnswerIndex;
    public string reference;
    public string explanation;
}

[Serializable]
public class TriviaQuestionCollection
{
    public TriviaQuestion[] questions;
}
