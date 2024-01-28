using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.APIModularization.Providers
{
    public class ModuleNames
    {
        public static string Timesheet = "Timesheet".ToLower();
        public static string ProjectManagement = "ProjectManagement".ToLower();

        /// /////////////////////////////////////////////////////////////////////////
        public static string UserManagement = "UserManagement".ToLower();
        public static string ResourceManagement = "ResourceManagement".ToLower();
    }
    public class ApiControllerNames
    {
        //TimeSheet
        public static string TimeSheetConfig = "TimeSheetConfig".ToLower();
        public static string TimeSheet = "TimeSheet".ToLower();
        //project Management
        public static string AssignResource = "AssignResource".ToLower();
        public static string Client = "Client".ToLower();
        public static string Employees = "Employees".ToLower();
        public static string Project = "Project".ToLower();
        public static string ProjectStatus = "ProjectStatus".ToLower();


        /////////////////////////////////////////////////////////////////////////
        //User Management
        public static string GroupSet = "GroupSet".ToLower();
        public static string User = "User".ToLower();

        //Employee
        public static string Employee = "Employee".ToLower();
        public static string Category = "Category".ToLower();
        public static string Country = "Country".ToLower();
        public static string DeviceClassification = "DeviceClassification".ToLower();
        public static string DeviceDetails = "DeviceDetails".ToLower();
        public static string EmergencyContacts = "EmergencyContacts".ToLower();
        public static string EmergencyAddress = "EmergencyAddress".ToLower();
        public static string EmployeePhoto = "EmployeePhoto".ToLower();
        public static string FamilyDetail = "FamilyDetail".ToLower();
        public static string PersonalAddress = "PersonalAddress".ToLower();
        public static string SubCategory = "SubCategory".ToLower();

    }

    public class ActionNames
    {
        //Generic Action
        public static string Get = "Get".ToLower();
        public static string Post = "Post".ToLower();
        public static string Put = "Put".ToLower();
        public static string Delete = "Delete".ToLower();

        //Specific Actions for time sheet
        public static string GetTimesheet = "GetTimesheet".ToLower();
        public static string GetTimeEntries = "GetTimeEntries".ToLower();
        public static string AddTimeEntry = "AddTimeEntry".ToLower();
        public static string UpdateTimeEntry = "UpdateTimeEntry".ToLower();
        public static string DeleteTimeEntry = "DeleteTimeEntry".ToLower();
        public static string GetApprovalStatus = "GetApprovalStatus".ToLower();
        public static string AddApprovalStatus = "AddApprovalStatus".ToLower();
        public static string RejectTimeSheet = "RejectTimeSheet".ToLower();
        public static string RequestForReview = "RequestForReview".ToLower();

        public static string TimesheetAdmin = "TimesheetAdmin".ToLower();
        public static string GetTimeSheetConfiguration = "GetTimeSheetConfiguration".ToLower();
        //Specific Actions for project
        public static string GetAssignResource = "GetAssignResource".ToLower();
        public static string AddAssignResource = "AddAssignResource".ToLower();
        public static string GetAssignResourceById = "GetAssignResourceById".ToLower();
        public static string UpdateAssignResource = "UpdateAssignResource".ToLower();
        public static string DeleteAssignResource = "DeleteAssignResource".ToLower();
        public static string Add = "Add".ToLower();
        public static string Edit = "Edit".ToLower();
        public static string Remove = "Remove".ToLower();
        public static string GetAll = "GetAll".ToLower();
        public static string GetById = "GetById".ToLower();
        public static string GetProjectStatusById = "GetProjectStatusById".ToLower();
        public static string AssignResource = "AssignResource".ToLower();
        public static string AssignClient = "AssignClient".ToLower();
        public static string ProjectsAdmin = "ProjectsAdmin".ToLower();



        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        //User Management
        public static string UserManagementAdmin = "UserManagementAdmin".ToLower();
        public static string CreateGroup = "CreateGroup".ToLower();
        public static string ViewGroup = "ViewGroup".ToLower();
        public static string UpdateGroup = "UpdateGroup".ToLower();
        public static string AddUser = "AddUser".ToLower();
        public static string ViewUser = "ViewUser".ToLower();
        public static string UpdateUser = "UpdateUser".ToLower();
        public static string DeleteUser = "DeleteUser".ToLower();

        //Resource Management
        public static string EmployeeAdmin = "EmployeeAdmin".ToLower();
        public static string CreateEmployee = "CreateEmployee".ToLower();
        public static string ViewEmployee = "ViewEmployee".ToLower();
        public static string UpdateEmployee = "UpdateEmployee".ToLower();
        public static string CreateMyProfile = "DeleteUser".ToLower();
        public static string ViewMyProfile = "ViewMyProfile".ToLower();
        public static string UpdateMyProfile = "UpdateMyProfile".ToLower();

        //Report Module
        public static string ReportsAdmin = "ReportsAdim".ToLower();
        public static string ViewReports = "ViewReports".ToLower();
    }

}
