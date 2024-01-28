using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Interface.Service;
using Excellerent.SharedModules.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Services
{
  public class RelationshipService : CRUD<RelationshipEntity, Relationship>, IRelationshipService
    {
        private readonly IRelationshipRepository _familyMemberRepository;

        public RelationshipService(IRelationshipRepository familyMemberRepository) : base(familyMemberRepository)
        {
            _familyMemberRepository = familyMemberRepository;
        }

        public async Task<IEnumerable<Relationship>> GetAll()
        {
            return await _familyMemberRepository.GetAllAsync();
        }
    }
}
