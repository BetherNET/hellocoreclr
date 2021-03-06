﻿using System;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using HelloCoreClrApp.WebApi;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using Xunit;

namespace HelloCoreClrApp.Test.WebApi
{
    public class WebHostServiceTest
    {
        [Fact]
        public async Task ShouldStartAndStopTestAsync()
        {
            Startup.Container = new Container();
            var conf = A.Fake<IConfiguration>();
            var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(500));
            var sut = new WebHostService();
            await sut.Run(conf, cts.Token);

            cts.IsCancellationRequested.Should().BeTrue();
        }
    }
}
