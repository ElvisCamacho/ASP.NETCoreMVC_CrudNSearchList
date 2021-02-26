using CrudOperation.Interfaces;
using CrudOperation.Models;
using CrudOperation.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace CrudOperation.Controllers
{
    public class StudentController : Controller
    {


        private IStudentRepository _studentRepo;
        private readonly IWebHostEnvironment _hostEnvironment;

        public int CountStudents { get; set; }

        public StudentController(IStudentRepository studentRepo, IWebHostEnvironment hostEnvironment)
        {
            _studentRepo = studentRepo;
            _hostEnvironment = hostEnvironment;
        }


        //private bool SpeakerExists(int id)  
        //{  
        //    return _studentRepo.GetAllStudents()
        //        .Any(e => e.Key == id);  
        //}  






        //******************************** DELETE STUDENT ********************************************''''


        public IActionResult DeleteStudent(int Id)
        {
            if (Id != 0)
            {
                _studentRepo.DeleteStudent(Id);
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }



        //******************************** EDIT & UPDATE  STUDENT ********************************************''''

        // when we click button edit , first time the page appears is EditStudent

        public IActionResult EditStudent(int id)
        {
            Student student = null;
            if (id != 0)
            {
                student = _studentRepo.GetStudentById(id);
            }
            else
            {
                return NotFound();
            }

            return View(nameof(EditStudent), student);
        }

        // This method also send Post request for editing information
        public IActionResult UpdateStudent(Student student)
        {
            try
            {
                // if model state property is valid then we create new student succesfully           
                if (ModelState.IsValid)
                {
                    _studentRepo.UpdateStudent(student);
                    return RedirectToAction(nameof(Index));
                }

                return EditStudent(student.Id);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Edit", "Could not update the Student: " + ex);
                throw;
            }
        }


        //******************************** CREATE STUDENT ********************************************''''
        public IActionResult CreateStudent(StudentVm model)
        {
            string uniqueFileName = null;
            try
            {
                if (ModelState.IsValid)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.UploadImage.FileName;
                    String filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.UploadImage.CopyTo(new FileStream(filePath, FileMode.Create));


                    Student newStudent = new Student
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Image = uniqueFileName,
                        Profession = model.Profession

                    };

                    _studentRepo.AddStudent(newStudent);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Create", "Unable to Create new Student: " + ex);
            }

            return View(model);
        }
    




//public async Task<IActionResult> CreateStudent(Student student)
//{
//    try
//    {    // if model state property is valid then we create new student succesfully           
//        if (ModelState.IsValid)
//        {


//            //adding students

//            _studentRepo.AddStudent(student);
//            return RedirectToAction(nameof(Index));
//        }
//        else
//        {
//            return View(student);
//        }
//    }
//    catch (Exception ex)
//    {
//        ModelState.AddModelError("Create", "Unable to Create new Student: " + ex);
//    }

//    return View(student);
//}
//******************************** UPLOAD Image and pass it into create action  ********************************************''''
//image upload and save image in the wwwRoot - images
//*************** Index STUDENT ********************************************''''
public IActionResult Index(string searchCriteria)
{
    var dicStudents = _studentRepo.GetAllStudents();
    // if search criteria is not empty then find the filter data
    if (!String.IsNullOrEmpty(searchCriteria))
    {
        dicStudents = _studentRepo.FilterName(searchCriteria);
    }

    int CountStudents = dicStudents.Count;

    // Before pass data into View , we VM (because contains complex data in it)
    StudentVm vm = new StudentVm()
    {
        Students = dicStudents,
        TotalCount = CountStudents,

    };
    // here we pass Student View Model
    return View(vm);
}
    }
}
