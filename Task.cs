using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todolist
{
    class Task
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public Boolean State { get; set; }
        public DateTimeOffset Date { get; set; }
        public String Content { get; set; }
    }
}
