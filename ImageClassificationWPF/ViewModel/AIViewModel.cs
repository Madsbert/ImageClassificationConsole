using ImageClassificationConsole._2_Application;
using ImageClassificationWPF.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImageClassificationWPF.ViewModel
{
    /// <summary>
    /// a class to interact with view
    /// </summary>
    public class AIViewModel:Bindable
    {
        private PictureInfo _currentPictureInfo;
        public PictureInfo CurrentPictureInfo
        {
            get=> _currentPictureInfo;
            set
            {
                _currentPictureInfo = value;
                propertyIsChanged();
            }
        }
        private AIStatistics _currentAIStatistics;
        public AIStatistics CurrentAIStatistics
        {
            get => _currentAIStatistics;
            set
            {
                _currentAIStatistics = value;
                propertyIsChanged();
            }
        }



        private ICommandBase _findPictureCmd = null;

        /// <summary>
        /// A class to handle the handle the button 
        /// the button to first open the file path for a picture
        /// then insert the text to the textbox
        /// then use the application to get the different variables and statistics
        /// Then make the object PictureInfo and then the AIStatistics
        /// </summary>
        public ICommandBase FindPictureCmd => _findPictureCmd ?? (
            _findPictureCmd = new FindPictureCommand(
                (obj) =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    //openFileDialog.Filter = "Image Files (.png;.jpg; .jpeg)|.png;.jpg;.jpeg|All Files (.)|.";
                    bool? succes = openFileDialog.ShowDialog();
                    if (succes == true)
                    {
                        Application application = new Application();
                        string path = openFileDialog.FileName;
                        var result = application.ClassifyPicture(path);
                        string catagoryName = result.ClassName;

                        double accuracy = application.CalculateAccuracy();
                        double recall = application.CalculateRecall(catagoryName);
                        double precision = application.CalculatePrecision(catagoryName);
                        double f1 = application.CalculateF1(catagoryName);
                        double recallMacro = application.CalculateMacroRecall();
                        double precisionMacro = application.CalculateMacroPrecision();
                        double f1Macro = application.CalculateF1(catagoryName);


                        CurrentPictureInfo = new PictureInfo(path, catagoryName, result.ConfidenceScore);
                        CurrentAIStatistics = new AIStatistics(accuracy,recall, precision, f1, recallMacro,precisionMacro,f1Macro);
                    }

                }, obj=>true)                
             );




    }
}
