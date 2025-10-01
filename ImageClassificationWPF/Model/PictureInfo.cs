using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageClassificationWPF.Model
{
    public class PictureInfo: Bindable
    {
        private string _path;
        public string Path {
            get { return _path; }
            set {  _path = value;
                propertyIsChanged();
            }
        }

        private string _category;
        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                propertyIsChanged();
            }
        }

        private float _score;
        public float Score
        {
            get { return _score; }
            set
            {
                _score = value;
                propertyIsChanged();
            }

        }




        public PictureInfo(string path, string category, float score)
        {
            Path = path;
            Category = category;
            Score = score;
        }
    }
}
