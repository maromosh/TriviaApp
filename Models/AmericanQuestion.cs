using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaAppClean.Models
{
    public class AmericanQuestion
    {
        public string QText { get; set; }
        public string CorrectAnswer { get; set; }
        public string Bad1 { get; set; }
        public string Bad2 { get; set; }
        public string Bad3 { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
        public int Status { get; set; }
    }

}
