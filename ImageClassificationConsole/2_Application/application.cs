using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageClassificationConsole._1_Domain;
using ImageClassificationConsole._3_GateWay;

namespace ImageClassificationConsole._2_Application
{
    public class application
    {
        AbstractClassifierService abstractClassifierService = AbstractClassifierService.Create();

        public double CalculateAccuracy()
        { return 0; }

        public double CalculatePrecision()
        { return 0; }
        public double CalculateF1()
        { return 100; }
        public double CalculateRecall()
        { return 0; }
        public double SelectPicture()
        { return 0; }

        public ClassificationResult ClassifyPicture(string SelectedImagePath)
        {
            try
            {
                // Classify an image, throught abstractClassifierService, because Clean Architecture
                Console.WriteLine("Classifying image...");
                var result = abstractClassifierService.ClassifyImage(SelectedImagePath);
 
                // Print results
                Console.WriteLine("\nClassification Results:");
                Console.WriteLine($"Class: {result.ClassName}");
                Console.WriteLine($"Confidence Score: {result.ConfidenceScore:P2}");
                Console.WriteLine($"Class Index: {result.ClassIndex}");
                

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw new ApplicationException("Failed to classify image", ex);
            }
        }
    }

}

