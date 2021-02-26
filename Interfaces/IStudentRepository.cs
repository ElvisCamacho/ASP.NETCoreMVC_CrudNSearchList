using CrudOperation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudOperation.ViewModel;

namespace CrudOperation.Interfaces
{
   public interface IStudentRepository
    {
        public Dictionary<int, Student> GetAllStudents();
        public void AddStudent(Student student);
        public Student GetStudentById(int id);
        public void UpdateStudent(Student student);
        public void DeleteStudent(int student);

        public Dictionary<int, Student> FilterName(string searchCriteria);

    }
}
