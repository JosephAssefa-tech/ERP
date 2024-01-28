using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.ResourceManagement.Presentation.Dtos;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Excellerent.ResourceManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EmployeePhotoController 
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IEmployeeService _employeeService;

        public EmployeePhotoController(IWebHostEnvironment environment, IEmployeeService employeeService)
        {
            _environment = environment;
            _employeeService = employeeService;
        }

        [Authorize]
        [HttpGet]
        public ResponseDTO Get(string id)
        {
            Byte[] b;
            if (id == null)
            {
                return new ResponseDTO(ResponseStatus.Error," there is no type value given",null);
            }
            else 
            {
                Employee emp = _employeeService.GetEmployeesByEmpNumber(id);
                if (emp != null)
                {
                    if (!string.IsNullOrEmpty(emp.Photo))
                    {
                        try
                        {
                            b = System.IO.File.ReadAllBytes(emp.Photo);
                        }
                        catch (Exception ex) 
                        {
                            return new ResponseDTO(ResponseStatus.Error, " there is no type value given", null);
                        }
                    }
                    else 
                    {
                        b = null;
                        return new ResponseDTO(ResponseStatus.Error, " there is no type value given", null);
                    }
                }
                else {
                    return new ResponseDTO(ResponseStatus.Error, " there is no type value given", null);
                }
            }
           
            return new ResponseDTO(ResponseStatus.Success,"image loaded", Convert.ToBase64String(b));

        }

        [Authorize]
        [HttpPost]
        public ResponseDTO Post([FromForm]EmployeeFileUpload? photo)
        {
            
            if (photo.data != null)
            {
                if (photo.data.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\EmployeesPhoto\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\EmployeesPhoto\\");
                    }


                    using (FileStream fileStream =  System.IO.File.Create(_environment.WebRootPath + "\\EmployeesPhoto\\" + photo.data.FileName))
                    {

                        photo.data.CopyTo(fileStream);
                        fileStream.Flush();
                    }

                    System.IO.FileInfo fi = new System.IO.FileInfo(_environment.WebRootPath + "\\EmployeesPhoto\\" + photo.data.FileName);
                    // Check if file is there  
                    if (fi.Exists)
                    {
                        try
                        {
                            // Move file with a new name. Hence renamed.  
                            fi.MoveTo(_environment.WebRootPath + "\\EmployeesPhoto\\" + photo.id);
                        }
                        catch (Exception ex) { return new ResponseDTO(ResponseStatus.Error, "Entry Failed", "\\EmployeesPhoto\\"); }
                    }

                    Employee emp = _employeeService.GetEmployeesByEmpNumber(photo.id);
                    if (emp != null)
                    {
                        emp.Photo = _environment.WebRootPath + "\\EmployeesPhoto\\" + photo.id;
                        _employeeService.UpdateEmployee(emp);
                    }
                    return   new ResponseDTO(ResponseStatus.Success, "Entry Succesfull", _environment.WebRootPath + "\\EmployeesPhoto\\" + photo.id);

                }
            }
                return  new ResponseDTO(ResponseStatus.Error, "Entry Failed", "\\EmployeesPhoto\\");
           
            
        }
        
     

    }
}
