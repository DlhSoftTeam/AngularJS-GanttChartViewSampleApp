//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectDataAccessWebApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Predecessor
    {
        public int Id { get; set; }
        public int TargetTaskId { get; set; }
        public int SourceTaskId { get; set; }
        public string DependencyType { get; set; }
    
        public virtual Task SourceTask { get; set; }
        public virtual Task TargetTask { get; set; }
    }
}
