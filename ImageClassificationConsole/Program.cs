using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;
using Tensorflow;

namespace ImageClassificationConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var appDir = AppContext.BaseDirectory;
                Console.WriteLine($"App base directory: {appDir}");

                // FIXED: Go up to project root from bin\Debug\net9.0\
                var projectRoot = Path.GetFullPath(Path.Combine(appDir, @"..\..\..\"));

                var modelFolder = Path.Combine(projectRoot, "4_AI", "Trained_Model", "converted_savedmodel", "model.savedmodel"); //"modeldir
                var modelFile = Path.Combine(modelFolder, "saved_model.pb"); // The actual model file
                var labelsPath = Path.Combine(projectRoot, "4_AI", "Trained_Model", "converted_savedmodel", "labels.txt");
                var imagePath = Path.Combine(projectRoot, "4_AI", "Trained_Model", "InputImages", "TUK.png");

                // Check if files exist
                Console.WriteLine($"Model folder exists: {Directory.Exists(modelFolder)}");
                Console.WriteLine($"Model file exists: {File.Exists(modelFile)}");
                Console.WriteLine($"Labels exists: {File.Exists(labelsPath)}");
                Console.WriteLine($"Image exists: {File.Exists(imagePath)}");

                if (!Directory.Exists(modelFolder) || !File.Exists(modelFile) || !File.Exists(labelsPath) || !File.Exists(imagePath))
                {
                    Console.WriteLine("One or more required files are missing. Please check the paths above.");

                    // List what's actually in the model folder
                    if (Directory.Exists(modelFolder))
                    {
                        Console.WriteLine("\nFiles in model folder:");
                        foreach (var file in Directory.GetFiles(modelFolder, "*.*", SearchOption.AllDirectories))
                        {
                            Console.WriteLine($"  {file}");
                        }
                    }
                    return;
                }



                // Initialize the classifier
                Console.WriteLine("Initializing classifier...");
                var classifier = new ImageClassifier(modelFolder, labelsPath);

                // Classify an image
                Console.WriteLine("Classifying image...");
                var result = classifier.ClassifyImage(imagePath);

                // Print results
                Console.WriteLine("\nClassification Results:");
                Console.WriteLine($"Class: {result.ClassName}");
                Console.WriteLine($"Confidence Score: {result.ConfidenceScore:P2}");
                Console.WriteLine($"Class Index: {result.ClassIndex}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
