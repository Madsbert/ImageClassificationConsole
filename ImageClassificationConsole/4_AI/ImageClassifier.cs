using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;


public class ImageClassifier
{
    private readonly MLContext _mlContext;
    private readonly PredictionEngine<ImageInput, ImagePrediction> _predictionEngine;
    private readonly string[] _labels;

    public ImageClassifier(string modelPath, string labelsPath)
    {
        _mlContext = new MLContext();
        _labels = File.ReadAllLines(labelsPath);

        // Optional: log TensorFlow details
        _mlContext.Log += (sender, e) => Console.WriteLine($"[ML.NET] {e.Message}");

        if (!Directory.Exists(modelPath))
        {
            throw new DirectoryNotFoundException($"Model folder not found: {modelPath}");
        }

        // Load TensorFlow model
        var tensorFlowModel = _mlContext.Model.LoadTensorFlowModel(modelPath);

        // Debug: print model input/output schema
        //PrintModelSchema(tensorFlowModel);

        var pipeline = _mlContext.Transforms.LoadImages(
                outputColumnName: "serving_default_sequential_1_input", // <-- replace with actual input name from schema
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
                outputColumnNames: new[] { "StatefulPartitionedCall" },   // <-- replace with actual output name
                inputColumnNames: new[] { "serving_default_sequential_1_input" },    // <-- replace with actual input name
                addBatchDimensionInput: false));

        var emptyData = _mlContext.Data.LoadFromEnumerable(new List<ImageInput>());
        var model = pipeline.Fit(emptyData);
        _predictionEngine = _mlContext.Model.CreatePredictionEngine<ImageInput, ImagePrediction>(model);


    }

    private void PrintModelSchema(TensorFlowModel model)
    {
        var schema = model.GetModelSchema();
        Console.WriteLine("===== TensorFlow Model Schema =====");
        foreach (var col in schema)
        {
            Console.WriteLine($"Name: {col.Name}, Type: {col.Type}");
        }
        Console.WriteLine("===================================");


    }

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


        // DEBUG: Check the prediction object
        Console.WriteLine($"Prediction object null: {prediction == null}");
        Console.WriteLine($"PredictedLabels null: {prediction.PredictedLabels == null}");


        var scores = prediction.PredictedLabels ?? Array.Empty<float>();
        Console.WriteLine($"Scores array length: {scores.Length}");

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

// Input data class
public class ImageInput
{
    public string ImagePath { get; set; }
}

// Prediction output class
public class ImagePrediction
{
    [VectorType] // don’t hardcode size, let ML.NET infer it
    [ColumnName("StatefulPartitionedCall")]
    public float[] PredictedLabels { get; set; }
}

// Result class
public class ClassificationResult
{
    public string ClassName { get; set; }
    public float ConfidenceScore { get; set; }
    public int ClassIndex { get; set; }
}
