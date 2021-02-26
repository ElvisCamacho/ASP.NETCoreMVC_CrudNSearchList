using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudOperation.Interfaces;
using CrudOperation.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CrudOperationASP.NETMVCCore5.Components
{
    public class StudentCount: ViewComponent
    {
        // creata a class that much a business logic
        //2: inherit base class viwComponent

        private IStudentRepository _StudentRepo;

        public StudentCount(IStudentRepository studentRepo)
        {
            _StudentRepo = studentRepo;
        }
        // view invoke logic
        public IViewComponentResult Invoke()
        {
            var sCount = _StudentRepo.GetAllStudents().Count;
            var vm = new StudentVm
            {
                TotalCount = sCount
            };

            //3: create a view component razor page
            // 4:pass data into component
            return View(sCount);
        }



    }
}
