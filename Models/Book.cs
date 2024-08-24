﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2Dapper.Models
{
    internal class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public Theme Theme { get; set; }
    }
}
