using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechQandA.DataAccess;
using TechQandA.DataAccess.DocumentDb;
using TechQandA.Models.Dto;

namespace TechQandA.DataAccess.DocumentDb
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DocumentDBRepositoryCollection<Category>>().As<IRepositoryCollection<Category>>()
                .WithParameter("collectionName", "Category");

            builder.RegisterType<DocumentDBRepositoryCollection<SubCategory>>().As<IRepositoryCollection<SubCategory>>()
               .WithParameter("collectionName", "SubCategory");
        }
    }
}
