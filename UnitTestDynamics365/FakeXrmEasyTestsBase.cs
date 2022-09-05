using System.Reflection;
using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.Enums;
using FakeXrmEasy.FakeMessageExecutors;
using FakeXrmEasy.FakeMessageExecutors.CustomExecutors;
using FakeXrmEasy.Middleware;
using FakeXrmEasy.Middleware.Crud;
using FakeXrmEasy.Middleware.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace UnitTestDynamics365;

public class FakeXrmEasyTestsBase
{
    protected readonly IXrmFakedContext _context;
    protected readonly IOrganizationServiceAsync2 _service;

    public FakeXrmEasyTestsBase() 
    {
        _context = MiddlewareBuilder
            .New()
            .AddCrud()
            .AddFakeMessageExecutors(Assembly.GetAssembly(typeof(AddListMembersListRequestExecutor)))
            .AddGenericFakeMessageExecutors(Assembly.GetAssembly(typeof (NavigateToNextEntityOrganizationRequestExecutor)))
            .UseCrud()
            .UseMessages()
            .SetLicense(FakeXrmEasyLicense.RPL_1_5)
            .Build();

        _service = _context.GetAsyncOrganizationService2();
    }
}