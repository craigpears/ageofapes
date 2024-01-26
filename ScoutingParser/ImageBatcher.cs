using System.Drawing;
using Syncfusion.Pdf.Graphics.Images.Decoder;

namespace ScoutingParser;

public class ImageBatcher
{
    public void BatchImages(string imagesToProcessDirectory)
    {
        var croppedImagesDirectoryPath = $"{imagesToProcessDirectory}\\CroppedImages";
        var directoryInfo = new DirectoryInfo(imagesToProcessDirectory);
        var imagesToProcess = directoryInfo
            .GetFiles()
            .Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden))
            .ToList();
        
        Console.WriteLine($"{imagesToProcess.Count} found to process");
        var imageCount = 0;

        // Crop the images
        foreach (var image in imagesToProcess)
        {
            var img = new Bitmap(image.FullName);
            var cropArea = new Rectangle(1045, 270, 2208 - 1045, 540 - 270);
            var croppedImage = img.Clone(cropArea, img.PixelFormat);
            croppedImage.Save($"{croppedImagesDirectoryPath}\\{image.Name}");
            img.Dispose();
            image.Delete();
            imageCount++;
            if(imageCount % 10 == 0)
                Console.WriteLine($"{imageCount}/{imagesToProcess.Count} images processed");
        }

        var croppedImagesDirectory = new DirectoryInfo(croppedImagesDirectoryPath);
        var croppedImagesToProcess = croppedImagesDirectory
            .GetFiles()
            .Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden))   
            .ToList();
        
        var chunkSize = 50;
        var batches = croppedImagesToProcess.Select((x, i) => new { Index = i, Value = x })
            .GroupBy(x => x.Index / chunkSize)
            .Select(x => x.Select(v => v.Value).ToList())
            .ToList();



        for (var i = 0; i < batches.Count; i++)
        {
            var folderName = $"{DateTime.Now.ToString("MMMM_dd_HHmm")}_{i}";
            var batchDirectory = $"{imagesToProcessDirectory}/{folderName}/";
            Directory.CreateDirectory(batchDirectory);
            foreach (var imageFile in batches[i])
            {
                imageFile.MoveTo($"{batchDirectory}/{imageFile.Name}");
            }
        }

    }
}