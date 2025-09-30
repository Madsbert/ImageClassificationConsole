using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageClassificationWPF.ViewModel
{
    class FindPictureCommand : ICommandBase
    {
        public FindPictureCommand(Action<object> executeAction, Func<object, bool> canExecuteAction) : base(executeAction, canExecuteAction) { }
    }
}
