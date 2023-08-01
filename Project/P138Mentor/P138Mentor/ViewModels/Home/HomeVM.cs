using P138Mentor.Models;

namespace P138Mentor.ViewModels.Home
{
    public class HomeVM
    {
        public IEnumerable<Trainer> Trainers { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
