﻿using AutoMapper;
using Caliburn.Micro;
using Nis.WpfApp.Mappings;
using Nis.Core.Extensions;
using Nis.WpfApp.Requests;
using Nis.Core.Persistence;

namespace Nis.WpfApp.Extensions;

internal static class SimpleContainerExtensions
{
    internal static SimpleContainer RegisterRequests(this SimpleContainer container)
    {
        typeof(BaseRequest).Assembly
            .GetTypes()
            .Where(type => !type.IsAbstract && type.Name.EndsWith("Request"))
            .ToList()
            .ForEach(request =>
            {
                container.RegisterPerRequest(
                    service: request,
                    key: request.ToString(),
                    implementation: request
                );
            });

        return container;
    }

    internal static SimpleContainer RegisterMappings(this SimpleContainer container)
    {
        container.Instance(
            new MapperConfiguration(configuration => configuration.AddProfile<MappingProfile>()).CreateMapper()
        );

        return container;
    }

    internal static SimpleContainer RegisterDatabase(this SimpleContainer container)
    {
        container.Instance(
            new DataContext(DatabaseExtensions.ConnectToDatabase(DatabaseExtensions.ConnectionString))
        );

        return container;
    }

    internal static SimpleContainer RegisterViewModels(this SimpleContainer container)
    {
        typeof(Bootstrapper).Assembly
            .GetTypes()
            .Where(type => type.IsClass && type.Name.EndsWith("ViewModel"))
            .ToList()
            .ForEach(vm => container.RegisterSingleton(
                service: vm,
                key: vm.ToString(),
                implementation: vm
            ));

        return container;
    }
}
