using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organiser.Model
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool Complete { get; set; }
        public TimeSpan ReminderTimeOfDay { get; set; }
        public DateTime ReminderDate { get; set; }
    }
}
