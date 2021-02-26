﻿using CrudOperation.Interfaces;
using CrudOperation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudOperation.ViewModel;

namespace CrudOperation.Services
{
    public class FakeStudentRepository : IStudentRepository
    {
        public Dictionary<int,Student> Students { get; set; }
//public Dictionary<int, StudentVm> StudentVms { get; set; }

        public FakeStudentRepository()
        {
            Students = new Dictionary<int, Student>();
            Students.Add(1, new Student(1, "John", "jh@gmail.com", "john.jpg",Profession.Administration));
            Students.Add(2, new Student(2, "Alex", "al@gmail.com", "alex.jpg", Profession.Production));
            Students.Add(3, new Student(3, "Malina", "ma@gmail.com", "malina.jpg", Profession.Business));
            Students.Add(4, new Student(4, "Nilma", "ni@gmail.com", "nilma.jpg", Profession.Accounting));

        }

        public Dictionary<int, Student> FilterName(string searchCriteria)
        {
            Dictionary<int, Student> output = new Dictionary<int, Student>();

            foreach (var student in Students.Values)
            {
                string stName = student.Name.ToLower();

                if(stName.StartsWith(searchCriteria))
                {
                    output.Add(student.Id, student);
                }
            }
            return output;
        }

        public Dictionary<int, Student> GetAllStudents()
        {
            return Students;
        }

        public Student GetStudentById(int id)
        {
            if (Students.ContainsKey(id))
            {
                return Students[id];
            }
            return new Student();
        }

        public void UpdateStudent(Student student)
        {
            if(student != null)
            {
               // Students[student.Id].Id = student.Id;
                Students[student.Id].Name = student.Name;
                Students[student.Id].Email = student.Email;
                //Students[student.Id].Image = student.Image;
                Students[student.Id].Profession = student.Profession;
            }
        }
        public void AddStudent(Student student)
        {
            var stu = AutoIncrementId(student);
            Students.Add(student.Id, student);
        }

        public Student AutoIncrementId(Student student)
        {

            //var st = Students.Values.Max(student => student.Id) + 1;

            //OR 

            List<int> emp = new List<int>();
            foreach (var stu in Students.Values)
            {
                emp.Add(stu.Id);
            }
            if (emp.Count != 0)
            {
                int inc = emp.Max() + 1;
                student.Id = inc;
            }
            else
            {
                student.Id = 1;
            }

            return student;
        }   

        public void DeleteStudent(int id)
        {
            if (id != 0)
            {
                Students.Remove(id);
            }
        }
    }
}
