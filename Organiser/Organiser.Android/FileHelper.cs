﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Organiser.Data;
using Organiser.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Organiser.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalDataBaseFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}