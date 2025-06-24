
using School.Core.Entities;

namespace School.Core.DTOs
{
    public class StudentReport
    {

      public string FullName { get; set; }
      public List<ResponseClass> Classes { get; set; } = new List<ResponseClass>();
      public decimal averagemarks { get; set; }
    
    }
}
