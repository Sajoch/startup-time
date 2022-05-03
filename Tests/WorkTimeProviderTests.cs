using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using StartupTimer.Models;
using StartupTimer.TimeProviders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests;

public class WorkTimeProviderTests {
    [TestCase("09:10:00")]
    [TestCase("00:00:01")]
    public void IsRequiredTimeSameAsInConfiguration(string workTime) {
        IOptions<WorkConfiguration> configuration = CreateConfiguration(workTime, "10:00:00");
        ICurrentTimeProvider? currentTime = Substitute.For<ICurrentTimeProvider>();
        currentTime.GetTime().Returns(DateTime.MinValue);
        WorkTimeProvider provider = new(configuration, Enumerable.Empty<ITimeProvider>(), currentTime);

        WorkTime? model = provider.GetWorkTime();

        Assert.That(model.RequiredTime, Is.EqualTo(configuration.Value.WorkTime));
    }

    [TestCase("01:00:00", "00:00:00")]
    [TestCase("01:00:00", "00:59:59")]
    [TestCase("01:00:00", "01:00:00")]
    public void IsOverworkBeforeEndTime(string workTime, string timeOffset) {
        IOptions<WorkConfiguration> configuration = CreateConfiguration(workTime, "10:00:00");
        TimeSpan offset = TimeSpan.Parse(timeOffset);
        ICurrentTimeProvider? currentTime = Substitute.For<ICurrentTimeProvider>();
        currentTime.GetTime().Returns(DateTime.MinValue + offset);
        ITimeProvider? beginTime = Substitute.For<ITimeProvider>();
        beginTime.GetTime().Returns(DateTime.MinValue);
        WorkTimeProvider provider = new(configuration, new List<ITimeProvider> {beginTime}, currentTime);
        WorkTime? model = provider.GetWorkTime();

        bool result = model.IsOverwork(currentTime.GetTime());

        Assert.That(result, Is.False);
    }

    [TestCase("01:00:00", "01:00:01")]
    [TestCase("01:00:00", "24:00:00")]
    public void IsOverworkAfterEndTime(string workTime, string timeOffset) {
        IOptions<WorkConfiguration> configuration = CreateConfiguration(workTime, "10:00:00");
        TimeSpan offset = TimeSpan.Parse(timeOffset);
        ICurrentTimeProvider? currentTime = Substitute.For<ICurrentTimeProvider>();
        currentTime.GetTime().Returns(DateTime.MinValue + offset);
        ITimeProvider? beginTime = Substitute.For<ITimeProvider>();
        beginTime.GetTime().Returns(DateTime.MinValue);
        WorkTimeProvider provider = new(configuration, new List<ITimeProvider> {beginTime}, currentTime);
        WorkTime? model = provider.GetWorkTime();

        bool result = model.IsOverwork(currentTime.GetTime());

        Assert.That(result, Is.True);
    }

    [TestCase("00:00:00")]
    [TestCase("10:00:00")]
    public void IsNotExpiredModelTheSame(string timeOffset) {
        IOptions<WorkConfiguration> configuration = CreateConfiguration("01:00:00", "10:00:00");
        TimeSpan offset = TimeSpan.Parse(timeOffset);
        ICurrentTimeProvider? currentTime = Substitute.For<ICurrentTimeProvider>();
        currentTime.GetTime().Returns(DateTime.MinValue);
        ITimeProvider? beginTime = Substitute.For<ITimeProvider>();
        beginTime.GetTime().Returns(DateTime.MinValue);
        WorkTimeProvider provider = new(configuration, new List<ITimeProvider> {beginTime}, currentTime);
        WorkTime? model = provider.GetWorkTime();
        currentTime.GetTime().Returns(DateTime.MinValue + offset);

        WorkTime? result = provider.GetWorkTime();

        Assert.That(model, Is.EqualTo(result));
    }

    [TestCase("10:00:01")]
    [TestCase("24:00:00")]
    public void IsExpiredModelReplaceByNew(string timeOffset) {
        IOptions<WorkConfiguration> configuration = CreateConfiguration("01:00:00", "10:00:00");
        TimeSpan offset = TimeSpan.Parse(timeOffset);
        ICurrentTimeProvider? currentTime = Substitute.For<ICurrentTimeProvider>();
        currentTime.GetTime().Returns(DateTime.MinValue);
        ITimeProvider? beginTime = Substitute.For<ITimeProvider>();
        beginTime.GetTime().Returns(DateTime.MinValue);
        WorkTimeProvider provider = new(configuration, new List<ITimeProvider> {beginTime}, currentTime);
        WorkTime? model = provider.GetWorkTime();
        currentTime.GetTime().Returns(DateTime.MinValue + offset);

        WorkTime? result = provider.GetWorkTime();

        Assert.That(model, Is.Not.EqualTo(result));
    }

    static IOptions<WorkConfiguration> CreateConfiguration(string? workTime, string? maxWorkTime) {
        WorkConfiguration configuration = new() {
            WorkTime = workTime == null ? TimeSpan.MaxValue : TimeSpan.Parse(workTime),
            MaxWorkTime = maxWorkTime == null ? TimeSpan.MaxValue : TimeSpan.Parse(maxWorkTime)
        };
        return Options.Create(configuration);
    }
}
