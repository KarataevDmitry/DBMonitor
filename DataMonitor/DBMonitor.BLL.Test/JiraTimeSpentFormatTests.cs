using DBMonitor.BLL.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DBMonitor.BLL.Test
{
    [TestClass]
    public class JiraTimeSpentFormatTests
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow("1h", 3600)]
        [DataRow("1m", 60)]
        [DataRow("1d", 3600 * 8)]
        [DataRow("1h1m", 3660)]
        public void ToJiraStringWorkDaysTest(string expected, int secValue) => Assert.AreEqual(expected, secValue.AsJiraString());
        [TestMethod]
        [DataTestMethod]
        [DataRow("1d", 3600 * 24)]
        public void ToJiraStringDaysTest(string expected, int secValue) => Assert.AreEqual(expected, secValue.AsJiraString(false));
        [TestMethod]
        public void MyTestMethod()
        {

        }
    }
}