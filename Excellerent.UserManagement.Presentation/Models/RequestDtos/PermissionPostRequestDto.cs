
namespace Excellerent.UserManagement.Presentation.Models.RequestDtos
{
    public class PermissionPostRequestDto
    {
        public string PermissionCode { get; set; }
        public string Name { get; set; }
        public string KeyValue { get; set; }
        public string Level { get; set; }
        public string ParentCode { get; set; }
    }
}
