using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace ImageClassificationConsole._1_Domain
{
    // Prediction output class
    public class ImagePrediction
    {
        [VectorType] // don’t hardcode size, let ML.NET infer it
        [ColumnName("StatefulPartitionedCall")]
        public float[] PredictedLabels { get; set; }
    }
}
