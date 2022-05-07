﻿using AutoMapper;
using Caliburn.Micro;
using Nis.WpfApp.Mappings;
using Nis.Core.Extensions;
using Nis.Core.Persistence;

namespace Nis.WpfApp.Extensions;

public static class SimpleContainerExtensions
{
    public static SimpleContainer RegisterMappings(this SimpleContainer container)
    {
        container.Instance(
            new MapperConfiguration(configuration => configuration.AddProfile<MappingProfile>()).CreateMapper()
        );

        return container;
    }

    public static SimpleContainer RegisterDatabase(this SimpleContainer container)
    {
        container.Instance(new DataContext(DatabaseExtensions.ConnectToDatabase(
            DatabaseExtensions.ConnectionString)
        ));

        return container;
    }

    public static SimpleContainer RegisterViewModels(this SimpleContainer container)
    {
        typeof(Bootstrapper).Assembly
            .GetTypes()
            .Where(type => type.IsClass && type.Name.EndsWith("ViewModel"))
            .ToList()
            .ForEach(vm => container.RegisterPerRequest(
                service: vm,
                key: vm.ToString(),
                implementation: vm
            ));

        return container;
    }
}
