using System;
using System.Collections.Generic;
using System.Text;

namespace Organiser.Data
{
    public interface IMessenger
    {
        void ShortAlert(string message);
        void LongAlert(string message);
    }
}
