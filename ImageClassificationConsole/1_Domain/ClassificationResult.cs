using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageClassificationConsole._1_Domain
{
    public class ClassificationResult
    {
        public string ClassName { get; set; }
        public float ConfidenceScore { get; set; }
        public int ClassIndex { get; set; }

        // Property to hold the top 3 predictions
        public List<ClassificationResult> Top3Predictions { get; set; } = new List<ClassificationResult>();
    }
}
