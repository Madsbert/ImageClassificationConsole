using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageClassificationWPF.Model
{

    /// <summary>
    /// A class to know what AI statistics is with different parameters
    /// </summary>
    public class AIStatistics : Bindable
    {

        private double _accuracy;
        public double Accuracy
        {
            get { return _accuracy; }
            set
            {
                _accuracy = value;
                propertyIsChanged();
            }
        }

        private double _recall;
        public double Recall
        {
            get { return _recall; }
            set
            {
                _recall = value;
                propertyIsChanged();
            }
        }

        private double _precision;
        public double Precision
        {
            get { return _precision; }
            set
            {
                _precision = value;
                propertyIsChanged();
            }
        }

        private double _f1;
        public double F1
        {
            get { return _f1; }
            set
            {
                _f1 = value;
                propertyIsChanged();
            }
        }


        private double _recallMacro;
        public double RecallMacro
        {
            get { return _recallMacro; }
            set
            {
                _recallMacro = value;
                propertyIsChanged();
            }
        }

        private double _precisionMacro;
        public double PrecisionMacro
        {
            get { return _precisionMacro; }
            set
            {
                _precisionMacro = value;
                propertyIsChanged();
            }
        }
        private double _f1Macro;
        public double F1Macro
        {
            get { return _f1Macro; }
            set
            {
                _f1Macro = value;
                propertyIsChanged();
            }
        }


        public AIStatistics (double accuracy, double recall, double precision, double f1, double recallMacro, double precisionMacro, double f1Macro)
        {
            Accuracy = accuracy;
            Recall = recall;
            Precision = precision;
            F1 = f1;
            RecallMacro = recallMacro;
            PrecisionMacro = precisionMacro;
            F1Macro = f1Macro;

        }
    }
}
