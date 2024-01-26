using ScoutingParser;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ShouldParseSuccessfullyWhenResourceElementMissing()
    {
        // TODO: Should assert this is all zeros, 80 on its own is usually just detecting the symbol next to 0 troops as an 8
        var parser = new WebScoutingTextParser();
        var testData = System.IO.File.ReadAllLines("TestWebFiles/Gamgamstyle.txt").ToList();
        var parsedText = parser.ParseScoutingText(testData);
        
    }
    
    [Test]
    public void ShouldParseRickFile()
    {
        // TODO: Should assert this is all zeros, 80 on its own is usually just detecting the symbol next to 0 troops as an 8
        var parser = new WebScoutingTextParser();
        var testData = System.IO.File.ReadAllLines("TestWebFiles/rick.txt").ToList();
        var parsedText = parser.ParseScoutingText(testData);
        
    }
}