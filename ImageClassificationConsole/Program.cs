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
            application application = new application();

            application.ClassifyPicture("C:\\Users\\jakob\\Source\\Repos\\ImageClassificationConsole\\ImageClassificationConsole\\4_AI\\Trained_Model\\InputImages\\græs.png");

        }
    }
}
