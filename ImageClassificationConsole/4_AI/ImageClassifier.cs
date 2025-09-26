using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using ImageClassificationConsole._1_Domain;


namespace ImageClassificationConsole._4_AI
{

    /// <summary>
    /// Image classification service using ML.NET and TensorFlow model
    /// Provides functionality to classify images using a pre-trained TensorFlow model
    /// </summary>
    public class ImageClassifier
    {
        private readonly MLContext _mlContext;
        private readonly PredictionEngine<ImageInput, ImagePrediction> _predictionEngine;
        private readonly string[] _labels;


        /// <summary>
        /// Initializes a new instance of the ImageClassifier class
        /// </summary>
        /// <param name="modelPath">Path to the directory containing the TensorFlow model files</param>
        /// <param name="labelsPath">Path to the text file containing class labels (one per line)</param>
        public ImageClassifier(string modelPath, string labelsPath)
        {
            _mlContext = new MLContext();
            _labels = File.ReadAllLines(labelsPath);

            // Validate that model directory exists
            if (!Directory.Exists(modelPath))
            {
                throw new DirectoryNotFoundException($"Model folder not found: {modelPath}");
            }

            // Load TensorFlow model
            var tensorFlowModel = _mlContext.Model.LoadTensorFlowModel(modelPath);

            // Define the image processing and model scoring pipeline
            var pipeline = _mlContext.Transforms.LoadImages(
                    outputColumnName: "serving_default_sequential_1_input", 
                    imageFolder: "",
                    inputColumnName: nameof(ImageInput.ImagePath))
                .Append(_mlContext.Transforms.ResizeImages(
                    outputColumnName: "serving_default_sequential_1_input",
                    imageWidth: 224,
                    imageHeight: 224,
                    inputColumnName: "serving_default_sequential_1_input"))
                .Append(_mlContext.Transforms.ExtractPixels(
                    outputColumnName: "serving_default_sequential_1_input",
                    inputColumnName: "serving_default_sequential_1_input",
                    scaleImage: 1f / 127.5f,
                    offsetImage: -1f))
                .Append(tensorFlowModel.ScoreTensorFlowModel(
                    outputColumnNames: new[] { "StatefulPartitionedCall" }, // Model output node
                    inputColumnNames: new[] { "serving_default_sequential_1_input" }, // Model input node
                    addBatchDimensionInput: false)); // No batch dimension for single predictions

            // Create an empty dataset to fit the pipeline
            var emptyData = _mlContext.Data.LoadFromEnumerable(new List<ImageInput>());
           
            // Fit the pipeline to create the model
            var model = pipeline.Fit(emptyData);

            // Create prediction engine for making individual predictions
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<ImageInput, ImagePrediction>(model);


        }

        /// <summary>
        /// Classifies an image and returns the prediction results
        /// </summary>
        /// <param name="imagePath">Full path to the image file to classify</param>
        /// <returns>ClassificationResult containing class name, confidence score, and class index</returns>
        /// <exception cref="FileNotFoundException">Thrown when image file is not found</exception>
        public ClassificationResult ClassifyImage(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException($"Image file not found: {imagePath}");
            }

            // Create input data
            var input = new ImageInput { ImagePath = imagePath };

            // Make prediction
            var prediction = _predictionEngine.Predict(input);

            // Get prediction scores (probabilities for each class)
            var scores = prediction.PredictedLabels ?? Array.Empty<float>();

            // Find the highest confidence score
            var maxScore = scores.DefaultIfEmpty().Max();

            var maxIndex = Array.IndexOf(scores, maxScore);

            return new ClassificationResult
            {
                ClassName = (_labels.Length > maxIndex && maxIndex >= 0) ? _labels[maxIndex].Trim() : "Unknown",
                ConfidenceScore = maxScore,
                ClassIndex = maxIndex
            };
        }
    }
}






