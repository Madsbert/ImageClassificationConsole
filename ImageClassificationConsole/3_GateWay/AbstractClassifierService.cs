using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageClassificationConsole._1_Domain;
using ImageClassificationConsole._4_AI;

namespace ImageClassificationConsole._3_GateWay
{
    public abstract class AbstractClassifierService
    {

        public static AbstractClassifierService Create()
        {
            return new Service();
        }

        public abstract ClassificationResult ClassifyImage(string imagePath);


        private class Service : AbstractClassifierService { 
        
        
          private readonly ImageClassifier _imageClassifier;

            public Service()
            {
                _imageClassifier = new ImageClassifier();

            }

            public override ClassificationResult ClassifyImage(string imagePath) { 
                return _imageClassifier.ClassifyImage(imagePath);
            }






        }




    }
}
