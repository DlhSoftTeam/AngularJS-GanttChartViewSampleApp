using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectDataAccessWebApi.Models
{
    public class PredecessorDto
    {
        public int SourceTaskId { get; set; }
        public string DependencyType { get; set; }
    }
}