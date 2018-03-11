using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Organiser.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool Complete { get; set; }
    }
}
