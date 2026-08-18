using DysonContext.Entities;
using DysonContext.Interface;

using System;
using System.Collections.Generic;
using System.Text;

namespace DysonContext.Repository;

public class UserRepository(IContext context) : BaseRepository<UserEntity>(context), IUserRepository
{
}
