using System;
using System.Collections.Generic;
using System.Text;

namespace Organiser.Data
{
    interface IFileHelper
    {
        string GetLocalDataBaseFilePath(string filename);
    }
}
