// See https://aka.ms/new-console-template for more information

using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using ScoutingParser;
using Syncfusion.Drawing;
using Tesseract;

var baseDirectory = @"\\nas-pears\documents\AgeOfApes\ScoutingScreenshots\";
var filesDirectory = $"{baseDirectory}\\FilesToProcess";
var resultsDirectory = $"{filesDirectory}\\ToProcess";
var errorsDirectory = $"{filesDirectory}\\Errors";
var imagesToProcessDirectory = $"{baseDirectory}\\ImagesToProcess";


var sb = new StringBuilder();

Console.WriteLine("Organising images to process into batches");
var batcher = new ImageBatcher();
batcher.BatchImages(imagesToProcessDirectory);

Console.WriteLine("Parsing images");
using var engine = new TesseractEngine(@"./tessdata",
    "eng", EngineMode.Default,
    Array.Empty<string>(), 
    new Dictionary<string, object>()
    {
        { "tessedit_char_whitelist", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789[]:, "}
    }, 
    false);

var tesseractTextParser = new TesseractScoutingTextParser();
var imagesDirectory = new DirectoryInfo(imagesToProcessDirectory);
var imagesToProcess = imagesDirectory.GetFiles().Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden));
var imagesErrorFolderPath = $"{imagesToProcessDirectory}\\Errors";
var textOutputFolder = $"{imagesToProcessDirectory}\\TextOutput";
foreach (var image in imagesToProcess)
{
    
    try
    {

        #region tesseract

        /*
        using var img = Pix.LoadFromFile(image.FullName);
        using var page = engine.Process(img);
        var text = page.GetText();
        
        File.WriteAllText($"{textOutputFolder}\\{image.Name}.txt", text);
        Console.WriteLine(text.Replace("\n", ""));
        var lines = text.Split("\n").ToList();
        var scoutingEvent = tesseractTextParser.ParseScoutingText(lines);
        sb.Append($"{scoutingEvent.xCoordinates},{scoutingEvent.yCoordinates},{scoutingEvent.FoodAmount},{scoutingEvent.IronAmount},{scoutingEvent.ArmyCount},{scoutingEvent.ClanName},{scoutingEvent.PlayerName}\r\n");
        */

        #endregion
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message); 
        image.MoveTo($"{imagesErrorFolderPath}\\{image.Name}");
    }
    

}

// TODO: Pick up files from the download folder matching the right name pattern.  Also unzip and copy them.
Console.WriteLine("Checking for downloaded files");
var downloadsDirectory = new DirectoryInfo(@"C:\users\craig\Downloads");
var downloadedFiles = downloadsDirectory
    .GetFiles()
    .Where(x => x.Name.Contains("file") && x.Name.Contains("zip"))
    .ToList();

foreach (var downloadZip in downloadedFiles)
{
    Console.WriteLine($"Extracting {downloadZip.FullName}");
    ZipFile.ExtractToDirectory(downloadZip.FullName, $"{filesDirectory}\\ToProcess\\{downloadZip.Name}");
    File.Delete(downloadZip.FullName);
}

var directories = Directory.GetDirectories(resultsDirectory);

Console.WriteLine("Checking for text files to process");
foreach (var directory in directories)
{
    var files = Directory.GetFiles(directory);
    foreach (var file in files)
    {
        try
        {
            var textLines = File.ReadLines(file).ToList();
            var parser = new WebScoutingTextParser();
            var scoutingEvent = parser.ParseScoutingText(textLines);
            sb.Append(scoutingEvent.ToOutputLine());

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            File.Move(file, $"{errorsDirectory}/error_{DateTime.Now.Ticks}");
        }
    }

}


// TODO: Would be nice to have a similar program that scans for names of known players in screenshots that have moved from their previous co-ordinates, or even use a crawler of some kind that does this automatically
// TODO: Move files from ToProcess to Processed folder

var output = sb.ToString();
Console.Write(output);

if (!string.IsNullOrEmpty(output))
{
    File.WriteAllText($"{filesDirectory}\\output\\{DateTime.Now.ToString("ddMMyyyy_mmss")}.csv", sb.ToString());
}

foreach (var directory in directories)
{
    System.IO.Directory.Delete(directory, true);
}