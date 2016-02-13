using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectDataAccessWebApi.Models
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public DateTime CompletedFinish { get; set; }
        public string Assignments { get; set; }
        public int Indentation { get; set; }
        public IEnumerable<PredecessorDto> Predecessors { get; set; }
    }
}