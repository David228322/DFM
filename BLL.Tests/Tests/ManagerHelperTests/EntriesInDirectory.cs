using System.IO;
using Xunit;
using Moq;

namespace BLL.Tests.Tests.ManagerHelperTests
{
    public class EntriesInDirectory
    {
        [Fact]
        public void EntriesInDirectory_ReturnsTrueDirIsntEmpty()
        {
            string dirName = @"FakeFolder";
            string pathName = Path.GetFullPath(dirName);
            pathName = pathName.Replace(@"\\", @"\");
            DFMLib.ManagerHelper managerHelper = new DFMLib.ManagerHelper();

            var actual = managerHelper.EntriesInDirectory("\\", pathName);
            Assert.True(actual);
        }

    }
}
