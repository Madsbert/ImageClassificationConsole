using ImageClassificationConsole._1_Domain;
using ImageClassificationConsole._3_GateWay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Tensorflow.Operations.Initializers;

namespace ImageClassificationConsole._2_Application
{
    public class Application
    {
        AbstractClassifierService _abstractClassifierService = AbstractClassifierService.Create();

        /// <summary>
        /// Calculate the accuracy of the AI model by classifying images from test folders and comparing predictions to actual labels.
        /// </summary>
        /// <returns></returns>
        public double CalculateAccuracy()
        {
            string baseDirectory = AppContext.BaseDirectory;
            var projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\"));
            string resourcesPathCar = Path.Combine(projectRoot, "Resources", "Model_Test Car");
            string resourcesPathTruck = Path.Combine(projectRoot, "Resources", "Model_Test Truck");
            string resourcesPathMotorCycle = Path.Combine(projectRoot, "Resources", "Model_Test Motorcycle");


            int carActualCount = 0;
            int truckActualCount = 0;
            int motorcycleActualCount = 0;

           

            // Get all image files from the folder
            var carImageFiles = Directory.GetFiles(resourcesPathCar, "*.*", SearchOption.AllDirectories)
                .Where(file => file.ToLower().EndsWith(".jpg") ||
                      file.ToLower().EndsWith(".jpeg") ||
                      file.ToLower().EndsWith(".png"))
                .ToArray();

            // Classify each image and calculate accuracy


            foreach (var imagePath in carImageFiles)
            {
                try
                {
                    var result = _abstractClassifierService.ClassifyImage(imagePath);
                    if (result.ClassName == "Car")
                    {
                        carActualCount++;
                    }
                    Console.WriteLine("-----------New-----------");
                    Console.WriteLine($"Image: {Path.GetFileName(imagePath)} - Predicted: {result.ClassName}");
                    Console.WriteLine("Classification Results:");
                    Console.WriteLine($"Class: {result.ClassName}");
                    Console.WriteLine($"Confidence Score: {result.ConfidenceScore:P2}");
                    Console.WriteLine($"Class Index: {result.ClassIndex}");
                    Console.WriteLine("-----------Stop-----------");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {imagePath}: {ex.Message}");
                }
            }
            

            // Get all image files from the folder
            var TruckImageFiles = Directory.GetFiles(resourcesPathTruck, "*.*", SearchOption.AllDirectories)
                .Where(file => file.ToLower().EndsWith(".jpg") ||
                      file.ToLower().EndsWith(".jpeg") ||
                      file.ToLower().EndsWith(".png"))
                .ToArray();

            // Classify each image and calculate accuracy


            foreach (var imagePath in TruckImageFiles)
            {
                try
                {
                    var result = _abstractClassifierService.ClassifyImage(imagePath);
                    if (result.ClassName == "Truck")
                    {
                        truckActualCount++;
                    }
                    Console.WriteLine("-----------New-----------");
                    Console.WriteLine($"Image: {Path.GetFileName(imagePath)} - Predicted: {result.ClassName}");
                    Console.WriteLine("Classification Results:");
                    Console.WriteLine($"Class: {result.ClassName}");
                    Console.WriteLine($"Confidence Score: {result.ConfidenceScore:P2}");
                    Console.WriteLine($"Class Index: {result.ClassIndex}");
                    Console.WriteLine("-----------Stop-----------");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {imagePath}: {ex.Message}");
                }
            }
            


            // Get all image files from the folder
            var MotorcycleImageFiles = Directory.GetFiles(resourcesPathMotorCycle, "*.*", SearchOption.AllDirectories)
                .Where(file => file.ToLower().EndsWith(".jpg") ||
                      file.ToLower().EndsWith(".jpeg") ||
                      file.ToLower().EndsWith(".png"))
                .ToArray();

            // Classify each image and calculate accuracy


            foreach (var imagePath in MotorcycleImageFiles)
            {
                try
                {
                    var result = _abstractClassifierService.ClassifyImage(imagePath);
                    if (result.ClassName == "Motorcycle")
                    {
                        motorcycleActualCount++;
                    }
                    Console.WriteLine("-----------New-----------");
                    Console.WriteLine($"Image: {Path.GetFileName(imagePath)} - Predicted: {result.ClassName}");
                    Console.WriteLine("Classification Results:");
                    Console.WriteLine($"Class: {result.ClassName}");
                    Console.WriteLine($"Confidence Score: {result.ConfidenceScore:P2}");
                    Console.WriteLine($"Class Index: {result.ClassIndex}");
                    Console.WriteLine("-----------Stop-----------");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {imagePath}: {ex.Message}");
                }
            }

            double carRsult = (double)carActualCount / carImageFiles.Length;
            double truckRsult = (double)truckActualCount / TruckImageFiles.Length;
            double motorcycleRsult = (double)motorcycleActualCount / MotorcycleImageFiles.Length;

            Console.WriteLine("Accuracy Car: " + carRsult);
            Console.WriteLine("Accuracy Truck: " + truckRsult);
            Console.WriteLine("Accuracy Motorcycle: " + motorcycleRsult);

            double totalAccuracy = (carRsult + truckRsult + motorcycleRsult) / 3;
            Console.WriteLine("Total Accuracy: " + totalAccuracy);

            return totalAccuracy;
        }

