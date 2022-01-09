using System.IO;
using Xunit;

namespace BLL.Tests.Tests.ManagerHelperTests
{
    public class GetFileAttributes
    {
        [Fact]
        public void GetFileAttributes_ReturnsFileAttribute()
        {
            string dirName = @"FakeFolder";
            string pathName = Path.GetFullPath(dirName);
            DFMLib.ManagerHelper managerHelper = new DFMLib.ManagerHelper();

            var actual = managerHelper.GetFileAttributes("\\", pathName);

            Assert.True(actual.HasFlag(FileAttributes.Directory));
        }
    }
}
