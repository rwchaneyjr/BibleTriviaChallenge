using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class TriviaDataTests
{
    [Test]
    public void TriviaDatabaseHasUniqueFourChoiceQuestions()
    {
        var asset = Resources.Load<TextAsset>("Trivia/questions");
        Assert.That(asset, Is.Not.Null, "Trivia/questions.json is missing.");
        var data = JsonUtility.FromJson<TriviaQuestionCollection>(asset.text);
        Assert.That(data, Is.Not.Null);
        Assert.That(data.questions, Has.Length.EqualTo(5000));
        Assert.That(data.questions.Select(q => q.id).Distinct().Count(), Is.EqualTo(5000));
        Assert.That(data.questions.All(q => q.answers != null && q.answers.Length == 4), Is.True);
        Assert.That(data.questions.All(q => q.answers.Distinct().Count() == 4), Is.True);
        Assert.That(data.questions.All(q => q.correctAnswerIndex >= 0 && q.correctAnswerIndex < 4), Is.True);
    }
}
