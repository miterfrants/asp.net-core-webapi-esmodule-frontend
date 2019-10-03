using System;
using AutoMapper;
using Sample.Data.Entities;
using Sample.Domain.Models;

namespace Sample.Domain.Mapping
{
    public partial class UsersProfile
        : AutoMapper.Profile
    {
        public UsersProfile()
        {
            CreateMap<Sample.Data.Entities.Users, Sample.Domain.Models.UsersReadModel>();
            CreateMap<Sample.Domain.Models.UsersCreateModel, Sample.Data.Entities.Users>();
            CreateMap<Sample.Data.Entities.Users, Sample.Domain.Models.UsersUpdateModel>();
            CreateMap<Sample.Domain.Models.UsersUpdateModel, Sample.Data.Entities.Users>();
        }

    }
}
