using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.ManagerHelperTests
{
    public class DirectoryHasNotIllegalChar
    {
        [Theory]
        [InlineData(@"fake.txt")]
        [InlineData(@"FakeFolder")]
        [InlineData("TrueValue")]
        public void DirectoryHasNotIllegalChar_ReturnsTrueFilesAndDirName(string fileName)
        {
            DFMLib.ManagerHelper managerHelper = new DFMLib.ManagerHelper();

            var actual = managerHelper.DirectoryHasNotIllegalChar(fileName);
            Assert.True(actual);
        }

        [Theory]
        [InlineData(@"fake.t\xt")]
        [InlineData(@"<>:\\")]
        [InlineData("False*Value")]
        public void DirectoryHasNotIllegalChar_ReturnsFalseFilesAndDirName(string fileName)
        {
            DFMLib.ManagerHelper managerHelper = new DFMLib.ManagerHelper();

            var actual = managerHelper.DirectoryHasNotIllegalChar(fileName);
            Assert.False(actual);
        }
    }
}
