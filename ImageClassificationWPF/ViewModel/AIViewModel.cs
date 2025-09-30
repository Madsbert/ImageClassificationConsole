using ImageClassificationWPF.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImageClassificationWPF.ViewModel
{
    public class AIViewModel:Bindable
    {

        
        private PicturePath _currentPath;
        public PicturePath CurrentPath
        {
            get=> _currentPath;
            set
            {
                _currentPath = value;
                propertyIsChanged();
            }
        }



        private ICommandBase _findPictureCmd = null;

        public ICommandBase FindPictureCmd => _findPictureCmd ?? (
            _findPictureCmd = new FindPictureCommand(
                (obj) =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Image Files (.png;.jpg; .jpeg)|.png;.jpg;.jpeg|All Files (.)|.";
                    bool? succes = openFileDialog.ShowDialog();
                    if (succes == true)
                    {
                        string path = openFileDialog.FileName;
                        CurrentPath = new PicturePath(path);
                    }

                }, obj=>true)                
             );




    }
}
