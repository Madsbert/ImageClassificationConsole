using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;
using Tensorflow;
using ImageClassificationConsole._1_Domain;
using ImageClassificationConsole._4_AI;
using ImageClassificationConsole._2_Application;

namespace ImageClassificationConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            //test
            //Application application = new Application();

            //application.ClassifyPicture("C:\\Users\\jakob\\Source\\Repos\\ImageClassificationConsole\\ImageClassificationConsole\\4_AI\\Trained_Model\\InputImages\\græs.png");
            //Console.WriteLine("Total Accuracy: " + application.CalculateAccuracy());

            //Console.WriteLine("Car Recall: " + application.CalculateRecall("Car"));
            //Console.WriteLine("Truck Recall: " + application.CalculateRecall("Truck"));
            //Console.WriteLine("Motorcycle Recall: " + application.CalculateRecall("Motorcycle"));
            //Console.WriteLine("Macro-Average Recall: " + application.CalculateMacroRecall());

            //Console.WriteLine(application.CalculatePrecision("Truck"));

            //Console.WriteLine(application.CalculateF1("Truck"));
            //Console.WriteLine(application.CalculateF1("Car"));



            //Console.WriteLine("Macro F1: " + application.CalculateMacroF1());

        }
    }
}