        /// <summary>
        /// Calculate the recall for a specific class by classifying images from the corresponding test folder and comparing predictions to actual labels.
        /// recall = true positive / (true positive + false negative)
        /// </summary>
        /// <param name="selectedClassName"></param>
        /// <returns></returns>
        public double CalculateRecall(string selectedClassName)
        {
            double recall = 0;
            int truePositive = 0;
            int falseNegatives = 0;


            string baseDirectory = AppContext.BaseDirectory;
            var projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\"));
            string resourcesPathCar = Path.Combine(projectRoot, "Resources", $"Model_Test {selectedClassName}");
            

            var ImageFiles = Directory.GetFiles(resourcesPathCar, "*.*", SearchOption.AllDirectories)
                .Where(file => file.ToLower().EndsWith(".jpg") ||
                      file.ToLower().EndsWith(".jpeg") ||
                      file.ToLower().EndsWith(".png"))
                .ToArray();

            foreach (var imagePath in ImageFiles)
            {
                try
                {
                    var result = _abstractClassifierService.ClassifyImage(imagePath);
                    if (result.ClassName == selectedClassName)
                    {
                        truePositive++;
                    }
                    else
                    {
                        falseNegatives++;
                    }   
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {imagePath}: {ex.Message}");
                }
            }
            Console.WriteLine("TP: " + truePositive);
            Console.WriteLine("FP: " + falseNegatives);

            recall = (double)truePositive / (truePositive + falseNegatives);
            return recall;
        }
        /// <summary>
        /// Average recall across all classes (Car, Truck, Motorcycle)
        /// </summary>
        /// <returns></returns>
        public double CalculateMacroRecall()
        {
            return (CalculateRecall("Car") + CalculateRecall("Truck") + CalculateRecall("Motorcycle")) / 3;
        }
        /// <summary>
        /// Calculate the precision for a specific class by classifying images from the corresponding test folder and comparing predictions to actual labels.
        /// precision = true positive / (true positive + false positive)
        /// </summary>
        /// <param name="selectedClassName"></param>
        /// <returns></returns>
        public double CalculatePrecision(string selectedClassName)
        {

            string className1;
            string className2;
            

            if (selectedClassName == "Car")
            {
                className1 = "Truck";
                className2 = "Motorcycle"; 
            }
            else if (selectedClassName == "Truck")
            {
                className1 = "Car";
                className2 = "Motorcycle";
            }
            else
            {
                className1 = "Car";
                className2 = "Truck";
            }

            double precision = 0;
            int truePositive = 0;
            int falsePositive = 0;


            string baseDirectory = AppContext.BaseDirectory;
            var projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\"));

            //root Path to resources folder for selcted class
            string resourcesPath = Path.Combine(projectRoot, "Resources", $"Model_Test {selectedClassName}");
            // Get all image files from the selected folder, to find true positive (+ false negative)
            var selecetedImageFilePaths = Directory.GetFiles(resourcesPath, "*.*", SearchOption.AllDirectories)
                .Where(file => file.ToLower().EndsWith(".jpg") ||
                      file.ToLower().EndsWith(".jpeg") ||
                      file.ToLower().EndsWith(".png"))
                .ToArray();

            // Get all image files from the other two folders, to find false positives
            string resourcesPathClass1 = Path.Combine(projectRoot, "Resources", $"Model_Test {className1}");
            var class1ImageFilesPaths = Directory.GetFiles(resourcesPathClass1, "*.*", SearchOption.AllDirectories)
                .Where(file => file.ToLower().EndsWith(".jpg") ||
                      file.ToLower().EndsWith(".jpeg") ||
                      file.ToLower().EndsWith(".png"))
                .ToArray();

            string resourcesPathClass2 = Path.Combine(projectRoot, "Resources", $"Model_Test {className2}");
             var class2ImageFilePaths = Directory.GetFiles(resourcesPathClass2, "*.*", SearchOption.AllDirectories)
                .Where(file => file.ToLower().EndsWith(".jpg") ||
                      file.ToLower().EndsWith(".jpeg") ||
                      file.ToLower().EndsWith(".png"))
                .ToArray();

            //combine the two other class image paths
            List<string> allOtherImageFiles = new List<string>();
            allOtherImageFiles.AddRange(class1ImageFilesPaths);
            allOtherImageFiles.AddRange(class2ImageFilePaths);

            //finding true posives
            foreach (var imagePath in selecetedImageFilePaths)
            {
                try
                {
                    var result = _abstractClassifierService.ClassifyImage(imagePath);
                    if (result.ClassName == selectedClassName)
                    {
                        truePositive++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {imagePath}: {ex.Message}");
                }
            }
            Console.WriteLine("TP: " + truePositive);

            //finding false positives
            foreach (var imagePath in allOtherImageFiles)
            {
                try
                {
                    var result = _abstractClassifierService.ClassifyImage(imagePath);
                    if (result.ClassName == selectedClassName)
                    {
                        falsePositive++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {imagePath}: {ex.Message}");
                }
            }

            Console.WriteLine("FP: " + falsePositive);

            precision = (double)truePositive / (truePositive + falsePositive);

            return precision;
        }

        /// <summary>
        /// Average precision across all classes (Car, Truck, Motorcycle)
        /// </summary>
        /// <returns></returns>
        public double CalculateMacroPrecision()
        {
            return (CalculatePrecision("Car") + CalculatePrecision("Truck") + CalculatePrecision("Motorcycle")) / 3;
        }

        /// <summary>
        /// Calculate the F1 score for a specific class using precision and recall.
        /// </summary>
        /// <param name="selectedClassName"></param>
        /// <returns></returns>
        public double CalculateF1(string selectedClassName)
        {
            double f1 = 0;
            double precision = CalculatePrecision(selectedClassName);
            double recall = CalculateRecall(selectedClassName);

            f1 = 2 * (precision * recall) / (precision + recall);

            return f1;
        }

        /// <summary>
        /// average F1 score across all classes (Car, Truck, Motorcycle)
        /// </summary>
        /// <returns></returns>
        public double CalculateMacroF1()
        {
            return (CalculateF1("Car") + CalculateF1("Truck") + CalculateF1("Motorcycle")) / 3;
        }

        /// <summary>
        /// Classifies the image at the specified file path and returns the classification result.
        /// </summary>
        /// <remarks>This method uses an abstract classification service to analyze the image and
        /// determine its classification.  Ensure that the provided image path is accessible and points to a valid image
        /// file.</remarks>
        /// <param name="SelectedImagePath">The file path of the image to classify. This must be a valid path to an image file.</param>
        /// <returns>A <see cref="ClassificationResult"/> object containing the classification details, including the class name,
        /// confidence score, and class index.</returns>
        /// <exception cref="ApplicationException">Thrown if an error occurs during the classification process.</exception>
        public ClassificationResult ClassifyPicture(string SelectedImagePath)
        {
            try
            {
                // Classify an image, throught abstractClassifierService, because Clean Architecture
                Console.WriteLine("Classifying image...");
                var result = _abstractClassifierService.ClassifyImage(SelectedImagePath);
 
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

