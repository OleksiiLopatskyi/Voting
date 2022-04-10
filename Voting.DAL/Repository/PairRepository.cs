﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.DAL.Context;
using Voting.DAL.Contracts;
using Voting.DAL.Entities;

namespace Voting.DAL.Repository
{
    public class PairRepository : BaseRepository<Pair>, IPairRepository
    {
        public PairRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
