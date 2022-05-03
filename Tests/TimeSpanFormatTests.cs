using NUnit.Framework;
using StartupTimer.Views;
using System;

namespace Tests;

public class TimeSpanFormatTests {
    [TestCase("00:00:00", "0")]
    [TestCase("00:00:01", "0-00-")]
    [TestCase("00:15:00", "0-15-")]
    [TestCase("00:16:00", "0-15- (1)")]
    [TestCase("10:00:00", "10-00-")]
    public void IsSpanFormattedCorrectly(string span, string expected) {
        TimeSpan timeSpan = TimeSpan.Parse(span);
        TimeSpanFormat format = new("{0}-{1}-{2}");

        string? result = format.Format(timeSpan);

        Assert.That(result, Is.EqualTo(expected));
    }
}
