using Excellerent.APIModularization.Providers;
using Excellerent.TestData.ResourceManagement;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.TestData.UserManagement
{
    class UserManagementTestData
    {
        private static List<Permission> permissions = new List<Permission>()
        {
            //Timesheet Api Controller
            new Permission(){Guid = Guid.NewGuid(), Level= "0", Name =ActionNames.AddTimeEntry, KeyValue ="Timesheet_Module", PermissionCode ="001", ParentCode="",IsDeleted = false},
            new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.AddTimeEntry, KeyValue ="Submit_Timesheet", PermissionCode ="00101", ParentCode="001",IsDeleted = false},
            new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.GetTimesheet, KeyValue ="View_Timesheet", PermissionCode ="00102", ParentCode="001",IsDeleted = false},
            new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.UpdateTimeEntry, KeyValue ="Edit_TimeEntry", PermissionCode ="00103", ParentCode="001",IsDeleted = false},
            new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.GetTimeEntries, KeyValue ="Re-submit_Timesheet", PermissionCode ="00104", ParentCode="001",IsDeleted = false},
            new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.GetApprovalStatus, KeyValue ="View_Timesheet_Submissions", PermissionCode ="00105", ParentCode="001",IsDeleted = false},
            new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.DeleteTimeEntry, KeyValue ="Delete_TimeEntry", PermissionCode ="00106", ParentCode="001",IsDeleted = false},
            new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.AddApprovalStatus, KeyValue ="Approve_Timesheet", PermissionCode ="00107", ParentCode="001",IsDeleted = false},
            new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.RejectTimeSheet, KeyValue ="Return_for_Review", PermissionCode ="00108", ParentCode="001",IsDeleted = false},
            new Permission(){Guid = Guid.NewGuid(), Level= "1", Name ="Admin", KeyValue ="Timesheet_Admin", PermissionCode ="00109", ParentCode="001"},

            //project management Module
            new Permission(){Guid = Guid.NewGuid(), Level= "0", Name ="Project", KeyValue ="Project_Module", PermissionCode ="002", ParentCode="",IsDeleted = false},
            new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.Add, KeyValue ="Create_Project", PermissionCode ="00201", ParentCode="002",IsDeleted = false},
            new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.GetAll, KeyValue ="View_Project", PermissionCode ="00202", ParentCode="002",IsDeleted = false},
            new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.Edit, KeyValue ="Update_Project", PermissionCode ="00203", ParentCode="002",IsDeleted = false},
            new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.Delete, KeyValue ="Delete_Project", PermissionCode ="00204", ParentCode="002",IsDeleted = false},
            new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.AssignResource, KeyValue ="Assign_Resource", PermissionCode ="00208", ParentCode="002",IsDeleted = false},
            new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.Get, KeyValue ="View_Resources", PermissionCode ="00209", ParentCode="002",IsDeleted = false},
            new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.UpdateAssignResource, KeyValue ="Update_Resources", PermissionCode ="00210", ParentCode="002"},
            new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.DeleteAssignResource, KeyValue ="Delete_Resources", PermissionCode ="00211", ParentCode="002"},
            new Permission(){Guid = Guid.NewGuid(), Level= "1", Name ="Admin", KeyValue ="Projects_Admin", PermissionCode ="00212", ParentCode="002",IsDeleted = false},

            
            //Resource Management
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "0", Name =ActionNames.EmployeeAdmin, KeyValue ="Employee_Module", PermissionCode ="003", ParentCode=""},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.CreateEmployee, KeyValue ="Create_Employee", PermissionCode ="00301", ParentCode="003"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.ViewEmployee, KeyValue ="View_Employee", PermissionCode ="00302", ParentCode="003"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.UpdateEmployee, KeyValue ="Update_Employee", PermissionCode ="00303", ParentCode="003"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.CreateMyProfile, KeyValue ="Delete_Employee", PermissionCode ="00304", ParentCode="003"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.UpdateMyProfile, KeyValue ="Update_My_Profile", PermissionCode ="00306", ParentCode="003"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name ="Admin", KeyValue ="Employee_Admin", PermissionCode ="00307", ParentCode="003"},

            //User Management
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "0", Name =ActionNames.UserManagementAdmin, KeyValue ="User_Module", PermissionCode ="004", ParentCode=""},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.CreateGroup, KeyValue ="Create_Group", PermissionCode ="00401", ParentCode="004"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.ViewGroup, KeyValue ="View_Group", PermissionCode ="00402", ParentCode="004"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.UpdateGroup, KeyValue ="Update_Group", PermissionCode ="00403", ParentCode="004"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.DeleteUser, KeyValue ="Delete_Group", PermissionCode ="00404", ParentCode="004"},

            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.AddUser, KeyValue ="Add_User", PermissionCode ="00405", ParentCode="004"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.ViewUser, KeyValue ="View_User", PermissionCode ="00406", ParentCode="004"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.UpdateUser, KeyValue ="Update_User", PermissionCode ="00407", ParentCode="004"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.DeleteUser, KeyValue ="Delete_User", PermissionCode ="00408", ParentCode="004"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name ="Admin", KeyValue ="User_Management_Admin", PermissionCode ="00409", ParentCode="004"},

            //Client Management
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "0", Name =ActionNames.UserManagementAdmin, KeyValue ="Client_Module", PermissionCode ="005", ParentCode=""},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.CreateGroup, KeyValue ="Create_Client", PermissionCode ="00501", ParentCode="005"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.UpdateGroup, KeyValue ="View_Client", PermissionCode ="00502", ParentCode="005"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.ViewGroup, KeyValue ="Update_Client", PermissionCode ="00503", ParentCode="005"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.AddUser, KeyValue ="Delete_Client", PermissionCode ="00504", ParentCode="005"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name ="Admin", KeyValue ="Client_Admin", PermissionCode ="00505", ParentCode="005"},
                
            //Configuration Management
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "0", Name =ActionNames.UserManagementAdmin, KeyValue ="Configuration_Module", PermissionCode ="006", ParentCode=""},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.CreateGroup, KeyValue ="Create_Department", PermissionCode ="00601", ParentCode="006"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.UpdateGroup, KeyValue ="View_Department", PermissionCode ="00602", ParentCode="006"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.ViewGroup, KeyValue ="Update_Department", PermissionCode ="00603", ParentCode="006"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.AddUser, KeyValue ="Delete_Department", PermissionCode ="00604", ParentCode="006"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.ViewUser, KeyValue ="Create_Job_Title", PermissionCode ="00605", ParentCode="006"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.ViewGroup, KeyValue ="View_Job_Title", PermissionCode ="00606", ParentCode="006"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.CreateGroup, KeyValue ="Update_Job_Title", PermissionCode ="00607", ParentCode="006"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.UpdateGroup, KeyValue ="Delete_Job_Title", PermissionCode ="00608", ParentCode="006"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.ViewUser, KeyValue ="View_Timesheet_Configuration", PermissionCode ="00609", ParentCode="006"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.ViewUser, KeyValue ="Update_Timesheet_Configuration", PermissionCode ="00610", ParentCode="006"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name ="Admin", KeyValue ="Configuration_Admin", PermissionCode ="00611", ParentCode="006"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.AddUser, KeyValue = "Create_Country", PermissionCode = "00612", ParentCode="006"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.ViewUser, KeyValue = "Delete_Country", PermissionCode = "00613", ParentCode="006"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.ViewUser, KeyValue = "Update_Country", PermissionCode = "00614", ParentCode="006"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.ViewUser, KeyValue = "View_Country", PermissionCode = "00615", ParentCode="006"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.AddUser, KeyValue = "Create_DutyStation", PermissionCode = "00616", ParentCode="006"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.ViewUser, KeyValue = "Update_DutyStation", PermissionCode = "00617", ParentCode="006"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.ViewUser, KeyValue = "Delete_DutyStation", PermissionCode = "00618", ParentCode="006"},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.ViewUser, KeyValue = "View_DutyStation", PermissionCode = "00619", ParentCode="006"},

            //Reports module
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "0", Name =ActionNames.ReportsAdmin, KeyValue ="Reports", PermissionCode ="007", ParentCode=""},
            new Permission(){Guid = Guid.NewGuid(),IsDeleted = false, Level= "1", Name =ActionNames.ViewReports, KeyValue ="View_Monthly_Timesheet_Report", PermissionCode ="00701", ParentCode="007"}
        };
        private static User user = new User()
        {
            Guid = Guid.NewGuid(),
            UserName = EmployeeTestData._sampleEmployees[0].ToString().Split(" ")[0], // EmployeeTestData._sampleEmployees[0].FirstName,
            Email = EmployeeTestData._sampleEmployees[0].EmployeeOrganization.CompaynEmail,
            Password = "AC3lAGSFr3KkOrgakON2f1Yd95JSFQqR2eAHSP3ZimA=:FRfxP8AB/gOcjKkjNRgiyA==",
            Tel = EmployeeTestData._sampleEmployees[0].Phone1,
            // FirstName = EmployeeTestData._sampleEmployees[0].FirstName,
            // MiddleName = EmployeeTestData._sampleEmployees[0].FatherName,
            // LastName = EmployeeTestData._sampleEmployees[0].GrandFatherName,
            FullName = EmployeeTestData._sampleEmployees[0].FullName,
            Status = 0,
            EmployeeId = EmployeeTestData._sampleEmployees[0].Guid,
            IsActive = true,
            CreatedDate = DateTime.Now,
            IsDeleted = false,
            CreatedbyUserGuid = Guid.Empty
        };
        private static GroupSet groupSet = new GroupSet()
        {
            Guid = Guid.NewGuid(),
            Name = "Admin",
            Description = "Administrator",
            IsActive = true,
            CreatedDate = DateTime.Now,
            IsDeleted = false,
            CreatedbyUserGuid = Guid.Empty

        };
        private static UserGroups userGroups = new UserGroups()
        {
            Guid = Guid.NewGuid(),
            GroupSetGuid = groupSet.Guid,
            UserGuid = user.Guid,
            IsActive = true,
            CreatedDate = DateTime.Now,
            IsDeleted = false,
            CreatedbyUserGuid = Guid.Empty
        };

        public static async Task Add(
            IUserRepository userRepository,
            IGroupSetRepository groupSetRepository,
            IUserGroupsRepository userGroupsRepository,
            IPermissionRepository permissionRepository,
            IGroupSetPermissionRepository groupSetPermissionRepository
        ) 
        {
            await userRepository.AddAsync(user);
            await groupSetRepository.AddAsync(groupSet);
            await userGroupsRepository.AddAsync(userGroups);

            foreach (Permission permission in permissions) 
            {
                await permissionRepository.AddAsync(permission);
                await groupSetPermissionRepository.AddAsync(
                        new GroupSetPermission() 
                        {
                            Guid = Guid.NewGuid(),
                            GroupSetId = groupSet.Guid,
                            PermissionId = permission.Guid,
                            IsActive = true,
                            CreatedDate = DateTime.Now,
                            CreatedbyUserGuid = Guid.Empty,
                            IsDeleted = false
                        }
                    );
            }     
        }
    }
}
