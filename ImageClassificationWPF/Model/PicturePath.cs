using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageClassificationWPF.Model
{
    public class PicturePath: Bindable
    {
        private string _name;
        public string Name {
            get { return _name; }
            set {  _name = value;
                propertyIsChanged();
            }
        }



        public PicturePath(string name) {
        Name=name;
        }
    }
}
