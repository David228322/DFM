using Xunit;
using System.IO;

namespace BLL.Tests.Tests.ManagerHelperTests
{
    public class FileOrDirectoryExists
    {
        //[Theory]
        //[InlineData(@"FakeFile")]
        //[InlineData(@"FakeFolder")]
        //public void FileOrDirectoryExists_ReturnFileAndDirExist(string dirName)
        //{
        //    string pathName = Path.GetFullPath(dirName);
        //    DFMLib.ManagerHelper managerHelper = new DFMLib.ManagerHelper();

        //    var actual = managerHelper
        //        .FileOrDirectoryExists("\\", pathName, DFMLib.ManagerHelper.expectedAttributes.Directory);

        //    Assert.True(actual);
        //}
        [Theory]
        [InlineData(@"FakeFolder")]
        public void FileOrDirectoryExists_ReturnFalse(string dirName)
        {
            var pathName = Path.GetFullPath(@"DFM\BLL.Tests\Resources\");
            DFMLib.ManagerHelper managerHelper = new DFMLib.ManagerHelper();

            var actual = managerHelper
                .FileOrDirectoryExists(dirName, pathName, DFMLib.ManagerHelper.ExpectedAttributes.Directory);

            Assert.False(actual);
        //C: \Users\dzaba\source\repos\DFM\BLL.Tests\bin\Debug\net5.0\BLL.Tests\Resources
        //        C:\Users\dzaba\source\repos\DFM\BLL.Tests\Resources\FakeFolder\
        }
    }
}
