using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace DysonContext.Repository;

public class FileNodeRepository(IContext context) : BaseRepository<Entities.FileNodeEntity>(context)
{
}
