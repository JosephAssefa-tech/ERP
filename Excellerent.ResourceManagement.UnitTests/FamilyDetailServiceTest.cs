using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.ResourceManagement.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ResourceManagement.UnitTests
{
   public class FamilyDetailServiceTest
    {
        private readonly FamilyDetailService _famDetailService;
       private readonly Mock<IFamilyDetailRepository> famDetailServiceRepo = new Mock<IFamilyDetailRepository>();

        public FamilyDetailServiceTest()
        {
          //  _famDetailService = new FamilyDetailService(famDetailServiceRepo.Object);
        }

       /* [Fact]
        public async Task GetEmployeeFamilyDetails()
        {
            Guid Id = Guid.NewGuid();
            var familyDetailsEntity = new List<FamilyDetails>() {
                new FamilyDetails(){
                      Guid = Guid.NewGuid(),
                       CreatedbyUserGuid = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                          DoB = DateTime.Now,
                           EmployeeId = Guid.NewGuid(),
                            RelationshipId = Guid.NewGuid(),
                              FullName = "abel",
                               Gender = "male",
                                IsActive = true,
                                 IsDeleted = false,

                                   Remark = "ok",
                                  Relationship = new Relationship()
                }
            };

            Guid empId= new Guid();

            famDetailServiceRepo.Setup(x => x.GetFamilyDetailByEmployeeId(empId)).ReturnsAsync(familyDetailsEntity);
            //Act
            var retrivedFamDetail = await _famDetailService.GetFamilyDetailByEmployeeId(empId);
            //Assert
            Assert.Equal(familyDetailsEntity, retrivedFamDetail);



        }*/



    }
}
